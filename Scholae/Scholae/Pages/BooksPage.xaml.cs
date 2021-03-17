using System;
using System.Collections.Generic;
using Npgsql;
using Scholae.ViewModels;
using Xamarin.Forms;

namespace Scholae
{
    public partial class BooksPage : ContentPage
    {

        public List<Libro> allLibri { get; set; }

        public BooksPage()
        {
            InitializeComponent();
        }

        async void HomeButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /*private async System.Threading.Tasks.Task InitDataAsync()
        {

            var connString = "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();


            using (var cmd = new NpgsqlCommand("SELECT (@p) FROM Libro", conn))
            {
                allLibri = new List<Libro>();
                for (var i = 1; i <= 1000; i++)
                {
                    allLibri.Add(new Libro
                    {

                    });
                }
            }
        }*/
    }
}
