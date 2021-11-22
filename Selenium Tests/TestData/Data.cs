using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumTests.TestData
{
    public class Data
    {
        public List<Product> Products { get; set; }
        public Card Card { get; set; }
        public ValidationMessages ValidationMessages { get; set; }
        public Customer Customer { get; set; }
    }
}
