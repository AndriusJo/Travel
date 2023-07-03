using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Classes.OutputModels
{
    public class Matches
    {
        public string? region { get; }
        public List<string> matched_locations { get; }

        public Matches(string? region, List<string> matched_locations)
        {
            this.region = region;
            this.matched_locations = matched_locations;
        }
    }
}
