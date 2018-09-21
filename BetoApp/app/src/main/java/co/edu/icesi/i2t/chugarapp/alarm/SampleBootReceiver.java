package co.edu.icesi.i2t.chugarapp.alarm;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;

import co.edu.icesi.i2t.chugarapp.helpers.Config;

/**
 * This BroadcastReceiver automatically (re)starts the alarm when the device is
 * rebooted. This receiver is set to be disabled (android:enabled="false") in the
 * application's manifest file. When the user sets the alarm, the receiver is enabled.
 * When the user cancels the alarm, the receiver is disabled, so that rebooting the
 * device will not trigger this receiver.
 */
// BEGIN_INCLUDE(autostart)
public class SampleBootReceiver extends BroadcastReceiver {

    private SharedPreferences sharedpreferences;
    SampleAlarmReceiver alarm = new SampleAlarmReceiver();
    @Override
    public void onReceive(Context context, Intent intent) {
        if (intent.getAction().equals("android.intent.action.BOOT_COMPLETED")) {
            sharedpreferences = context.getSharedPreferences(Config.PREFERENCIAS, Context.MODE_PRIVATE);
            String tomaMed = sharedpreferences.getString(Config.CANTIDAD_MEDICAMENTO_ALARMA, null);
            String horarioAlarma = sharedpreferences.getString(Config.HORARIO_ALARMA_MEDICAMENTO, null);
            if (tomaMed != null) {
                switch (tomaMed) {
                    case "uno":
                        alarm.setAlarm(context, Config.ALARMA_MEDICAMENTO_MANANA);
                        break;
                    case "dos":
                        alarm.setAlarm(context, Config.ALARMA_MEDICAMENTO_MANANA);
                        alarm.setAlarm(context, Config.ALARMA_MEDICAMENTO_TARDE);
                        break;
                    case "tres":
                        alarm.setAlarm(context, Config.ALARMA_MEDICAMENTO_MANANA);
                        alarm.setAlarm(context, Config.ALARMA_MEDICAMENTO_TARDE);
                        alarm.setAlarm(context, Config.ALARMA_MEDICAMENTO_NOCHE);
                        break;
                }
            }
            if(horarioAlarma !=null){
                switch (horarioAlarma) {
                    case "no molestar":
                        break;
                    case "mañana":
                        alarm.setAlarm(context, Config.ALARMA_ESTADO_ANIMO_MANANA);
                        break;
                    case "medio dia":
                        alarm.setAlarm(context, Config.ALARMA_ESTADO_ANIMO_MEDIO_DIA);
                        break;
                    case "tarde":
                        alarm.setAlarm(context, Config.ALARMA_ESTADO_ANIMO_TARDE);
                        break;
                    case "noche":
                        alarm.setAlarm(context, Config.ALARMA_ESTADO_ANIMO_NOCHE);
                        break;
                }
            }
        }
    }
}
//END_INCLUDE(autostart)
