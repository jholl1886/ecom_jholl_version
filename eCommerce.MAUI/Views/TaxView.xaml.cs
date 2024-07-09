namespace eCommerce.MAUI.Views;

public partial class TaxView : ContentPage
{
	public TaxView()
	{
		InitializeComponent();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void SetTaxClicked(object sender, EventArgs e)
    {

    }
}