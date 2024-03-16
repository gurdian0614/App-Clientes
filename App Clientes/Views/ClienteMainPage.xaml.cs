using App_Clientes.ViewModels;

namespace App_Clientes.Views;

public partial class ClienteMainPage : ContentPage
{
	private ClienteMainPageViewModel viewModel;
	public ClienteMainPage()
	{
		InitializeComponent();
		viewModel = new ClienteMainPageViewModel();
		this.BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		viewModel.GetAll();
    }
}