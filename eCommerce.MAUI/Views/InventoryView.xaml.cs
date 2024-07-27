using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI.Views;

public partial class InventoryView : ContentPage
{
	public InventoryView()
	{
		InitializeComponent();
		BindingContext = new InventoryViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel).EditProduct();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        
            (BindingContext as InventoryViewModel).DeleteProduct();
       
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel)?.Search();
    }

    private void ImportClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel)?.Import();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryViewModel).Refresh();
    }
}