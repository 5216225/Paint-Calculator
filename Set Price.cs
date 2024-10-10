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
    internal partial class Set_Price : Form {

        public Set_Price() {
            InitializeComponent();
        }

        public Set_Price(string title) {
            InitializeComponent();

            this.Text = title;
        }

        private void Set_Price_Load(object sender, EventArgs e) {
            foreach (var item in ColourValues.Colours) {
                this.comboBox1.Items.Add(item.Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (var item in ColourValues.Colours) {
                if(item.Name == this.comboBox1.Text) {
                    this.numericUpDown1.Value = item.Price;
                }
            }

             
        }

        private void buttonSet_Click(object sender, EventArgs e) {
            bool found = false;
            foreach (var item in ColourValues.Colours) {
                if (item.Name == this.comboBox1.Text) {
                    found = true;
                    item.Price = this.numericUpDown1.Value;

                    MessageBox.Show(String.Format("Colour with name {0} set to price {1}", item.Name, item.Price));
                    break;
                }
                }
            if (!found) {
                    MessageBox.Show(String.Format("Colour with name {0} not found!", this.comboBox1.Text));
            }
            this.Close();
        }
    }

    public static class MessageBoxSetPrice {
        public static void Show() {
            // using construct ensures the resources are freed when form is closed
            using (var form = new Set_Price("Set Price")) {
                form.ShowDialog();
            }
        }

        public static void Show(string title) {
            // using construct ensures the resources are freed when form is closed
            using (var form = new Set_Price(title)) {
                form.ShowDialog();
            }
        }
    }
}
