using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TheScheduler
{
    public class Logger
    {
		public string Logfile { get; set; }

		public Logger(string logfile)
		{
			Logfile = logfile;
		}

		public void Message(string message, params object[] args)
		{
			string text = string.Format( message, args );
			File.AppendAllLines( Logfile, new[] { text } );
		}

		public void Message(string message)
		{
		
		}
	}
}
