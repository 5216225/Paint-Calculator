using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_Calculator {
    class Colour {

        private String _Name;
        public String Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }

        private decimal _Price;
        public decimal Price {
            get {
                return _Price;
            }
            set {
                _Price = value;
            }
        }

        public Colour(String name, decimal price) {
            this.Name = name;
            this.Price = price;
        }

    }
}
