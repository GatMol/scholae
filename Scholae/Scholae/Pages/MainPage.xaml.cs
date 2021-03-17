using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Amazon.CognitoIdentity;
using Amazon;

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

        //async void OnRegistrationPage(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new Registrazione());
        //}
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

        

        async void Login(object sender, EventArgs e)
        {
            
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("No internet", "", "Ok");
                return;
            }

            /*E' VALIDO SE NON SONO VUOTI E SE E' PRESENTE NEL DATABASE*/
            /*TODO : CONTROLLO PRESENZA ACCOUNT NEL DATABASE*/
            var isValid = true;

            if(string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await DisplayAlert("Error login", "Email e/o password non validi", "Ok");
                isValid = false;
            }

            //if (string.IsNullOrEmpty(EmailEntry.Text))
            //{
            //    VisualStateManager.GoToState(EmailEntry, "Invalid");
            //    isValid = false;
            //}

            //else
            //{
            //  VisualStateManager.GoToState(EmailEntry, "valid");
            //}

            //if (string.IsNullOrEmpty(PasswordEntry.Text))
            //{
            //    //VisualStateManager.GoToState(PasswordEntry, "Invalid");
            //    isValid = false;
            //}

            /*...*/
            //var service = DependencyService.Get<IStatusBar>();
            //service?.SetStatusBarColor(isValid ? Color.Green : Color.Red);
            /*...*/

            if (isValid)
            {
                try
                {
                    await SecureStorage.SetAsync("token", PasswordEntry.Text);
                }
                catch (Exception ex)
                {
                    /*device does not support secure storage?*/
                }
                //await DisplayAlert("Login succeded", "", "Grazie!");
                //await Clipboard.SetTextAsync("1234");
                await Navigation.PushAsync(new TabbedHomePage());
            }
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

            //Accelerometer.Start(SensorSpeed.Game);
        }

        //private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        //{
        //    MainThread.BeginInvokeOnMainThread(() =>
        //    {
        //        PasswordEntry.Text = string.Empty;
        //        EmailEntry.Text = string.Empty;
        //    });
        //}

        //private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        //{
        //    if (e.NetworkAccess == NetworkAccess.Internet)
        //    {
        //        LabelConnection.FadeTo(0).ContinueWith((result) => { });
        //    }
        //    else
        //    {
        //        LabelConnection.FadeTo(1).ContinueWith((result) => { });
        //    }
        //}

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    //Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        //    //Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
        //    //Accelerometer.Stop();
        //}
    }
}
