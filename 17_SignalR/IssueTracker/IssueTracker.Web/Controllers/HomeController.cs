using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IssueTracker.Web.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace IssueTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new IssuesViewModel());
        }

        private void FixIssue(string id)
        {
            var man = GlobalHost.DependencyResolver.GetService(typeof(IConnectionManager)) as IConnectionManager;
            IHubContext hubContext = man.GetHubContext<TrackingHub>();

            hubContext.Clients.Group(id).issueFixed(id, true);
            hubContext.Clients.Group("AllFixes").issueFixed(id, false);
        }
    }
}