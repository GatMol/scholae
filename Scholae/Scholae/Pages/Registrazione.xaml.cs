using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Npgsql;
using Scholae.Services;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Scholae.ViewModels;
using RestSharp;

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