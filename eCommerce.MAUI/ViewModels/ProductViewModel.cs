using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
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
        public Product? Model { get; set; }

        public string DisplayPrice
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return $"{Model.Price:C}";
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
            Model = new Product();
        }

        public ProductViewModel(Product? model)
        {
            if(model != null)
            {
                Model = model;
            }
            else
            {
                Model = new Product();
            }
        }

        public ProductViewModel(int id)
        {
            Model = InventoryServiceProxy.Current?.Products?.FirstOrDefault(c => c.Id == id);
            if (Model == null)
            {
                Model = new Product();
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
                return Model.IsBogo ? "Item is BoGo" : "Item is not BoGo";
            }
        }


    }
}
