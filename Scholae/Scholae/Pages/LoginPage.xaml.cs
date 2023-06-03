using Scholae.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Scholae
{
    public partial class LoginPage : ContentPage
    {
        //public static string Email;
        public Utente UtenteCorrente { get; set; }

        public LoginPage()
        {
            Device.SetFlags(new string[] { "Shapes_Experimental" });
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = this;
        }

        async void OnRegistrationPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrazione());
        }

        void LoginClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;
            var isValid = true;
            loading.IsVisible = true;
            loading.IsRunning = true;

            
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    isValid = false;
                    Device.BeginInvokeOnMainThread(async () => { loading.IsRunning = false; loading.IsVisible = false;  await DisplayAlert("Error login", "Email e/o password non validi", "Ok");  });
                }
                if (isValid)
                {
                    if (Authentication.AuthenticateUser(email, password).Result)
                    {
                        Navigation.InsertPageBefore(new TabbedHomePage(), this);
                        Device.BeginInvokeOnMainThread(async () => { await Navigation.PopAsync(); loading.IsRunning = false; loading.IsVisible = false; });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => 
                        { 
                            await DisplayAlert("Login error", "", "Try again"); 
                            loading.IsRunning = false;
                            loading.IsVisible = false;
                            PasswordEntry.Text = string.Empty; 
                        });
                    }
                }
            
        }
    }
}
