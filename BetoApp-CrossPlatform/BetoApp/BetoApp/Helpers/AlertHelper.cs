using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BetoApp.Helpers
{
    public class AlertHelper
    {
        public static Task<bool> ShowLogOutConfirmAlert()
        {
            return Application.Current.MainPage.DisplayAlert(Constants.TEXT_ALERT, Constants.TEXT_LOGOUT_MESSAGE, Constants.TEXT_LOGOUT, Constants.TEXT_CANCEL);
        }

        public static Task<bool> ShowRecordErrorAlert()
        {
            return Application.Current.MainPage.DisplayAlert(Constants.TEXT_ALERT, Constants.TEXT_ERROR_RECORD, Constants.TEXT_OK, Constants.TEXT_CANCEL);
        }

        public static Task ShowMicRequestAlert()
        {
            return Application.Current.MainPage.DisplayAlert(Constants.TEXT_ALERT, Constants.TEXT_MIC_PERMISSION, Constants.TEXT_OK);
        }

        public static Task ShowMicRequestDeniedAlert()
        {
            return Application.Current.MainPage.DisplayAlert(Constants.TEXT_ALERT, Constants.TEXT_MIC_PERMISSION_DENIED, Constants.TEXT_OK);
        }

        public static Task<bool> ShowDeleteConfirmAlert()
        {
            return Application.Current.MainPage.DisplayAlert(Constants.TEXT_ALERT, Constants.TEXT_DELETE_PACKAGE_MESSAGE, Constants.TEXT_DELETE_PACKAGE, Constants.TEXT_CANCEL);
        }
    }
}
