using System;
namespace Scholae.ViewModels
{
    public class Libro
    {

        public Libro()
        {

        }

        public Libro(long id, String nome, String ISBN, String autore, String editore, String edizione, int prezzo, User utente, Materia materia)
        {
            this.id = id;
            this.Nome = nome;
            this.ISBN = ISBN;
            this.Autore = autore;
            this.Editore = editore;
            this.Edizione = edizione;
            this.Prezzo = prezzo;
            this.Utente = utente;
            this.Materia = materia;
        }

        public long id
        {
            get;
            set;

        }

        public String Nome
        {
            get;
            set;
        }

        public String ISBN
        {
            get;
            set;

        }

        public String Autore
        {
            get;
            set;
        }

        public String Editore
        {
            get;
            set;
        }

        public String Edizione
        {
            get;
            set;
        }

        public int Prezzo
        {
            get;
            set;
        }

        public User Utente
        {
            get;
            set;
        }

        public Materia Materia
        {
            get;
            set;
        }
    }
}
