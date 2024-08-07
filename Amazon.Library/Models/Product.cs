﻿using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Amazon.Library.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
 
        public int Id { get; set; }

        public decimal MarkDown { get; set; }
        public bool IsBogo { get; set; }
        public int Quantity { get; set; }
        //IF I had any other properties that API didnt need or defunct they would not be in the dto file
        public Product ()
        {

        }
        public Product (Product p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            MarkDown = p.MarkDown;
            IsBogo = p.IsBogo;
            Quantity = p.Quantity;

        }

        public Product(ProductDTO d)
        {
            Name = d.Name;
            Description = d.Description;
            Price = d.Price;
            Id = d.Id;
            MarkDown = d.MarkDown;
            IsBogo = d.IsBogo;
            Quantity = d.Quantity;
        }
    }
}
