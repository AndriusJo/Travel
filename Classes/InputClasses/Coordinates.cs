using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Classes.InputModels
{
    public class Coordinates
    {
        public double? Longitude { get; }
        public double? Latitude { get; }

        public Coordinates(double? longitude, double? latitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
