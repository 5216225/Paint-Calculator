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
    internal partial class Add_Colour : Form {

        public Add_Colour() {
            InitializeComponent();
        }

        public Add_Colour(string title) {
            InitializeComponent();

            this.Text = title;
        }

        private void buttonAddColour_Click(object sender, EventArgs e) {
            if (!this.textBox1.Text.Contains("!") && !this.textBox1.Text.Contains("|")) {
                var x = new Colour(this.textBox1.Text, this.numericUpDown1.Value);

                ColourValues.Colours.Add(x);
                MessageBox.Show(String.Format("Colour added with name {0} and price {1}", x.Name, x.Price));
                this.Close();
            } else {
                MessageBox.Show("The name of the paint cannot contain special characters");
            }
        }

        private void Add_Colour_Load(object sender, EventArgs e) {

        }
    }

    public static class MessageBoxAddColour {
    public static void Show() {
        // using construct ensures the resources are freed when form is closed
        using (var form = new Add_Colour("Add Colour")) {
            form.ShowDialog();
        }
    }

        public static void Show(string title) {
            // using construct ensures the resources are freed when form is closed
            using (var form = new Add_Colour(title)) {
                form.ShowDialog();
            }
        }
    }
    
}
