using System;
using System.Collections.Generic;
using System.Text;

namespace InterTransfer
{
    public class TransferTestDefinition
    {

        private int numberOfCells;

        public int NumberOfCells
        {
            get { return numberOfCells; }
            set { numberOfCells = value; }
        }

        private int numberOfTransfers;

        public int NumberOfTransfers
        {
            get { return numberOfTransfers; }
            set { numberOfTransfers = value; }
        }

        private int numberOfWorkers;

        public int NumberofWorkers
        {
            get { return numberOfWorkers; }
            set { numberOfWorkers = value; }
        }


    }
}
