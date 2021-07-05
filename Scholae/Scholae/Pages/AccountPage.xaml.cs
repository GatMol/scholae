using System;
using System.Collections.Generic;
using Scholae.Pages;
using Xamarin.Forms;

namespace Scholae
{
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            Console.WriteLine($"ScrollX: {e.ScrollX}, ScrollY: {e.ScrollY}");
        }

        async void tipologia_account_tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AccountTypePage());
        }

        async void Libri_in_vendita_tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LibriInVenditaPage());
        }
    }
}