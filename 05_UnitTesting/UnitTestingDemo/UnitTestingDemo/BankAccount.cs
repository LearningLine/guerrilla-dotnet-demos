using System.Runtime.InteropServices;

namespace UnitTestingDemo
{
	public class BankAccount
	{
		private readonly IAccountRepository accountRepo;

		public BankAccount(int id)
		{
			this.accountRepo = new AccountRepository(id);
		}

		public BankAccount(IAccountRepository accountRepo)
		{
			this.accountRepo = accountRepo;
		}

		public decimal Balance { get { return accountRepo.GetBalance(); } }

		public void Deposit(decimal amount)
		{
			accountRepo.Deposit(amount);
		}

		public void Withdraw(decimal amount)
		{
			accountRepo.Withdraw(amount);
		}

		public void TransferAmountTo(decimal transferAmount, BankAccount account2)
		{
			Withdraw(transferAmount);
			account2.Deposit(transferAmount);
		}

		public override string ToString()
		{
			return string.Format("Balance={0}", Balance);
		}
	}
}