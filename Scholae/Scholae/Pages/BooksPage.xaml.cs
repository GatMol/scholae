﻿using System;
using System.Collections.Generic;
using Scholae.ViewModels;
using Xamarin.Forms;
using System.Linq;
using LinqToDB;
using LinqToDB.Common;
using System.Windows.Input;

namespace Scholae

{
    public partial class BooksPage : ContentPage
    {

        public BooksPage()
        {
            InitializeComponent();
        }


        async void HomeButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void SearchBar_SearchButtonPressed(System.Object sender, System.EventArgs e)
        {
        }

    }
}
