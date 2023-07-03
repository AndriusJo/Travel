# Installation

Firstly - Download the zip file and unzip it at your chosen directory.
In the Travel folder and in the "Publish" folder (which is inside) there are folders: "input" and "output". You can replace or add extra files to the "input" folder, by default there are two files that you can use.  
In the files I have provided, I expanded the 1st region, to include the second region (overlap). I also added locations 7 and 8, which are on the edges of the regions 2 and 1 respectively.  
# Running the program

At this point there are two options:

Option 1 (Using the .exe file, which was generated for windows x86, so may not work on other machines):  
1. Open the terminal.
2. Change the directory to the "Published" folder (use the cd command).
3. Write the command **".\Travel nameoflocationfile nameofregionfile nameofoutputfile"** (The output file name is optional). e.g. **".\Travel locations.json regions.json matches.json"**.
4. Check the output in the "output" folder.

Option 2:  
1. Install .NET Core on your machine (**If you already have it, skip this step**), instructions how to do it: https://learn.microsoft.com/en-us/dotnet/core/install/ .
2. Open the terminal.
3. Change directory to the main directory of this file (where the .sln file is) (use the cd command).
4. Write the command **"dotnet run nameoflocationfile nameofregionfile nameofoutputfile"** (The output file name is optional). e.g. **"dotnet run locations.json regions.json matches.json"**.
5. Check the output in the "output" folder.
