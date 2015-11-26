using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace CSVFileProcessing
{
    public class VirtualCsv
    {
        public List<string[]> CsvRows;
    }
    public class CsvRepository
    {
        private readonly string directory;
        private readonly Func<string, StreamReader> createStreamReader;
        //   private readonly Dictionary<string, List<string[]>> csvFiles;
        // private readonly Dictionary<string, VirtualCsv> csvFiles;
       // private readonly Dictionary<string, Lazy<List<string[]>>> csvFiles;
        private readonly ConcurrentDictionary<string, Lazy<List<string[]>>> csvFiles;

        public CsvRepository(string directory) : this(directory, s => new StreamReader(s))
        {
        }

        public CsvRepository(string directory, Func<string, StreamReader> createStreamReader)
        {
            this.directory = directory;
            this.createStreamReader = createStreamReader;
            csvFiles = new ConcurrentDictionary<string, Lazy<List<string[]>>>();
                //Files
                //                .ToDictionary(f => f, f => LoadData(f).ToList());
                // .ToDictionary(f => f, f => (List<string[]>)null);
                //.ToDictionary(f => f, f => new Lazy<List<string[]>>(() => 
                //                                LoadData(f).ToList()));

        }

        public IEnumerable<string> Files
        {
            get
            {
                return new DirectoryInfo(directory)
                    .GetFiles("*.csv").Select(fi => fi.Name);
            }
        }

        public IEnumerable<T> Map<T>(string dataFile, Func<string[], T> map)
        {

            //Lazy<List<string[]>> csvRows = 
            //    new Lazy<List<string[]>>(() => LoadData(dataFile).ToList());

            //            csvRows = csvFiles.GetOrAdd(dataFile, csvRows);
            Lazy<List<string[]>> csvRows = 
                csvFiles.GetOrAdd(dataFile, 
                        key => new Lazy<List<string[]>>(() => LoadData(dataFile).ToList()));

            return csvRows.Value.Skip(1).Select(map);

            //return csvFiles[dataFile].Skip(1).Select(map);

            //            VirtualCsv rows = csvFiles[dataFile];
            //            if (rows.CsvRows == null)
            //            {
            //                lock (rows)
            //                {
            //                    // List<string[]> csvRows = csvFiles[dataFile];
            //                    if (rows.CsvRows == null)
            //                    {
            //                        rows.CsvRows = LoadData(dataFile).ToList();
            //                    }
            ////                    csvFiles[dataFile] = csvRows;
            //                }
            //            }

            //            return rows.CsvRows.Skip(1).Select(map);
        }

        private IEnumerable<string[]> LoadData(string filename)
        {
            using (var reader = createStreamReader(Path.Combine(directory, filename)))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine().Split(',');
                }
            }
        }
    }

}