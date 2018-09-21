using System;
using System.Collections.Generic;
using BetoApp.Models;
using BetoApp.ViewModels;

namespace BetoApp.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public SignInViewModel SignIn
        {
            get => new SignInViewModel();
        }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
