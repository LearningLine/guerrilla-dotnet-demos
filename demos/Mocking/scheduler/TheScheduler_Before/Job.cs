using System;

namespace TheScheduler
{
	public class Job
	{
		public int JobId { get; set; }
		public DateTime Time { get; set; }
		public string Data { get; set; }

		public Job(int jobId)
		{
			JobId = jobId;
		}

	}
}