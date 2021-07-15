using MvvmHelpers;
using Scholae.Pages;
using Scholae.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using BaseViewModel = Scholae.Services.BaseViewModel;

namespace Scholae.ViewModels
{
    public class LibriViewModel : BaseViewModel
    {
        public Utente utenteCorrente;

        public List<Libro> libri { get; set; }

        public string testoSearchBar { get; set; }

        public bool mieiLibri = false;

        private bool visibilitapreferiti;

        private Libro libroDaVisualizzare;

        public Libro LibroDaVisualizzare
        {
            get
            {
                return libroDaVisualizzare;
            }
            set
            {
                libroDaVisualizzare = value;
                OnPropertyChanged();
            }
        }

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
            get
            {
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

        public ICommand OttieniInformazioniLibro => new Command<long>((long id) =>
        {
            OttieniInfoLibro(id);
        });

        public LibriViewModel()
        {
            utenteCorrente = Session.GetSession().UtenteCorrente;
            InitData();
            visibilitanonpreferiti = true;
            //Task.Run(() => Visibilitapreferiti = false);
        }

        private void InitData()
        {
            libri = APIConnector.GetAllLibri(utenteCorrente.Id);
            /*for (int i = 0; i < 10; i++)
            {
            Libro libro = new Libro((long)i, "Nome", "ISBN", "autore", "editore", "edizione", 5);
            libri.Add(libro);
            }*/
            LibriDaMostrare = new ObservableRangeCollection<Libro>(libri ?? new List<Libro>());
        }

        async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
                if (mieiLibri == true)
                {
                    await LibriSalvati();
                }
                else
                {
                if (testoSearchBar == null || testoSearchBar.Equals(""))
                    InitData();
                else
                    PerformSearch.Execute(testoSearchBar);
                }
            IsRefreshing = false;
        }

        public Task CercaPerNome(String nome)
        {
            Debug.WriteLine("\nNome quando entriamo dentro cercaPerNome " + nome);
            libri = APIConnector.GetLibroPerNome(nome, utenteCorrente.Id);
            LibriDaMostrare = new ObservableRangeCollection<Libro>(libri ?? new List<Libro>());
            return Task.CompletedTask;
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
            if (utenteCorrente.LibriSalvati != null)
            {
                LibriDaMostrare = new ObservableRangeCollection<Libro>(utenteCorrente.LibriSalvati.Values.ToList());
            }
            else
            {
                LibriDaMostrare = new ObservableRangeCollection<Libro>(GetLibriSalvatiDb().Values.ToList());
            }
                mieiLibri = true;
            
            visibilitanonpreferiti = false;
            return Task.CompletedTask;
        }

        private Dictionary<long, Libro> GetLibriSalvatiDb()
        {
            List<Libro> libri = APIConnector.GetLibriSalvati(utenteCorrente.Id);
            foreach (Libro l in libri)
            {
                utenteCorrente.AddLibroSalvato(l);
                }
            return utenteCorrente.LibriSalvati;
        }

        private void AggiungiPreferito(long id)
        {
            APIConnector.AddLibroSalvatoAdUtente(id, utenteCorrente.Id);
            utenteCorrente.AddLibroSalvato(APIConnector.GetLibroPerId(id));
        }

        private void EliminaPreferito(long id)
        {
            APIConnector.DeleteLibroSalvatoAdUtente(id, utenteCorrente.Id);
            utenteCorrente.LibriSalvati.Remove(id);
            LibriDaMostrare = new ObservableRangeCollection<Libro>(utenteCorrente.LibriSalvati.Values);
        }

        private void OttieniInfoLibro(long id)
        {
            LibroDaVisualizzare = APIConnector.GetLibroPerId(id);
            (Application.Current.MainPage as NavigationPage).CurrentPage.Navigation.ShowPopup(new PopupBookPage(LibroDaVisualizzare));
        }
    }
}
