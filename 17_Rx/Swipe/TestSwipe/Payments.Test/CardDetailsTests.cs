using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Payments.Test
{
    [TestClass]
    public class CardDetailsTests
    {
        private ITimeService timeService;

        [TestInitialize]
        public void Setup()
        {
            timeService = MockRepository.GenerateStub<ITimeService>();

            timeService.Stub(t => t.Now)
                .Return(new DateTime(2009, 1, 1));

        }

        [TestMethod]
        public void Ctor_WhenPassedData_ShouldSetFieldsCorrectly()
        {
            string number = "1234567890123456";
            string name = "Bob";
            string month = "09";
            string year = "10";

            var details = new CardDetails(number, name, month, year, timeService);

            Assert.AreEqual(number, details.Number);
            Assert.AreEqual(name, details.Name);
            Assert.AreEqual(month, details.ExpiryMonth);
            Assert.AreEqual(year, details.ExpiryYear);
        }

        [TestMethod]
        [ExpectedException(typeof(CardValidationException))]
        public void Validate_WhenPassedAnInvalidCardNumber_ShouldThrowException()
        {
            string number = "123456789A123456";
            string name = "Bob";
            string month = "09";
            string year = "10";

            var details = new CardDetails(number, name, month, year, timeService);

            details.Validate();

        }


        [TestMethod]
        [ExpectedException(typeof(CardValidationException))]
        public void Validate_WhenPassedAnInvalidYear_ShouldThrowException()
        {
            string number = "1234567890123456";
            string name = "Bob";
            string month = "09";
            string year = "1A";

            var details = new CardDetails(number, name, month, year, timeService);

            details.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(CardValidationException))]
        public void Validate_WhenPassedANonNumericMonth_ShouldThrowException()
        {
            string number = "1234567890123456";
            string name = "Bob";
            string month = "A9";
            string year = "10";

            var details = new CardDetails(number, name, month, year, timeService);

            details.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(CardValidationException))]
        public void Validate_WhenPassedAZeroMonth_ShouldThrowException()
        {
            string number = "1234567890123456";
            string name = "Bob";
            string month = "00";
            string year = "10";

            var details = new CardDetails(number, name, month, year, timeService);

            details.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(CardValidationException))]
        public void Validate_WhenPassedAMonthGreaterThan12_ShouldThrowException()
        {
            string number = "1234567890123456";
            string name = "Bob";
            string month = "13";
            string year = "10";

            var details = new CardDetails(number, name, month, year, timeService);

            details.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(CardValidationException))]
        public void Validate_WhenPassedAnExpiredCard_ShouldThrowException()
        {
            string number = "1234567890123456";
            string name = "Bob";
            string month = "12";
            string year = "10";

            timeService = MockRepository.GenerateStub<ITimeService>();
            timeService.Stub(t => t.Now)
                .Return(new DateTime(2011, 1, 1));

            var details = new CardDetails(number, name, month, year, timeService);

            details.Validate();
        }

        [TestMethod]
        [ExpectedException(typeof(CardValidationException))]
        public void Validate_WhenPassedAnExpiredCardYear90_ShouldThrowException()
        {
            string number = "1234567890123456";
            string name = "Bob";
            string month = "12";
            string year = "90";

            timeService = MockRepository.GenerateStub<ITimeService>();
            timeService.Stub(t => t.Now)
                .Return(new DateTime(2011, 1, 1));

            var details = new CardDetails(number, name, month, year, timeService);

            details.Validate();
        }

        [TestMethod]
        public void Validate_WhenPassedANonExpiredCardYear89_ShouldPassValidation()
        {
            string number = "1234567890123456";
            string name = "Bob";
            string month = "12";
            string year = "89";

            timeService = MockRepository.GenerateStub<ITimeService>();
            timeService.Stub(t => t.Now)
                .Return(new DateTime(2011, 1, 1));

            var details = new CardDetails(number, name, month, year, timeService);

            details.Validate();

            Assert.IsTrue(true);
        }
    }
}
