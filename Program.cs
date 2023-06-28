// See https://aka.ms/new-console-template for more information

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

        public ArrayList? Locations { get; set; }

        public bool ShouldSerializeCoordinates()
        {
            return false;
        }
    }

    public static class JsonFileUtils
    {
        private static readonly JsonSerializerSettings _options
            = new() { NullValueHandling = NullValueHandling.Ignore };

        public static void PrettyWrite(object obj, string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, _options);
            File.WriteAllText(fileName, jsonString);
        }
    }

    class Program
    { 
        static void Main(string[] args)
        {

            string _filePath = Environment.CurrentDirectory;
            Console.WriteLine(_filePath);

            if (!File.Exists($"{_filePath}\\input\\{args[0]}") || !File.Exists($"{_filePath}\\input\\{args[1]}"))
            {
                Console.WriteLine("FILES DO NOT EXIST");
                return;
            }

            string json = File.ReadAllText($"{_filePath}\\input\\{args[0]}");
            string json1 = File.ReadAllText($"{_filePath}\\input\\{args[1]}");

            var data = JsonConvert.DeserializeObject<List<Location>>(json);
            var data1 = JsonConvert.DeserializeObject<List<Region>>(json1);

            if(data is null || data1 is null)
            {
                return;
            }

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

            string output = JsonConvert.SerializeObject(data1);
            Console.WriteLine(output);

            string name = "output";

            if (args.Length > 2)
            {
                name = Environment.CurrentDirectory +"\\output\\"+ args[2];
            }

            JsonFileUtils.PrettyWrite(data1, name);
        }

        static bool isInside(Region region,Location location)
        {
            int count = 0;

            if (location.Coordinates == null || region.Coordinates == null)
            {
                return false;
            }

            double x = location.Coordinates[0];
            double y = location.Coordinates[1];

            for (int i = 0; i < region.Coordinates.Count(); i++)
            {
                var polygon = region.Coordinates[i];
                for(int j = 0; j < polygon.Count()-1; j++)
                {
                    double xp1 = Convert.ToDouble(polygon[j].GetValue(0));
                    double yp1 = Convert.ToDouble(polygon[j].GetValue(1));
                    double xp2 = Convert.ToDouble(polygon[j+1].GetValue(0));
                    double yp2 = Convert.ToDouble(polygon[j+1].GetValue(1));
                    bool inter = rayCast(x, y, xp1, yp1, xp2, yp2);

                    if (inter)
                    {
                        count++;
                    }
                }
            }
            return count % 2 == 1;
        }

        static bool rayCast(double lx, double ly, double ex1, double ey1, double ex2, double ey2)
        {
            if ((ly <= ey1) != (ly <= ey2))
            {
                double x0 = ex1 + (ly - ey1) / (ey2 - ey1) * (ex2 - ex1);
                if (lx <= x0)
                {
                    return true;
                }
            }

            return false;
        }
    } 
}