using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Polling.Entities
{
    [DataContract]
    public class Poll
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [StringLength(100)]
        public string QuestionText { get; set; }

        [DataMember]
        public IList<Choice> Choices { get; set; }

        [IgnoreDataMember]
        public IList<Vote> Votes { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
