using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ParallelUtil
{
    public static class Cores
    {
        public static int Max
        {
            get
            {

                return Environment.ProcessorCount;
            }
        }

        public static int PhysicalCores
        {
            get
            {
                SYSTEM_LOGICAL_PROCESSOR_INFO[] buffer = new SYSTEM_LOGICAL_PROCESSOR_INFO[256];
                int returnLength = buffer.Length * Marshal.SizeOf(typeof(SYSTEM_LOGICAL_PROCESSOR_INFO));


                bool success = Win32.GetLogicalProcessorInformation(buffer, out returnLength);
                if (!success)
                {
                    throw new ParallelUtilException("Failed to Get Logical Processor Information, only supported on Vista + OS's");
                }

                int nRealCores = 0;
                int nResults = returnLength / Marshal.SizeOf(typeof(SYSTEM_LOGICAL_PROCESSOR_INFO));

                for (int nResult = 0; nResult < nResults; nResult++)
                {
                    if (buffer[nResult].Relationship == LOGICAL_PROCESSOR_RELATIONSHIP.RelationProcessorCore)
                    {
                        if (buffer[nResult].Flags > 0)
                        {
                            nRealCores++;
                        }
                    }
                }

                return nRealCores;
            }
        }

        public static int CoresInUse
        {
            get
            {
                IntPtr cores =
                Process.
                    GetCurrentProcess()
                    .ProcessorAffinity;

                int nCores = 0;
                while (cores != IntPtr.Zero)
                {
                    if (((int)cores & 1) == 1)
                    {
                        nCores++;
                    }
                    cores = (IntPtr)((int)cores >> 1);
                }
                return nCores;
            }

            set
            {
                if ((value < 1) || (value > Environment.ProcessorCount))
                {
                    throw new ArgumentException("Illegal number of cores");
                }

                int cores = 1;
                for (int nShift = 0; nShift < value - 1; nShift++)
                {
                    cores = 1 | (cores << 1);
                }

                Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)cores;
            }
        }
    }
}
