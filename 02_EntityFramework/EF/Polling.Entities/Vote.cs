namespace Polling.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public Choice Choice { get; set; }
        public int ChoiceId { get; set; }
    }
}