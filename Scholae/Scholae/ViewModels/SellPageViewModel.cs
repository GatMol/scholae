using MvvmHelpers.Commands;
using Scholae.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Scholae.ViewModels
{
    public class SellPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Session sessioneCorrente;
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
            sessioneCorrente = Session.GetSession();
            utenteCorrente = sessioneCorrente.UtenteCorrente;
        }

        public bool VendiLibro()
        {
            //TODO: salva in locale quello che prendi dal SS
            //string email = App.email;
            //Utente u = APIConnector.GetUtentePerEmail(email);
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
                Debug.WriteLine($"\nSellPVM aggiungo il libro in vendita alla lista nell'utente corrente\n");
                utenteCorrente.AddLibroInVendita(Libro);
                Debug.WriteLine($"\nSellPVM libriInVendita dell' UC: \n");
                foreach (Libro l in utenteCorrente.LibriInVendita)
                {
                    Debug.WriteLine($"\n {l} \n");
                }
                return response.IsSuccessful;
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
            foreach(Materia m in materie) {
                nM.Add(m.Nome);
            }
            return nM;
        }
    }
}
