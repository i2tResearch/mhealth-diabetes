using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using BetoApp.Helpers;

namespace BetoApp.Pages
{
    public partial class FeelingsPage : ContentPage
    {
        public FeelingsPage()
        {
            InitializeComponent();
            CheckPermissions();
        }

        private async void CheckPermissions()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Microphone);

            if (status != PermissionStatus.Granted)
            {
                var shouldRequest = await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Microphone);
                if (shouldRequest)
                {
                    await AlertHelper.ShowMicRequestAlert();
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Microphone);
                if (results.ContainsKey(Permission.Microphone))
                {
                    status = results[Permission.Microphone];
                }
            }
            else if (status == PermissionStatus.Unknown)
            {
                await AlertHelper.ShowMicRequestDeniedAlert();
            }
        }
    }
}
