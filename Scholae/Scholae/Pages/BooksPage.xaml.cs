using Scholae.ViewModels;
using Xamarin.Forms;
namespace Scholae

{
    public partial class BooksPage : ContentPage
    {

        public BooksPage()
        {
            InitializeComponent();

            LV.BindingContext = new LibriViewModel();
        }


        async void HomeButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


    }
}
