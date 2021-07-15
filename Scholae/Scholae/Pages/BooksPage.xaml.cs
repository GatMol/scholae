using Scholae.Pages;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace Scholae

{
    public partial class BooksPage : ContentPage
    {

        public bool mieiLibri = false;

        public BooksPage()
        {
            InitializeComponent();
        }


        async void RendiPreferito_Clicked(System.Object sender, System.EventArgs e)
        {
            await DisplayAlert("Salvato", "Il libro è stato salvato in 'I miei libri'", "OK");
        }

        async void TogliPreferito_Clicked(System.Object sender, System.EventArgs e)
        {
            await DisplayAlert("Eliminato", "Il libro è stato eliminato da 'I miei libri'", "OK");
        }

        async void Miei_Libri(System.Object sender, System.EventArgs e)
        {
            if (mieiLibri == false)
            {
                await Task.Run(async () =>
                {
                    await sottolineatura.TranslateTo(Width / 2.5, 0, 500, Easing.CubicInOut);
                });
                libri.FontAttributes = FontAttributes.None;
                imieilibri.FontAttributes = FontAttributes.Bold;
                mieiLibri = true;
            }
        }

        async void Libri(object sender, System.EventArgs e)
        {

            if (mieiLibri == true)
            {
                await Task.Run(async () =>
                {
                    await sottolineatura.TranslateTo(0, 0, 500, Easing.CubicInOut);
                });
                libri.FontAttributes = FontAttributes.Bold;
                imieilibri.FontAttributes = FontAttributes.None;
                mieiLibri = false;
            }
        }
    }
}