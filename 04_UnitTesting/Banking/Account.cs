using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banking
{
    public class Account
    {
        public Account(decimal initialBalance)
        {
            if (initialBalance < 0m)
                throw new ArgumentException("Cannot be negative", "initialBalance");

            this.Balance = initialBalance;
        }

        public decimal Balance { get; private set; }

        public void Credit(decimal amount)
        {
            Balance += amount;
        }
    }
}
