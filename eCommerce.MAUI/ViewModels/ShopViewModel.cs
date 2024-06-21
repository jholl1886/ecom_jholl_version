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
        public ShopViewModel() {
            InventoryQuery = string.Empty;
            Cart = ShoppingCartService.Current.Cart;
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

        public ShoppingCart Cart { get; set; }

        public void Refresh()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(ProductToBuy));
            NotifyPropertyChanged(nameof(Cart.Contents));
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
            NotifyPropertyChanged(nameof(Cart.Contents));
            if(Cart.Contents != null)
            {
                Refresh();
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
