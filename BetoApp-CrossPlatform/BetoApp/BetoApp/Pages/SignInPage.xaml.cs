using System;
using System.Collections.Generic;
using BetoApp.ViewModels;
using Xamarin.Forms;

namespace BetoApp.Pages
{
    public partial class SignInPage : ContentPage
    {
        private SignInViewModel context;

        public SignInPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            context = (SignInViewModel)BindingContext;
        }

        protected override bool OnBackButtonPressed()
        {
            var defaultBack = context.GoBack();
            if (defaultBack)
            {
                return base.OnBackButtonPressed();
            }

            return true;
        }
    }
}
