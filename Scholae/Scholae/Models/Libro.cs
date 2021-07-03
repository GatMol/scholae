using System;
namespace Scholae.ViewModels
{
    public class Libro
    {

        public Libro()
        {

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

        public Utente Utente
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
