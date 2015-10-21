namespace UnitTestingDemo
{
	public interface IAccountRepository
	{
		decimal Deposit(decimal amount);
		decimal Withdraw(decimal amount);
		decimal GetBalance();
	}
}