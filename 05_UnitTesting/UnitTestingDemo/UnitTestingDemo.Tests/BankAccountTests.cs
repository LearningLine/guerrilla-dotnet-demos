using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.SimpleLogger.Writers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestingDemo.Tests
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class BankAccountTests
	{
		[TestMethod]
		public void BankAccount_WhenMoneyDeposited_BalanceIsIncreasedByDepositedAmount()
		{

			var bankAccount = new BankAccount(new MockAccountRepository(id:17));
			var previousBalance = bankAccount.Balance;

			var depositedAmount = 15.0m;
			bankAccount.Deposit(depositedAmount);

			Assert.AreEqual(depositedAmount + previousBalance, bankAccount.Balance);
		}

		[TestMethod]
		public void BankAccount_WhenMoneyWithdrawn_BalanceIsDecreasedByWithdrawnAmount()
		{
			var bankAccount = new BankAccount(new MockAccountRepository(id: 17));
			var initialAmount = 100.0m;
			bankAccount.Deposit(initialAmount);
			var previousBalance = bankAccount.Balance;

			var withdrawnAmount = 15.0m;
			bankAccount.Withdraw(withdrawnAmount);

			var expectedAmount = previousBalance - withdrawnAmount;
			Assert.AreEqual(expectedAmount, bankAccount.Balance);
		}

		[TestMethod]
		public void BankAccount_WhenTransferBetweenAccounts_AllBalancesAreCorrect()
		{
			var account1 = new BankAccount(new MockAccountRepository(id: 17));
			var account1StartingBalance = account1.Balance;
			var account2 = new BankAccount(new MockAccountRepository(id: 42));
			var account2StartingBalance = account2.Balance;

			var transferAmount = 5.0m;
			account1.TransferAmountTo(transferAmount, account2);

			//var account1Expected = account1StartingBalance - transferAmount;
			//Assert.AreEqual(account1Expected, account1.Balance);
			//var account2Expected = account2StartingBalance + transferAmount;
			//Assert.AreEqual(account2Expected, account2.Balance);

			Approvals.VerifyAll(new[] { account1, account2}, "account");
		}
	}
}
