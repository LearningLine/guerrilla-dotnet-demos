using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Linq;
using System.Media;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

// Create audit thread
//      Show it doesn't work
//          Add lock, doesn't work
//          Remove lock, Make copy on update, PIMPL Point to implementation
//              Copies whole list
//              Still busted...needs to do a deep copy
//          Move to immutable collections
//              Need to make the data immutable too, with pattern
//    
using Microsoft.Win32;

namespace Immutable
{
    internal class Program
    {
        static void OldMain(string[] args)
        {
            var items = ImmutableList<int>.Empty
                                          .Add(5)
                                          .Add(7)
                                          .Add(12)
                                          .Add(42)
                                          .Add(13)
                                          .Add(666)
                                          .Add(876)
                                          .Add(123);

            foreach (int item in items)
            {
                Console.WriteLine(item);
            }
        }
        private static void Main(string[] args)
        {
            const int numberOfAccounts = 10000;
            const decimal initialBalance = 100;

            IBank bank = new ImmutableBank();

            InitializeBank(bank,numberOfAccounts,initialBalance);

            decimal expectedTotalMoneyInBank =
                bank.Accounts.Sum(a => a.Balance);

            Task.Run(() =>
            {
                while (true)
                {
                    decimal totalMoneyInBank = bank.Accounts.Sum(a => a.Balance);
                    Console.Write(totalMoneyInBank == expectedTotalMoneyInBank? "." : "!");
                }
            });

            var rnd = new Random();
            while (true)
            {
                int source = rnd.Next(numberOfAccounts);
                int dest = rnd.Next(numberOfAccounts);

                if (source != dest)
                    bank.TransferFunds(source, dest, 10);
            }
        }

        private static void InitializeBank(IBank bank, int nAccounts, decimal initialBalance)
        {
            for (int nAccount = 0; nAccount < nAccounts; nAccount++)
            {
                bank.AddAccount(initialBalance);
            }
        }
    }
}