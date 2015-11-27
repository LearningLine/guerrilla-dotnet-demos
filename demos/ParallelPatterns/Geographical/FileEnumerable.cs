using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Geographical
{
    public static class FileEnumerable
    {
        public static IEnumerable<string> GetLines(this StreamReader reader)
        {
            while (reader.EndOfStream == false)
            {
                yield return reader.ReadLine();
            }
        }
    }
}
