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
}