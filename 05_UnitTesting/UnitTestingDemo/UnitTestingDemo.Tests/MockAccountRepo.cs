using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingDemo.Tests
{
	public class MockAccountRepository : IAccountRepository
	{
		private decimal balance;

		public MockAccountRepository(int id)
		{
			balance = 15.0m;
		}

		public decimal Deposit(decimal amount)
		{
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
