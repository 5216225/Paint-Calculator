using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Paint_Calculator
{
    internal partial class Set_Price : Form
    {
        public Set_Price()
        {
            InitializeComponent();
            InitializeColorComboBox();
            CustomizeUI();
        }

        public Set_Price(string title)
        {
            InitializeComponent();
            this.Text = title;
            InitializeColorComboBox();
            CustomizeUI();
        }

        private void InitializeColorComboBox()
        {
            comboBox1.Items.Clear();
            foreach (var color in ColourValues.Colours)
            {
                comboBox1.Items.Add(color.Name);
            }
        }

        private void CustomizeUI()
        {
            // Set a modern font
            this.Font = new Font("Segoe UI", 12);
            this.BackColor = Color.FromArgb(30, 30, 30); // Dark background

            // Customize ComboBox
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; // Make it a drop-down list
            comboBox1.BackColor = Color.FromArgb(50, 50, 50);
            comboBox1.ForeColor = Color.White;
            comboBox1.FlatStyle = FlatStyle.Flat;

            // Customize NumericUpDown
            numericUpDown1.BackColor = Color.FromArgb(50, 50, 50);
            numericUpDown1.ForeColor = Color.White;
            numericUpDown1.BorderStyle = BorderStyle.None; // Remove border
            numericUpDown1.DecimalPlaces = 2;

            // Customize Button
            buttonSet.BackColor = Color.FromArgb(0, 122, 204);
            buttonSet.ForeColor = Color.White;
            buttonSet.FlatStyle = FlatStyle.Flat;
            buttonSet.FlatAppearance.BorderSize = 0;
            buttonSet.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 150, 255); // Hover color

            // Add tooltips for buttons
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(buttonSet, "Click to set the price for the selected color");

            // Set rounded corners for form and controls (requires custom drawing)
            this.FormBorderStyle = FormBorderStyle.None; // Optional: make form borderless
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            // (CreateRoundRectRgn needs P/Invoke, see below)
        }

        // P/Invoke to create rounded corners
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern System.IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePriceForSelectedColor();
        }

        private void UpdatePriceForSelectedColor()
        {
            var selectedColorName = comboBox1.Text;
            var selectedColor = ColourValues.Colours.FirstOrDefault(c => c.Name == selectedColorName);

            if (selectedColor != null)
            {
                numericUpDown1.Value = selectedColor.Price; // Set the price in the numeric up-down control
            }
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            var selectedColorName = comboBox1.Text;
            var selectedColor = ColourValues.Colours.FirstOrDefault(c => c.Name == selectedColorName);

            if (selectedColor != null)
            {
                decimal newPrice = numericUpDown1.Value;
                var result = MessageBox.Show($"Are you sure you want to set the price of {selectedColor.Name} to {newPrice:C}?",
                                              "Confirm Price Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    selectedColor.Price = newPrice;
                    MessageBox.Show($"Price for {selectedColor.Name} has been updated to {newPrice:C}.", "Price Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Color with name '{selectedColorName}' not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close(); // Close the form after processing the action
        }
    }

    // Static class for displaying the Set_Price form
    public static class MessageBoxSetPrice
    {
        public static void Show()
        {
            using (var form = new Set_Price("Set Price"))
            {
                form.ShowDialog(); // Display the form as a modal dialog
            }
        }

        public static void Show(string title)
        {
            using (var form = new Set_Price(title))
            {
                form.ShowDialog(); // Display the form as a modal dialog
            }
        }
    }
}
