using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Scholae
{
    public partial class TutorPage : ContentPage
    {
        public TutorPage()
        {
            InitializeComponent();
        }

        async void Plus_Button_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        async void Book_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
            await Navigation.PushModalAsync(new BooksPage());
        }

        void Home_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
