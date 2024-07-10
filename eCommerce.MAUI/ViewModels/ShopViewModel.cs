using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        public ShopViewModel() 
        {
            InventoryQuery = string.Empty;
            Cart = ShoppingCartService.Current.Cart;
            CartPrice = ShoppingCartService.Current.Cart.Price;
            //CartTaxRate = ShoppingCartService.Current.Cart.TaxRate;
            NotifyPropertyChanged(nameof(CartContents));
            NotifyPropertyChanged(nameof(CartPrice));
            
        }

        
        private string inventoryQuery;
        public string InventoryQuery {
            set
            {
                inventoryQuery = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Products));
            }
            get { return inventoryQuery; }
        }
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current.Products.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        private ProductViewModel productToBuy;
        public ProductViewModel ProductToBuy
        {
            get => productToBuy;
            set
            {
                if (productToBuy != value)
                {
                    productToBuy = value;
                    
                    NotifyPropertyChanged();
                }
            }
        }

        private decimal cartPrice;
        public decimal CartPrice
        {
            get => cartPrice;
            set
            {
                if (cartPrice != value)
                {
                    cartPrice = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //private decimal cartTaxRate;
        //public decimal CartTaxRate
        //{
        //    get => cartTaxRate;
        //    set
        //    {
        //        if (cartTaxRate; != value)
        //        {
        //            cartTaxRate; = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}




        private ShoppingCart cart;
       public ShoppingCart Cart
        {
            get => cart;
            set
            {
                cart = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(CartContents));
                NotifyPropertyChanged(nameof(CartPrice));
                

            }
        }

        //public List<Product> CartContents => Cart?.Contents; something about needing to convert carts products to product view models
        public List<ProductViewModel> CartContents => Cart?.Contents .Select(p => new ProductViewModel(p)).ToList();
        public void Refresh()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(ProductToBuy));
            CartPrice = ShoppingCartService.Current.Cart.Price;
            NotifyPropertyChanged(nameof(CartPrice));
            NotifyPropertyChanged(nameof(CartContents));
        }

        public void Search()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        
        public void PlaceInCart()
        {
            if (ProductToBuy?.Model == null)
            {
                return;
            }
            ShoppingCartService.Current.AddToCart(ProductToBuy.Model);
            CartPrice = ShoppingCartService.Current.Cart.Price; // Update CartPrice after adding to cart
            NotifyPropertyChanged(nameof(CartPrice));
            Refresh();
        }

        public void RemoveFromCart()
        {
            ShoppingCartService.Current.RemoveFromCart(ProductToBuy.Model);
            Refresh();
        }

        
        


        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
