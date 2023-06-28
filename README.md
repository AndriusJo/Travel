# Installation

1. Download the zip file and unzip it at your chosen directory.
In the main directory and in the "Publish" folder there are folders: "input" and "output". You can replace or add extra files to the "input" folder, by default there are two files that you can use.

# Running the program

At this point there are two options:
Option 1 (if you have a windows x86 system):
1. Open the terminal.
2. Change the directory to the "Published" folder (use the cd command).
3. Write the command ** .\Travel nameoflocationfile nameofregionfile nameofoutputfile ** (The output file name is optional). e.g. ** .\Travel locations.json regions.json matches.json **.
4. Check the output in the "output" folder.

Option 2:
1. Install .NET Core on your machine (**If you already have it, skip this step**), instructions how to do it: https://learn.microsoft.com/en-us/dotnet/core/install/ .
2. Open the terminal.
3. Change directory to the main directory of this file (where the .sln file is) (use the cd command).
4. Write the command **dotnet run nameoflocationfile nameofregionfile nameofoutputfile** (The output file name is optional). e.g. ** dotnet run locations.json regions.json matches **.
5. Check the output in the "output" folder.
