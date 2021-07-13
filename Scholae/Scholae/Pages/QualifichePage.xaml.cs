
using Xamarin.Forms;

namespace Scholae.Pages
{
    public partial class QualifichePage : ContentPage
    {
        public QualifichePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        void Back_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
