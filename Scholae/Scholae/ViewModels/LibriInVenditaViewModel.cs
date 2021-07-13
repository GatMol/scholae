using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Scholae.Services;
using System.Linq;

namespace Scholae.ViewModels
{
    public class LibriInVenditaViewModel: BaseViewModel
    {
        public Session sessioneCorrente;

        public List<Libro> libri;

        private ObservableCollection<Libro> iMieiLibri;

        public ICommand Elimina => new Command<long>((long id) =>
        {
            EliminaLibro(id);
        });

        private void EliminaLibro(long id)
        {
            APIConnector.DeleteLibro(id);
            InitData();
        }

        public ObservableCollection<Libro> IMieiLibri
        {
            get
            {
                return iMieiLibri;
            }
            set
            {
                iMieiLibri = value;
                OnPropertyChanged();
            }
        }

        public LibriInVenditaViewModel()
        {
            InitData();
            sessioneCorrente = Session.GetSession();
        }

        private void InitData()
        {
            Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            libri = APIConnector.tuttiImieiLibri(utente.Id);
            IMieiLibri = new ObservableCollection<Libro>(libri);
            //Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            //libri = APIConnector.tuttiImieiLibri(utente.Id);
        }

    }
}
