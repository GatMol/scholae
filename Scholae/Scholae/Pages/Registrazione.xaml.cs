using RestSharp;
using Scholae.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scholae
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrazione : ContentPage
    {
        RegistrazioneViewModel registrazioneVM;
        public Registrazione()
        {
            Device.SetFlags(new string[] { "Shapes_Experimental" });
            InitializeComponent();
            registrazioneVM = new RegistrazioneViewModel();
            BindingContext = registrazioneVM;
        }

        public async void SignUp(Object sender, EventArgs e)
        {
            IRestResponse response = registrazioneVM.SignUp();
            if (response != null)
            {
                if (((int)response.StatusCode).Equals(409))
                    await DisplayAlert("Errore", "Email già utilizzata", "Ok");
                else if (response.IsSuccessful)
                    await Navigation.PopToRootAsync();
                else
                    await DisplayAlert("Errore", "Errore durante la registrazione", "Riprova");
            }
        }
    }
}