using System;
using BetoApp.Models;
using BetoApp.Pages;
using BetoApp.Helpers;
using Xamarin.Forms;

namespace BetoApp.ViewModels
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

        private bool _busy;
        public bool Busy
        {
            get => _busy;
            set
            {
                _busy = value;
                NotifyPropertyChanged();
                SetFeelingCommand.ChangeCanExecute();
                OpenCalendarCommand.ChangeCanExecute();
            }
        }

        private FeelingsViewModel _feelings;
        public FeelingsViewModel Feelings
        {
            get => _feelings;
            set
            {
                _feelings = value;
                NotifyPropertyChanged();
            }
        }

        private CalendarViewModel _calendar;
        public CalendarViewModel Calendar
        {
            get => _calendar;
            set
            {
                _calendar = value;
                NotifyPropertyChanged();
            }
        }

        public MainViewModel()
        {
            LogOutCommand = new Command(DoLogOut);
            SetFeelingCommand = new Command(SetFeeling, (feeling) => !Busy);
            OpenCalendarCommand = new Command(OpenCalendar, () => !Busy);
        }

        public Command LogOutCommand { get; }
        private async void DoLogOut()
        {
            var confirm = await AlertHelper.ShowLogOutConfirmAlert();
            if (!confirm) { return; }

            CurrentUser.LogOut();

            var navigationPage = (NavigationPage)Application.Current.MainPage;
            navigationPage.Navigation.InsertPageBefore(new SignInPage(), navigationPage.CurrentPage);
            await navigationPage.Navigation.PopAsync();
        }

        public Command OpenCalendarCommand { get; }
        private async void OpenCalendar()
        {
            Busy = true;
            FreeResources();
            Calendar = new CalendarViewModel()
            {
                CurrentUser = CurrentUser
            };
            await Application.Current.MainPage.Navigation.PushAsync(new CalendarPage());
            Busy = false;
        }

        public Command SetFeelingCommand { get; }
        private async void SetFeeling(object feeling)
        {
            Busy = true;
            FreeResources();
            Feelings = new FeelingsViewModel()
            {
                CurrentUser = CurrentUser,
                Feeling = feeling as string
            };
            await Application.Current.MainPage.Navigation.PushAsync(new FeelingsPage());
            Busy = false;
        }

        public void LoadUser()
        {
            CurrentUser = User.Load();
        }

        private void FreeResources()
        {
            Calendar = null;
            Feelings = null;
        }
    }
}
