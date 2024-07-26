﻿using Amazon.Library.Models;
using Amazon.Library.Services;
using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class ProductViewModel
    {
        public override string ToString()
        {
            if(Model == null)
            {
                return string.Empty;
            }
            return $"{Model.Id} - {Model.Name} - {Model.Price:C}";
        }
        public ProductDTO? Model { get; set; }

        public string DisplayPrice
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return $"{Model.Price:C}";
            }
        }

        public string DisplayUserPrice
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return $"{(Model.Price - Model.MarkDown):C}";
            }
        }

        public string PriceAsString
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return $"{Model.Price:C}";
            }
            set
            {
                if (Model == null)
                {
                    return;
                }
                if(decimal.TryParse(value, out var price)) {
                    Model.Price = price;
                }else
                {

                }
            }
        }

        public ProductViewModel()
        {
            Model = new ProductDTO();
        }

        public ProductViewModel(ProductDTO? model)
        {
            if(model != null)
            {
                Model = model;
            }
            else
            {
                Model = new ProductDTO();
            }
        }

        public ProductViewModel(int id)
        {
            Model = InventoryServiceProxy.Current?.Products?.FirstOrDefault(c => c.Id == id);
            if (Model == null)
            {
                Model = new ProductDTO();
            }
        }

        public void AddOrEdit()
        {
            if (Model != null)
            {
                InventoryServiceProxy.Current.AddOrUpdate(Model);
            }
        }

        public void SetBuyOneGetOne(bool isBuyOneGetOne)
        {
            Model.IsBogo = isBuyOneGetOne;
            
        }

        public string DisplayIsBogo
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return Model.IsBogo ? "BoGo" : "";
            }
        }

        public string DisplayMarkDown
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return Model.MarkDown > 0 ? "Sale!" : "";
            }
        }

        //MarkDownStuff
        


    }
}
