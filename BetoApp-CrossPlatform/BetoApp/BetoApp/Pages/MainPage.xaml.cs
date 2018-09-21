using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetoApp.ViewModels;
using Xamarin.Forms;

namespace BetoApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel context;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            context = (MainViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            context.LoadUser();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
