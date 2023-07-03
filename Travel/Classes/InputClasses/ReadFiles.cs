using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Classes.InputModels;

namespace Travel.Classes.InputClasses
{
     internal class ReadFiles
    {

        internal static void Read(string[] args, ref List<Location> locations, ref List<Region> regions)
        {
            string _filePath = Environment.CurrentDirectory;

            //Check if the input files exist
            if (!File.Exists($"{_filePath}\\input\\{args[0]}") || !File.Exists($"{_filePath}\\input\\{args[1]}"))
            {
                throw new FileNotFoundException("One or more files not found!");
            }

            if (new FileInfo($"{_filePath}\\input\\{args[0]}").Length == 0 || new FileInfo($"{_filePath}\\input\\{args[1]}").Length == 0)
            {
                throw new FileNotFoundException("One or more files are empty!");
            }

            //Read location and region files
            string json = File.ReadAllText($"{_filePath}\\input\\{args[0]}");
            string json1 = File.ReadAllText($"{_filePath}\\input\\{args[1]}");

            JArray ljson = JArray.Parse(json);
            JArray rjson = JArray.Parse(json1);

            foreach (var location in ljson)
            {
                locations.Add(new Location(
                    (string)location["name"]!,
                    new Coordinates(
                        (double)location["coordinates"]![0]!,
                        (double)location["coordinates"]![1]!)
                    ));
            }

            regions = new List<Region>();
            foreach (var region in rjson)
            {
                var poly = new List<Polygon>();
                foreach (var polygon in region["coordinates"]!)
                {
                    var cord = new List<Coordinates>();
                    foreach (var coordinate in polygon)
                    {
                        cord.Add(new Coordinates(
                        (double)coordinate[0]!,
                        (double)coordinate[1]!));
                    }
                    poly.Add(new Polygon(cord));
                }
                regions.Add(new Region(
                    (string)region["name"]!,
                    poly
                    ));
            }
        }
    }
}
