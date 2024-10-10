using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_Calculator {
    class FileHandling {

        private static Colour[] DefaultColours = { new Colour("Red", Convert.ToDecimal(1.0)), new Colour("Blue", Convert.ToDecimal(1.0)), new Colour("Green", Convert.ToDecimal(1.0)) };

        private static string _FileLocation = System.Windows.Forms.Application.CommonAppDataPath + "/colours.txt";
        public static string FileLocation {
            get {
                return _FileLocation;
            }
        }

        public static void LoadColours() {
            if (!File.Exists(FileLocation)) {
                CleanFile();
                WriteColours(DefaultColours);
            }

            StreamReader sr = new StreamReader(FileLocation);

            while (!sr.EndOfStream) {
                String x = sr.ReadLine();

                String[] z = x.Split("!|!".ToCharArray());
                List<String> y = new List<string>();

                foreach (var item in z) {
                    if(!(item == "")) {
                        y.Add(item);
                    }
                }

                decimal price = decimal.Parse(y.ToArray()[1]);

                ColourValues.Colours.Add(new Colour(y.ToArray()[0], price));
            }

            sr.Close();
        }

        public static void WriteColours(Colour[] colours) {
            StreamWriter sw = new StreamWriter(FileLocation);

            CleanFile();
            foreach (var item in colours) {
                sw.WriteLine(item.Name + "!|!" + item.Price);
            }

            sw.Close();
        }

        private static void CleanFile() {
            try {
                File.Delete(FileLocation);
            } catch (Exception) {

            }
            try {
                File.Create(FileLocation).Close();
            } catch (Exception) {

            }

        }
    }
}
