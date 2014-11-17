using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Banking;

namespace Banking.Tests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void ctor_WhenPassedAnInitialBalance_ShouldSetBalance()
        {
            // Arrange
            var initialBalance = 100m;

            // Act
            var sut = new Account(initialBalance);
            
            // Assert
            Assert.AreEqual(initialBalance, sut.Balance);
        }

        [TestMethod]
        public void Credit_WhenPassedAnAmount_ShouldIncrementBalanceByAmount()
        {
            // Arrange
            var initial = 101m;
            var inc = 52m;
            var account = new Account(initial);

            // Act
            account.Credit(inc);

            // Assert
            Assert.AreEqual(initial+inc, account.Balance);
            //NUnit: Assert.That(account.Balance, Is.EqualTo(initial + inc));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_WhenPassedNegativeBalance_ShouldThrowArgException()
        {
             new Account(-.0000000001m);
        }

        [TestMethod]
        public void this_is_complicated_ctor_WhenPassedNegativeBalance_ShouldThrowArgException()
        {
            try
            {
                new Account(-.0000000001m);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true, "that was cool, it worked. good job code monkey");
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
