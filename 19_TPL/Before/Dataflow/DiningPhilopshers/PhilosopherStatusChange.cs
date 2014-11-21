namespace DiningPhilopshers
{
    public class PhilosopherStatusChange
    {
        public int PhilospherId { get; set; }
        public string Status { get; set; }

        public PhilosopherStatusChange(int philospherId, string status)
        {
            PhilospherId = philospherId;
            Status = status;
        }
    }
}