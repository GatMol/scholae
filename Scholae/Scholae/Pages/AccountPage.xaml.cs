using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Scholae
{
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        async void tipologia_account_clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new BooksPage());
        }

        void libri_in_vendita_clicked(System.Object sender, System.EventArgs e)
        {
        }

        void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            Console.WriteLine($"ScrollX: {e.ScrollX}, ScrollY: {e.ScrollY}");
        }
    }
}