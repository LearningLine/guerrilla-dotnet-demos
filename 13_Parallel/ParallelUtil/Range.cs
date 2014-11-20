using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParallelUtil
{
    public struct Range
    {
        public int Start;
        public int End;

        
        public IEnumerable<Range> CreateSubRanges(int nRanges)
        {
            if (nRanges < 1)
            {
                throw new ArgumentException("Number of Ranges must be greater than zero");
            }

            int subRangeStart = Start;
            int subRangeStep = ((End - Start)+1) / nRanges;
            while (nRanges > 1)
            {
                yield return new Range() { Start = subRangeStart,  End = subRangeStart + subRangeStep-1 };
                subRangeStart += subRangeStep;
                nRanges--;
            }

            if (subRangeStart != End)
            {
                yield return new Range() { Start = subRangeStart, End = End };
            }

        }
        public override string ToString()
        {
            return String.Format("{0} - {1}", Start, End);
        }
    }
}
