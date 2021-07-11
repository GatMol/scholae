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

        public Libro Libro { get; set; }
        public Materia Materia { get; set; }
        public byte[] Img { get; set; }
        public string Filename { get; set; }

        public SellPageViewModel()
        {
            Libro = new Libro();
            Materia = new Materia();
            Libro.Materia = Materia;
        }

        public bool VendiLibro()
        {
            //TODO: salva in locale quello che prendi dal SS
            string email = App.email;
            Utente u = APIConnector.GetUtentePerEmail(email);
            Debug.WriteLine("\n\nVENDILIBRO NEL VM, UTENTE: ");
            Debug.WriteLine(u.ToString());
            Libro.Utente = u;
            Libro libro = APIConnector.CreaLibro(Libro);
            if (libro != null)
            {
                var response = APIConnector.AggiungiFotoLibro(libro.id, Img, Filename);
                return response.IsSuccessful;
            }
            return false;
        }
        public List<string> GetMaterie()
        {
            List<Materia> materie = APIConnector.GetAllMaterie();
            return nomiMaterie(materie);
        }

        private List<string> nomiMaterie(List<Materia> materie)
        {
            List<string> nM = new List<string>();
            foreach(Materia m in materie) {
                nM.Add(m.Nome);
            }
            return nM;
        }
    }
}
