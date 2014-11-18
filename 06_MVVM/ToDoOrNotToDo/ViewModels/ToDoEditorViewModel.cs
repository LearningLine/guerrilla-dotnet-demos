using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using MVVM;

namespace ToDoOrNotToDo.ViewModels
{
    public class ToDoEditorViewModel : ViewModel
    {
        private readonly Func<string, bool> _confirm;
        private ToDoItemViewModel _newItem;

        public ToDoEditorViewModel(Func<string,bool> confirm )
        {
            _confirm = confirm;
            NewItem = new ToDoItemViewModel();
            Items = new ObservableCollection<ToDoItemViewModel>();
            AddNewItem = new DelegateCommand(DoAddNewItem);

            NewItem.Title = "<add title>";
        }

        private void DoAddNewItem(object obj)
        {
            if (_confirm("Are you sure?"))
            {

                Items.Add(NewItem);
                NewItem = new ToDoItemViewModel();
            }
        }

        public ToDoItemViewModel NewItem
        {
            get { return _newItem; }
            set { _newItem = value; OnPropertyChanged();}
        }

        public ICommand AddNewItem { get; set; }
        public ObservableCollection<ToDoItemViewModel> Items { get; set; }
    }
}