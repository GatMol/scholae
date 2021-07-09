using Scholae.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace Scholae
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellPhotoPage : ContentPage
    {
        public SellPhotoPage()
        {
            InitializeComponent();
        }
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Fai una foto"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }
        async void Button1_Clicked(System.Object sender, System.EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        async void SalvaFoto(object sender, EventArgs e)
        {
            //TODO: Prendo immagine dal picker e la mando all APIConnector e pusho nuova pagina
            Debug.WriteLine($"SOURCE: {resultImage.Source}");
            APIConnector.SalvaImmagine(resultImage.Source);
            //var stream = File.ReadAllBytes(resultImage.Source.ToString());
            //APIConnector.SalvaImmagine(stream);
            await Navigation.PopAsync();
        }
    }
}