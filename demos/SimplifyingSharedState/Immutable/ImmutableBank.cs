using System.Collections.Generic;
using System.Collections.Immutable;

namespace Immutable
{
    public class ImmutableBank : IBank
    {
        ImmutableList<ImmutableAccount> accounts = ImmutableList<ImmutableAccount>.Empty;

        public void AddAccount(decimal initialBalance)
        {
            accounts = 
                accounts.Add(new ImmutableAccount(accounts.Count, initialBalance));
        }

        public IEnumerable<IAccount> Accounts { get { return accounts; } }
        public void TransferFunds(int sourceAccountId, int destAccountId, decimal amount)
        {
            accounts = accounts.SetItem(sourceAccountId,
                accounts[sourceAccountId].WithDebit(amount))
                .SetItem(destAccountId,
                    accounts[destAccountId].WithCredit(amount));
        }
    }

    class ImmutableAccount : IAccount
    {
        public ImmutableAccount(int id, decimal balance)
        {
            Id = id;
            Balance = balance;
        }

        public int Id { get; private set; }
        public decimal Balance { get; private set; }

        public ImmutableAccount WithCredit(decimal amount)
        {
            return new ImmutableAccount(Id, Balance + amount);
        }

        public ImmutableAccount WithDebit(decimal amount)
        {
            return WithCredit(-amount);
        }

    }
}