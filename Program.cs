using System; // Provides fundamental classes and base classes for commonly-used value and reference data types
using System.Windows.Forms; // Provides classes for creating Windows-based applications that take full advantage of the rich user interface features of the Windows operating system

namespace PaintCalculator
{
    // Static class that serves as the entry point for the application
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] // Indicates that the COM threading model for the application is single-threaded apartment
        static void Main()
        {
            // Enables visual styles for the application
            Application.EnableVisualStyles();
            // Sets the default rendering for text to be compatible with the visual styles
            Application.SetCompatibleTextRenderingDefault(false);
            // Starts the application and opens the main form (Form1)
            Application.Run(new Paint_Calculator.Form1());
        }
    }

    // Class that encapsulates the functionality for paint calculations
    public class PaintCalculator
    {
        public double CalculateFloorArea(double length, double width)
        {
            return length * width; // Area = Length x Width
        }
        public double CalculateWallArea(double length, double width, double height)
        {
            // Wall Area = 2 * (Length x Height + Width x Height)
            return 2 * (length * height + width * height);
        }
        public double CalculatePaintRequired(double wallArea, double coveragePerLiter)
        {
            // Paint Required = Wall Area / Coverage per Liter
            return wallArea / coveragePerLiter;
        }
        public double CalculateRoomVolume(double length, double width, double height)
        {
            // Volume = Length x Width x Height
            return length * width * height;
        }
    }
}
