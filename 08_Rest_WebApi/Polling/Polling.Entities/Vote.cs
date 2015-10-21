using System.Runtime.Serialization;

namespace Polling.Entities
{
    [DataContract]
    public class Vote
    {
        [DataMember]
        public int Id { get; set; }
        public Choice Choice { get; set; }
        public int ChoiceId { get; set; }
        public Poll Poll { get; set; }
        public int PollId { get; set; }
    }
}