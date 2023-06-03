using System;
using System.Diagnostics;
using Scholae.Services;

namespace Scholae.ViewModels
{
    public class AccountPageViewModel: BaseViewModel
    {
        private Utente utenteCorrente;

        public Utente UtenteCorrente
        {
            get
            {
                return utenteCorrente;
            }
            set
            {
                utenteCorrente = value;
                OnPropertyChanged();
            }
        }

        public AccountPageViewModel()
        {
            UtenteCorrente = Session.GetSession().UtenteCorrente;
            Debug.WriteLine("\n\nbase view model: utente corrente: " + UtenteCorrente.ToString());
        }
    }
}
