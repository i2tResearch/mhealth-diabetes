using System;
using System.Collections.Generic;
using BetoApp.ViewModels;
using BetoApp.Models;
using Xamarin.Forms;

namespace BetoApp.Pages
{
    public partial class CalendarPage : ContentPage
    {
        private CalendarViewModel context;

        public CalendarPage()
        {
            InitializeComponent();
            context = ((MainViewModel)BindingContext).Calendar;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            context.UpdateList();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            context.EditAlarm(e.Item as Alarm);
        }
    }
}
