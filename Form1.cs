using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paint_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadInitialColours();
            SetupToolTips();
            CustomizeUI();
            CustomizeTextBoxes(); // Call to customize text boxes
        }

        private void CustomizeUI()
        {
            // Set a modern font
            this.Font = new System.Drawing.Font("Segoe UI", 12);
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            // Customize buttons with hover effects
            CustomizeButton(buttonCalculate, System.Drawing.Color.FromArgb(0, 122, 204));
            CustomizeButton(buttonClear, System.Drawing.Color.FromArgb(255, 69, 58));

            // Event handlers for hover effects
            buttonCalculate.MouseEnter += (s, e) => OnButtonHover(buttonCalculate, true);
            buttonCalculate.MouseLeave += (s, e) => OnButtonHover(buttonCalculate, false);

            buttonClear.MouseEnter += (s, e) => OnButtonHover(buttonClear, true);
            buttonClear.MouseLeave += (s, e) => OnButtonHover(buttonClear, false);
        }

        private void OnButtonHover(Button button, bool isHovered)
        {
            button.BackColor = isHovered ? System.Drawing.Color.FromArgb(0, 150, 255) : System.Drawing.Color.FromArgb(0, 122, 204);
            button.ForeColor = isHovered ? System.Drawing.Color.White : System.Drawing.Color.White;
            button.Font = new System.Drawing.Font("Segoe UI", isHovered ? 13 : 12, isHovered ? FontStyle.Bold : FontStyle.Regular);
            button.FlatAppearance.BorderSize = 0;
        }

        private void CustomizeTextBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = System.Drawing.Color.White;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.Font = new System.Drawing.Font("Segoe UI", 12);
                    textBox.ForeColor = System.Drawing.Color.Black;
                    textBox.Padding = new Padding(10); // Add padding for better aesthetics
                }
            }
        }

        private void CustomizeButton(Button button, System.Drawing.Color color)
        {
            button.BackColor = color;
            button.ForeColor = System.Drawing.Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new System.Drawing.Font("Segoe UI", 12);
        }

        private void LoadInitialColours()
        {
            FileHandling.LoadColours();
            ColourValues.InitializeChosenColour();
        }

        private void SetupToolTips()
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(textBox1, "Enter length in meters");
            toolTip.SetToolTip(textBox2, "Enter width in meters");
            toolTip.SetToolTip(textBox3, "Enter height in meters");
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            ((TextBox)sender).Clear();
        }

        private void setToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxSetPrice.Show();
        }

        private void addColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxAddColour.Show();
        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxChooseColour.Show();
        }

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private bool ValidateInputs(out double length, out double width, out double height)
        {
            length = width = height = 0;

            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                ShowMessage("Please fill in all fields.");
                return false;
            }

            if (!TryParseInput(textBox1.Text, out length, "length") ||
                !TryParseInput(textBox2.Text, out width, "width") ||
                !TryParseInput(textBox3.Text, out height, "height"))
            {
                return false;
            }

            return true;
        }

        private bool TryParseInput(string input, out double value, string dimension)
        {
            if (!double.TryParse(input, out value) || value <= 0)
            {
                ShowMessage($"Please enter a valid positive number for {dimension}.");
                return false;
            }
            return true;
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out double length, out double width, out double height)) return;

            try
            {
                double floorArea = length * width;
                double wallArea = 2 * (length * height + width * height);
                double paintRequired = wallArea / 10.0;
                double paintCost = paintRequired * (double)ColourValues.ChosenColour.Price;
                double roomVolume = length * width * height;

                var resultMessage = new StringBuilder()
                    .AppendLine($"Floor Area: {floorArea:F2} square meters")
                    .AppendLine($"Paint Required: {paintRequired:F2} liters")
                    .AppendLine($"Room Volume: {roomVolume:F2} cubic meters")
                    .AppendLine($"Total Paint Cost: {paintCost:C2}");

                ShowMessage(resultMessage.ToString());
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                ShowMessage("An unexpected error occurred. Please try again.");
            }
        }

        private void Form1_FormClosed(object sender, EventArgs e)
        {
            SaveColours();
        }

        private void SaveColours()
        {
            try
            {
                FileHandling.WriteColours(ColourValues.Colours.ToArray());
            }
            catch (Exception ex)
            {
                LogError("Error saving colors: " + ex.Message);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
            ShowMessage("Fields cleared. Please enter new values.");
        }

        private void LogError(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("errorlog.txt", true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error logging message: " + ex.Message);
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
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
    }
}
