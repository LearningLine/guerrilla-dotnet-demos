using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Geographical
{
    public class Trip
    {
        private MapLocation[] wayPoints;

        private double totalDistance;

        public MapLocation Start { get; private set; }
        public MapLocation End { get; private set; }

        public IEnumerable<MapLocation> WayPoints
        {
            get { return wayPoints; }
        }

        public Trip(MapLocation start , MapLocation end , MapLocation[] wayPoints)
        {
            this.wayPoints = wayPoints;
            Start = start;
            End = end;

            totalDistance = InternalTotalDistance();
        }

        public override string ToString()
        {
            return String.Format(" Starting : {0} via {1} Ending : {2}",
                Start.Name,
                string.Join(",", wayPoints.Select(w => w.Name).ToArray()),
                End.Name);
        }
        public double TotalDistance
        {
            get { return totalDistance; }
        }

        private double InternalTotalDistance()
        {
            MapLocation from = Start;
            double tripTotal = 0.0;

            foreach (var wayPoint in wayPoints)
            {
                double distance = Map.Distance(from, wayPoint);
                tripTotal += distance;

                from = wayPoint;
            }

            tripTotal += Map.Distance(from, End);

            return tripTotal;
        }
    }
}
