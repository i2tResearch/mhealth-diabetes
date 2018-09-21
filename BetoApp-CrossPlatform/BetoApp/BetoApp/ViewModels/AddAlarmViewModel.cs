using System;
using System.Collections.Generic;
using Xamarin.Forms;
using BetoApp.Models;
using BetoApp.Helpers;

namespace BetoApp.ViewModels
{
    public class AddAlarmViewModel : BindableBase
    {
        private static Random randomInstance = new Random();

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

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value?.ToUpper();
                NotifyPropertyChanged();
            }
        }

        private string _items;
        public string Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyPropertyChanged();
            }
        }

        private TimeSpan _hour;
        public TimeSpan Hour
        {
            get => _hour;
            set
            {
                _hour = value;
                NotifyPropertyChanged();
            }
        }

        private bool _monday;
        public bool Monday
        {
            get => _monday;
            set
            {
                _monday = value;
                NotifyPropertyChanged();
            }
        }

        private bool _tuesday;
        public bool Tuesday
        {
            get => _tuesday;
            set
            {
                _tuesday = value;
                NotifyPropertyChanged();
            }
        }

        private bool _wednesday;
        public bool Wednesday
        {
            get => _wednesday;
            set
            {
                _wednesday = value;
                NotifyPropertyChanged();
            }
        }

        private bool _thursday;
        public bool Thursday
        {
            get => _thursday;
            set
            {
                _thursday = value;
                NotifyPropertyChanged();
            }
        }

        private bool _friday;
        public bool Friday
        {
            get => _friday;
            set
            {
                _friday = value;
                NotifyPropertyChanged();
            }
        }

        private bool _saturday;
        public bool Saturday
        {
            get => _saturday;
            set
            {
                _saturday = value;
                NotifyPropertyChanged();
            }
        }

        private bool _sunday;
        public bool Sunday
        {
            get => _sunday;
            set
            {
                _sunday = value;
                NotifyPropertyChanged();
            }
        }

        private bool _editMode;
        public bool EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                NotifyPropertyChanged();
            }
        }

        private Alarm _currentAlarm;

        public AddAlarmViewModel(Alarm alarm = null)
        {
            SetDayCommand = new Command((d) => SetDay(d as string));
            AddPackageCommand = new Command(AddPackage);
            DeletePackageCommand = new Command(DeletePackage);

            if (alarm != null)
            {
                EditMode = true;
                _currentAlarm = alarm;
                Name = _currentAlarm.Id;
                Items = _currentAlarm.Items;
                Hour = _currentAlarm.Hour;
                Monday = _currentAlarm.Monday;
                Tuesday = _currentAlarm.Tuesday;
                Wednesday = _currentAlarm.Wednesday;
                Thursday = _currentAlarm.Thursday;
                Friday = _currentAlarm.Friday;
                Saturday = _currentAlarm.Saturday;
                Sunday = _currentAlarm.Sunday;
            }
            else
            {
                _currentAlarm = new Alarm()
                {
                    NotificationId = randomInstance.Next(1, 100)
                };
            }
        }

        public Command SetDayCommand { get; }
        private void SetDay(string day)
        {
            switch (day)
            {
                case "MO":
                    Monday = !Monday;
                    break;
                case "TU":
                    Tuesday = !Tuesday;
                    break;
                case "WE":
                    Wednesday = !Wednesday;
                    break;
                case "TH":
                    Thursday = !Thursday;
                    break;
                case "FR":
                    Friday = !Friday;
                    break;
                case "SA":
                    Saturday = !Saturday;
                    break;
                case "SU":
                    Sunday = !Sunday;
                    break;
            }
        }

        public Command AddPackageCommand { get; }
        private async void AddPackage()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
               string.IsNullOrWhiteSpace(Items) ||
               !(Monday || Tuesday || Wednesday || Thursday || Friday || Saturday || Sunday))
            {
                return;
            }

            _currentAlarm.Id = Name.Trim();
            _currentAlarm.Items = Items;
            _currentAlarm.Hour = Hour;
            _currentAlarm.Monday = Monday;
            _currentAlarm.Tuesday = Tuesday;
            _currentAlarm.Wednesday = Wednesday;
            _currentAlarm.Thursday = Thursday;
            _currentAlarm.Friday = Friday;
            _currentAlarm.Saturday = Saturday;
            _currentAlarm.Sunday = Sunday;

            if (EditMode)
            {
                NotificationsHelper.UpdateNotification(_currentAlarm);
            }
            else
            {
                CurrentUser.Alarms.Add(_currentAlarm);
                NotificationsHelper.ProgramNotification(_currentAlarm);
            }

            CurrentUser.Save();
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public Command DeletePackageCommand { get; }
        private async void DeletePackage()
        {
            var confirm = await AlertHelper.ShowDeleteConfirmAlert();
            if (!confirm) { return; }

            NotificationsHelper.RemoveNotification(_currentAlarm);
            CurrentUser.Alarms.Remove(_currentAlarm);
            CurrentUser.Save();
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
