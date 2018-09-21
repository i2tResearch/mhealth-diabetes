package co.edu.icesi.i2t.chugarapp.alarm;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.support.v4.content.WakefulBroadcastReceiver;

import java.util.Calendar;

import co.edu.icesi.i2t.chugarapp.helpers.Config;

/**
 * When the alarm fires, this WakefulBroadcastReceiver receives the broadcast Intent
 * and then starts the IntentService {@code SampleSchedulingService} to do some work.
 */
public class AlarmReceiver extends WakefulBroadcastReceiver {
    // The app's AlarmManager, which provides access to the system alarm services.
    private AlarmManager alarmMgr;
    // The pending intent that is triggered when the alarm fires.
    private PendingIntent alarmIntent;

    @Override
    public void onReceive(Context context, Intent intent) {
        // BEGIN_INCLUDE(alarm_onreceive)
        /*
         * If your receiver intent includes extras that need to be passed along to the
         * service, use setComponent() to indicate that the service should handle the
         * receiver's intent. For example:
         *
         * ComponentName comp = new ComponentName(context.getPackageName(),
         *      MyService.class.getName());
         *
         * // This intent passed in this call will include the wake lock extra as well as
         * // the receiver intent contents.
         * startWakefulService(context, (intent.setComponent(comp)));
         *
         * In this example, we simply create a new intent to deliver to the service.
         * This intent holds an extra identifying the wake lock.
         *
         */

        Bundle extra = intent.getExtras();
        int id = extra.getInt("id");

        Intent service = new Intent(context, SchedulingService.class);
        service.putExtra("id", id);

        // Start the service, keeping the device awake while it is launching.
        startWakefulService(context, service);
        // END_INCLUDE(alarm_onreceive)
    }

    private void alarma(int id, Context context, int hour, int minute) {
        Intent intent;
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(System.currentTimeMillis());
        calendar.set(Calendar.HOUR_OF_DAY, hour);
        calendar.set(Calendar.MINUTE, minute);
        intent = new Intent(context, AlarmReceiver.class);
        intent.putExtra("id", id);
        alarmIntent = PendingIntent.getBroadcast(context, id, intent, PendingIntent.FLAG_UPDATE_CURRENT);
        alarmMgr.setInexactRepeating(AlarmManager.RTC_WAKEUP, calendar.getTimeInMillis(), AlarmManager.INTERVAL_DAY, alarmIntent);
    }

    // BEGIN_INCLUDE(set_alarm)

    /**
     * Sets a repeating alarm that runs once a day depends on the schedule of the user. When the
     * alarm fires, the app broadcasts an Intent to this WakefulBroadcastReceiver.
     *
     * @param context
     */
    public void setAlarm(Context context, int idAlarm) {
        alarmMgr = (AlarmManager) context.getSystemService(Context.ALARM_SERVICE);

        switch (idAlarm) {
            case Config.ALARMA_ESTADO_ANIMO_MANANA:
                alarma(idAlarm, context, 9, 30);
                break;
            case Config.ALARMA_ESTADO_ANIMO_MEDIO_DIA:
                alarma(idAlarm, context, 12, 30);
                break;
            case Config.ALARMA_ESTADO_ANIMO_TARDE:
                alarma(idAlarm, context, 16, 30);
                break;
            case Config.ALARMA_ESTADO_ANIMO_NOCHE:
                alarma(idAlarm, context, 19, 30);
                break;
            case Config.ALARMA_MEDICAMENTO_MANANA:
                alarma(idAlarm, context, 8, 30);
                break;
            case Config.ALARMA_MEDICAMENTO_TARDE:
                alarma(idAlarm, context, 14, 30);
                break;
            case Config.ALARMA_MEDICAMENTO_NOCHE:
                alarma(idAlarm, context, 20, 30);
                break;
        }

        ComponentName receiver = new ComponentName(context, BootReceiver.class);
        PackageManager pm = context.getPackageManager();

        pm.setComponentEnabledSetting(receiver,
                PackageManager.COMPONENT_ENABLED_STATE_ENABLED,
                PackageManager.DONT_KILL_APP);
    }
    // END_INCLUDE(set_alarm)

    /**
     * Cancels the alarm.
     *
     * @param context
     */
    // BEGIN_INCLUDE(cancel_alarm)
    public void cancelAlarm(Context context, int idAlarm) {
        // If the alarm has been set, cancel it.
        if (alarmMgr != null) {
            alarmMgr.cancel(alarmIntent);
        }

        // Disable {@code SampleBootReceiver} so that it doesn't automatically restart the 
        // alarm when the device is rebooted.
        ComponentName receiver = new ComponentName(context, BootReceiver.class);
        PackageManager pm = context.getPackageManager();

        pm.setComponentEnabledSetting(receiver,
                PackageManager.COMPONENT_ENABLED_STATE_DISABLED,
                PackageManager.DONT_KILL_APP);
    }
    // END_INCLUDE(cancel_alarm)
}
