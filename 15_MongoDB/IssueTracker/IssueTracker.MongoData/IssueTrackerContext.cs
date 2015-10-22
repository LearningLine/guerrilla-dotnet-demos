using IssueTracker.Data;
using MongoDB.Driver;

namespace IssueTracker.MongoData
{
    public class IssueTrackerContext
    {
        private MongoDatabase _database;

        public IssueTrackerContext()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            _database = server.GetDatabase("Support");
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