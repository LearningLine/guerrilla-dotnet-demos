using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace InterTransfer
{
    public partial class TransferForm : Form
    {

      
        private int numberOfTransfersRemaining;
        private TransferTestDefinition testDefinition = new TransferTestDefinition();
        private long expectedTotal = 0;

        private long[] cells;
        private object[] cellLocks;

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
            cellLocks = new object[testDefinition.NumberOfCells];
            expectedTotal = 0;

            for (int nCell = 0; nCell < cells.Length; nCell++)
            {
                expectedTotal += cells[nCell] = 1000;
                cellLocks[nCell] = new object();
            }

            bankTotal.Text = expectedTotal.ToString();

            numberOfTransfersRemaining = testDefinition.NumberOfTransfers;

            for (int nThread = 0; nThread < testDefinition.NumberofWorkers; nThread++)
            {
                Thread worker = new Thread(DoTransfer);

                worker.Start();
            }

            auditTimer.Start();
        }

        private void DoTransfer()
        {
            Random rnd = new Random();

            while (Interlocked.Decrement(ref numberOfTransfersRemaining) >= 0)
            {
                int src = rnd.Next(cells.Length);
                int dest = rnd.Next(cells.Length);

                int amount = rnd.Next(1000);

                MoveValue(src, dest, amount);

                
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

        private void PerformAudit()
        {
            long total = 0;

            lock (guard)
            {
                for (int nCell = 0; nCell < cells.Length; nCell++)
                {
                    total += cells[nCell];
                }
            }

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

        private object guard = new object();

        private void MoveValue(int src, int dest, int amount)
        {
            if (src == dest)
            {
                return;
            }



            // Move from 3 to 8
            // Move from 8 to 3

            // Lock lowest account first not src
            // Lock highest account last not dest

            object firstLock = cellLocks[Math.Min(src,dest)];
            object secondLock = cellLocks[Math.Max(src,dest)];

            lock (firstLock)
            {
                lock (secondLock)
                {

                    cells[dest] += amount;
                    cells[src] -= amount;

                }
            }


            //using (guard.Lock(TimeSpan.FromSeconds(2)))
            //{
            //    cells[dest] += amount;
            //    cells[src] -= amount;
            //}

            // Monitor.Enter, waits for monitor
            //
            //lock (guard)
            //if (Monitor.TryEnter(guard, TimeSpan.FromSeconds(2)))
            //{

            //    try
            //    {
            //        cells[dest] += amount;
            //        cells[src] -= amount;
            //    }
            //    finally
            //    {
            //        Monitor.Exit(guard);
            //    }
            //}
            //else
            //{
            //    throw new TimeoutException("Failed to get monitor");
            //}

            //Monitor.Enter(guard);
            //try
            //{
            //    cells[dest] += amount;
            //    cells[src] -= amount;

            //}
            //finally
            //{

            //    Monitor.Exit(guard);
            //}
        }
    }
}