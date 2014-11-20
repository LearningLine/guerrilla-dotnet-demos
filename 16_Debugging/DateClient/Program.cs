using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DateClient
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}

	partial class Form1
	{
		string[] Replies = new string[]
		{
			"WHATEVER!:",
			"Sorry, I'm engaged",
			"I'm just here to hang out with my friends",
			"Sorry, no thanks",
			"Here's my number: 555-1212",
			"Sorry I'm moving.  To Australia",
			"Why don't you message me on my mySpace page",
			"What did you just say to me???",
			"Aren't you the guy with the Yugo?",
			"No thanks, but my friend over there might be interested.",
			"Does that line actually work for you?",
			"You should think about working out a bit",
			"It's not you, it's me",
			"Maybe later, I have to meet my probation officer",
		};

		string[] Exceptions = new string[]
		{
			 "That is the most insulting thing I've ever heard!",
			 "Take a shower, creep",
			 "Nice video game shirt. Loser.",
			 "OMG You're like, my dad's age!",
			 "I thought they banned you from this place!",
			 "Move out of your mom's basement.",
			 "Get a tan, nerd.",
			 "I don't date guys that speak Klingon"
		};

		Random rng = new Random();

		public string TryLine(string line)
		{
			string standardInsult = Replies[rng.Next(Replies.Length)];
			int next = rng.Next(10);
			if (next != 0)
				return standardInsult;

			string extremeInsult = Exceptions[rng.Next(Exceptions.Length)];

			DrinkInYourFaceException ex = new DrinkInYourFaceException(extremeInsult);
			throw ex;
		}
	}

	[global::System.Serializable]
	public class DrinkInYourFaceException : Exception
	{
		public DrinkInYourFaceException() { }
		public DrinkInYourFaceException(string message) : base(message) { }
		public DrinkInYourFaceException(string message, Exception inner) : base(message, inner) { }
		protected DrinkInYourFaceException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
