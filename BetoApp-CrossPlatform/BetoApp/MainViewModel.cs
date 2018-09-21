using System;
using Xamarin.Forms;
using BetoApp.Base;
using BetoApp.Models;
using BetoApp.SignIn;

namespace BetoApp
{
    public class MainViewModel : BindableBase
    {
        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                NotifyPropertyChanged();
            }
        }

        public MainViewModel()
        {
            LogOutCommand = new Command(DoLogOut);
        }

        public Command LogOutCommand { get; }
        public async void DoLogOut()
        {
            CurrentUser.Delete();

            var navigationPage = (NavigationPage)Application.Current.MainPage;
            navigationPage.Navigation.InsertPageBefore(new SignInPage(), navigationPage.CurrentPage);
            await navigationPage.Navigation.PopAsync();
        }

        public void LoadUser()
        {
            CurrentUser = User.Load();
        }
    }
}
