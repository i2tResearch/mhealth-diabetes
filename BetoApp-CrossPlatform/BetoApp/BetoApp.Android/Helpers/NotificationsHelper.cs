using System;
using Plugin.CurrentActivity;
using Java.Util;
using Android.Content;
using Android.App;
using BetoApp.Models;
using BetoApp.Droid;
using Android.OS;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace BetoApp.Helpers
{
    public partial class NotificationsHelper
    {
        static partial void PlatformProgramNotification(Alarm alarm)
        {
            var context = CrossCurrentActivity.Current.AppContext;
            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

            Calendar cal = Calendar.Instance;
            cal.Set(CalendarField.HourOfDay, alarm.Hour.Hours);
            cal.Set(CalendarField.Minute, alarm.Hour.Minutes);
            cal.Set(CalendarField.Second, 0);

            Bundle extras = new Bundle();
            extras.PutString("alarm", JsonConvert.SerializeObject(alarm));
            var intent = new Intent(context, typeof(BetoAlarmBroadcastReceiver));
            intent.PutExtras(extras);

            var alarmIntent = PendingIntent.GetBroadcast(context, alarm.NotificationId, intent, PendingIntentFlags.UpdateCurrent);
            //alarmManager.SetRepeating(AlarmType.RtcWakeup, cal.TimeInMillis, AlarmManager.IntervalDay, alarmIntent);
            alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, cal.TimeInMillis, alarmIntent);
        }

        static partial void PlatformRemoveNotification(Alarm alarm)
        {
            var context = CrossCurrentActivity.Current.AppContext;
            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

            var intent = new Intent(context, typeof(BetoAlarmBroadcastReceiver));
            var alarmIntent = PendingIntent.GetBroadcast(context, alarm.NotificationId, intent, PendingIntentFlags.UpdateCurrent);
            alarmManager.Cancel(alarmIntent);
            alarmIntent.Cancel();
        }

        static partial void PlatformUpdateNotification(Alarm alarm)
        {
            PlatformProgramNotification(alarm);
        }

        public static void SetNotificationsAfterReboot(User user)
        {
            foreach (var alarm in user.Alarms)
            {
                PlatformProgramNotification(alarm);
            }
        }

        public static void RescheduleNotification(Alarm alarm)
        {
            var context = CrossCurrentActivity.Current.AppContext;
            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

            Calendar cal = Calendar.Instance;
            cal.Set(CalendarField.HourOfDay, alarm.Hour.Hours);
            cal.Set(CalendarField.Minute, alarm.Hour.Minutes);
            cal.Set(CalendarField.Second, 0);
            cal.Add(CalendarField.HourOfDay, 24); // Tomorrow

            Bundle extras = new Bundle();
            extras.PutString("alarm", JsonConvert.SerializeObject(alarm));
            var intent = new Intent(context, typeof(BetoAlarmBroadcastReceiver));
            intent.PutExtras(extras);

            var alarmIntent = PendingIntent.GetBroadcast(context, alarm.NotificationId, intent, PendingIntentFlags.UpdateCurrent);
            alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, cal.TimeInMillis, alarmIntent);
        }
    }
}
