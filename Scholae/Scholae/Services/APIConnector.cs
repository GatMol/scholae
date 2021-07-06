using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using Scholae.ViewModels;

namespace Scholae.Services
{
    public class APIConnector
    {
        private static string bearerToken = App.token;

        public static void Signup(Utente u)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/utente/signup", Method.POST);
            request.AddJsonBody(
                new { 
                    email = u.Email,
                    nome = u.Nome,
                    cognome = u.Cognome,
                    password = u.Password,
                    telefono = u.Telefono.ToString(),
                    citta = u.Citta,
                    nazionalita = u.Nazionalita
                });
            IRestResponse response = client.Execute(request);
        }

        public static IRestResponse Login(string email, string password)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/utente/login", Method.POST);
            request.AddJsonBody(
                new { 
                    email = email,
                    password = password
                });
            return client.Execute(request);
        }

        public static List<Libro> GetAllLibri()
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/libro", Method.GET);
            IRestResponse response = client.Execute(request);
            List<Libro> allLibri = JsonConvert.DeserializeObject<List<Libro>>(response.Content);
            return allLibri;
        }

        public static List<Libro> GetLibroPerNome(String nome)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/libro/cercaPerNome/{nome}" , Method.GET);
            IRestResponse response = client.Execute(request);
            List<Libro> libriPerNome = JsonConvert.DeserializeObject<List<Libro>>(response.Content);
            return libriPerNome;
        }
    }
}