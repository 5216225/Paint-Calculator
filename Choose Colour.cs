using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Calculator {
    public partial class Choose_Colour : Form {
        public Choose_Colour() {
            InitializeComponent();
        }

        public Choose_Colour(string title) {
            InitializeComponent();

            this.Text = title;
        }

        private void Choose_Colour_Load(object sender, EventArgs e) {
            foreach (var item in ColourValues.Colours) {
                Button x = new Button();
                x.Text = item.Name + Environment.NewLine + "£" + item.Price;
                x.Name = item.Name.Replace(" ", "_");
                x.Click += X_Click;
                x.Tag = item;
                x.Height = 48;
                x.Width = 48;

                this.flowLayoutPanel1.Controls.Add(x);
            }
        }

        private void X_Click(object sender, EventArgs e) {
            ColourValues.ChosenColour = ((Colour)((Button)sender).Tag);
            MessageBox.Show(String.Format("Colour chosen with name {0} and price {1}", ((Colour)((Button)sender).Tag).Name, ((Colour)((Button)sender).Tag).Price));
            this.Close();
        }
    }

    public static class MessageBoxChooseColour {
        public static void Show() {
            // using construct ensures the resources are freed when form is closed
            using (var form = new Choose_Colour("Choose Colour")) {
                 form.ShowDialog();
            }
        }

        public static void Show(string title) {
            // using construct ensures the resources are freed when form is closed
            using (var form = new Choose_Colour(title)) {
                 form.ShowDialog();
            }
        }
    }
}
