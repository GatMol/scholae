using Scholae.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scholae
{
    public class Utente
    {
        private long id;
        private string nome;
        private string cognome;
        private string email;
        private string password;
        private long telefono;
        private string nazionalita;
        private string citta;
        //private List<LibroSalvato> libriSalvati;
        //private List<Libro> libriInVendita;

        public long Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cognome { get => cognome; set => cognome = value; }
        public string Email { get => email; set => email = value; }
        public long Telefono { get => telefono; set => telefono = value; }
        public string Nazionalita { get => nazionalita; set => nazionalita = value; }
        public string Citta { get => citta; set => citta = value; }
        public string Password{ get => password; }
        public string NomeCognome { get
            {
                return Nome + " " + Cognome;
            }
            set
            {
            }
        }
        
        public List<LibroSalvato> LibriSalvati { get; set; }
        public List<Libro> LibriInVendita { get; set; }


        public Utente()
        {

        }

        public Utente(string nome, string cognome, string email, string password, long numeroTelefono, string nazionalita, string citta)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.email = email;
            this.password = password;
            this.telefono = numeroTelefono;
            this.nazionalita = nazionalita;
            this.citta = citta;
        }

        public void AddLibroInVendita(Libro libro)
        {
            if (LibriInVendita == null)
            {
                LibriInVendita = new List<Libro>();
            }
            LibriInVendita.Add(libro);
        }

        /*Equals e hashCode*/
        public override bool Equals(object obj)
        {
            return obj is Utente user &&
                   email == user.email;
        }

        public override int GetHashCode()
        {
            return 848330207 + EqualityComparer<string>.Default.GetHashCode(email);
        }

        public override string ToString()
        {
            return $"Utente = {Id}" +
                $"nome: {this.nome}, " +
                $"cognome: {this.cognome}, " +
                $"email: {this.email}," +
                $"pwd: {this.password}," +
                $"citta: {this.citta}" +
                $"telefono: {this.telefono}" +
                $"nazionalita: {this.nazionalita},";
        }

    }
}
