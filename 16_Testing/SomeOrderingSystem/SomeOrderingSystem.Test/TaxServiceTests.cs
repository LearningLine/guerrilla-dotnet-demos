using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SomeOrderingSystem.Test
{
    public class TaxServiceTests
    {
        TaxService subject;
        public TaxServiceTests()
        {
            subject = new TaxService();
        }

        [Theory]
        [InlineData(10, .12, 1.2)]
        [InlineData(200, .1, 15)]
        public void Calc_calculates_tax_correctly(double total, double rate, double expected)
        {
            var result = subject.Calc(total, rate);
            Assert.Equal(expected, result);
        }
    }
}
