using System;
using System.Diagnostics;

namespace Scholae.ViewModels
{
    public class Libro
    {

        public Libro()
        {

        }

        public Libro(long id, String nome, String ISBN, String autore, String editore, String edizione, int prezzo)
        {
            this.id = id;
            this.Nome = nome;
            this.Isbn = ISBN;
            this.Autore = autore;
            this.Editore = editore;
            this.Edizione = edizione;
            this.Prezzo = prezzo;
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

        public String Isbn
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

        public Boolean IsSalvato
        {
            get {
                return LibriViewModels.Salvato(id);
            }
            set
            {
                IsSalvato = LibriViewModels.Salvato(id);
            }
        }

        public Boolean NotIsSalvato
        {
            get
            {
                Debug.WriteLine("\n\n\nmetodo get di NotIsSalvato\n\n\n\n");
                Debug.WriteLine(!LibriViewModels.Salvato(id));
                return !LibriViewModels.Salvato(id);
            }
            set
            {
                Debug.WriteLine("\n\n\nmetodo set di NotIsSalvato\n\n\n\n");
                NotIsSalvato = !LibriViewModels.Salvato(id);
                Debug.WriteLine(NotIsSalvato);
            }
        }

    }
}
