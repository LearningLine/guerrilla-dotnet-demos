using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeOrderingSystem
{
    public interface ITaxService
    {
        double Calc(double value, double rate);
    }

    public class TaxService : ITaxService
    {
        public TaxService()
        {

        }
        public double Calc(double value, double rate)
        {
            if (value >= 100)
            {
                var first = 100 * rate;
                var second = (value - 100) * rate/2;
                return first + second;
            }

            return value * rate;
        }
    }
}
