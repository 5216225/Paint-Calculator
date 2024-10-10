using System; // Provides fundamental classes and base classes for commonly-used value and reference data types

namespace Paint_Calculator
{
    // Represents a color with a name and a price.
    class Colour
    {
        // Auto-implemented properties for Name and Price
        public string Name { get; set; }
        public decimal Price { get; set; }        
        // Initializes a new instance of the Colour class.
        public Colour(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Color name cannot be null or empty.", nameof(name));

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));

            Name = name;
            Price = price;
        }        
        // Returns a string representation of the Colour object.       
        public override string ToString()
        {
            return $"{Name} (Price: £{Price:F2})"; // Format price to 2 decimal places
        }
    }
}
