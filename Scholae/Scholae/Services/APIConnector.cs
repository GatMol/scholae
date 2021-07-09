using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using Scholae.ViewModels;
using System;
using Xamarin.Forms;
using System.IO;
using System.Diagnostics;

namespace Scholae.Services
{
    public class APIConnector
    {
        private static string bearerToken = App.token;

        public static IRestResponse Signup(Utente u)
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
            return client.Execute(request);
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

        public static List<Libro> GetLibroPerNome(string nome)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/libro/cercaPerNome/{nome}" , Method.GET);
            IRestResponse response = client.Execute(request);
            List<Libro> libriPerNome = JsonConvert.DeserializeObject<List<Libro>>(response.Content);
            return libriPerNome;
        }

        public static Utente GetUtentePerEmail(string email)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/utente/cercaPerEmail/{email}", Method.GET);
            IRestResponse response = client.Execute(request);
            Utente utente = JsonConvert.DeserializeObject<Utente>(response.Content);
            return utente;
        }

        public static Libro GetLibroPerId(long id)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/libro/cercaPerId/{id}", Method.GET);
            IRestResponse response = client.Execute(request);
            Libro libro = JsonConvert.DeserializeObject<Libro>(response.Content);
            return libro;
        }

        public static void AddLibroSalvatoAdUtente(long id_libro, long id_utente)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/libroSalvato", Method.POST);
            request.AddJsonBody(
                new
                {
                    Libro_id = id_libro,
                    Utente_id = id_utente
                });
            client.Execute(request);
        }

        public static List<Libro> GetLibriSalvati(long id)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/libroSalvato/cercaPerUtente/{id}", Method.GET);
            IRestResponse response = client.Execute(request);
            List<LibroSalvato> libriSalvati = JsonConvert.DeserializeObject<List<LibroSalvato>>(response.Content);
            List<Libro> libri = new List<Libro>();
            foreach(LibroSalvato ls in libriSalvati)
            {
                libri.Add(ls.Libro);
            }
            return libri;
        }

        public static void DeleteLibroSalvatoAdUtente(long id_libro, long id_utente)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/libroSalvato", Method.DELETE);
            request.AddJsonBody(
                new
                {
                    Libro_id = id_libro,
                    Utente_id = id_utente
                });
            client.Execute(request);
        }

        public static LibroSalvato GetLibroSalvato(long id_libro, long id_utente)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/libroSalvato/ottienimi", Method.GET);
            request.AddJsonBody(
                new
                {
                    Libro_id = id_libro,
                    Utente_id = id_utente
                });
            IRestResponse response = client.Execute(request);
            LibroSalvato libro = JsonConvert.DeserializeObject<LibroSalvato>(response.Content);
            Debug.WriteLine("\n\n\n\n" + libro);
            return libro;

        }

        public static List<Libro> tuttiImieiLibri(long utenteId)
    {
        var client = new RestClient($"{Constants.API_ENDPOINT}");
        var request = new RestRequest($"/libro/cercaPerUtente/{utenteId}", Method.GET);
        IRestResponse response = client.Execute(request);
        List<Libro> libro = JsonConvert.DeserializeObject<List<Libro>>(response.Content);
        return libro;
    }

    public static void DeleteLibro(long libroId)
    {
        var client = new RestClient($"{Constants.API_ENDPOINT}");
        var request = new RestRequest($"/libro/elimina/{libroId}", Method.DELETE);
        client.Execute(request);
    }


    public static void SalvaImmagine(ImageSource image)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/libro/images", Method.POST);
            //nel body form-data ho key:libroImage value:streamImg
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("libroImage", image.ToString());
            var response = client.Execute(request);
            Debug.WriteLine($"\n\n{response}\n\n");
        }
    }
}