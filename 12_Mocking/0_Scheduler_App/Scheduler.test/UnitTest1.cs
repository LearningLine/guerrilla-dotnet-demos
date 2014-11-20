using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TheScheduler;

namespace Scheduler.test
{
    [TestClass]
    public class SchedulingServiceTest
    {
        private Mock<ILogger> logger;
        private Mock<ITimeService> timeService;

        private SchedulingService CreateSut()
        {
            return new SchedulingService(logger.Object, timeService.Object);
        }

        [TestInitialize]
        public void Init()
        {
            logger = new Mock<ILogger>();
            timeService = new Mock<ITimeService>();
        }

        [TestMethod]
        public void AScheduledJob_ShouldCompleteOnTime()
        {
            timeService.SetupSequence(ts => ts.Now)
                       .Returns(DateTime.Now)
                       .Returns(DateTime.Now +
                                TimeSpan.FromDays(300) +
                                TimeSpan.FromSeconds(1));

            var sut = CreateSut();

            sut.AddJob(1, "Hello", TimeSpan.FromDays(300));

            //timeService.Setup(ts => ts.Now)
            //           .Returns(DateTime.Now + 
            //                    TimeSpan.FromDays(300) + 
            //                    TimeSpan.FromSeconds(1));

            Assert.IsTrue(sut.IsJobFinished(1));
        }

        [TestMethod]
        public void AScheduledJob_ShouldCauseALogRecord()
        {
            var sut = CreateSut();

            sut.AddJob(1, "Hello", TimeSpan.FromDays(300));

            logger.Verify(l => l.Message(It.IsAny<string>()), Times.AtLeastOnce);            
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
