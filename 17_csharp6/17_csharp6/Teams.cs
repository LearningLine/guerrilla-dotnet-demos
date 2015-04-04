using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17_csharp6
{
	class RaceTeam
	{
		public Car Car { get; set; }
	}

	class Car
	{
		public Driver Driver { get; set; }
	}

	public class Driver
	{
		public int? Ranking { get; set; }
	}


}
