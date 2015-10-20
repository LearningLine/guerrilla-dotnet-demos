using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Polling.DataAccess;
using Polling.Entities;

namespace Polling.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PollingContext>());

            using (var context = new PollingContext())
            {
                //context.Database.Initialize(force: true);

                //Create
                var poll = new Poll
                {
                    QuestionText = "What is your favorite color",
                    Choices = new List<Choice>
                    {
                        new Choice {ChoiceText = "Red"},
                        new Choice {ChoiceText = "Yellow"},
                        new Choice {ChoiceText = "Green"},
                        new Choice {ChoiceText = "Azure"},
                    }
                };

                //context.Polls.Add(poll);
                //context.SaveChanges();

                //Update

                var firstPoll = context.Polls.First();
                //firstPoll.QuestionText = "What is your favorite color?";
                //context.SaveChanges();

                ////Delete
                //context.Polls.Remove(firstPoll);
                //context.SaveChanges();

            }
        }
    }
}
