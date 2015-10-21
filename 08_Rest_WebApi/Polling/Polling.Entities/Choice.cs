using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Polling.Entities
{
    [DataContract]
    public class Choice
    {
        [DataMember]
        public int Id { get; set; }

        [IgnoreDataMember]
        public Poll Poll { get; set; }

        [IgnoreDataMember]
        public int PollId { get; set; }

        [IgnoreDataMember]
        public ICollection<Vote> Votes { get; set; }

        [DataMember]
        [StringLength(100)]
        public string ChoiceText { get; set; }

        public Choice()
        {
            Votes = new List<Vote>();
        }
    }
}