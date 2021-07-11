using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Npgsql;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Scholae
{
    public partial class App : Application
    {
        public static string email;
        public static string token;

        public App()
        {
            InitializeComponent();
            _ = GetCurrentUser();
            MainPage = email != null ? new NavigationPage(new TabbedHomePage()) : new NavigationPage(new LoginPage());
        }

        private async Task GetCurrentUser()
        {
            try
            {
                email = await SecureStorage.GetAsync("email");
                token = await SecureStorage.GetAsync("accessToken");
                Debug.WriteLine($"\n\nAPP IN APERTURA: token in storage {token}\n\n");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
