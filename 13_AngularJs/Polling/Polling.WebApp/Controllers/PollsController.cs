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
        private IPollsRepository repository;

        public PollsController()
        {
            repository = new PollsRepository(new PollingContext());
        }

        public PollsController(IPollsRepository repo)
        {
            repository = repo;
        }

        // GET: Polls
        public ActionResult Index()
        {
            var polls = repository.GetAll();
            return View(polls);
        }

        public ActionResult Details(int id)
        {
            var poll = repository.GetById(id);
            return View("Details", poll);
        }

        public ActionResult Create()
        {
            // return Details(1); ??
            return View();
        }

        [HttpPost]
        public ActionResult Create(Poll poll)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            repository.Add(poll);
            repository.Save();

            return RedirectToAction("Index");
        }

        [Route("Polls/{id}/Vote")]
        public ActionResult Vote(int id)
        {
            var poll = repository.GetById(id);
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

            repository.AddVote(vote);
            repository.Save();

            return RedirectToAction("Details",new { id = vote.PollId });
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}