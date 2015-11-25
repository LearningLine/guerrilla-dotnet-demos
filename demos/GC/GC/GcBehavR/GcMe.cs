using System;

namespace GcBehavR
{
    class GcMe
    {
        private int data;
        private decimal d1;
        private decimal d2;
        private decimal d3;
        private decimal d4;
        private decimal d5;
        private decimal d6;
        private decimal d7;
        private decimal d8;
        private decimal d9;
        private decimal d10;


        public GcMe()
        {
            unsafe
            {
                fixed (int* p = &data)
                {
                    Console.WriteLine((int)p);
                }
            }
        }
    }
}