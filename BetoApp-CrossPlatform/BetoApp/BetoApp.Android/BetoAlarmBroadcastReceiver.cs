using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using BetoApp.Helpers;
using BetoApp.Models;
using Newtonsoft.Json;
using Plugin.CurrentActivity;

namespace BetoApp.Droid
{
    // From https://github.com/aritchie/notifications

    [BroadcastReceiver]
    public class BetoAlarmBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var strAlarm = intent.GetStringExtra("alarm");
            var alarm = JsonConvert.DeserializeObject<Alarm>(strAlarm);

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    if (alarm.Monday)
                    {
                        SendNotification(alarm);
                    }
                    break;
                case DayOfWeek.Tuesday:
                    if (alarm.Tuesday)
                    {
                        SendNotification(alarm);
                    }
                    break;
                case DayOfWeek.Wednesday:
                    if (alarm.Wednesday)
                    {
                        SendNotification(alarm);
                    }
                    break;
                case DayOfWeek.Thursday:
                    if (alarm.Thursday)
                    {
                        SendNotification(alarm);
                    }
                    break;
                case DayOfWeek.Friday:
                    if (alarm.Friday)
                    {
                        SendNotification(alarm);
                    }
                    break;
                case DayOfWeek.Saturday:
                    if (alarm.Saturday)
                    {
                        SendNotification(alarm);
                    }
                    break;
                case DayOfWeek.Sunday:
                    if (alarm.Sunday)
                    {
                        SendNotification(alarm);
                    }
                    break;
            }

            NotificationsHelper.RescheduleNotification(alarm);
        }

        private void SendNotification(Alarm alarm)
        {
            var context = CrossCurrentActivity.Current.AppContext;
            var notification = GetBuilder(alarm, context).Build();
            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.Notify(alarm.NotificationId, notification);
        }

        private NotificationCompat.Builder GetBuilder(Alarm alarm, Context context)
        {
            var bigContent = alarm.Id + ":\n" + alarm.Items;
            var content = alarm.Id + ": " + alarm.Items;

            NotificationCompat.BigTextStyle textStyle = new NotificationCompat.BigTextStyle();
            textStyle.BigText(bigContent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Ringtone))
                .SetSmallIcon(Resource.Drawable.betonotificationtr)
                .SetContentTitle("Hora del medicamento " + alarm.Hour.ToString("hh':'mm"))
                .SetContentText(content)
                .SetStyle(textStyle);

            return builder;
        }
    }
}
