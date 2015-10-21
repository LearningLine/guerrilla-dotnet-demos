using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Polling.DataAccess;
using Polling.Entities;
using Polling.WebApp.Controllers;

namespace Polling.WebApp.Test
{
    [TestClass]
    public class PollsControllerTest
    {
        [TestMethod]
        public void TestDetails()
        {
            var mockRepo = new Mock<IPollsRepository>();
            mockRepo.Setup(r => r.GetById(1)).Returns(new Poll { Id = 1 });
            var controller = new PollsController(mockRepo.Object);

            var result = controller.Details(1);

            Approvals.Verify(result.AssertViewModel<Poll>("Details"));
        }

    }
}
