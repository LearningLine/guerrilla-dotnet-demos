using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ParallelUtil
{
    public enum PROCESSOR_CACHE_TYPE
    {
        CacheUnified,
        CacheInstruction,
        CacheData,
        CacheTrace
    };

    public struct CACHE_DESCRIPTOR
    {
        public byte Level;
        public byte Associativity;
        public short LineSize;
        public int Size;
        public PROCESSOR_CACHE_TYPE Type;
    };

    public enum LOGICAL_PROCESSOR_RELATIONSHIP
    {
        RelationCache = 2,
        RelationNumaNode = 1,
        RelationProcessorCore = 0,
        RelationProcessorPackage = 3,
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct SYSTEM_LOGICAL_PROCESSOR_INFO
    {
        [FieldOffset(0)]
        public IntPtr ProcessorMask;
        [FieldOffset(4)]
        public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;

        [FieldOffset(8)]
        public byte Flags;

        [FieldOffset(8)]
        public int NodeNumber;

        [FieldOffset(8)]
        public CACHE_DESCRIPTOR CacheDescriptor;

        [FieldOffset(8)]
        public long Reserved1;
        [FieldOffset(12)]
        public long Reserved2;
    }

    public static class Win32
    {
      

        [DllImport("Kernel32.dll")]
        public static extern bool GetLogicalProcessorInformation(
        [Out] SYSTEM_LOGICAL_PROCESSOR_INFO[] buffer,
            out int returnLength);

       
         
    }
}
