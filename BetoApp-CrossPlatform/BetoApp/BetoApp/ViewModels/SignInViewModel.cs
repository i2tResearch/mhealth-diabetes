using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetoApp.Models;
using BetoApp.Pages;
using Xamarin.Forms;

namespace BetoApp.ViewModels
{
    public class SignInViewModel : BindableBase
    {
        // Static elements

        public IEnumerable<string> DaysList { get => Enumerable.Range(1, 31).Select(n => n < 10 ? "0" + n.ToString() : n.ToString()).ToList(); }
        public IEnumerable<string> MonthsList { get => Enumerable.Range(1, 12).Select(n => n < 10 ? "0" + n.ToString() : n.ToString()).ToList(); }
        public IEnumerable<string> YearsList { get => Enumerable.Range(DateTime.Now.Year - 149, 150).OrderByDescending(v => v).Select(m => m.ToString()).ToList(); }

        public const string STATE_INIT = "STATE_INIT";
        public const string STATE_ROLE = "STATE_ROLE";
        public const string STATE_BIRTHDAY_AND_GENRE = "STATE_BIRTHDAY_AND_GENRE";
        public const string STATE_CITY = "STATE_CITY";
        public const string STATE_EXERCISE = "STATE_EXERCISE";
        public const string STATE_FINAL = "STATE_FINAL";

        // Properties

        private string _state = STATE_INIT;
        public string State
        {
            get => _state;
            set
            {
                _state = value;
                NotifyPropertyChanged();
            }
        }

        private string _role;
        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                NotifyPropertyChanged();
            }
        }

        private string _day;
        public string Day
        {
            get => _day;
            set
            {
                _day = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("BirthdayIsValid");
            }
        }

        private string _month;
        public string Month
        {
            get => _month;
            set
            {
                _month = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("BirthdayIsValid");
            }
        }

        private string _year;
        public string Year
        {
            get => _year;
            set
            {
                _year = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("BirthdayIsValid");
            }
        }

        public DateTime? Birthday { get; set; }

        public bool BirthdayIsValid
        {
            get
            {
                try
                {
                    Birthday = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day));
                    return true;
                }
                catch (ArgumentNullException)
                {
                    Birthday = null;
                    return true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Birthday = null;
                    return false;
                }
            }
        }

        private string _gender;
        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                NotifyPropertyChanged();
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value?.ToUpper();
                NotifyPropertyChanged();
            }
        }

        private string _neighborhood;
        public string Neighborhood
        {
            get => _neighborhood;
            set
            {
                _neighborhood = value?.ToUpper();
                NotifyPropertyChanged();
            }
        }

        private string _exercise;
        public string Exercise
        {
            get => _exercise;
            set
            {
                _exercise = value;
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

        public SignInViewModel()
        {
            SetRoleCommand = new Command(SetRole);
            SetGenderCommand = new Command(SetGender);
            SetExerciseCommand = new Command(SetExercise);
            NextCommand = new Command(Next);
        }

        // Commands

        public Command SetRoleCommand { get; }
        private void SetRole(object value)
        {
            Role = value as string;
        }

        public Command SetGenderCommand { get; }
        private void SetGender(object value)
        {
            Gender = value as string;
        }

        public Command SetExerciseCommand { get; }
        private void SetExercise(object value)
        {
            Exercise = value as string;
        }

        public Command NextCommand { get; }
        public async void Next()
        {
            switch (State)
            {
                case STATE_INIT:
                    State = STATE_ROLE;

                    break;

                case STATE_ROLE:
                    if (Role != null)
                    {
                        State = STATE_BIRTHDAY_AND_GENRE;
                    }

                    break;

                case STATE_BIRTHDAY_AND_GENRE:
                    if (Birthday != null && Gender != null)
                    {
                        State = STATE_CITY;
                    }

                    break;

                case STATE_CITY:
                    if (!String.IsNullOrWhiteSpace(City) && !String.IsNullOrWhiteSpace(Neighborhood))
                    {
                        State = STATE_EXERCISE;
                    }

                    break;

                case STATE_EXERCISE:
                    if (Exercise != null)
                    {
                        State = STATE_FINAL;
                    }

                    break;

                case STATE_FINAL:
                    if (!String.IsNullOrWhiteSpace(Name))
                    {
                        await FinishSignUp();
                    }

                    break;
            }
        }

        // Others

        public async Task FinishSignUp()
        {
            var user = BuildNewUser();
            user.Save();
            ClearForm();
            State = STATE_INIT;
            await NavigateToMain();
        }

        public User BuildNewUser()
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name.Trim(),
                Role = Role,
                Birthday = Birthday.Value,
                Gender = Gender,
                City = City.Trim(),
                Neighborhood = Neighborhood.Trim(),
                Exercise = Exercise
            };

            return user;
        }

        private void ClearForm()
        {
            Name = null;
            Role = null;
            Day = null;
            Month = null;
            Year = null;
            Birthday = null;
            Gender = null;
            City = null;
            Neighborhood = null;
            Exercise = null;
        }

        public async Task NavigateToMain()
        {
            var navigationPage = (NavigationPage)Application.Current.MainPage;
            navigationPage.Navigation.InsertPageBefore(new MainPage(), navigationPage.CurrentPage);
            await navigationPage.Navigation.PopAsync();
        }

        public bool GoBack()
        {
            switch (State)
            {
                case STATE_INIT:
                    return true;
                case STATE_ROLE:
                    State = STATE_INIT;
                    break;
                case STATE_BIRTHDAY_AND_GENRE:
                    State = STATE_ROLE;
                    break;
                case STATE_CITY:
                    State = STATE_BIRTHDAY_AND_GENRE;
                    break;
                case STATE_EXERCISE:
                    State = STATE_CITY;
                    break;
                case STATE_FINAL:
                    State = STATE_EXERCISE;
                    break;
            }

            return false;
        }
    }
}
