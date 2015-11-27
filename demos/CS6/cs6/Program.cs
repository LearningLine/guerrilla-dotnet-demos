using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using static System.Console;

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
			ILogger log = new NullLogger();

			var first = "Rich";
			var last = "Blewett";

			var fullName = string.Format("{0} {1}", first, last);
			if (loggerConfig != null && loggerConfig.Logger != null)
			{
				if (loggerConfig.Logger != null)
				{
					log = loggerConfig.Logger;
				}
				else
				{
					log = new NullLogger();
				}
			}

			var p = new Person(log);

			WriteLine(p);
			p.Age = 21;

			WriteLine(p.IsAdult);

			WriteLine(p.IsOlderThan(20));

			WriteLine(p);
		}

		private static async void DoStuff(ILogger logger)
		{
			try
			{
				DecryptMaster();

				ApplyDataFixups();
			}
			catch (Win32Exception x)
			{
				if (x.NativeErrorCode == (int) Win32Error.AccessDenied)
				{
					// ...
				}
				else
				{
					throw;
				}
			}
			finally
			{
				ProtectMaster();
			}
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

		public Person(ILogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException(nameof(logger));
			}
			this._log = logger;
		}

		public Person()
			: this(new NullLogger())
		{
		}

		public string Name { get; set; } = "not set";
		public DateTime LastUpdated { get; private set; } = DateTime.Now;
		public int Age { get; set; }

		public bool IsAdult
		{
			get { return Age >= 18; }
		}

		public override string ToString()
		{
			return string.Format("{0} is {1}", Name, Age);
		}

		public bool IsOlderThan(int testAge)
		{
			return Age > testAge;
		}
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