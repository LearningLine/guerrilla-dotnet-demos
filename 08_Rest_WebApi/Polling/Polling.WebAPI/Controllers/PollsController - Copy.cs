using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Polling.DataAccess;
using Polling.Entities;

namespace Polling.WebAPI.Controllers
{
    public class PollsOldController : ApiController
    {
        private readonly IPollsRepository repo;

        public PollsOldController()
        {
            repo = new PollsRepository(new PollingContext());
        }
        public PollsOldController(IPollsRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<Poll> GetAll()
        {
            return repo.GetAll();
        }

        public IHttpActionResult GetById(int id)
        {
            var poll = repo.GetById(id);
            if (poll == null)
            {
                return NotFound();
            }

            return Ok(poll);
        }

        //public Poll GetByText(string id)
        //{
        //    return repo.GetAll().FirstOrDefault(p => p.QuestionText.Contains(id));
        //}

        public IHttpActionResult Post(Poll poll)
        {
            repo.Add(poll);
            repo.Save();

            return Created(Request.RequestUri + poll.Id.ToString(),poll);
        }

        public IHttpActionResult Put(int id, Poll poll)
        {
            var dbPoll = repo.GetById(id);

            if (dbPoll == null)
            {
                return NotFound();
            }
                
            dbPoll.QuestionText = poll.QuestionText;
            repo.Save();

            return Ok(poll);
        }

        public IHttpActionResult Delete(int id)
        {
            repo.RemoveById(id);
            repo.Save();

            return Ok();
        }
    }
}
