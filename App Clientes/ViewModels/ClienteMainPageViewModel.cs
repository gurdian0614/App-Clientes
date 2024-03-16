using App_Clientes.Models;
using App_Clientes.Services;
using App_Clientes.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace App_Clientes.ViewModels
{
    public partial class ClienteMainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Cliente> clienteCollection = new ObservableCollection<Cliente>();

        private readonly ClienteService _clienteService;

        public ClienteMainPageViewModel()
        {
            _clienteService = new ClienteService();
        }

        /// <summary>
        /// Obtiene el listado de clientes
        /// </summary>
        public void GetAll()
        {
            var getAll = _clienteService.GetAll();

            if (getAll.Count > 0)
            {
                ClienteCollection.Clear();
                foreach (var cliente in getAll)
                {
                    ClienteCollection.Add(cliente);
                }

            }
        }

        /// <summary>
        /// Redirecciona al formulario de clientes
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task GoToClienteFormPage()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new ClienteFormPage());
        }

        /// <summary>
        /// Selecciona el registro para editar o eliminar
        /// </summary>
        /// <param name="cliente">Registro a editar o eliminar</param>
        /// <returns>Redirecciona al formulario al editar, mostrar alerta al eliminar</returns>
        [RelayCommand]
        private async Task SelectCliente(Cliente cliente)
        {
            try
            {
                string res = await App.Current!.MainPage!.DisplayActionSheet("Operación", "Cancelar", null, "Actualizar", "Eliminar");

                switch (res)
                {
                    case "Actualizar":
                        await App.Current.MainPage.Navigation.PushAsync(new ClienteFormPage(cliente));
                        break;
                    case "Eliminar":
                        bool respuesta = await App.Current!.MainPage!.DisplayAlert("Eliminar Cliente", "¿Desea eliminar el cliente?", "Si", "No");

                        if (respuesta)
                        {
                            int del = _clienteService.Delete(cliente);
                            if (del > 0)
                            {
                                ClienteCollection.Remove(cliente);
                            }
                        }
                        break;
                }
            } catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

        /// <summary>
        /// Muestra alerta de error
        /// </summary>
        /// <param name="Mensaje">Mensaje a mostrar en la alerta</param>
        private void Error(String Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert("ERROR", Mensaje, "Aceptar"));
        }
    }
}
