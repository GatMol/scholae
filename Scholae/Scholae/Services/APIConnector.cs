﻿using Newtonsoft.Json;
using RestSharp;
using Scholae.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
                new
                {
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

        public static List<Libro> tuttiImieiLibri(long id)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/libro/cercaPerUtente/{id}", Method.GET);
            IRestResponse response = client.Execute(request);
            List<Libro> allLibri = JsonConvert.DeserializeObject<List<Libro>>(response.Content);
            return allLibri;
        }

        public static IRestResponse Login(string email, string password)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/utente/login", Method.POST);
            request.AddJsonBody(
                new
                {
                    email = email,
                    password = password
                });
            return client.Execute(request);
        }

        public static List<Libro> GetAllLibri(long idutente)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/libro/{idutente}", Method.GET);
            IRestResponse response = client.Execute(request);
            List<Libro> allLibri = JsonConvert.DeserializeObject<List<Libro>>(response.Content);
            return allLibri;
        }

        public static List<Libro> GetLibroPerNome(string nome, long utenteid)
        {
            Debug.WriteLine("\nNome quando entriamo dentro GETLIBROPERNOME " + nome + utenteid);
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            Debug.WriteLine("\nAPI  " + Constants.API_ENDPOINT);
            var request = new RestRequest($"/libro/cercaPerNome/{utenteid}/{nome}", Method.GET);
            Debug.WriteLine(client.BuildUri(request));
            IRestResponse response = client.Execute(request);
            Debug.WriteLine(response);
            List<Libro> libriPerNome = JsonConvert.DeserializeObject<List<Libro>>(response.Content);
            if(libriPerNome == null) Debug.WriteLine("\n\nLista libri per nome nulla");
            else
            {
                foreach(Libro l in libriPerNome)
                    Debug.WriteLine("\nLibro della lista: " + l.ToString());
            }
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
            var request = new RestRequest($"/libro/libroId/{id}", Method.GET);
            IRestResponse response = client.Execute(request);
            Libro libro = JsonConvert.DeserializeObject<Libro>(response.Content);
            Debug.WriteLine(libro.ToString());
            return libro;
        }

        public sealed class LibroEImmagine
        {
            [JsonProperty(propertyName: "Libro")]
            public Libro libro { set; get; }

            [JsonProperty(propertyName: "Stream")]
            public Stream img { set; get; }
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
            IRestResponse response = client.Execute(request);
        }

        public static List<Libro> GetLibriSalvati(long id)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/libroSalvato/cercaPerUtente/{id}", Method.GET);
            IRestResponse response = client.Execute(request);
            List<LibroSalvato> libriSalvati = JsonConvert.DeserializeObject<List<LibroSalvato>>(response.Content);
            List<Libro> libri = new List<Libro>();
            if (libriSalvati != null)
            {
                foreach (LibroSalvato ls in libriSalvati)
                {
                    libri.Add(ls.Libro);
                }
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
            IRestResponse response = client.Execute(request);
        }

        public static void DeleteLibro(long id)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/libro/{id}", Method.DELETE);
            IRestResponse response = client.Execute(request);
        }

        public static void DeleteUtente(object utenteid)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/utente/{utenteid}", Method.DELETE);
            IRestResponse response = client.Execute(request);
        }

        public static LibroSalvato GetLibroSalvato(long id_libro, long id_utente)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/libroSalvato/get", Method.GET);
            request.AddJsonBody(
                new
                {
                    Libro_id = id_libro,
                    Utente_id = id_utente
                });
            IRestResponse response = client.Execute(request);
            LibroSalvato libro = JsonConvert.DeserializeObject<LibroSalvato>(response.Content);
            return libro;
        }

        public static Libro CreaLibro(Libro libro)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/libro", Method.POST);
            Debug.WriteLine("\n\nCREA LIBRO:");
            Debug.WriteLine(libro.ToString());
            request.AddJsonBody(
                new
                {
                    ISBN = libro.Isbn,
                    Nome = libro.Nome,
                    Autore = libro.Autore,
                    Edizione = libro.Edizione,
                    Editore = libro.Editore,
                    Prezzo = libro.Prezzo,
                    Materia_id = libro.Materia.Nome,
                    Utente_id = libro.Utente.Id
                });
            return JsonConvert.DeserializeObject<Libro>(client.Execute(request).Content);
        }

        public static IRestResponse AggiungiFotoLibro(long libroId, byte[] img, string filename)
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest($"/libro/foto/{libroId}", Method.POST);
            request.AddFile("libroImage", img, filename, "multipart/form-data");
            return client.Execute(request);
        }

        public static List<Materia> GetAllMaterie()
        {
            var client = new RestClient($"{Constants.API_ENDPOINT}");
            var request = new RestRequest("/materia", Method.GET);
            var response = client.Execute(request);
            List<Materia> materie = JsonConvert.DeserializeObject<List<Materia>>(response.Content);
            return materie;
        }
    }
}