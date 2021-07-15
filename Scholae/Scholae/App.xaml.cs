using Scholae.Services;
using Scholae.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Scholae
{
    public partial class App : Application
    {
        public static string email;
        public static string token;
        private Session sessioneCorrente;

        public App()
        {
            InitializeComponent();
            sessioneCorrente = Session.GetSession();
            MainPage = AlreadyLoggedIn().Result ? new NavigationPage(new TabbedHomePage()) : new NavigationPage(new LoginPage());
        }

        private async Task<bool> AlreadyLoggedIn()
        {
            Debug.WriteLine("\nAPERTURA APP: vedo se e' loggato\n");
            try
            {
                email = await SecureStorage.GetAsync("email");
                Debug.WriteLine($"\nEMAIL APP: {email}\n");
                token = await SecureStorage.GetAsync("accessToken");
                Debug.WriteLine($"\nTOKEN APP: {token}\n");
                if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(token))
                {
                    Debug.WriteLine("\nAPP.CS : Email e token esistenti\n");
                    Debug.WriteLine("\nAPP.CS : Cerco per email\n");
                    Utente utenteCorrente = APIConnector.GetUtentePerEmail(email);
                    Debug.WriteLine($"\nAPP.CS : Utente = {utenteCorrente}\n");
                    if (utenteCorrente != null)
                    {
                        Debug.WriteLine($"\nAPP.CS : Utente NON NULL\n");
                        sessioneCorrente.UtenteCorrente = utenteCorrente;
                        sessioneCorrente.UtenteCorrente.LibriInVendita = new Dictionary<long, Libro>();
                        sessioneCorrente.UtenteCorrente.LibriSalvati = new Dictionary<long, Libro>();
                        Debug.WriteLine($"\nSettata in sessione l'utente corrente\n");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine($"\nUtente con quell email non esiste\n");
                        return false;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
