using Scholae.Services;
using System;

namespace Scholae.ViewModels
{
    public class Libro
    {

        private string _immagine;

        private string nome;

        public Libro()
        {

        }

        public Libro(String nome, String ISBN, String autore, String editore, String edizione, int prezzo)
        {
            this.Nome = nome;
            this.Isbn = ISBN;
            this.Autore = autore;
            this.Editore = editore;
            this.Edizione = edizione;
            this.Prezzo = prezzo;
        }

        public Libro(long id, String nome, String ISBN, String autore, String editore, String edizione, int prezzo, string immagine)
        {
            this.id = id;
            this.Nome = nome;
            this.Isbn = ISBN;
            this.Autore = autore;
            this.Editore = editore;
            this.Edizione = edizione;
            this.Prezzo = prezzo;
            this.Immagine = immagine;
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

        public string Immagine
        {
            get
            {
                return Constants.LOCALHOST + _immagine;
            }
            set
            {
                _immagine = value;
            }
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

        public string NomeCorto
        {
            get
            {
                if (Nome.Length > 14)
                {
                    return Nome.Substring(0, 13) + "...";
                }
                else
                    return Nome;
            }
        }

        public string AutoreCorto
        {
            get
            {
                if (Autore.Length > 18)
                {
                    String[] NomeeCognome = Autore.Split(' ');
                    return NomeeCognome[0];
                }
                else
                    return Autore;
            }
        }

        public override string ToString()
        {
            string UtenteLibro = Utente != null ? Utente.ToString() : "Nessun utente";
            string MateriaLibro = Materia != null ? Materia.ToString() : "Nessuna materia";
            return "Libro:\n" + $"Nome: {Nome}\n" + $"Autore: {Autore}\n" + $"Edizione: {Edizione}\n" + $"Editore: {Editore}\n"
                + $"ISBN: {Isbn}\n" + $"Immagine: {Immagine}\n" + $"Utente: {UtenteLibro}\n" + $"Materia: {MateriaLibro}\n";
        }

    }
}
