using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polling.Entities;
using System.Data.Entity;
using NLog;

namespace Polling.DataAccess
{
    public class PollsRepository : EFRepository<Poll>, IPollsRepository
    {
        private readonly PollingContext pollingContext;
        static Logger logger = LogManager.GetLogger("PollsContext");

        public PollsRepository(PollingContext context) : base(context)
        {
            pollingContext = context;
            pollingContext.Database.Log = s => logger.Debug(s);
        }

        public override Poll GetById(int id)
        {
            var poll = pollingContext.Polls
                .Include(p => p.Choices)
                .Include(p => p.Votes)
                .FirstOrDefault(p => p.Id == id);

            return poll;
        }

        public void AddVote(Vote vote)
        {
            pollingContext.Votes.Add(vote);
        }
    }
}
