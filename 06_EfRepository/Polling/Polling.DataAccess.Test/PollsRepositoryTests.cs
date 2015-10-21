using System;
using ApprovalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Polling.DataAccess.Test
{
    [TestClass]
    public class PollsRepositoryTests
    {
        [TestMethod]
        public void TestGetByIdReturnsPollsChoices()
        {
            var memoryTarget = TestLoggingExtensions.SetupNLogMemoryTarget();
            var repo = new PollsRepository(new PollingContext());

            repo.GetById(1);

            Approvals.VerifyAll(memoryTarget.Logs.FilterOutMigrationLogs(), "log");
        }
    }
}
