using App_Clientes.Models;
using App_Clientes.ViewModels;

namespace App_Clientes.Views;

public partial class ClienteFormPage : ContentPage
{
	private ClienteFormPageViewModel viewModel;
	public ClienteFormPage()
	{
		InitializeComponent();
		viewModel = new ClienteFormPageViewModel();
		this.BindingContext = viewModel;
	}

	public ClienteFormPage(Cliente cliente)
	{
		InitializeComponent();
		viewModel = new ClienteFormPageViewModel(cliente);
		this.BindingContext = viewModel;
	}
}