using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scholae
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellBooksPage : ContentPage
    {
        public SellBooksPage()
        {
            InitializeComponent();

            MainPicker.Items.Add("Analisi");
            MainPicker.Items.Add("APS");
            MainPicker.Items.Add("SIW");
        }

        private void MainPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = MainPicker.Items[MainPicker.SelectedIndex];


        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SellPhotoPage());
        }
    }
}