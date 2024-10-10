﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Calculator {
    public partial class Form1 : Form {

        public Form1() {

            FileHandling.LoadColours();

            ColourValues.ChosenColour = ColourValues.Colours.ElementAt(0);

            InitializeComponent();
        }

        private void setToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBoxSetPrice.Show();
        }

        private void addColourToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBoxAddColour.Show();
        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBoxChooseColour.Show();
        }

        private void textBox1_Click(object sender, EventArgs e) {
            this.textBox1.Clear();
        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            this.textBox2.Clear();
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            this.textBox3.Clear();
        }

        private bool ValidateInputs(out double length, out double width, out double height)
        {
            bool isValid = true;

            // Check if the input fields are empty
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                isValid = false;
            }

            // Attempt to parse the inputs
            if (!double.TryParse(textBox1.Text, out length) || length <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for length.");
                isValid = false;
            }

            if (!double.TryParse(textBox2.Text, out width) || width <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for width.");
                isValid = false;
            }

            if (!double.TryParse(textBox3.Text, out height) || height <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for height.");
                isValid = false;
            }

            return isValid;
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // Input validation and parsing
                double length = double.Parse(textBox1.Text);
                double width = double.Parse(textBox2.Text);
                double height = double.Parse(textBox3.Text);

                // 1. Calculate the area of the floor
                double floorArea = length * width;

                // 2. Calculate the wall area
                double wallArea = 2 * (length * height) + 2 * (width * height);

                // Assuming 1 liter of paint covers 10 square meters
                double paintRequired = wallArea / 10.0; // amount of paint required in liters

                // Total paint cost: Convert ColourValues.ChosenColour.Price to double
                double paintCost = paintRequired * (double)ColourValues.ChosenColour.Price;

                // 3. Calculate the volume of the room
                double roomVolume = length * width * height;

                // Prepare results for display
                string resultMessage = $"Floor Area: {floorArea:F2} square meters\n" +
                                        $"Paint Required: {paintRequired:F2} liters\n" +
                                        $"Room Volume: {roomVolume:F2} cubic meters\n" +
                                        $"Total Paint Cost: £{paintCost:F2}";

                // Display results in a single message box
                MessageBox.Show(resultMessage);
            }
            catch (FormatException)
            {
                // Handle invalid input gracefully (e.g., non-numeric input)
                MessageBox.Show("Please enter valid numeric values for length, width, and height.");
            }
        }

        private void Form1_FormClosed(object sender, EventArgs e) {
            FileHandling.WriteColours(ColourValues.Colours.ToArray());
        }
    }
}
