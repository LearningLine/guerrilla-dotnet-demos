using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deadlock
{
    public partial class Form1 : Form
    {
        readonly List<Account> _accounts = new List<Account>();
        readonly List<Task> _tasks = new List<Task>();

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 50; i++)
                _accounts.Add(new Account {Id = i, Balance = 1000 });
        }

        void TransferProc()
        {
            Random rng = new Random();
            for (; ; )
            {
                int a = rng.Next(_accounts.Count);
                int b = rng.Next(_accounts.Count);
                _accounts[a].TransferTo(_accounts[b], 10);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _tasks.Add(Task.Factory.StartNew(TransferProc, TaskCreationOptions.LongRunning));
            label1.Text = string.Format("{0} tasks running", _tasks.Count );
        }
    }
}
