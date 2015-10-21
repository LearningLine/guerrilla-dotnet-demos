namespace Polling.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public Choice Choice { get; set; }
        public int ChoiceId { get; set; }
        public Poll Poll { get; set; }
        public int PollId { get; set; }
    }
}