using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Classes.InputModels;
using static Travel.Classes.IntersectionEnum;
using Travel.Functions;

namespace Travel.Functions
{
    public static class LocationCheck
    {
        /// <summary>
        /// Check if a given location is inside of a given region
        /// </summary>
        /// <param name="region"> a specific region </param>
        /// <param name="location"> a sepecific location </param>
        /// <returns> true if a location is inside of a region, false otherwise</returns>
        public static bool isInRegion(Region region, Location location)
        {
            if (location.Coordinates == null || region.Polygons == null)
            {
                throw new ArgumentNullException("Region or Location has no points");
            }

            if (location.Coordinates.Latitude == null || location.Coordinates.Longitude == null)
            {
                throw new ArgumentNullException("One or more Location coordinates is empty");
            }
            var y = location.Coordinates.Longitude;
            var x = location.Coordinates.Latitude;

            var count = 0;
            for (int i = 0; i < region.Polygons.Count(); i++)
            {

                var polygon = region.Polygons[i];

                if (polygon.Coordinates == null || polygon.Coordinates.Count() < 3)
                {
                    throw new Exception("Polygon does not have enough points");
                }

                for (int j = 0; j < polygon.Coordinates.Count() - 1; j++)
                {
                    if (polygon.Coordinates[j].Latitude == null || polygon.Coordinates[j].Longitude == null || polygon.Coordinates[j+1].Latitude == null || polygon.Coordinates[j+1].Longitude == null)
                    {
                        throw new ArgumentNullException("One or more coordinates is empty");
                    }
                    var xp1 = polygon.Coordinates[j].Latitude;
                    var yp1 = polygon.Coordinates[j].Longitude;
                    var xp2 = polygon.Coordinates[j + 1].Latitude;
                    var yp2 = polygon.Coordinates[j + 1].Longitude;
                    intersectType inter = RayCast.rayCast(x, y, xp1, yp1, xp2, yp2);

                    if (inter == intersectType.full)
                    {
                        count++;
                    }

                    //If a location is on the edge of a region, I count it as inside the region
                    //(Can be easily changed)
                    if (inter == intersectType.edge)
                    {
                        return true;
                    }
                }
            }
            return count % 2 == 1;
        }
    }
}
