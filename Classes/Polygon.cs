using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Classes
{
    public class Polygon
    {
        public List<Coordinates>? Coordinates { get; }

        public Polygon(List<Coordinates>? coordinates)
        {
            Coordinates = coordinates;
        }
    }
}
