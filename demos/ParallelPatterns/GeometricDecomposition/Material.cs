using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeometricDecomposition
{
    public class Material
    {
        private double[] cells;

        public Material(int width)
        {
            cells = new double[width+1];
        }

        public Material(int width, double initialTemperature) : this(width)
        {
            for (int nCell = 0; nCell < width; nCell++)
            {
                cells[nCell] = initialTemperature;
            }
        }

        public int Width { get { return cells.Length-1; } }

        public double this[int index]
        {
            get { return cells[index]; }
            set { cells[index] = value; }
        }


    }
}
