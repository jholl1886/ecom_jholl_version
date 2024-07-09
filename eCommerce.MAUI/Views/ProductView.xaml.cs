using Amazon.Library.Models;
using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI.Views;
[QueryProperty(nameof(ProductId), "productId")]
public partial class ProductView : ContentPage
{

    public int ProductId { get; set; }
    public ProductView()
	{
		InitializeComponent();
        
    }

    

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Inventory");
    }

    private void AddOrEditClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel).AddOrEdit();
        Shell.Current.GoToAsync("//Inventory");
    }

    private void YesButtonClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel).SetBuyOneGetOne(true);
    }

    private void NoButtonClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel).SetBuyOneGetOne(false);
    }




        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProductViewModel(ProductId);
    }

    
}