using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polling.Entities
{
    public class Poll
    {
        public virtual int Id { get; set; }
        [StringLength(100)]
        public virtual string QuestionText { get; set; }
        public virtual ICollection<Choice> Choices { get; set; } 
    }
}
