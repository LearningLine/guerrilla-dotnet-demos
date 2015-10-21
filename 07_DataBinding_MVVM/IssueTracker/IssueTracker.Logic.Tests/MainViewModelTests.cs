using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IssueTracker.Logic.Tests
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void Login_SetsIsLoggedIn_True()
        {
            //Arrange
            var vm = new MainViewModel();

            //Act
            vm.LoginCommand.Execute("Brad");
           
            //Assert
            Assert.IsTrue(vm.IsLoggedIn);
        }
    }
}
