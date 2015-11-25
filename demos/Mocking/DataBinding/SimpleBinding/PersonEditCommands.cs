using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SimpleBinding
{
    public class PersonEditCommands
    {
        public static readonly RoutedCommand Save = new RoutedCommand();
        public static readonly RoutedCommand Birthday = new RoutedCommand();

        public static readonly RoutedCommand Next = new RoutedCommand();
        public static readonly RoutedCommand Prev = new RoutedCommand();

    }
}
