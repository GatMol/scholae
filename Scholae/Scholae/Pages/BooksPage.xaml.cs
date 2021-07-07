using System;
using System.Collections.Generic;
using Scholae.ViewModels;
using Xamarin.Forms;
using System.Linq;
using LinqToDB;
using LinqToDB.Common;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Scholae

{
    public partial class BooksPage : ContentPage
    {

        public bool mieiLibri = false;

        public BooksPage()
        {
            InitializeComponent();
        }


        async void HomeButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void SearchBar_SearchButtonPressed(System.Object sender, System.EventArgs e)
        {
        }

        async void Miei_Libri(System.Object sender, System.EventArgs e)
        {
            if (mieiLibri == false)
            {
                await Task.Run(async () =>
                {
                    await sottolineatura.TranslateTo(Width / 2.5, 0, 500, Easing.CubicInOut);
                });
                mieiLibri = true;
            }
        }

        async void Libri(System.Object sender, System.EventArgs e)
        {

            if (mieiLibri == true)
            {
                await Task.Run(async () =>
                {
                    await sottolineatura.TranslateTo(0, 0, 500, Easing.CubicInOut);
                });
                mieiLibri = false;
            }
        }
    }
}