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

namespace Scholae
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrazione : ContentPage
    {
        public Registrazione()
        {
            Device.SetFlags(new string[] { "Shapes_Experimental" });
            InitializeComponent();
            BindingContext = this;
        }

        /*salvo nelle variabili i cambi text delle entry per portarmi dietro le informazioni di ogni utente
          da mettere successivamente nel database*/

        async void Registration(object sender, EventArgs e)
        {
            string nome = NomeEntry.Text;
            string cognome = CognomeEntry.Text;
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            long telefono = long.Parse(NumeroDiTelefonoEntry.Text);
            string nazionalita = NazionalitaEntry.Text;
            string citta = CittaEntry.Text;
            //TODO: VALIDAZIONE DATI
            /* controllo che i dati siano giusti (altrimenti popUp) e che possa metterli nel database (altrimenti popup)*/
            /* allora li metto nel database e torno alla homepage da cui accedo con i dati messi qui*/
            if (InvalidData())
                await DisplayAlert("Errore", "Campi non validi", "Ok");
            else {
                if (InvalidVerificationPassword())
                    await DisplayAlert("Errore", "Password non coincidenti", "Ok");
                else
                {
                    Utente u = new Utente(nome, cognome, email, password, telefono, nazionalita, citta);
                    Debug.WriteLine(u.ToString());
                    APIConnector.Signup(u);
                    await Navigation.PopToRootAsync();
                }
            }
        }

        public bool InvalidData()
        {
            return string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                     string.IsNullOrWhiteSpace(NomeEntry.Text) ||
                     string.IsNullOrWhiteSpace(CognomeEntry.Text) ||
                     string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
                     string.IsNullOrWhiteSpace(VerificaPasswordEntry.Text);
        }

        private bool InvalidVerificationPassword()
        {
            return !string.Equals(PasswordEntry.Text, VerificaPasswordEntry.Text);
        }   
    }
}