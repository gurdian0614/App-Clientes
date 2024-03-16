using App_Clientes.Models;
using App_Clientes.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App_Clientes.ViewModels
{
    public partial class ClienteFormPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string direccion;

        [ObservableProperty]
        private string telefono;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private double limiteCredito;

        private readonly ClienteService _clienteService;

        public ClienteFormPageViewModel()
        {
            _clienteService = new ClienteService();
        }

        public ClienteFormPageViewModel(Cliente cliente)
        {
            Id = cliente.Id;
            Nombre = cliente.Nombre;
            Direccion = cliente.Direccion;
            Telefono = cliente.Telefono;
            Email = cliente.Email;
            LimiteCredito = cliente.LimiteCredito;
            _clienteService = new ClienteService();
        }

        [RelayCommand]
        private async Task CreateUpdate()
        {
            try
            {
                Cliente cliente = new Cliente
                {
                    Id = Id,
                    Nombre = Nombre,
                    Direccion = Direccion,
                    Telefono = Telefono,
                    Email = Email,
                    LimiteCredito = LimiteCredito
                };

                if (Validar(cliente))
                {
                    if (Id == 0)
                    {
                        _clienteService.Insert(cliente);
                    } else
                    {
                        _clienteService.Update(cliente);
                    }
                    await App.Current!.MainPage!.Navigation.PopAsync();
                }
            } catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

        private bool Validar(Cliente cliente)
        {
            try
            {
                if (cliente.Nombre == null || cliente.Nombre == "")
                {
                    Warning("Escriba el nombre.");
                    return false;
                } else if (cliente.Direccion == null || cliente.Direccion == "")
                {
                    Warning("Escriba la dirección.");
                    return false;
                }
                else
                {
                    return true;
                }
            } catch (Exception ex)
            {
                Error(ex.Message);
                return false;
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

        /// <summary>
        /// Muestra alerta de advertencia
        /// </summary>
        /// <param name="Mensaje">Mensaje a mostrar en la alerta</param>
        private void Warning(String Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert("ADVERTENCIA", Mensaje, "Aceptar"));
        }
    }
}
