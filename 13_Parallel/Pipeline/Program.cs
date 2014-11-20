using System;
using System.Collections.Generic;
using System.Linq;
using Exocortex.DSP;
using System.IO;

namespace MyFFT
{
    internal class Program
    {
        private const string INPUT_FILENAME = "bigFile.dat";
        private const string OUTPUT_FILENAME = "output.dat";
        private const string PARALLEL_OUTPUT_FILENAME = "parallel_output.dat";

        private const string PLINQ_OUTPUT_FILENAME = "plinq_output.dat";
        private const string LINQ_OUTPUT_FILENAME = "linq_output.dat";

        private static void Main(string[] args)
        {
            CreateDataFile(INPUT_FILENAME, 50000 );

            Fourier.SyncLookupTableLength(4096);

            while (true)
            {
                TimeIt(SequentialVersion);
                
                TimeIt(LinqVersion);
             
                TimeIt(PLinqVersion);
                   
                CompareFiles(OUTPUT_FILENAME, LINQ_OUTPUT_FILENAME);
                CompareFiles(OUTPUT_FILENAME,PLINQ_OUTPUT_FILENAME);
            }
        }

        private static void CompareFiles(string lhsFilename, string rhsFilename)
        {
            using (var lhs = File.OpenRead(lhsFilename))
            {
                using (var rhs = File.OpenRead(rhsFilename))
                {


                    if (lhs.Length != rhs.Length)
                    {
                        Console.WriteLine("Different lengths");
                        return;
                    }

                    int bufferSize = 1024 * 4;
                    var lhsBuffer = new byte[bufferSize];
                    var rhsBuffer = new byte[bufferSize];

                    while (lhs.Position < lhs.Length)
                    {
                        int lhsRead = lhs.Read(lhsBuffer, 0, lhsBuffer.Length);
                        int rhsRead = rhs.Read(rhsBuffer, 0, rhsBuffer.Length);

                        for (int nByte = 0; nByte < lhsRead; nByte++)
                        {
                            if (lhsBuffer[nByte] != rhsBuffer[nByte])
                            {
                                Console.WriteLine("Different {0},{1} compared to {0},{2}", nByte, lhsBuffer[nByte],
                                    rhsBuffer[nByte]);
                                return;
                            }
                        }

                    }

                }
            }
        }

        private static void SequentialVersion()
        {
            using (Stream output = File.Create(OUTPUT_FILENAME))
            {
                byte[] outputBuffer = new byte[1024 * 4];

                // Stage 1
                foreach (ComplexF[] complexBuffer in LoadBlocks(INPUT_FILENAME, outputBuffer.Length))
                {
                    // Stage 2
                    ComplexF[] result = Fourier.FFT(complexBuffer, FourierDirection.Forward);

                    // Stage 3
                    result = LowPassFilter(result);

                    // Stage 4
                    result = Fourier.FFT(result, FourierDirection.Backward);

                    // Stage 5
                    WriteComplexBuffer(output, result);
                }
            }
        }


        private static void LinqVersion()
        {
            byte[] outputBuffer = new byte[1024 * 4];

            var filter =
                LoadBlocks(INPUT_FILENAME, outputBuffer.Length)
                    .Select(cb => Fourier.FFT(cb, FourierDirection.Forward))
                    .Select(cb => LowPassFilter(cb))
                    .Select(cb => Fourier.FFT(cb, FourierDirection.Backward));

            using (Stream output = File.Create(LINQ_OUTPUT_FILENAME))
            {
                foreach (ComplexF[] complexBuffer in filter)
                {
                    WriteComplexBuffer(output, complexBuffer);
                }
            }
        }



        private static void WriteComplexBuffer(Stream output, ComplexF[] complexBuffer)
        {
            byte[] outputBuffer = new byte[complexBuffer.Length];
            for (int nvalue = 0; nvalue < complexBuffer.Length; nvalue++)
            {
                outputBuffer[nvalue] = (byte)complexBuffer[nvalue].Re;
            }

            output.Write(outputBuffer, 0, complexBuffer.Length);
        }

        private static void PLinqVersion()
        {
            byte[] outputBuffer = new byte[1024*4];

            var filter =
                LoadBlocks(INPUT_FILENAME, outputBuffer.Length).AsParallel().AsOrdered()
                    .Select(cb => Fourier.FFT(cb, FourierDirection.Forward))
                    .Select(cb => LowPassFilter(cb))
                    .Select(cb => Fourier.FFT(cb, FourierDirection.Backward));

            using (Stream output = File.Create(PLINQ_OUTPUT_FILENAME))
            {
                foreach (ComplexF[] complexBuffer in filter)
                {
                    WriteComplexBuffer(output, complexBuffer);
                }
            }
        }



     

        private static IEnumerable<ComplexF[]> LoadBlocks(string filename, int blockSize)
        {
            byte[] buffer = new byte[blockSize];
            using (Stream input = File.OpenRead(INPUT_FILENAME))
            {
                while (input.Position < input.Length)
                {

                    // Stage 1

                    int bytesRead = input.Read(buffer, 0, buffer.Length);

                    var complexBuffer = ByteArrayToComplexBuffer(bytesRead, buffer);

                    yield return complexBuffer;
                }
            }
        }

        private static ComplexF[] ByteArrayToComplexBuffer(int bytesRead, byte[] buffer)
        {
            ComplexF[] complexBuffer = new ComplexF[bytesRead];
            return ByteArrayToComplexBuffer(bytesRead, buffer, complexBuffer);
        }

        private static ComplexF[] ByteArrayToComplexBuffer(int bytesRead, byte[] buffer, ComplexF[] complexBuffer)
        {

            for (int nByte = 0; nByte < buffer.Length; nByte++)
            {
                complexBuffer[nByte] = new ComplexF(buffer[nByte], 0);
            }
            return complexBuffer;
        }

        private static ComplexF[] LowPassFilter(ComplexF[] complexBuffer)
        {
            for (int nItem = 0; nItem < complexBuffer.Length; nItem++)
            {
                if (nItem > complexBuffer.Length * 0.6)
                {
                    complexBuffer[nItem] = new ComplexF(0, 0);
                }
            }

            return complexBuffer;

        }


        private static void CreateDataFile(string filename, int numberOfBlocks)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            Random rnd = new Random();
            using (Stream fs = File.OpenWrite(filename))
            {
                byte[] data = new byte[1024];

                for (int nBlock = 0; nBlock < numberOfBlocks; nBlock++)
                {
                    for (int nByte = 0; nByte < data.Length; nByte++)
                    {
                        data[nByte] = (byte)rnd.Next(255);
                    }

                    fs.Write(data, 0, data.Length);
                }
            }


        }



        private static void TimeIt(Action action)
        {
            DateTime start = DateTime.Now;
            action();
            Console.WriteLine("{0} took {1}", action.Method.Name, DateTime.Now.Subtract(start));
        }
    }
}
