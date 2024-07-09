﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Models
{
    public class ShoppingCart
    {
        int Id { get; set; }

        decimal TaxRate { get; set; }

        public decimal Price { get; set; } = 0;

        public List<Product> Contents { get; set; } = new List<Product>();
    }
}
