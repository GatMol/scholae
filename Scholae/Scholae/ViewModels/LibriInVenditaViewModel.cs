using MvvmHelpers.Commands;
using Scholae.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scholae.ViewModels
{
    public class LibriInVenditaViewModel : BaseViewModel
    {
        public Utente utenteCorrente;        

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
            if (utenteCorrente.LibriInVendita.Values != null)
                IMieiLibri = new ObservableCollection<Libro>(utenteCorrente.LibriInVendita.Values);
            else
                IMieiLibri = new ObservableCollection<Libro>();
        }

        public void EliminaLibro()
        {
            Task.Run(() =>
            {
                IMieiLibri = new ObservableCollection<Libro>(utenteCorrente.LibriInVendita.Values);
            });
        }

    }
}
