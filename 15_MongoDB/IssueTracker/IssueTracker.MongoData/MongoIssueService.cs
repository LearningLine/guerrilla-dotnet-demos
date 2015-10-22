using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTracker.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
namespace IssueTracker.MongoData
{
    public class MongoIssueService : IIssueService
    {
        IssueTrackerContext _context = new IssueTrackerContext();

        public IEnumerable<Issue> AllIssues
        {
            get
            {
                return _context.Issues.FindAll().ToList();
            }
        }

        public IEnumerable<Issue> ActiveIssues
        {
            get
            {
                // TODO: get issues with no fix
                return new List<Issue>();
            }
        }

        public Issue GetIssue(string text)
        {
            return _context.Issues.FindOne(new QueryBuilder<Issue>().Where(i => i.Text == text));
        }

        public User GetUser(string name)
        {
            return _context.Users.FindOne(new QueryBuilder<User>().Where(i => i.Name == name));
        }

        public void ReportIssue(Issue issue, User user)
        {
            issue.ReportCount++;

            _context.Issues.Save(issue);

            if (!user.ReportedIssues.Contains(issue))
            {
                user.ReportedIssues.Add(issue);

                _context.Users.Save(user);
            }
        }

        public void ReportExistingIssue(Issue issue)
        {
            _context.Issues.FindAndModify(new FindAndModifyArgs
            {
                Query = new QueryBuilder<Issue>().Where(i => i.Id == issue.Id),
                Update = new UpdateBuilder<Issue>().Inc(i => i.ReportCount, 1),
                Upsert = false,
            });
        }

        public void ResolveIssue(Issue issue, string fix, DateTime found)
        {
        }
    }
}
