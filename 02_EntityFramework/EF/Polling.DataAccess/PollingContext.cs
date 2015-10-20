using System.Collections.Specialized;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Polling.Entities;

namespace Polling.DataAccess
{
    public class ChoiceConfiguration : EntityTypeConfiguration<Choice>
    {
        public ChoiceConfiguration()
        {
            Property(c => c.ChoiceText)
                .HasMaxLength(100);

        }
    }
    public class PollingContext : DbContext
    {


        public PollingContext()
            : base("name=pollingConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //var entityTypeConfiguration = modelBuilder.Entity<Choice>();
            modelBuilder.Configurations.Add(new ChoiceConfiguration());
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
