using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;
using Exocortex.DSP;
using System.IO;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MyFFT
{
    class Program
    {
        private const string INPUT_FILENAME="bigFile.dat";
        private const string OUTPUT_FILENAME = "output.dat";

        static void Main(string[] args)
        {

            Fourier.SyncLookupTableLength(4096);
            CreateDataFile(INPUT_FILENAME, 50000);
            
            TimeIt(SequentialVersion);
        }

        private static void SequentialVersion()
        {
            using (Stream output = File.OpenWrite(OUTPUT_FILENAME))
            {
                byte[] outputBuffer = new byte[1024 * 4 ] ;

                // Stage 1
                foreach (ComplexF[] complexBuffer in LoadBlocks(INPUT_FILENAME, outputBuffer.Length))
                {
                   // Stage 2
                    ComplexF[] result = Fourier.FFT(complexBuffer, FourierDirection.Forward);


                    // Stage 3
                    result = LowPassFilter(result);

                    // Stage 4
                    result =Fourier.FFT(result, FourierDirection.Backward);

                    // Stage 5
                    WriteComplexBuffer(output, result);
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


        private static IEnumerable<ComplexF[]> LoadBlocks(string filename,int blockSize)
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
            for (int nByte = 0; nByte < buffer.Length; nByte++)
            {
                complexBuffer[nByte] = new ComplexF(buffer[nByte], 0);
            }
            return complexBuffer;
        }

        private static ComplexF[] LowPassFilter(ComplexF[] complexBuffer )
        {
            for (int nItem = 0; nItem < complexBuffer.Length; nItem++)
            {
                if (nItem > complexBuffer.Length*0.6)
                {
                    complexBuffer[nItem] = new ComplexF(0, 0);
                }
            }

            return complexBuffer;

        }
       

        private static void CreateDataFile(string filename, int numberOfBlocks )
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

        private static void SimpleFFT()
        {
            ComplexF[] values = new ComplexF[1024];
            ComplexF[] expectedValues = new ComplexF[1024];

            Random rnd = new Random();

            for (int i = 0; i < 1024; i++)
            {
                values[i] = new ComplexF(20, 0);
            }

            Array.Copy(values, expectedValues, expectedValues.Length);

            Fourier.FFT(values, FourierDirection.Forward);
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine(values[i]);
            }

            Fourier.FFT(values, FourierDirection.Backward);
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine("{0} {1} {2}",
                  values[i] / values.Length
                   , expectedValues[i],
                   values[i] / expectedValues[i]);
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
