using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Classes.InputModels
{
    public class Region
    {
        public string? Name { get; }
        public List<Polygon>? Polygons { get; }
        public Region(string? name, List<Polygon>? polygons)
        {
            Name = name;
            Polygons = polygons;
        }
    }
}
