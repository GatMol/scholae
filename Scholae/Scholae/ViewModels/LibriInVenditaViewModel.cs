using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Scholae.Services;

namespace Scholae.ViewModels
{
    public class LibriInVenditaViewModel
    {
        public Session sessioneCorrente;

        public List<Libro> libri;

        private ObservableCollection<Libro> iMieiLibri;

        public ObservableCollection<Libro> IMieiLibri
        {
            get
            {
                return iMieiLibri;
            }
            set
            {
                iMieiLibri = value;
            }
        }

        public LibriInVenditaViewModel()
        {
            InitData();
            sessioneCorrente = Session.GetSession();
        }

        private void InitData()
        {
            //Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            //libri = APIConnector.tuttiImieiLibri(utente.Id);
        }
    }
}
