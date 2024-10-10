using System; // Provides fundamental classes and base classes for commonly-used value and reference data types
using System.Collections.Generic; // Provides classes for defining generic collections
using System.IO; // Provides functionality for working with files and data streams
using System.Linq; // Provides classes and interfaces that support queries using Language-Integrated Query (LINQ)

namespace Paint_Calculator
{
    class FileHandling
    {
        // Default color options with their prices
        private static Colour[] DefaultColours = {
            new Colour("Red", Convert.ToDecimal(1.0)),
            new Colour("Blue", Convert.ToDecimal(1.0)),
            new Colour("Green", Convert.ToDecimal(1.0))
        };

        // File location for storing color data
        private static string _FileLocation =
            System.Windows.Forms.Application.CommonAppDataPath + "/colours.txt";

        // Public property to access the file location
        public static string FileLocation
        {
            get
            {
                return _FileLocation;
            }
        }

        public static void LoadColours()
        {
            // Check if the colors file exists, if not create it and write default colors
            if (!File.Exists(FileLocation))
            {
                CleanFile(); // Ensure the file is clean
                WriteColours(DefaultColours); // Write default colors to the file
            }

            // Use a StreamReader to read colors from the file
            using (StreamReader sr = new StreamReader(FileLocation))
            {
                // Read lines until the end of the file
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine(); // Read a line from the file

                    // Split the line into parts
                    string[] parts = line.Split(new[] { "!|!" }, StringSplitOptions.RemoveEmptyEntries);

                    // Ensure we have the correct number of parts
                    if (parts.Length >= 2)
                    {
                        // Parse color name and price
                        string name = parts[0];
                        decimal price = decimal.Parse(parts[1]);

                        // Add the color to the ColourValues collection
                        ColourValues.Colours.Add(new Colour(name, price));
                    }
                }
            }
        }

        public static void WriteColours(Colour[] colours)
        {
            // Clean the file before writing new colors
            CleanFile();

            // Use a StreamWriter to write colors to the file
            using (StreamWriter sw = new StreamWriter(FileLocation))
            {
                foreach (var item in colours)
                {
                    sw.WriteLine($"{item.Name}!|!{item.Price}"); // Write each color as a line in the file
                }
            }
        }

        private static void CleanFile()
        {
            try
            {
                if (File.Exists(FileLocation))
                {
                    File.Delete(FileLocation); // Delete the existing file
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }

            // Create a new empty file
            try
            {
                File.Create(FileLocation).Close(); // Ensure the file is created and closed
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error creating file: {ex.Message}");
            }
        }
    }
}
