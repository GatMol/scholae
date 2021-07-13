namespace Scholae.ViewModels
{
    public class LibroSalvato
    {
        public LibroSalvato()
        {
        }

        public LibroSalvato(Libro libro, Utente utente)
        {
            Libro = libro;
            Utente = utente;
        }

        public Libro Libro
        {
            get;
            set;

        }

        public Utente Utente
        {
            get;
            set;

        }
    }
}
