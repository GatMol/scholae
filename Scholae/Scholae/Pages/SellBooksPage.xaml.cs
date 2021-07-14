using Scholae.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scholae
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellBooksPage : ContentPage
    {
        public SellPageViewModel sbVM;
        public SellBooksPage()
        {
            sbVM = new SellPageViewModel();
            InitializeComponent();
            BindingContext = sbVM;
            List<string> materie = sbVM.GetMaterie();
            MainPicker.ItemsSource = materie;
        }

        private void MainPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = MainPicker.Items[MainPicker.SelectedIndex];
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            SellPhotoPage spPage = new SellPhotoPage()
            {
                spPage = sbVM,
                BindingContext = sbVM
            };
            await Navigation.PushAsync(spPage);
        }
    }
}