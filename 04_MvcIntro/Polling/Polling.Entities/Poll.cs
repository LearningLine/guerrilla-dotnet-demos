using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Polling.Entities
{
    public class Poll
    {
        public virtual int Id { get; set; }
        [StringLength(100)]
        public virtual string QuestionText { get; set; }
        public virtual IList<Choice> Choices { get; set; } 
        public virtual IList<Vote> Votes { get; set; } 
    }
}
