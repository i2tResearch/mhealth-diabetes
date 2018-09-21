using System;
using BetoApp.Models;

namespace BetoApp.Helpers
{
    public partial class NotificationsHelper
    {
        public static void ProgramNotification(Alarm alarm)
        {
            PlatformProgramNotification(alarm);
        }

        public static void RemoveNotification(Alarm alarm)
        {
            PlatformRemoveNotification(alarm);
        }

        public static void UpdateNotification(Alarm alarm)
        {
            PlatformUpdateNotification(alarm);
        }

        static partial void PlatformProgramNotification(Alarm alarm);
        static partial void PlatformRemoveNotification(Alarm alarm);
        static partial void PlatformUpdateNotification(Alarm alarm);
    }
}
