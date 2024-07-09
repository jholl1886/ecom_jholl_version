using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace eCommerce.MAUI.ViewModels
{
    public class TaxViewModel : INotifyPropertyChanged
    {

        public TaxViewModel()
        {
            TaxRate = ShoppingCartService.Current.Cart.TaxRate;
        }

        private decimal taxRate;
        public decimal TaxRate
        {
            get => taxRate;
            set
            {
                if (taxRate != value)
                {
                    taxRate = value;
                    NotifyPropertyChanged(nameof(TaxRate));
                }
            }
        }
        public void SetTaxRate()
        {
            ShoppingCartService.Current.UpdateTaxRate(TaxRate);
            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(TaxRate));
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
