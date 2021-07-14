
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Scholae
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedHomePage : Xamarin.Forms.TabbedPage
    {
        public TabbedHomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
