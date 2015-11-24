using System;

namespace Banking
{
    public class Account
    {
        public Account(decimal initialBalance)
        {
            if ( initialBalance < 0 )
                throw new ArgumentOutOfRangeException("initialBalance","Balance must be > 0");
            this.Balance = initialBalance;
        }

        public decimal Balance { get; private set; }
    }
}