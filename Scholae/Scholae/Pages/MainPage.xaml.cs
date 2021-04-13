using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Amazon;
using Amazon.CognitoIdentityProvider.Model;
using System.Diagnostics;
using Amazon.Extensions.CognitoAuthentication;

namespace Scholae
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            Device.SetFlags(new string[] { "Shapes_Experimental" });
            InitializeComponent();
            BindingContext = this;
        }

      
        async void OnRegistrationPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrazione());
        }

        

        /*DA VEDERE: 
         * - Tasto per recuperare passwd
         * - Bottone rememberMe (obsoleto)
         */
        //private async void ForgotPasswordTapped(object sender, EventArgs e)
        //{
        //    await Browser.OpenAsync("http://xamarin.com", BrowserLaunchMode.SystemPreferred);
        //}

        //public bool RememberMe
        //{
        //    get => Preferences.Get(nameof(RememberMe), false);
        //    set 
        //    {
        //        Preferences.Set(nameof(RememberMe), value);
        //        if (!value)
        //        {
        //            Preferences.Set(nameof(Email), string.Empty);
        //        }
        //        OnPropertyChanged(nameof(RememberMe));
        //    }
        //}

        /*IDEA DI LOG-IN:
         * Accedo subito se già registrato e ho dati nel secure storage e l'email nelle preferences, 
         * da registrare altrimenti (torno alla MainPage e aspetto verifica via email) e inserisco credenziali.
         */
        string email = Preferences.Get(nameof(Email), string.Empty);
        public string Email
        {
            get => email;
            set
            {
                email = value;
                //if (RememberMe)
                Preferences.Set(nameof(Email), value);
                //OnPropertyChanged(nameof(RememberMe));
            }
        }

        

        async void LoginClicked(object sender, EventArgs e)
        {
            var username = EmailEntry.Text;
            var password = PasswordEntry.Text;
            
            var isValid = true;

            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error login", "Email e/o password non validi", "Ok");
                isValid = false;
            }

            if (isValid)
            {
                try
                {
                    await AuthenticateUser(username, password);
                    Navigation.InsertPageBefore(new TabbedHomePage(), this);
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    LoginErrorHandler(ex);
                    PasswordEntry.Text = string.Empty;
                }
            }
        }

        private async Task AuthenticateUser(string username, string password)
        {
            
        }

        private void LoginErrorHandler(Exception ex)
        {
            Debug.WriteLine(ex.Message);
            DisplayAlert("Login error", ex.Message, "Close");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            //Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;

            try
            {
                EmailEntry.Text = email;
                var password = await SecureStorage.GetAsync("token");
                PasswordEntry.Text = password;
                /*TODO: controllo account nel database, per accedere subito*/
                //if (inDatabase(email,password)
                //{
                //    await Navigation.PushAsync(new TabbedHomePage());
                //}
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }
    }
}
