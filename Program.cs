using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using Travel.Classes.InputClasses;
using Travel.Classes.InputModels;
using Travel.Classes.OutputModels;
using static Travel.Classes.IntersectionEnum;
using Travel.Functions;

namespace Travel
{

    class Program
    {
        const string hello = "HI! Welcome to my program.\n";
        const string bad = "Oh no! There seems to be an issue: ";
        const string help =
            "Here is a rundown of how the program should be operated:\n" +
            "   1. Open the terminal on your computer\n" +
            "   2. Change the directory:\n" +
            "       Option 1 - To the main directory where the program is installed (eg. C:/../../Travel)\n" +
            "       Option 2 - To the Published folder in the main directory (eg. C:/../../Travel/Published)\n" +
            "   3. Write the request line:\n" +
            "       Option 1 - dotnet run name-of-location-file name-of-region-file name-of-output-file\n" +
            "       Option 2 - .\\Travel name-of-location-file name-of-region-file name-of-output-file\n" +
            "       (make sure the extensions of all the file names, are .json)\n" +
            "   The option you choose is the task you must complete at each step\n\n" +
            "   Make sure the files you ar using have the .json file extension, also check if the structure of your json\n" +
            "   files is correct (as shown in the example files in the input folder)\n\n" +
            "To find more information on how to run this program and which option would work best for you, make sure to visit github:\n" +
            "https://github.com/AndriusJo/Travel.git\n";

        static void Main(string[] args)
        {   
            var locations = new List<Location>();
            var regions = new List<Region>();

            //Read file and validate information
            if(args.Length < 2)
            {
                Console.WriteLine(hello + help);
                return;
            }

            try 
            {
                ReadFiles.Read(args, ref locations, ref regions);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(bad + ex.Message);
                Console.WriteLine("Do you need help? y/n");
                string? r = Console.ReadLine();
                if (r == "y")
                {
                    Console.WriteLine(help);
                    Console.WriteLine("Full exception output:\n" + ex);
                    return;
                }
                else
                {
                    return;
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine(bad + "Issue with deserializing your files");
                Console.WriteLine("Do you need help? y/n");
                string? r = Console.ReadLine();
                if (r == "y")
                {
                    Console.WriteLine(help);
                    Console.WriteLine("Full exception output:\n" + ex);
                    return;
                }
                else
                {
                    return;
                }
            }

            //Match locations to regions
            var matches = new List<Matches>();
            foreach (var region in regions)
            {
                var som = from loc in locations
                          where LocationCheck.isInRegion(region, loc) == true
                          select loc.Name;

                matches.Add(new Matches(region.Name, som.ToList()));
            }

            //Set the name of the outupt file if it is provided
            string name;
            if (args.Length > 2)
            {
                name = Environment.CurrentDirectory + "\\output\\" + args[2];
            }
            else
            {
                name = Environment.CurrentDirectory + "\\output\\" + "output.json";
            }

            //Write the output file
            JsonFileUtils.PrettyWrite(matches, name);

            Console.WriteLine("FINISHED!");
        }
    } 
}