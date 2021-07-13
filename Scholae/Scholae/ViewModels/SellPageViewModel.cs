using Newtonsoft.Json;
using Scholae.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Scholae.ViewModels
{
    public class SellPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Utente utenteCorrente;
        public Libro Libro { get; set; }
        public Materia Materia { get; set; }
        public byte[] Img { get; set; }
        public string Filename { get; set; }

        public SellPageViewModel()
        {
            Libro = new Libro();
            Materia = new Materia();
            Libro.Materia = Materia;
            utenteCorrente = Session.GetSession().UtenteCorrente;
        }

        public bool VendiLibro()
        {
            Debug.WriteLine("\nVendi libro in SellPVM, UTENTE: ");
            Debug.WriteLine(utenteCorrente.ToString());
            Debug.WriteLine("\n");
            Libro.Utente = utenteCorrente;
            Debug.WriteLine("\nSellPVM creo il libro nel DB: \n");
            Libro libro = APIConnector.CreaLibro(Libro);
            if (libro != null)
            {
                Debug.WriteLine($"\nSellPVM creato il libro nel DB\n");
                Debug.WriteLine($"\nSellPVM setto id del libro in memoria {libro.id}: \n");
                Libro.id = libro.id;
                Debug.WriteLine($"\nSellPVM libro NON NULL\n");
                Debug.WriteLine($"\nSellPVM aggiungoFotoLibro\n");
                var response = APIConnector.AggiungiFotoLibro(Libro.id, Img, Filename);
                if (response.IsSuccessful)
                {
                    Debug.WriteLine($"\nSellPVM aggiungoFotoLibro risposa con successo\n");
                    string pathImmagine = response.Content;
                    Debug.WriteLine($"\nSellPVM path immagine in memoria {pathImmagine}\n");
                    Libro.Immagine = pathImmagine;
                    Debug.WriteLine($"\nSellPVM aggiungo il libro in vendita alla lista nell'utente corrente\n");
                    utenteCorrente.AddLibroInVendita(Libro);
                    Debug.WriteLine($"\nSellPVM libriInVendita dell' UC: \n");
                    foreach (Libro l in utenteCorrente.LibriInVendita)
                    {
                        Debug.WriteLine($"\n {l} \n");
                    }
                    return true;
                }
                Debug.WriteLine($"\nSellPVM cancello libro senza immagine\n");
                APIConnector.DeleteLibro(Libro.id);
                return false;
            }
            Debug.WriteLine($"\nSellPVM libro NULL\n");
            return false;
        }
        public List<string> GetMaterie()
        {
            List<Materia> materie = APIConnector.GetAllMaterie();
            return NomiMaterie(materie);
        }

        private List<string> NomiMaterie(List<Materia> materie)
        {
            List<string> nM = new List<string>();
            foreach (Materia m in materie)
            {
                nM.Add(m.Nome);
            }
            return nM;
        }
    }
}
