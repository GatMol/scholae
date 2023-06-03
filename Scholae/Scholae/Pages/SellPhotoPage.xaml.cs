﻿using Scholae.ViewModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scholae
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellPhotoPage : ContentPage
    {
        public SellPageViewModel spPage { get; set; }
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public SellPhotoPage()
        {
            InitializeComponent();
        }
        async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Fai una foto"

            });

            if (result != null)
            {
                await Task.Run(async () =>
                {
                    var stream = await result.OpenReadAsync();

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        resultImage.Source = ImageSource.FromFile(result.FullPath);
                        vendiLibro.IsVisible = true;
                    });
                    spPage.Img = ReadFully(stream);
                    stream.Position = 0;
                    spPage.Filename = result.FileName;
                });
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static void SaveStreamAsFile(string filePath, Stream inputStream, string fileName)
        {
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }

            string path = Path.Combine(filePath, fileName);
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                inputStream.CopyTo(outputFileStream);
            }
        }

        async void Button1_Clicked(System.Object sender, System.EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                string nf = DateTime.Now.GetHashCode().ToString() + "jpg";
                SaveStreamAsFile(folderPath, stream, nf);
                resultImage.Source = ImageSource.FromFile(folderPath + "/" + nf);
                vendiLibro.IsVisible = true;
            }
        }

        void MettiLibroInVendita(object sender, EventArgs e)
        {
            Debug.WriteLine(spPage != null ? spPage.ToString() : "Nullo");
            loading.IsVisible = true;
            loading.IsRunning = true;
            Task.Run(() =>
            {
                if (spPage.VendiLibro())
                {
                    Debug.WriteLine("\nSellPhotoP.cs : Ho messo in vendita il libro");
                    Device.BeginInvokeOnMainThread(async () => { await Navigation.PopToRootAsync(); loading.IsRunning = false; loading.IsVisible = false; });
                }
                else
                    Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Errore", "Riprova", "Ok"); loading.IsRunning = false; loading.IsVisible = false; });
            });
        }

    }
}