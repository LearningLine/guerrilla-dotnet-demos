using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace Geographical
{
    public class TripSerialiser
    {
        private Map map;

        public TripSerialiser(Map map)
        {
            this.map = map;
        }


        public Trip Load(string filename)
        {
            XElement tripDoc = XElement.Load(filename);

            Dictionary<string, MapLocation> locations = map.Locations.ToDictionary(l => l.Name);

            MapLocation start = locations[ (string) tripDoc.Element("Start").Element("Name")];
            MapLocation end = locations[(string)tripDoc.Element("End").Element("Name")];
            
            MapLocation[] wayPoints = (from waypoint in tripDoc.Element("WayPoints").Elements("WayPoint")
                                      select locations[(string)waypoint.Element("Name")]).ToArray();

            return new Trip( start , end , wayPoints);
        }

       


        public void Save(Trip trip , string filename)
        {
            XElement tripDoc = new XElement(
                "Trip",
                    new XElement("Start",
                        new XElement("Name", trip.Start.Name),
                        new XElement("Longitude", trip.Start.Longitude),
                        new XElement("Latitude", trip.Start.Latitude)),
                   new XElement("WayPoints",
                    from waypoint in trip.WayPoints
                    select new XElement("WayPoint",
                        new XElement("Name", waypoint.Name),
                        new XElement("Longitude", waypoint.Longitude),
                        new XElement("Latitude", waypoint.Latitude))),
                    new XElement("End",
                        new XElement("Name", trip.End.Name),
                        new XElement("Longitude", trip.End.Longitude),
                        new XElement("Latitude", trip.End.Latitude))
                        );

            tripDoc.Save(filename);
        }

    }
}
