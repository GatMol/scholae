using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Scholae.Pages
{
    public partial class MateriePage : ContentPage
    {
        public MateriePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        void Back_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

        void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            Console.WriteLine($"ScrollX: {e.ScrollX}, ScrollY: {e.ScrollY}");
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
        }
    }
}
