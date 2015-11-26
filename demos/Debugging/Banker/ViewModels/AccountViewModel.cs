using JulMar.Windows.Mvvm;

namespace WpfBankerSimulation.ViewModels
{
    /// <summary>
    /// ViewModel (data bindable entity) for the account
    /// </summary>
    public partial class AccountViewModel : SimpleViewModel
    {
        /// <summary>
        /// Underlying account
        /// </summary>
        private readonly Models.Account _account;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="account"></param>
        public AccountViewModel(Models.Account account)
        {
            _account = account;
        }

        /// <summary>
        /// Id of the account
        /// </summary>
        public int Id
        {
            get { return _account.Id; }
        }

        /// <summary>
        /// Balance
        /// </summary>
        public int Balance
        {
            get
            {
                return _account.Balance;
            }
            set
            {
                if (_account.Balance != value)
                {
                    _account.Balance = value;
                    OnPropertyChanged("Balance");
                }
            }
        }

        public void TransferTo(AccountViewModel destination, int amount)
        {
            TransferAmountBetweenAccounts(this, destination, amount);
        }
    }
}
