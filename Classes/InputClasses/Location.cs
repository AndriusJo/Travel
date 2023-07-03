using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Classes.InputModels
{
    public class Location
    {
        public string? Name { get; }
        public Coordinates? Coordinates { get; }

        public Location(string? name, Coordinates? coordinates)
        {
            Name = name;
            Coordinates = coordinates;
        }
    }
}
