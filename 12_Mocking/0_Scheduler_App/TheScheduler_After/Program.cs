using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TheScheduler
{
	class Program
	{
		static void Main(string[] args)
		{
			SchedulingService svc = new SchedulingService();

			svc.AddJob( 1, "some inputs", TimeSpan.FromSeconds(5) );
			while ( !svc.IsJobFinished( 1 ) )
			{
				Thread.Sleep( 1000 );
				Console.Write( "." );
			}
			Console.WriteLine();

			Console.WriteLine( "Job 1 finished." );
			Job job = svc.GetJob( 1 );
			Console.WriteLine( "Job data: " + job.Data );
		}
	}
}
