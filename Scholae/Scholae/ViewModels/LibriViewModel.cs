using MvvmHelpers;
using Scholae.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BaseViewModel = Scholae.Services.BaseViewModel;

namespace Scholae.ViewModels
{
    public class LibriViewModel : BaseViewModel
    {
        public Session sessioneCorrente;
        public List<Libro> libri { get; set; }

        public string testoSearchBar { get; set; }

        public bool mieiLibri = false;

        private bool visibilitapreferiti;

        public bool visibilitanonpreferiti
        {
            get
            {
                return visibilitapreferiti;
            }
            set
            {
                visibilitapreferiti = value;
                OnPropertyChanged();
            }
        }

        public ObservableRangeCollection<Libro> LibriDaMostrare
        {
            get {
                return libriDaMostrare;
            }
            set
            {
                libriDaMostrare = value;
                OnPropertyChanged();
            }
        }


        private ObservableRangeCollection<Libro> libriDaMostrare;

        bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        const int RefreshDuration = 1;

        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());

        public ICommand AllLibri => new Command(async () => await TuttiLibri());

        public ICommand LibriSaved => new Command(async () => await LibriSalvati());

        public ICommand PerformSearch => new Command<string>((string nome) =>
        {
            CercaPerNome(nome);
        });

        public ICommand RendiPreferito => new Command<long>((long id) =>
        {
            AggiungiPreferito(id);
        });

        public ICommand RendiNonPreferito => new Command<long>((long id) =>
        {
            EliminaPreferito(id);
        });

        public LibriViewModel()
        {
            InitData();
            visibilitanonpreferiti = true;
            //Task.Run(() => Visibilitapreferiti = false);
        }
            sessioneCorrente = Session.GetSession();
    }

        private void InitData()
        {
            Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            libri = APIConnector.GetAllLibri(utente.Id);
            /*for (int i = 0; i < 10; i++)
            {
                Libro libro = new Libro((long)i, "Nome", "ISBN", "autore", "editore", "edizione", 5);
                libri.Add(libro);
            }*/
            LibriDaMostrare = new ObservableRangeCollection<Libro>(libri.ToList());
            foreach(Libro libro in libri) {
                Debug.WriteLine(libro.ToString());
            }
        }

        async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
            if (mieiLibri == true) await LibriSalvati();
            else
            {
                if (testoSearchBar == null || testoSearchBar.Equals(""))
                    InitData();
                else
                    PerformSearch.Execute(testoSearchBar);
            }
            IsRefreshing = false;
        }

        public void CercaPerNome(String nome)
        {
            Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            libri = APIConnector.GetLibroPerNome(nome, utente.Id);
            LibriDaMostrare = new ObservableRangeCollection<Libro>(libri ?? new List<Libro>());
        }

        public Task TuttiLibri()
        {
            mieiLibri = false;
            InitData();
            visibilitanonpreferiti = true;
            return Task.CompletedTask;
        }

        public Task LibriSalvati()
        {
            mieiLibri = true;
            Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            List<Libro> libriSalvati = APIConnector.GetLibriSalvati(utente.Id);
            LibriDaMostrare = new ObservableRangeCollection<Libro>(libriSalvati.ToList());
            visibilitanonpreferiti = false;
            //Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            List<Libro> libriSalvati = APIConnector.GetLibriSalvati(sessioneCorrente.UtenteCorrente.Id);
            //TODO: Mettere i libriSalvati in memoria nell'utenteCorrente
            LibriDaMostrare = new ObservableRangeCollection<Libro>(libriSalvati.Take(10).ToList());
            mieiLibri = true;
            return Task.CompletedTask;
        }

        private void AggiungiPreferito(long id)
        {
            //Utente utente = APIConnector.GetUtentePerEmail(SecureStorage.GetAsync("email").Result);
            //Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            APIConnector.AddLibroSalvatoAdUtente(id, sessioneCorrente.UtenteCorrente.Id);
            //TODO: Aggiungere libroSalvato in memoria
        }

        private void EliminaPreferito(long id)
        {
            //Utente utente = APIConnector.GetUtentePerEmail(SecureStorage.GetAsync("email").Result);
            //Utente utente = APIConnector.GetUtentePerEmail(LoginPage.Email);
            APIConnector.DeleteLibroSalvatoAdUtente(id, sessioneCorrente.UtenteCorrente.Id);
            //TODO: Rimuovere libroSalvato in memoria
        }

        public static bool Salvato(long id_libro)
        {
            List<LibroSalvato> boo = APIConnector.GetLibroSalvato(id_libro, id_utente: APIConnector.GetUtentePerEmail(LoginPage.Email).Id);
            return boo != null && boo.Count > 0;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
