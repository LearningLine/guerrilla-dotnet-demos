using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace InterTransfer
{
    public partial class TransferForm : Form
    {

        private long[] cells = new long[1];
        private int numberOfTransfersRemaining;
        private TransferTestDefinition testDefinition = new TransferTestDefinition();
        private long expectedTotal = 0;
        private object[] smallAsslocks;

        public TransferForm()
        {
            InitializeComponent();


            testDefinition.NumberOfCells = 1000000;
            testDefinition.NumberOfTransfers = 10000000;
            testDefinition.NumberofWorkers = 1;


            transferTestDefinitionBindingSource.DataSource = testDefinition;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            manualAuditButton.Enabled = false;


            cells = new long[testDefinition.NumberOfCells];
            smallAsslocks = new object[testDefinition.NumberOfCells];

            expectedTotal=0;

            for (int nCell = 0; nCell < cells.Length; nCell++)
            {
               expectedTotal +=  cells[nCell] = 1000;
                smallAsslocks[nCell] = new object();
            }

            bankTotal.Text = expectedTotal.ToString();

            numberOfTransfersRemaining = testDefinition.NumberOfTransfers;

            for (int nThread = 0; nThread < testDefinition.NumberofWorkers; nThread++)
            {
                Task worker = Task.Run(() => DoTransfer());
            }

            auditTimer.Start();
        }


        private void DoTransfer()
        {
            Random rnd = new Random();

            while (numberOfTransfersRemaining > 0)
            {
                int src = rnd.Next(cells.Length);
                int dest = rnd.Next(cells.Length);

                int amount = rnd.Next(1000);

                MoveValue(src, dest, amount);
            
                numberOfTransfersRemaining--;    
            }

        }

        

        private void DoAudit(object sender, EventArgs e)
        {
            PerformAudit();


            if (numberOfTransfersRemaining <= 0)
            {
                numberOfTransfersRemaining = 0;
                manualAuditButton.Enabled = true;
                button1.Enabled = true;
                auditTimer.Stop();

                PerformAudit();
            }

            noTransfersLabel.Text = numberOfTransfersRemaining.ToString();
        }

        private TimeoutLock auditLock = new TimeoutLock();
        private void PerformAudit()
        {
            long total = 0;

            using (auditLock.Lock(TimeSpan.FromSeconds(2)))
            {
                for (int nCell = 0; nCell < cells.Length; nCell++)
                {
                    total += cells[nCell];
                }
            }

            //if(!Monitor.TryEnter(bigAssLock, TimeSpan.FromSeconds(2)))
            //    throw new TimeoutException();

            //try
            //{
            //    for (int nCell = 0; nCell < cells.Length; nCell++)
            //    {
            //        total += cells[nCell];
            //    }

            //}
            //finally
            //{
            //    Monitor.Exit(bigAssLock);
            //}


            auditTotal.Text = total.ToString();

            if (total == expectedTotal)
            {
                auditTotal.ForeColor = Color.Black;
            }
            else
            {
                auditTotal.ForeColor = Color.Red;
            }
        }

        private object bigAssLock = new object();
        private void MoveValue(int src, int dest, int amount)
        {
            if (src == dest)
            {
                return;
            }

            //lock (bigAssLock)

            object firstLock = smallAsslocks[Math.Min(src,dest)];
            object secondLock = smallAsslocks[Math.Max(src,dest)];

            lock(firstLock)
            {
                lock (secondLock)
                {
                    cells[dest] += amount;
                    cells[src] -= amount;
                }
            }
        }

        
    }

    public class TimeoutLock
    {
        readonly object toLock = new object();
        public LockReleaser Lock(TimeSpan timeout)
        {
            if(!Monitor.TryEnter(toLock, timeout))
                throw new TimeoutException("Arrggh");

            return new LockReleaser(toLock);
        }

        public struct LockReleaser : IDisposable
        {
            private readonly object toRelease;

            public LockReleaser(object toRelease)
            {
                this.toRelease = toRelease;
            }

            public void Dispose()
            {
                Monitor.Exit(toRelease);
            }
        }
    }




}