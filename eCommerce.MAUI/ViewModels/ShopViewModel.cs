using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
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
            UpdateCarts();
            NotifyPropertyChanged(nameof(CartContents));
            NotifyPropertyChanged(nameof(CartPrice));
        }

        private string inventoryQuery;
        public string InventoryQuery
        {
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

        private ShoppingCart selectedCart;

        public ShoppingCart SelectedCart
        {
            get => selectedCart;
            set
            {
                if (selectedCart != value)
                {
                    
                    selectedCart = value;
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

        private List<ShoppingCart> carts;
        public List<ShoppingCart> Carts
        {
            get => carts;
            set
            {
                carts = value;
                NotifyPropertyChanged();
            }
        }

        public List<ProductViewModel> CartContents => Cart?.Contents.Select(p => new ProductViewModel(p)).ToList();

        public void Refresh()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(ProductToBuy));
            CartPrice = ShoppingCartService.Current.Cart.Price;
            NotifyPropertyChanged(nameof(CartPrice));
            NotifyPropertyChanged(nameof(CartContents));
            UpdateCarts();
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
            CartPrice = ShoppingCartService.Current.Cart.Price;
            NotifyPropertyChanged(nameof(CartPrice));
            Refresh();
        }

        public void RemoveFromCart()
        {
            if (ProductToBuy?.Model == null)
            {
                return;
            }
            ShoppingCartService.Current.RemoveFromCart(ProductToBuy.Model);
            Refresh();
        }

        public void NewCart()
        {
            ShoppingCartService.Current.AddNewCart();
            UpdateCarts(); // Update the carts collection
        }

        private void UpdateCarts()
        {
            Carts = new List<ShoppingCart>(ShoppingCartService.Current.Carts);
            NotifyPropertyChanged(nameof(Carts));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
