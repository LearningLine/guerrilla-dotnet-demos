using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JulMar.Windows.Mvvm;
using System.Collections.ObjectModel;
using System.Threading;

namespace WpfBankerSimulation.ViewModels
{
    public class SimulationViewModel : SimpleViewModel
    {
        #region Private Data
        private int _tellerCount;
        private int _numberOfAccounts;
        private volatile bool _isBankOpen;
        private TimeSpan _bankClosesInSeconds;
        private DateTimeOffset _bankCloseTimer;
        private Timer _closeTimer;
        private long _startingBalance, _currentBalance;
        private long _completedTransfers;
        private bool _isBalanceCorrect;
        #endregion

        /// <summary>
        /// List of accounts
        /// </summary>
        public IList<AccountViewModel> Accounts { get; private set; }

        /// <summary>
        /// Number of tellers working today
        /// </summary>
        public int TellerCount
        {
            get { return _tellerCount; }
            set
            {
                if (_tellerCount != value && value > 0)
                {
                    _tellerCount = Math.Min(value,100);
                    OnPropertyChanged("TellerCount", "CanOpenBank");
                }
            }
        }

        /// <summary>
        /// Number of accounts
        /// </summary>
        public int NumberOfAccounts
        {
            get { return _numberOfAccounts; }
            set
            {
                if (_numberOfAccounts != value && value > 0)
                {
                    _numberOfAccounts = value;
                    CreateAccounts();
                    OnPropertyChanged("NumberOfAccounts", "CanOpenBank");
                }
            }
        }

        /// <summary>
        /// True if bank is open (running)
        /// </summary>
        public bool IsBankOpen
        {
            get { return _isBankOpen; }
            private set
            {
                _isBankOpen = value;
                OnPropertyChanged("IsBankOpen", "CanOpenBank");
            }
        }

        /// <summary>
        /// True if we can open the bank.  False otherwise.
        /// </summary>
        public bool CanOpenBank
        {
            get { return !IsBankOpen && NumberOfAccounts > 0 && TellerCount > 0; }
        }

        /// <summary>
        /// Number of completed transfers
        /// </summary>
        public long CompletedTransfers
        {
            get { return _completedTransfers; }
            private set
            {
                if (_completedTransfers != value)
                {
                    _completedTransfers = value;
                    OnPropertyChanged("CompletedTransfers");
                }
            }
        }

        /// <summary>
        /// Starting balance
        /// </summary>
        public long StartingBalance
        {
            get { return _startingBalance; }
            private set
            {
                if (_startingBalance != value)
                {
                    _startingBalance = value;
                    OnPropertyChanged("StartingBalance");
                    IsBalanceCorrect = (value == _currentBalance);
                }
            }
        }

        /// <summary>
        /// Current balance
        /// </summary>
        public long CurrentBalance
        {
            get { return _currentBalance; }
            private set
            {
                if (_currentBalance != value)
                {
                    _currentBalance = value;
                    OnPropertyChanged("CurrentBalance");
                    IsBalanceCorrect = (value == _startingBalance);
                }
            }
        }

        /// <summary>
        /// Returns whether the balance is correct
        /// </summary>
        public bool IsBalanceCorrect
        {
            get { return _isBalanceCorrect; }
            private set
            {
                if (_isBalanceCorrect != value)
                {
                    _isBalanceCorrect = value;
                    OnPropertyChanged("IsBalanceCorrect");
                }
            }
        }

        /// <summary>
        /// Time until the bank is closing
        /// </summary>
        public TimeSpan BankClosesIn
        {
            get { return _bankClosesInSeconds; }
            private set
            {
                if (_bankClosesInSeconds != value)
                {
                    _bankClosesInSeconds = value;
                    OnPropertyChanged("BankClosesIn");
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SimulationViewModel()
        {
            Accounts = new ObservableCollection<AccountViewModel>();
        }

        /// <summary>
        /// Method that is called to open the bank (start running).
        /// Should be called to begin the simulation
        /// </summary>
        public void OpenBank()
        {
            if (IsBankOpen)
                return;

            CompletedTransfers = 0;

            // Mark the bank as open
            IsBankOpen = true;
            BankClosesIn = TimeSpan.FromSeconds(10);
            _bankCloseTimer = DateTime.Now.AddSeconds(10);
            _closeTimer = new Timer(OnTimer, null, 200, 200);

            // Create all the tasks - each runs on a separate thread
            for (int i = 0; i < TellerCount; i++)
                Task.Factory.StartNew(RunDailyWork, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Recalculates the current balance
        /// </summary>
        private void RecalculateCurrentBalance()
        {
            CurrentBalance = Accounts.Sum(account => (long) account.Balance);
        }

        /// <summary>
        /// Adjusts the countdown timer until the bank closes.
        /// </summary>
        private void OnTimer(object unused)
        {
            // Recalculate the current balance
            RecalculateCurrentBalance();

            var bci = (_bankCloseTimer - DateTime.Now);
            if (bci.TotalSeconds <= 0)
            {
                IsBankOpen = false;
                BankClosesIn = TimeSpan.FromSeconds(0);
                _closeTimer.Dispose();
            }
            else
            {
                BankClosesIn = TimeSpan.FromSeconds(bci.Seconds);
            }
        }

        /// <summary>
        /// This routine is run by a single teller (thread)
        /// </summary>
        private void RunDailyWork()
        {
            Random rng = new Random();

            while (_isBankOpen)
            {
                int dest;
                int source = rng.Next(Accounts.Count);
                do
                {
                    dest = rng.Next(Accounts.Count);
                } while (dest == source);

                var sourceAccount = Accounts[source];
                if (sourceAccount.Balance > 1)
                    Transfer(sourceAccount, Accounts[dest], rng.Next(1, sourceAccount.Balance));

                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Perform a balance transfer between two accounts.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="value"></param>
        private void Transfer(AccountViewModel source, AccountViewModel dest, int value)
        {
            // Perform the transfer
            source.TransferTo(dest, value);

            // Change the completed transfers underlying field and
            // raise property changed.
            Interlocked.Increment(ref _completedTransfers);
            OnPropertyChanged("CompletedTransfers");
        }

        /// <summary>
        /// Method to create the account list
        /// </summary>
        private void CreateAccounts()
        {
            Random RNG = new Random();

            // Create our accounts
            Accounts.Clear();
            for (int i = 0; i < NumberOfAccounts; i++)
            {
                Accounts.Add(new AccountViewModel(new Models.Account { Id = i + 1, Balance = RNG.Next(10000) }));
            }

            // CurrentBalance changed - we generated new accounts.
            RecalculateCurrentBalance();
            StartingBalance = CurrentBalance;
        }
    }
}
