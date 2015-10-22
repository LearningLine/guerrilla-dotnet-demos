using IssueTracker.Data;
using MongoDB.Driver;

namespace IssueTracker.MongoData
{
    public class IssueTrackerContext
    {
        private MongoDatabase _database;

        public IssueTrackerContext()
        {
            _database = new MongoClient("mongodb://localhost:27017").GetServer().GetDatabase("Support");
        }

        public MongoCollection<Issue> Issues
        {
            get { return _database.GetCollection<Issue>(); }
        }

        public MongoCollection<User> Users
        {
            get { return _database.GetCollection<User>(); }
        }
    }
}