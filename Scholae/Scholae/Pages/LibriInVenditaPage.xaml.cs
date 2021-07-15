using System;
using Scholae.Services;
using Scholae.ViewModels;
using Xamarin.Forms;

namespace Scholae.Pages
{
    public partial class LibriInVenditaPage : ContentPage
    {
        private Utente utenteCorrente;

        public LibriInVenditaPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            utenteCorrente = Session.GetSession().UtenteCorrente;
        }

        void Back_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Sei sicuro di voler eliminare il libro?\nQuesta azione è irriversibile.", "Cancel", "Elimina");
            if (action.Equals("Elimina"))
            {
                Label l = (sender as Label);
                var gesture = (TapGestureRecognizer)l.GestureRecognizers[0];
                long id = (long)gesture.CommandParameter;
                APIConnector.DeleteLibro(id);
                utenteCorrente.LibriInVendita.Remove(id);
                (BindingContext as LibriInVenditaViewModel).EliminaLibro();
            }
        }
    }
}
