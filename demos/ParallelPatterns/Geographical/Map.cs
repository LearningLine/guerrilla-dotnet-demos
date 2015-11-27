using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Geographical
{
    public class MapLocation
    {
        public MapLocation(string name , double longitude , double latitude)
        {
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
        }
        public string Name { get; private set; }

        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public override string ToString()
        {
            return String.Format("{0} {1},{2}", Name, Latitude , Longitude);
        }

    }

    public class Map
    {

        private class MapLocationComparitor : IEqualityComparer<MapLocation>
        {
            public bool Equals(MapLocation x, MapLocation y)
            {
                return x.Name == y.Name;
            }

            public int GetHashCode(MapLocation obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        private Dictionary<string, MapLocation> locations = new Dictionary<string, MapLocation>();

        public Map(string mapFile)
        {
            LoadMap(mapFile);
        }

        private void LoadMap(string mapFile)
        {
            MapLocationComparitor loctionEquality = new MapLocationComparitor();

            using (StreamReader reader = new StreamReader(mapFile))
            {
                locations = (from mapFileEntry in reader.GetLines().Skip(1)
                             let columns = mapFileEntry.Split(',')
                             select new MapLocation(columns[1].Substring(1,columns[1].Length-2), double.Parse(columns[5]), double.Parse(columns[4])))
                            .Distinct(loctionEquality)
                            .ToDictionary( l => l.Name );



            }
        }

        public double DistanceBetween(string source, string dest)
        {
            MapLocation src = locations[source];
            MapLocation end = locations[dest];

            return Distance(src, end);
        }

        public IEnumerable<MapLocation> Locations
        {
            get { return locations.Values.OrderBy(l => l.Name); }
        }



        // 1 Degree latitude is appx. 69.1 miles
        private const double LATITUDE_TO_MILES = 69.1;
        // 1 Degree logitude is appx. 53 miles
        private const double LONGITUDE_TO_MILES = 53;

        public static double Distance(
            MapLocation start,
            MapLocation end)
        {
            // Calculate distance of pointToConsider from center point
            // using trig.
            // SQR( x^2 + y^2 )

            double distance = Math.Sqrt(
                Math.Pow(LONGITUDE_TO_MILES * (start.Longitude - end.Longitude), 2) +
                Math.Pow(LATITUDE_TO_MILES * (start.Latitude - end.Latitude), 2));

            return distance;
        }
    }
}
