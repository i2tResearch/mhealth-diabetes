using System;
using BetoApp.Infrastructure;
using Xamarin.Forms;

namespace BetoApp.Helpers
{
    public class ResourceHelper
    {
        public static InstanceLocator Locator
        {
            get
            {
                return Application.Current.Resources["Locator"] as InstanceLocator;
            }
        }
    }
}
