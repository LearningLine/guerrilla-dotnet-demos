using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Polling.Entities
{
    public class Choice
    {
        public int Id { get; set; }
        public Poll Poll { get; set; }
        public int PollId { get; set; }
        public ICollection<Vote> Votes { get; set; }
        [StringLength(100)]
        public string ChoiceText { get; set; }
    }
}