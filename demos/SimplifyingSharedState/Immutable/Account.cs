namespace Immutable
{
    public interface IAccount
    {
        int Id { get; }
        decimal Balance { get; }
    }

    public class Account : IAccount
    {
        private readonly int id;

        public Account(int id)
        {
            this.id = id;
        }
        public int Id { get { return id; } }

        public decimal Balance { get;  set; }
    }
}