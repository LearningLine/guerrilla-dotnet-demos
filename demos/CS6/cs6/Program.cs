using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

//using static System.Console;
//using static System.Diagnostics.Debug;

namespace cs6
{
	internal static class Foo
	{
		public static void DoIt()
		{
		}
	}

	internal static class Bar
	{
		public static void DoIt()
		{
		}
	}

	internal enum AuthenticationData
	{
		UserId,
		Password,
		OTP,
		Certificate
	}

	internal enum AuthenticationDataType
	{
		Text,
		ProtectedText,
		List
	}

	internal class Program
	{
		private static LoggerConfig loggerConfig;

		private static void NewMain(string[] args)
		{
			var authMap = new Dictionary<AuthenticationData, AuthenticationDataType>
			{
				{AuthenticationData.UserId, AuthenticationDataType.Text},
				{AuthenticationData.Password, AuthenticationDataType.ProtectedText}
			};
		}

		private static void Main(string[] args)
		{
			//ILogger log = new NullLogger();

			var first = "Rich";
			var last = "Blewett";

			var fullName2 = string.Format("{0} {1}", first, last);
			var fullName = $"My name is {first.ToUpper()} {last}";

			Console.WriteLine(fullName);

			//bool needsNullLogger = loggerConfig == null ||
			//	loggerConfig.Logger == null;

			//bool needsNullLogger = loggerConfig?.Logger == null;

			var log = loggerConfig?.Logger ?? new NullLogger();


			//if (loggerConfig != null && loggerConfig.Logger != null)
			//{
			//	if (loggerConfig.Logger != null)
			//	{
			//		log = loggerConfig.Logger;
			//	}
			//	else
			//	{
			//		log = new NullLogger();
			//	}
			//}

			var p = new Person(log);

			Console.WriteLine(p);
			p.Age = 21;

			Console.WriteLine(p.IsAdult);

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine(p.IsOlderThan(20));

			Console.WriteLine(p);
		}

		private static async void DoStuff(ILogger logger)
		{
			try
			{
				DecryptMaster();
				ApplyDataFixups();
				ProtectMaster();
			}
			catch
			{
				ProtectMaster();
				throw;
			}


			//try
			//{
			//	DecryptMaster();

			//	ApplyDataFixups();
			//}
			//catch (Win32Exception x)
			//when(x.NativeErrorCode == (int)Win32Error.AccessDenied)
			//{
			//	// ...
			//}
			//finally
			//{
			//	ProtectMaster();
			//}
		}

		private static void ApplyDataFixups()
		{
			throw new NotImplementedException();
		}

		private static void ProtectMaster()
		{
			throw new NotImplementedException();
		}

		private static void DecryptMaster()
		{
			throw new NotImplementedException();
		}

		private static void OpenDatabase()
		{
		}

		private static Task InitializeCloudData()
		{
			return Task.FromResult(1);
		}

		private static ILogger GetLogger()
		{
			return (loggerConfig != null ? loggerConfig.Logger : null) ??
			       new NullLogger();
		}

		private enum Win32Error
		{
			AccessDenied = 5
		}
	}

	internal class LoggerConfig
	{
		public ILogger Logger { get; set; }
	}

	internal class Person
	{
		private ILogger _log;
		private int age = 25;

		public Person(ILogger theLogger)
		{
			if (theLogger == null)
			{
				//throw new ArgumentException($"logger is {logger}");
				throw new ArgumentNullException(nameof(theLogger));
			}
			this._log = theLogger;
			WriteLine("We made a person!");
		}

		public Person() : this(new NullLogger())
		{
		}

		public string Name { get; set; }
		public DateTime LastUpdated { get; } = DateTime.Now;

		public int Age
		{
			get { return age; }
			set
			{
				age = value;
				OnIsAdult?.Invoke(this, EventArgs.Empty);
			}
		}

		public bool IsAdult => Age >= 18;

		public void WriteLine(string s)
		{
			Console.WriteLine("This is the: " + s);
		}

		public event EventHandler OnIsAdult;

		//public bool IsAdult
		//{
		//	get { return Age >= 18; }
		//}

		public override string ToString() => $"{Name} is {Age}";

		//public override string ToString()
		//{
		//	return $"{Name} is {Age}";
		//}

		public bool IsOlderThan(int testAge) => Age > testAge;
		//}
		//	return Age > testAge;
		//{

		//public bool IsOlderThan(int testAge)
	}

	internal interface ILogger
	{
		void Log(string format, params object[] args);
		Task LogAsync(string format, params object[] args);
	}

	internal class NullLogger : ILogger
	{
		public void Log(string format, params object[] args)
		{
		}

		public Task LogAsync(string format, params object[] args)
		{
			return Task.FromResult<object>(null);
		}
	}

	internal class FileLogger : ILogger
	{
		private readonly string fileName;

		public FileLogger(string fileName)
		{
			this.fileName = fileName;
		}

		public void Log(string format, params object[] args)
		{
			using (var writer = new StreamWriter(fileName))
			{
				writer.WriteLine(format, args);
			}
		}

		public async Task LogAsync(string format, params object[] args)
		{
			using (var writer = new StreamWriter(fileName))
			{
				await writer.WriteLineAsync(string.Format(format, args))
					.ConfigureAwait(false);
			}
		}
	}
}