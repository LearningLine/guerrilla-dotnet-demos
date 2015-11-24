using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Banking.Test
{
    [TestFixture]
    public class AccountTests
    {
        [Test]
        [TestCase(100)]
        [TestCase(200)]
        public void ctor_WhenPassedAnInitialBalance_ShouldSetTheBalance(decimal expectedInitialBalance)
        {
            // Arrange
            //decimal expectedInitialBalance = 100m;

            // Act
            var sut = new Account(expectedInitialBalance);

            // Assert

            Assert.That(sut.Balance,Is.EqualTo(expectedInitialBalance));
        }

        [Test]
       // [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ctor_WhenPassedANegativeBalance_ShouldThrowArgumentOutOfRangeException()
        {

            decimal initialBalance = -100m;


            Assert.That(() => new Account(initialBalance),
                Throws.InstanceOf<ArgumentOutOfRangeException>());

            //var sut = new Account(initialBalance);

            //try
            //{
            //    var sut = new Account(initialBalance);
            //    Assert.Fail();
            //}
            //catch (ArgumentOutOfRangeException error)
            //{
            //    Assert.Pass();
            //}



        }
    }
}
