using Scholae.Services;
using System;
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
            InitializeComponent();
            BindingContext = this;
        }

        async void OnRegistrationPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrazione());
        }

        async void LoginClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;
            var isValid = true;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error login", "Email e/o password non validi", "Ok");
                isValid = false;
            }

            if (isValid)
            {
                if (await Authentication.AuthenticateUser(email, password))
                {
                    Navigation.InsertPageBefore(new TabbedHomePage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Login error", "", "Try again");
                    PasswordEntry.Text = string.Empty;
                }
            }
        }
    }
}
