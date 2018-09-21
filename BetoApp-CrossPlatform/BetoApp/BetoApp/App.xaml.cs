using System;
using BetoApp.Models;
using BetoApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BetoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var user = User.Load();
            if (user == null)
            {
                MainPage = new NavigationPage(new SignInPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
