using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Classes
{
    public class Coordinates
    {
        public double Latitude { get; }
        public double Longtitude { get; }

        public Coordinates(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }
    }
}
