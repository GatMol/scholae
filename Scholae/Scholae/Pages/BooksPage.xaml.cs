using System.Threading.Tasks;
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


        async void HomeButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
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

        async void Libri(System.Object sender, System.EventArgs e)
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