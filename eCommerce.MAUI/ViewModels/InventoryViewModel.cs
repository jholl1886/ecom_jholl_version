﻿using Amazon.Library.Models;
using Amazon.Library.Services;
using eCommerce.Library.DTO;
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

        public string Query { get; set; }

        public Product? Product;
        public List<ProductViewModel> Products { 
            get {
                return InventoryServiceProxy.Current.Products.Where(p=>p != null)
                    .Select(p => new ProductViewModel(p)).ToList() 
                    ?? new List<ProductViewModel>();
            } 
        }

        public async void Refresh() //being async and line 34 fixed UI not showing up after add in database
        {
            await InventoryServiceProxy.Current.Get();
            NotifyPropertyChanged(nameof(Products));
            SelectedProduct = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       public ProductViewModel SelectedProduct { get; set; }
        
        public async void DeleteProduct()
        {
            if(SelectedProduct?.Model == null)
            {
                return;
            }

            await InventoryServiceProxy.Current.Delete(SelectedProduct.Model.Id); // got stuck here for awhile because we had to make instance of ProductViewModel and it was called "Model" to retrieve list of products Id's
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
            Refresh();

        }

        public async void Search()
        {
            await InventoryServiceProxy.Current.Search(new Query(Query));
            Refresh();
        }

        public async void Import()
        {
            Shell.Current.GoToAsync("//Import");
        }
    }
}
 