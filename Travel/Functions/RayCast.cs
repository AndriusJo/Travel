using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Travel.Classes.IntersectionEnum;

namespace Travel.Functions
{
    internal static class RayCast
    {

        /// <summary>
        /// Check if a ray, that is cast (to the right) from the point of a location, intersects with a specific
        /// edge of a polygon (region).
        /// </summary>
        /// <param name="lx"> x coordinate of a location (latitude) </param>
        /// <param name="ly"> y coordinate of a location (longtitude) </param>
        /// <param name="ex1"> x coordinate of a polygon's vertice </param>
        /// <param name="ey1"> y coordinate of a polygon's vertice </param>
        /// <param name="ex2"> x coordinate of the next vertice of a polygon </param>
        /// <param name="ey2"> y coordinate of the next vertice of a polygon  </param>
        /// <returns> "full" if a ray intersects the edge
        ///           "edge" if a location is on the edge
        ///           "none" if the ray does not intersect the edge </returns>
        /// Source https://en.wikipedia.org/wiki/Point_in_polygon 
        internal static intersectType rayCast(double? lx, double? ly, double? ex1, double? ey1, double? ex2, double? ey2)
        {
            var y0 = ey1 + (lx - ex1) / (ex2 - ex1) * (ex2 - ex1);
            var x0 = ex1 + (ly - ey1) / (ey2 - ey1) * (ex2 - ex1);

            //Check if the ray intersects the edge
            if ((ly < ey1) != (ly < ey2))
            {
                if (lx < x0)
                {
                    return intersectType.full;
                }
            }

            //Check if the location is on the given edge
            if ((lx == x0) && (ly == y0))
            {
                return intersectType.edge;
            }

            return intersectType.none;
        }
    }
}
