﻿using App_Clientes.Views;

namespace App_Clientes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ClienteMainPage());
        }
    }
}
