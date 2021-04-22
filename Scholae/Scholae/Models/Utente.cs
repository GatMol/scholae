using System;
using System.Collections.Generic;
using System.Text;

namespace Scholae
{
    public class Utente
    {
        /*campo password?*/
        private string nome;
        private string cognome;
        private string email;
        private long numeroTelefono;
        private string lingua;
        private string citta;

        public string Nome { get => nome; set => nome = value; }
        public string Cognome { get => cognome; set => cognome = value; }
        public string Email { get => email; set => email = value; }
        public long NumeroTelefono { get => numeroTelefono; set => numeroTelefono = value; }
        public string Lingua { get => lingua; set => lingua = value; }
        public string Citta { get => citta; set => citta = value; }

        public Utente(string nome, string cognome, string email, long numeroTelefono, string lingua, string citta)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.email = email;
            this.numeroTelefono = numeroTelefono;
            this.lingua = lingua;
            this.citta = citta;
        }

        /*costruttore senza numero di telefono*/
        public Utente(string nome, string cognome, string email, string lingua, string citta)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.email = email;
            this.lingua = lingua;
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
    }

    
    

}
