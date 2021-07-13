using Scholae.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json;


namespace Scholae
{
    public partial class LoginPage : ContentPage
    {
        public static string Email;

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

            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error login", "Email e/o password non validi", "Ok");
                isValid = false;
            }
            if (isValid)
            {
                try
                {
                    await AuthenticateUser(email, password);
                }
                catch (Exception ex)
                {
                    LoginErrorHandler(ex);
                    PasswordEntry.Text = string.Empty;
                }
            }
        }

        private async Task AuthenticateUser(string email, string password)
        {
            //var authentication = new Authentication(email);
            //var accessToken = await authentication.AuthenticateUser(password);
            //var userAttributes = Authentication.GetUserAttributes(accessToken);
            //TODO: Crea connessione con API e scrivi metodi per user e libri 
            //TokenResponse token = API.GetToken(userAttributes.First().Value);
            //
            //await SecureStorage.SetAsync("accessToken", accessToken);
            //
            var response = APIConnector.Login(email, password);
            if (!response.IsSuccessful)
            {
                await DisplayAlert("Login fallito", "Email e/o password errati", "Riprova");
                PasswordEntry.Text = string.Empty;
            }
            else
            {
                BearerToken token = JsonConvert.DeserializeObject<BearerToken>(response.Content);
                Debug.WriteLine("\n\nLOGINPAGE: loggato e il token e': " + token.AccessToken + "\n\n");
                //await SecureStorage.SetAsync("email", email);
                //await SecureStorage.SetAsync("accessToken", token.AccessToken);
                Email = email;
                Navigation.InsertPageBefore(new TabbedHomePage(), this);
                await Navigation.PopAsync();
            }
        }

        private void LoginErrorHandler(Exception ex)
        {
            Debug.WriteLine(ex.Message);
            DisplayAlert("Login error", ex.Message, "Try again");
        }

        public sealed class BearerToken
        {
            [JsonProperty(propertyName: "token")]
            public string AccessToken { set; get; }
        }
    }
}
