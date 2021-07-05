using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scholae.ViewModels
{
    public class LibriViewModels
    {

        public List<Libro> libri { get; set; }

        public ObservableRangeCollection<Libro> LibriDaMostrare
        {
            get;
            set;
        }


        private ObservableRangeCollection<Libro> libriDaMostrare;

        public LibriViewModels()
        {

            InitData();
        }

        private void InitData()
        {
            libri = new List<Libro>();
            for (int i = 0; i < 10; i++)
            {
                Libro libro = new Libro((long)i, "Nome", "ISBN", "autore", "editore", "edizione", 5);
                libri.Add(libro);
            }
            LibriDaMostrare = new ObservableRangeCollection<Libro>(libri.Take(10).ToList());
        }
    }
}
