using Scholae.Pages;
using Scholae.Services;
using Scholae.ViewModels;
using System;
using System.Collections.Generic;
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
            Utente utenteCorrente = Session.GetSession().UtenteCorrente;
            List<Libro> libri = APIConnector.tuttiImieiLibri(utenteCorrente.Id);
            foreach (Libro l in libri)
            {
                utenteCorrente.AddLibroInVendita(l);
            }
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
            Libri_In_Vendita.BackgroundColor = Color.LightGray;
            await Navigation.PushAsync(new LibriInVenditaPage());
            Libri_In_Vendita.BackgroundColor = Color.Transparent;
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


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Sei sicuro di voler eliminare l'account?\nQuesta azione è irriversibile.", "Cancel", "Elimina");
            if (action.Equals("Elimina"))
            {
                SecureStorage.Remove("email");
                SecureStorage.Remove("accessToken");
                APIConnector.DeleteUtente(Session.GetSession().UtenteCorrente.Id);
                Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack[0]);
                await Navigation.PopAsync();
            }
        }

    }
}