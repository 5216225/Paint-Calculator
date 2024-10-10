using System; // Provides fundamental classes and base classes for commonly-used value and reference data types
using System.Collections.Generic; // Provides classes for defining generic collections

namespace Paint_Calculator
{
    // Class to manage color values in the Paint Calculator application.
    class ColourValues
    {
        // Backing field for the Colours list
        private static List<Colour> _colours = new List<Colour>();

        // Gets the list of colors.
        public static List<Colour> Colours
        {
            get { return _colours; }
        }

        // Gets or sets the currently chosen color.
        public static Colour ChosenColour { get; set; }

        // Adds a new color to the list.
        public static void AddColour(Colour colour)
        {
            if (colour != null && !_colours.Contains(colour))
            {
                _colours.Add(colour);
            }
            else
            {
                Console.WriteLine("Color already exists or is null.");
            }
        }
        // Removes a color from the list.
        public static void RemoveColour(Colour colour)
        {
            if (colour != null && _colours.Contains(colour))
            {
                _colours.Remove(colour);
            }
            else
            {
                Console.WriteLine("Color does not exist or is null.");
            }
        }      
        // Initializes the chosen color to a default color, if available.
        public static void InitializeChosenColour()
        {
            if (_colours.Count > 0)
            {
                ChosenColour = _colours[0]; // Set to the first color in the list
            }
            else
            {
                ChosenColour = null; // No colors available
            }
        }
    }
}
