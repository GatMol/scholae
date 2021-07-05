using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Npgsql;


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
            /* controllo che i dati siano giusti (altrimenti popUp) e che possa metterli nel database (altrimenti popup)*/
            /* allora li metto nel database e torno alla homepage da cui accedo con i dati messi qui*/
            //await Navigation.PopToRootAsync();
            if (InvalidData())
                await DisplayAlert("Errore", "Campi non validi", "Ok");
            else {
                if (InvalidVerificationPassword())
                    await DisplayAlert("Errore", "Password non coincidenti", "Ok");
                else
                {
                    var connectionString = "host=scholae-database.cu9bh0ufrged.us-east-2.rds.amazonaws.com;database=myDB;username=gatto;password=scholaemobile2021";
                    var conn = new NpgsqlConnection(connectionString);
                    await conn.OpenAsync();
                    /*metti nel database cryptando password*/
                    using (var cmd = new NpgsqlCommand("INSERT INTO public.\"Utente\" VALUES (@email,@nome,@cognome,@password,@tel,@citta,@lingua)"
                        , conn))
                    {
                        cmd.Parameters.AddWithValue("email", EmailEntry.Text);
                        cmd.Parameters.AddWithValue("nome", NomeEntry.Text);
                        cmd.Parameters.AddWithValue("cognome", CognomeEntry.Text);
                        cmd.Parameters.AddWithValue("password", PasswordEntry.Text);
                        cmd.Parameters.AddWithValue("tel", long.Parse(NumeroDiTelefonoEntry.Text));
                        cmd.Parameters.AddWithValue("citta", CittaEntry.Text);
                        cmd.Parameters.AddWithValue("lingua", LinguaEntry.Text);
                        cmd.ExecuteNonQuery();
                    }
                    await conn.CloseAsync();
                    await Navigation.PushAsync(new TabbedHomePage());
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