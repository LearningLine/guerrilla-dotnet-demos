using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sampling
{
    public class TemperatureSensor
    {
        double currentTemp;
        Random rnd = new Random();

        public TemperatureSensor(double startingTemp)
        {
            this.currentTemp = startingTemp;
        }

        public void Cool()
        {
            currentTemp -= 1.5;
        }

        public IEnumerable<double> GetTemperatures()
        {
            while (true)
            {
                int direction = rnd.Next(100) > 51 ? -1 : 1;

                double delta = rnd.NextDouble() / 50000.0 * direction;

                currentTemp += delta;

                yield return currentTemp;
            }
        }
    }
}
