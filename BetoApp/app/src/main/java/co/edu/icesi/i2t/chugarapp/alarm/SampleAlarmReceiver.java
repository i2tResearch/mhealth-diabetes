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
public class SampleAlarmReceiver extends WakefulBroadcastReceiver {
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

        Intent service = new Intent(context, SampleSchedulingService.class);
        service.putExtra("id", id);
        
        // Start the service, keeping the device awake while it is launching.
        startWakefulService(context, service);
        // END_INCLUDE(alarm_onreceive)
    }

    private void alarma(int id, Context context, int hour, int minute){
        Intent intent;
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(System.currentTimeMillis());
        calendar.set(Calendar.HOUR_OF_DAY, hour);
        calendar.set(Calendar.MINUTE, minute);
        intent = new Intent(context, SampleAlarmReceiver.class);
        intent.putExtra("id", id);
        alarmIntent = PendingIntent.getBroadcast(context, id, intent, PendingIntent.FLAG_UPDATE_CURRENT);
        alarmMgr.setInexactRepeating(AlarmManager.RTC_WAKEUP, calendar.getTimeInMillis(), AlarmManager.INTERVAL_DAY, alarmIntent);
        //alarmMgr.set(AlarmManager.ELAPSED_REALTIME_WAKEUP, SystemClock.elapsedRealtime() + 60*100, alarmIntent);
    }

    // BEGIN_INCLUDE(set_alarm)
    /**
     * Sets a repeating alarm that runs once a day depends on the schedule of the user. When the
     * alarm fires, the app broadcasts an Intent to this WakefulBroadcastReceiver.
     * @param context
     */
    public void setAlarm(Context context, int idAlarm) {
        alarmMgr = (AlarmManager)context.getSystemService(Context.ALARM_SERVICE);

        switch (idAlarm){
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

        /*Intent intent;
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(System.currentTimeMillis());
        if(idAlarm == Config.ALARMA_ESTADO_ANIMO_MANANA){
            // Set the alarm's trigger time to 12:45 p.m.
            calendar.set(Calendar.HOUR_OF_DAY, 12);
            calendar.set(Calendar.MINUTE, 45);
            intent = new Intent(context, SampleAlarmReceiver.class);
            intent.putExtra("id", Config.ALARMA_ESTADO_ANIMO_MANANA);
            alarmIntent = PendingIntent.getBroadcast(context, Config.ALARMA_ESTADO_ANIMO_MANANA, intent, PendingIntent.FLAG_UPDATE_CURRENT);
            //alarmMgr.setInexactRepeating(AlarmManager.RTC_WAKEUP, calendar.getTimeInMillis(), AlarmManager.INTERVAL_HOUR, alarmIntent);
            alarmMgr.set(AlarmManager.ELAPSED_REALTIME_WAKEUP, SystemClock.elapsedRealtime() + 60*100, alarmIntent);
        }

        if(idAlarm == Config.ALARMA_MEDICAMENTO_MANANA){
            // Set the alarm's trigger time to 8:30 a.m.
            calendar.set(Calendar.HOUR_OF_DAY, 12);
            calendar.set(Calendar.MINUTE, 50);
            intent = new Intent(context, SampleAlarmReceiver.class);
            intent.putExtra("id", Config.ALARMA_MEDICAMENTO_MANANA);
            alarmIntent = PendingIntent.getBroadcast(context, Config.ALARMA_MEDICAMENTO_MANANA, intent, 0);
            //alarmMgr.setInexactRepeating(AlarmManager.RTC_WAKEUP, calendar.getTimeInMillis(), AlarmManager.INTERVAL_FIFTEEN_MINUTES, alarmIntent);
            alarmMgr.set(AlarmManager.ELAPSED_REALTIME_WAKEUP, SystemClock.elapsedRealtime() + 60*1000, alarmIntent);
        }*/


        //Intent intent = new Intent(context, SampleAlarmReceiver.class);
        //alarmIntent = PendingIntent.getBroadcast(context, 0, intent, 0);*/


        /*// Set the alarm's trigger time to 8:30 a.m.
        calendar.set(Calendar.HOUR_OF_DAY, 4);
        calendar.set(Calendar.MINUTE, 27);
        alarmMgr.setInexactRepeating(AlarmManager.RTC_WAKEUP, calendar.getTimeInMillis(),
                AlarmManager.INTERVAL_DAY, alarmIntent);

  

         * If you don't have precise time requirements, use an inexact repeating alarm
         * the minimize the drain on the device battery.
         * 
         * The call below specifies the alarm type, the trigger time, the interval at
         * which the alarm is fired, and the alarm's associated PendingIntent.
         * It uses the alarm type RTC_WAKEUP ("Real Time Clock" wake up), which wakes up 
         * the device and triggers the alarm according to the time of the device's clock. 
         * 
         * Alternatively, you can use the alarm type ELAPSED_REALTIME_WAKEUP to trigger 
         * an alarm based on how much time has elapsed since the device was booted. This 
         * is the preferred choice if your alarm is based on elapsed time--for example, if 
         * you simply want your alarm to fire every 60 minutes. You only need to use 
         * RTC_WAKEUP if you want your alarm to fire at a particular date/time. Remember 
         * that clock-based time may not translate well to other locales, and that your 
         * app's behavior could be affected by the user changing the device's time setting.
         * 
         * Here are some examples of ELAPSED_REALTIME_WAKEUP:
         * 
         * // Wake up the device to fire a one-time alarm in one minute.
         * alarmMgr.set(AlarmManager.ELAPSED_REALTIME_WAKEUP, 
         *         SystemClock.elapsedRealtime() +
         *         60*1000, alarmIntent);
         *        
         * // Wake up the device to fire the alarm in 30 minutes, and every 30 minutes
         * // after that.
         * alarmMgr.setInexactRepeating(AlarmManager.ELAPSED_REALTIME_WAKEUP, 
         *         AlarmManager.INTERVAL_HALF_HOUR, 
         *         AlarmManager.INTERVAL_HALF_HOUR, alarmIntent);
         */
        
        // Set the alarm to fire at approximately 8:30 a.m., according to the device's
        // clock, and to repeat once a day.
        //alarmMgr.setInexactRepeating(AlarmManager.RTC_WAKEUP, calendar.getTimeInMillis(), AlarmManager.INTERVAL_DAY, alarmIntent);

        //alarmMgr.set(AlarmManager.ELAPSED_REALTIME_WAKEUP, SystemClock.elapsedRealtime() + 60*100, alarmIntent);
        //Toast.makeText(context, "alarma seteada", Toast.LENGTH_LONG );
        // Enable {@code SampleBootReceiver} to automatically restart the alarm when the
        // device is rebooted.
        ComponentName receiver = new ComponentName(context, SampleBootReceiver.class);
        PackageManager pm = context.getPackageManager();

        pm.setComponentEnabledSetting(receiver,
                PackageManager.COMPONENT_ENABLED_STATE_ENABLED,
                PackageManager.DONT_KILL_APP);           
    }
    // END_INCLUDE(set_alarm)

    /**
     * Cancels the alarm.
     * @param context
     */
    // BEGIN_INCLUDE(cancel_alarm)
    public void cancelAlarm(Context context, int idAlarm) {
        // If the alarm has been set, cancel it.
        if (alarmMgr!= null) {
            alarmMgr.cancel(alarmIntent);
        }
        
        // Disable {@code SampleBootReceiver} so that it doesn't automatically restart the 
        // alarm when the device is rebooted.
        ComponentName receiver = new ComponentName(context, SampleBootReceiver.class);
        PackageManager pm = context.getPackageManager();

        pm.setComponentEnabledSetting(receiver,
                PackageManager.COMPONENT_ENABLED_STATE_DISABLED,
                PackageManager.DONT_KILL_APP);
    }
    // END_INCLUDE(cancel_alarm)
}
