using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SomeOrderingSystem.Test
{
    public class FakeTaxService : ITaxService
    {
        public double Value { get; set; }
        public bool WasCalled { get; set; }
        
        public double Calc(double value, double rate)
        {
            WasCalled = true;

            return Value;
        }
    }

    public class FakeCustomerRepository : ICustomerRepository
    {
        public double Value { get; set; }
        
        public double GetTaxRateForCustomer(int id)
        {
            return Value;
        }
    }

    public class OrderServiceTests
    {
        OrderService subject;
        //FakeTaxService fakeTax = new FakeTaxService();
        Mock<ITaxService> mockTaxService = new Mock<ITaxService>();
        FakeCustomerRepository fakeCustomer = new FakeCustomerRepository();

        public OrderServiceTests()
        {
            subject = new OrderService(fakeCustomer, mockTaxService.Object);

            //using (var s = this.GetType().Assembly.GetManifestResourceStream("SomeOrderingSystem.Test.TextFile1.txt"))
            //{
            //    StreamReader sr = new StreamReader(s);
            //    var data = sr.ReadToEnd();
            //}

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(Int32.MinValue)]
        public void PlaceOrder_should_not_allow_invalid_quantity(int qty)
        {
            // Act
            var result = subject.PlaceOrder(1, qty, 2);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void PlaceOrder_should_not_allow_negative_quantity()
        {
            // Act
            var result = subject.PlaceOrder(1, -10, 2);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void PlaceOrder_should_not_allow_zero_quantity()
        {
            // Act
            var result = subject.PlaceOrder(1, 0, 2);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        [Trait("Category", "Ordering")]
        public void PlaceOrder_should_allow_positive_quantity()
        {
            // Act
            var result = subject.PlaceOrder(1, 10, 2);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        void PlaceOrder_calculates_total_correctly()
        {
            fakeCustomer.Value = 10;
            //fakeTax.Value = 20;
            mockTaxService
                .Setup(x => x.Calc(It.IsAny<double>(), It.IsAny<double>()))
                .Returns(20);

            var result = subject.PlaceOrder(1, 1, 1);
            Assert.Equal(120, result.Total);
        }

        [Fact]
        public void PlaceOrder_gets_tax_rate_from_tax_service()
        {
            fakeCustomer.Value = 10;
            subject.PlaceOrder(1, 2, 3);

            mockTaxService
                .Verify(x => x.Calc(It.IsAny<double>(), It.IsAny<double>()));
            //Assert.True(fakeTax.WasCalled);
        }

        [Fact]
        public void PlaceOrder_when_customer_tax_rate_is_invalid_do_not_call_tax_service()
        {
            fakeCustomer.Value = -10;
            subject.PlaceOrder(1, 2, 3);

            mockTaxService
                .Verify(x => x.Calc(It.IsAny<double>(), It.IsAny<double>()),
                Times.Never());

            //Assert.False(fakeTax.WasCalled);
        }
    }
}
