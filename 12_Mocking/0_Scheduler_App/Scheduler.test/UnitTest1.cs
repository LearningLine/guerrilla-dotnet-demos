using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheScheduler;

namespace Scheduler.test
{
    [TestClass]
    public class SchedulingServiceTests
    {
        [TestMethod]
        public void AScheduledJob_ShouldCompleteOnTime()
        {
            var sut = new SchedulingService(null, null);

            sut.AddJob(1, "Hello", TimeSpan.FromDays(300));

            Thread.Sleep(1200);

            Assert.IsTrue(sut.IsJobFinished(1));
        }
    }

    //class FakeLogger : ILogger
    //{
    //    public void Message(string message, params object[] args)
    //    {
          
    //    }

    //    public void Message(string message)
    //    {
    //    }
    //}

}
