using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreFunctionApp.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}