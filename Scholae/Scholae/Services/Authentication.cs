using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Newtonsoft.Json;
using RestSharp;
using System;
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
                //await SecureStorage.SetAsync("email", email);
                Debug.WriteLine("\nLOGINPAGE: setto nel secureStorage email e token\n");
                await SecureStorage.SetAsync("email", utenteConToken.Utente.Email);
                await SecureStorage.SetAsync("accessToken", utenteConToken.AccessToken);
                //Email = email;
                Debug.WriteLine("\nLOGINPAGE: setto l'utenteCorrente nella sessione\n");
                Session.GetSession().UtenteCorrente = utenteConToken.Utente;
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
