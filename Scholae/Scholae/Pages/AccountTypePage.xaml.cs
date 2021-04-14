using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Scholae.Pages
{
    public partial class AccountTypePage : ContentPage
    {
        public AccountTypePage()
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

        async void tutor_switch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            string action = await DisplayActionSheet("ActionSheet: SavePhoto?", "Cancel", "Delete", "Photo Roll", "Email");
            Debug.WriteLine("Action: " + action);
        }

        void Materie_tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MateriePage());
        }

        void Qualifiche_tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new QualifichePage());
        }
    }
}
