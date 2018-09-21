using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BetoApp.Models;
using Xamarin.Forms;
using BetoApp.Pages;
using BetoApp.Helpers;

namespace BetoApp.ViewModels
{
    public class CalendarViewModel : BindableBase
    {
        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                UpdateList();
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Alarm> _alarms;
        public ObservableCollection<Alarm> Alarms
        {
            get => _alarms;
            set
            {
                _alarms = value;
                NotifyPropertyChanged();
            }
        }

        public CalendarViewModel()
        {
            AddAlarmCommand = new Command(AddAlarm);
        }

        public Command AddAlarmCommand { get; }
        private async void AddAlarm()
        {
            var alarmPage = new AddAlarmPage();
            alarmPage.BindingContext = new AddAlarmViewModel() { CurrentUser = CurrentUser };
            await Application.Current.MainPage.Navigation.PushAsync(alarmPage);
        }

        public async void EditAlarm(Alarm alarm)
        {
            var alarmPage = new AddAlarmPage();
            alarmPage.BindingContext = new AddAlarmViewModel(alarm) { CurrentUser = CurrentUser };
            await Application.Current.MainPage.Navigation.PushAsync(alarmPage);
        }

        public void UpdateList()
        {
            Alarms = new ObservableCollection<Alarm>(_currentUser.Alarms);
            _currentUser.Save();
        }
    }
}
