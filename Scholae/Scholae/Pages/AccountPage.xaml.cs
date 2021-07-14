using Scholae.Pages;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
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

        async void Libri_in_vendita_tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LibriInVenditaPage());
        }

        public async void Logout(System.Object sender, System.EventArgs e)
        {
            SecureStorage.Remove("email");
            SecureStorage.Remove("accessToken");
            Debug.WriteLine($"\n\nPAGINE NELLO STACK DOPO ACCOUNT PAGE: {Navigation.NavigationStack.Count}\n\n");
            Debug.WriteLine($"\n\nPAGINE NELLO STACK DOPO ACCOUNT PAGE: {Navigation.NavigationStack[0]}\n\n");
            Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack[0]);
            await Navigation.PopAsync();
        }
    }
}