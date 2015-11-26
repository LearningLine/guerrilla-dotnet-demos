using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Immutable
{
    public interface IBank
    {
        void AddAccount(decimal initialBalance);

        IEnumerable<IAccount> Accounts { get; }
        void TransferFunds(int sourceAccountId, int destAccountId,decimal amount);
    }

    public class Bank : IBank
    {
        private List<Account> accounts = new List<Account>();
       
        public IEnumerable<IAccount> Accounts
        {
            get
            {
               return accounts;
                    //.Select(a => new Account(a.Id) {Balance = a.Balance})
                    //.ToList();
            }
        }

        public void TransferFunds(int sourceAccountId, int destAccountId, decimal amount)
        {
            List<Account> copy = new List<Account>(accounts);

            copy[sourceAccountId] = new Account(sourceAccountId) {Balance = accounts[sourceAccountId].Balance};
            copy[destAccountId] = new Account(destAccountId) {Balance = accounts[destAccountId].Balance};

            copy[sourceAccountId].Balance -= amount;
            copy[destAccountId].Balance += amount;

            accounts = copy;
        }

        public void AddAccount(decimal initialBalance)
        {
         accounts.Add( new Account(accounts.Count));
        }
    }
}