using System;
using System.Collections.Generic;
using BetoApp.Models;
using BetoApp.ViewModels;

namespace BetoApp.Infrastructure
{
    public static class InstanceLocatorDesign
    {
        static CalendarViewModel _calendar;
        public static CalendarViewModel Calendar
        {
            get
            {
                if (_calendar == null)
                {
                    _calendar = new CalendarViewModel();

                    var alarm1 = new Alarm()
                    {
                        Id = "Paquete 1",
                        Hour = new TimeSpan(8, 0, 0),
                        Monday = true,
                        Wednesday = true,
                        Friday = true,
                        Items = "Medicamento 2, Medicamento 3",
                    };

                    var alarm2 = new Alarm()
                    {
                        Id = "Paquete 2",
                        Hour = new TimeSpan(12, 30, 0),
                        Tuesday = true,
                        Thursday = true,
                        Saturday = true,
                        Items = "Medicamento 1, Medicamento 2, Medicamento 5"
                    };

                    _calendar.Alarms.Add(alarm1);
                    _calendar.Alarms.Add(alarm2);
                }

                return _calendar;
            }
        }

        static MainViewModel _main;
        public static MainViewModel Main
        {
            get => _main ?? (_main = new MainViewModel() { Calendar = Calendar });
        }
    }
}
