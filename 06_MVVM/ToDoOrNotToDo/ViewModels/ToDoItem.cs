using System.Collections.Generic;
using MVVM;

namespace ToDoOrNotToDo.ViewModels
{
    public class ToDoItemViewModel : ViewModel
    {
        private Dictionary<int, string> priorityMap = new Dictionary<int, string>
        {
            {1, "Low"},
            {2, "Medium"},
            {3, "High"},
        };

        //private Dictionary<int, Brush> brushMap = new Dictionary<int, Brush>
        //{
        //    {1, Brushes.MidnightBlue},
        //    {2, Brushes.SteelBlue},
        //    {3, Brushes.DarkOrange},
        //};

        public ToDoItemViewModel()
        {
            Priority = 1;
        }
        private int _priority;
        private string _title;

        private string priorityAsString;
        public string PriorityAsString
        {
            get { return priorityAsString; }
            set
            {
                priorityAsString = value;
                OnPropertyChanged();
            }
        }

        //private Brush priorityBrush;

        //public Brush PriorityBrush
        //{
        //    get { return priorityBrush; }
        //    set { priorityBrush = value; OnPropertyChanged();}
        //}
        

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public int Priority
        {
            get { return _priority; }
            set 
            {
                _priority = value;
                PriorityAsString = priorityMap[_priority];
                OnPropertyChanged();
//                PriorityBrush = brushMap[_priority];
            }
        }
    }
}