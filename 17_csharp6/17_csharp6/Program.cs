using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Console;

namespace _17_csharp6
{
	class Program
	{
		static void Main(string[] args)
		{
			var o = new Order(7);
			o.Products.Add(new Product(7, 42.0, "The larch"));
			o.Products.Add(new Product(7, 42.0, "The larch II"));

			Console.WriteLine(o);
			WriteLine(o);

			var team = GetRaceTeam();

			if (team.Car != null)
			{
				var car = team.Car;
				if (car.Driver != null)
				{
					var d = car.Driver;
					if (d.Ranking != null)
						Console.WriteLine("The ranking is " + d.Ranking.Value);
					else
						Console.WriteLine("No ranking");
				}
				else
					Console.WriteLine("No ranking");
			}
			else
				Console.WriteLine("No ranking");


			// C# 6.0 way
			int? ranking = team.Car?.Driver?.Ranking;
			Console.WriteLine( ranking == null? "No ranking" : ranking.Value.ToString());
		}

		private static RaceTeam GetRaceTeam()
		{
			return new RaceTeam();
		}
	}
}
