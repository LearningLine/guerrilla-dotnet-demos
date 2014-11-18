using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheScheduler
{
    public class SchedulingService
	{
		private Logger logger = new Logger("logfile.txt");
		Dictionary<int, Job> jobs = new Dictionary<int, Job>();

		public SchedulingService()
	    {
	    }

	    public void AddJob(int jobId, string data, TimeSpan ts)
		{
			logger.Message(data);
         
			Job j = new Job(jobId);
			j.Data = data;
            j.Time = DateTime.Now + ts;

			jobs.Add( jobId, j );
		}

        public bool IsJobFinished(int jobId)
		{
			Job job = jobs[jobId];
			return job.Time < DateTime.Now;
		}

		public Job GetJob(int jobId)
		{
			logger.Message( "Finished job {0}.", jobId );

			return jobs[jobId];
		}
	}
}
