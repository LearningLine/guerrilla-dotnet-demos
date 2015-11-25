using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfTheRightWay.ViewModels;

namespace WpfTheRightWay.Test
{
    [TestClass]
    public class PictureViewModelTests
    {
        [TestMethod]
        public void CanExecute_WhenCalled_WithInvalidUri_ShouldReturnFalse()
        {
            var sut = CreateSut();

            sut.Url = "Not a URL";

            Assert.IsFalse(sut.GoCommand.CanExecute(null));
        }

        private PictureViewModel CreateSut()
        {
            return new PictureViewModel();
        }
    }
}
