using System;
using BetoApp.SignIn;

namespace BetoApp.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public SignInViewModel SignIn { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
            SignIn = new SignInViewModel();
        }
    }
}
