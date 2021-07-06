using System;
using System.Collections.Generic;
using System.Text;

namespace Scholae
{
    public class Utente
    {
        private string nome;
        private string cognome;
        private string email;
        private string password;
        private long telefono;
        private string nazionalita;
        private string citta;

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

        /*costruttore senza numero di telefono*/
        public Utente(string nome, string cognome, string email, string password, string nazionalita, string citta)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.email = email;
            this.password = password;
            this.nazionalita = nazionalita;
            this.citta = citta;
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
            return $"Utente = " +
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
