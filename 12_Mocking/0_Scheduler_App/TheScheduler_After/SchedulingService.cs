using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheScheduler
{
    public class SchedulingService
    {
        private ILogger logger;
        private ITimeService timeService;


		Dictionary<int, Job> jobs = new Dictionary<int, Job>();

		public SchedulingService(ILogger logger, ITimeService timeService)
		{
		    this.logger = logger;
		    this.timeService = timeService;
		}

        public void AddJob(int jobId, string data, TimeSpan ts)
		{
			logger.Message(data);
         
			Job j = new Job(jobId);
			j.Data = data;
            j.Time = timeService.Now + ts;

			jobs.Add( jobId, j );
		}

        public bool IsJobFinished(int jobId)
		{
			Job job = jobs[jobId];
			return job.Time < timeService.Now;
		}

		public Job GetJob(int jobId)
		{
			logger.Message( "Finished job {0}.", jobId );

			return jobs[jobId];
		}
	}

    public class RealTimeService : ITimeService
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }




}
















