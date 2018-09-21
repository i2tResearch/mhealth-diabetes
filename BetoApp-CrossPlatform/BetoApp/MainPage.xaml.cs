using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BetoApp
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
