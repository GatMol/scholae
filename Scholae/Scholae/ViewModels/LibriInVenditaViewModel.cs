using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Scholae.Services;

namespace Scholae.ViewModels
{
    public class LibriInVenditaViewModel: BaseViewModel
    {
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
                OnPropertyChanged();
            }
        }

        public ICommand Elimina => new Command<long>((long id) =>
        {
            EliminaLibro(id);
        });

        public LibriInVenditaViewModel()
        {
            InitData();
        }

        private void InitData()
        {
            Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            libri = APIConnector.tuttiImieiLibri(utente.Id);
            IMieiLibri = new ObservableCollection<Libro>(libri);
        }

        private void EliminaLibro(long id)
        {
            APIConnector.DeleteLibro(id);
            InitData();
        }
    }
}
