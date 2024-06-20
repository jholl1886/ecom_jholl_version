using Amazon.Library.Models;
using Amazon.Library.Services;
using eCommerce.MAUI.Views;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace eCommerce.MAUI.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {

        private ProductViewModel? _selectedProduct;

        public Product? Product;
        public List<ProductViewModel> Products { 
            get {
                return InventoryServiceProxy.Current.Products.Where(p=>p != null)
                    .Select(p => new ProductViewModel(p)).ToList() 
                    ?? new List<ProductViewModel>();
            } 
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       public ProductViewModel? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                NotifyPropertyChanged();
            }
        }
        public void DeleteProduct()
        {
            if(SelectedProduct?.Model == null)
            {
                return;
            }

            InventoryServiceProxy.Current.Delete(SelectedProduct.Model.Id); // got stuck here for awhile because we had to make instance of ProductViewModel and it was called "Model" to retrieve list of products Id's
                Refresh();
        }

        public void EditProduct()
        {
           if(SelectedProduct?.Model == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Product?productId={SelectedProduct.Model.Id}");
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct.Model);

        }
    }
}
