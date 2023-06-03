namespace Scholae.Services
{
    public class Session
    {
        private static Session istanza;

        public Utente UtenteCorrente
        {
            get;
            set;
        }

        public static Session GetSession()
        {
            if (istanza == null)
            {
                istanza = new Session();
            }
            return istanza;
        }
    }
}
