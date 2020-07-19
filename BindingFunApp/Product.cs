using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace BindingFunApp
{
    public class Product : TableEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Color { get; set; }
    }
}