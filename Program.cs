using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintCalculator {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Paint_Calculator.Form1());
        }
    }
    public class PaintCalculator
    {
        public double CalculateFloorArea(double length, double width)
        {
            return length * width;
        }

        public double CalculateWallArea(double length, double width, double height)
        {
            return 2 * (length * height + width * height);
        }

        public double CalculatePaintRequired(double wallArea, double coveragePerLiter)
        {
            return wallArea / coveragePerLiter;
        }

        public double CalculateRoomVolume(double length, double width, double height)
        {
            return length * width * height;
        }
    }
}
