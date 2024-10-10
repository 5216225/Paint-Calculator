using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Paint_Calculator
{
    public partial class Choose_Colour : Form
    {
        // Constructor without parameters
        public Choose_Colour()
        {
            InitializeComponent(); // Initializes the form components
        }

        // Constructor with title parameter
        public Choose_Colour(string title)
        {
            InitializeComponent(); // Initializes the form components
            this.Text = title; // Set the title of the form
        }

        // Event handler for when the form loads
        private void Choose_Colour_Load(object sender, EventArgs e)
        {
            PopulateColourButtons(); // Call method to populate buttons with colors
        }

        // Method to populate the color buttons in the flow layout panel
        private void PopulateColourButtons()
        {
            if (!ColourValues.Colours.Any())
            {
                MessageBox.Show("No colors available for selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close(); // Close the form if no colors are available
                return;
            }

            foreach (var item in ColourValues.Colours)
            {
                Button colorButton = CreateColorButton(item);
                this.flowLayoutPanel1.Controls.Add(colorButton); // Add button to the flow layout panel
            }
        }

        // Method to create a color button
        private Button CreateColorButton(Colour item)
        {
            Button button = new Button
            {
                Text = $"{item.Name}{Environment.NewLine}£{item.Price:F2}", // Set button text with price formatted
                Name = item.Name.Replace(" ", "_"), // Set name for the button
                Height = 48, // Button height
                Width = 80, // Button width (adjust for aesthetics)
                Tag = item // Store the Colour object in the button's Tag property
            };
            button.Click += ColorButton_Click; // Subscribe to the button click event

            // Add tooltip for additional info
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(button, $"Select {item.Name} for £{item.Price:F2}");

            return button; // Return the created button
        }

        // Event handler for when a color button is clicked
        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColourValues.ChosenColour = ((Colour)((Button)sender).Tag); // Set chosen color
            MessageBox.Show($"Colour chosen: {ColourValues.ChosenColour.Name}, Price: £{ColourValues.ChosenColour.Price:F2}", "Colour Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); // Close the form after selection
        }
    }

    // Static class for displaying the Choose_Colour form
    public static class MessageBoxChooseColour
    {
        // Method to show the Choose_Colour form with default title
        public static void Show()
        {
            using (var form = new Choose_Colour("Choose Colour"))
            {
                form.ShowDialog(); // Display the form as a modal dialog
            }
        }

        // Method to show the Choose_Colour form with a custom title
        public static void Show(string title)
        {
            using (var form = new Choose_Colour(title))
            {
                form.ShowDialog(); // Display the form as a modal dialog
            }
        }
    }
}
