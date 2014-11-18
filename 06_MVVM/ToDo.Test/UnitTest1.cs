using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoOrNotToDo.ViewModels;

namespace ToDo.Test
{
    [TestClass]
    public class ToDoEditorViewModelTests
    {
        [TestMethod]
        public void AddButton_WhenPressedAndTitleAndPriorityHaveBeenSet_ShouldAddNewToDoItemToCollection()
        {
            var sut = new ToDoEditorViewModel(_ => true);
            sut.NewItem.Title = "Foo";
            sut.NewItem.Priority = 3;

            sut.AddNewItem.Execute(null);

            Assert.AreEqual(1, sut.Items.Count());
        }

        [TestMethod]
        public void AddButton_WhenPressedAndNoConfirmationGiven_ShouldNOTAddNewToDoItemToCollection()
        {
            var sut = new ToDoEditorViewModel(_ => false);
            sut.NewItem.Title = "Foo";
            sut.NewItem.Priority = 3;

            sut.AddNewItem.Execute(null);

            Assert.AreEqual(0, sut.Items.Count());
        }
    }
}
