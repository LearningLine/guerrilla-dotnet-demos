using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using IssueTracker.MongoData;
using Microsoft.AspNet.SignalR.Hubs;

namespace IssueTracker.Web
{
    [HubName("tracking")]
    public class TrackingHub : Hub
    {
        private MongoIssueService _issueService = new MongoIssueService();

        public void ReportIssue(string issueId)
        {
            _issueService.ReportExistingIssue(issueId);
            Clients.All.issueChanged(issueId);
            Groups.Add(Context.ConnectionId, issueId);
        }

        public void ResolveIssue(string issueId)
        {
            _issueService.ResolveIssue(issueId, "Fixed itself", DateTime.UtcNow);
            Clients.OthersInGroup(issueId).issueFixed(issueId, true);
            Clients.Caller.issueFixed(issueId, false);
            Clients.Group("AllFixes").issueFixed(issueId, false);
        }

        [HubMethodName("watchFixes")]
        public void RegisterForAllFixNotifications()
        {
            Groups.Add(Context.ConnectionId, "AllFixes");
        }
    }
}