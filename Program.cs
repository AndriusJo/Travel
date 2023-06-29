using Newtonsoft.Json;
using System.Collections;


namespace Travel
{
    public class Location
    {
        public string? Name { get; set; }
        public double[]? Coordinates { get; set; }
    }

    public class Region
    {
        public string? Name { get; set; }

        public object[][][]? Coordinates { get; set; }

        //List of locations that are inside the region
        public ArrayList? Locations { get; set; }

        //Does not serialize coordinates when writing to file
        public bool ShouldSerializeCoordinates()
        {
            return false;
        }
    }

    public static class JsonFileUtils
    {
        /// <summary>
        /// Writes an indented json file
        /// </summary>
        /// <param name="obj"> String that will be converted to json</param>
        /// <param name="fileName"> Name or full path of the output file</param>
        public static void PrettyWrite(object obj, string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
        }
    }

    class Program
    { 
        static void Main(string[] args)
        {

            string _filePath = Environment.CurrentDirectory;

            //Check if the input files exist
            if (!File.Exists($"{_filePath}\\input\\{args[0]}") || !File.Exists($"{_filePath}\\input\\{args[1]}"))
            {
                Console.WriteLine("FILES DO NOT EXIST");
                return;
            }

            //Read location and region files
            string json = File.ReadAllText($"{_filePath}\\input\\{args[0]}");
            string json1 = File.ReadAllText($"{_filePath}\\input\\{args[1]}");

            //Deserialize location and region files into lists
            var data = JsonConvert.DeserializeObject<List<Location>>(json);
            var data1 = JsonConvert.DeserializeObject<List<Region>>(json1);

            //Check if lists are not empty
            if(data is null || data1 is null)
            {
                Console.WriteLine("One of the files is empty");
                return;
            }

            //Match the locations to each region
            for (int i = 0; i < data1.Count; i++)
            {
                var matches = new ArrayList();

                for (int j = 0; j < data.Count; j++)
                {
                    if (isInside(data1[i], data[j]))
                    {
                        matches.Add(data[j].Name);
                    }
                }
                data1[i].Locations = matches;
            }

            //Set the name of the outupt file if it is provided
            string name;
            if (args.Length > 2)
            {
                name = Environment.CurrentDirectory +"\\output\\"+ args[2];
            }
            else
            {
                name = Environment.CurrentDirectory + "\\output\\" + "output.json";
            }

            //Write the output file
            JsonFileUtils.PrettyWrite(data1, name);

            Console.WriteLine("FINISHED!");
        }

        /// <summary>
        /// Check if a given location is inside of a given region
        /// </summary>
        /// <param name="region"> a specific region </param>
        /// <param name="location"> a sepecific location </param>
        /// <returns> true if a location is inside of a region, false otherwise</returns>
        static bool isInside(Region region,Location location)
        {
            int count = 0;
            //Check if coordinates are not empty
            if (location.Coordinates == null || region.Coordinates == null)
            {
                return false;
            }

            double x = location.Coordinates[0];
            double y = location.Coordinates[1];

            //Itterate over all polygons inside a region
            //region.Coordinates - contains all the polygons
            //polygon - coordinates of a single polygon in a region
            for (int i = 0; i < region.Coordinates.Count(); i++)
            {
                var polygon = region.Coordinates[i];
                for(int j = 0; j < polygon.Count()-1; j++)
                {
                    double xp1 = Convert.ToDouble(polygon[j].GetValue(0));
                    double yp1 = Convert.ToDouble(polygon[j].GetValue(1));
                    double xp2 = Convert.ToDouble(polygon[j+1].GetValue(0));
                    double yp2 = Convert.ToDouble(polygon[j+1].GetValue(1));
                    string inter = rayCast(x, y, xp1, yp1, xp2, yp2);

                    if (inter == "intersect")
                    {
                        count++;
                    }

                    //If a location is on the edge of a region, I count it as inside the region
                    //(Can be easily changed)
                    if (inter == "edge")
                    {
                        return true;
                    }
                }
            }

            //If the ammount of interceptions of the ray (which is cast from a location) and the
            //polygon sides is uneven, then the location is inside the polygon, otherwise
            //it is outside
            return count % 2 == 1;
        }

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
        /// <returns> "intersect" if a ray intersects the edge
        ///           "edge" if a location is on the edge
        ///           "none" if the ray does not intersect the edge </returns>
        static string rayCast(double lx, double ly, double ex1, double ey1, double ex2, double ey2)
        {
            double y0 = ey1 + (lx - ex1) / (ex2 - ex1) * (ex2 - ex1);
            double x0 = ex1 + (ly - ey1) / (ey2 - ey1) * (ex2 - ex1);

            //Check if the ray intersects the edge
            if ((ly < ey1) != (ly < ey2))
            {
                if (lx < x0)
                {
                    return "intersect";
                }
            }

            //Check if the location is on the given edge
            if ((lx == x0) && (ly == y0))
            {
                return "edge";
            }

            return "none";
        }
    } 
}