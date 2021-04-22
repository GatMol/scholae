using System;
using System.Collections.Generic;
using System.Text;

namespace Scholae
{
    public class Utente
    {
        /*campo password?*/
        public string nome;
        public string cognome;
        public string email;
        public long numeroTelefono;
        public string lingua;
        public string citta;

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

        /*inizio getter setter*/
        public string getNome()
        {
            return this.nome;
        }

        public void setNome(string nome)
        {
            this.nome = nome;
        }

        public string getCognome()
        {
            return this.cognome;
        }

        public void setCognome(string cognome)
        {
            this.cognome = cognome;
        }

        public string getEmail()
        {
            return this.email;
        }

        public void setEmail(string email)
        {
            this.email = email;
        }

        public long getNumeroTelefono()
        {
            return this.numeroTelefono;
        }

        public void setNumeroDiTelefono(long numeroTelefono)
        {
            this.numeroTelefono = numeroTelefono;
        }

        public string getLingua()
        {
            return this.lingua;
        }

        public void setLingua(string lingua)
        {
            this.lingua = lingua;
        }

        public string getCitta()
        {
            return this.citta;
        }

        public void setCitta(string citta)
        {
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
