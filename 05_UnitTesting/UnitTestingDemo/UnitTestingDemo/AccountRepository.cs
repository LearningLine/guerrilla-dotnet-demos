using System;
using System.Xml;

namespace UnitTestingDemo
{
	public class AccountRepository : IAccountRepository
	{
		private decimal balance;

		public AccountRepository(int id)
		{
			balance = 15.0m;
		}

		public decimal Deposit(decimal amount)
		{
			throw new Exception("Server is down");
			balance += amount;
			return balance;
		}

		public decimal Withdraw(decimal amount)
		{
			balance -= amount;
			return balance;
		}

		public decimal GetBalance()
		{
			return balance;
		}
	}
}