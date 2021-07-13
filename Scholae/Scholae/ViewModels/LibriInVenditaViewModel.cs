using MvvmHelpers.Commands;
using Scholae.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Scholae.ViewModels
{
    public class LibriInVenditaViewModel : BaseViewModel
    {
        public Utente utenteCorrente;

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
            utenteCorrente = Session.GetSession().UtenteCorrente;
            InitData();
        }

        private void InitData()
        {
            //Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            //libri = APIConnector.tuttiImieiLibri(utente.Id);
            libri = APIConnector.tuttiImieiLibri(utenteCorrente.Id);
            IMieiLibri = new ObservableCollection<Libro>(libri);
        }

    }
}
