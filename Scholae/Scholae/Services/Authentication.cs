using Newtonsoft.Json;
using Scholae.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Scholae.Services
{
    public class Authentication
    {

        public static async Task<bool> AuthenticateUser(string email, string password)
        {
            var response = APIConnector.Login(email, password);
            if (response.IsSuccessful)
            {
                UtenteToken utenteConToken = JsonConvert.DeserializeObject<UtenteToken>(response.Content);
                Debug.WriteLine("\nLOGINPAGE: loggato e il token e': " + utenteConToken.AccessToken + "\n");
                Debug.WriteLine("\nLOGINPAGE: loggato e utenteCorrente: " + utenteConToken.Utente + "\n");
                Debug.WriteLine("\nLOGINPAGE: setto nel secureStorage email e token\n");
                await SecureStorage.SetAsync("email", utenteConToken.Utente.Email);
                await SecureStorage.SetAsync("accessToken", utenteConToken.AccessToken);
                Debug.WriteLine("\nLOGINPAGE: setto l'utenteCorrente nella sessione\n");
                Session.GetSession().UtenteCorrente = utenteConToken.Utente;
                Session.GetSession().UtenteCorrente.LibriInVendita = new Dictionary<long, Libro>();
                Session.GetSession().UtenteCorrente.LibriSalvati = new Dictionary<long, Libro>();
                Debug.WriteLine("\nLOGINPAGE: utenteCorrente nella sessione" + Session.GetSession().UtenteCorrente + "\n");
                return true;
            }
            return false;
        }

        public sealed class UtenteToken
        {
            [JsonProperty(propertyName: "token")]
            public string AccessToken { set; get; }

            [JsonProperty(propertyName: "Utente")]
            public Utente Utente { set; get; }
        }
    }
}
