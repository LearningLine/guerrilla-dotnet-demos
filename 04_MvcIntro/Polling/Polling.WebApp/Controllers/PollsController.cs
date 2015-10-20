using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Polling.DataAccess;
using Polling.Entities;
using System.Data.Entity;

namespace Polling.WebApp.Controllers
{
    public class PollsController : Controller
    {
        private PollingContext pollingContext;

        public PollsController()
        {
            pollingContext = new PollingContext();
        }

        // GET: Polls
        public ActionResult Index()
        {
            return View(pollingContext.Polls.ToArray());
        }

        public ActionResult Details(int id)
        {
            var poll = pollingContext.Polls
                .Include(p => p.Choices)
                .Include(p => p.Votes)
                .FirstOrDefault(p => p.Id == id);

            return View(poll);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Poll poll)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            pollingContext.Polls.Add(poll);
            pollingContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("Polls/{id}/Vote")]
        public ActionResult Vote(int id)
        {
            var poll = pollingContext.Polls.Find(id);
            return View(poll);
        }

        [HttpPost]
        [Route("Polls/{id}/Vote")]
        public ActionResult Vote(Vote vote)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            pollingContext.Votes.Add(vote);
            pollingContext.SaveChanges();

            return RedirectToAction("Details",new { id = vote.PollId });
        }

        protected override void Dispose(bool disposing)
        {
            pollingContext.Dispose();
            base.Dispose(disposing);
        }
    }
}