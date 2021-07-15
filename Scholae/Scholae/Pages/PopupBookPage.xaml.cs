using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scholae.ViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scholae.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupBookPage : Popup
    {
        public Libro libroDaVisualizzare;

        public PopupBookPage(Libro libroDaVisualizzare)
        {
            InitializeComponent();
            Debug.WriteLine("\nLibro nel costruttore: " + libroDaVisualizzare.ToString());
            BindingContext = libroDaVisualizzare;
        }
    }
}