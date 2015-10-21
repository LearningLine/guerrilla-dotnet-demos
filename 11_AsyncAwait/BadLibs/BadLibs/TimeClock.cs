using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows;

namespace BadLibs
{
    public class TimeClock
    {
        private Timer _timer;
        private readonly Window _window;
        private readonly Action _updateTime;

        public TimeClock(Window window, Action updateTime)
        {
            _updateTime = updateTime;
            _window = window;

            _timer = new Timer { Interval = 800 };
            _window.Closing += OnClosing;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _window.Dispatcher.Invoke(() =>
            {
                try
                {
                    _updateTime();
                }
                catch (Exception)
                {
                }
            });
        }

        protected void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _timer.Dispose();
        }
    }
}
