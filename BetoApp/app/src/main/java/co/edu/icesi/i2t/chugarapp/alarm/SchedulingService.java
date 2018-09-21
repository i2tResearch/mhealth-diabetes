package co.edu.icesi.i2t.chugarapp.alarm;

import android.app.IntentService;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.graphics.BitmapFactory;
import android.media.RingtoneManager;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.NotificationCompat;
import android.text.Html;

import co.edu.icesi.i2t.chugarapp.R;
import co.edu.icesi.i2t.chugarapp.activities.AlarmaAnimo;
import co.edu.icesi.i2t.chugarapp.activities.AlarmaMedicamento;
import co.edu.icesi.i2t.chugarapp.helpers.Config;

/**
 * This {@code IntentService} does the app's actual work.
 * {@code SampleAlarmReceiver} (a {@code WakefulBroadcastReceiver}) holds a
 * partial wake lock for this service while the service does its work. When the
 * service is finished, it calls {@code completeWakefulIntent()} to release the
 * wake lock.
 */
public class SchedulingService extends IntentService {

    public SchedulingService() {
        super("SchedulingService");
    }

    private NotificationManager mNotificationManager;
    NotificationCompat.Builder builder;

    @Override
    protected void onHandleIntent(Intent intent) {
        // BEGIN_INCLUDE(service_onhandle)
        Bundle extr = intent.getExtras();
        int id = extr.getInt("id");

        sendNotification("" + id);
        // Release the wake lock provided by the BroadcastReceiver.
        AlarmReceiver.completeWakefulIntent(intent);
        // END_INCLUDE(service_onhandle)
    }

    private void sendNotification(String msg) {
        mNotificationManager = (NotificationManager) this.getSystemService(Context.NOTIFICATION_SERVICE);

        if (msg != null) { // Todo Cambiar el título, texto y sonido de la notificación
            int param = Integer.parseInt(msg);
            if (param == Config.ALARMA_ESTADO_ANIMO_MANANA || param == Config.ALARMA_ESTADO_ANIMO_MEDIO_DIA ||
                    param == Config.ALARMA_ESTADO_ANIMO_TARDE || param == Config.ALARMA_ESTADO_ANIMO_NOCHE) {
                Intent notifyIntent = new Intent(getApplicationContext(), AlarmaAnimo.class);
                notifyIntent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                PendingIntent contentIntent = PendingIntent.getActivity(this, 0,
                        notifyIntent, PendingIntent.FLAG_ONE_SHOT);
                Uri defaultSoundUri = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_ALARM);

                NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(this)
                        .setSmallIcon(R.drawable.notificacion_2)
                        .setLargeIcon(BitmapFactory.decodeResource(getResources(), R.drawable.notificacion))
                        .setAutoCancel(true)
                        .setSound(defaultSoundUri)
                        .setDefaults(Notification.DEFAULT_ALL)
                        .setPriority(Notification.PRIORITY_HIGH)
                        .setContentTitle(Html.fromHtml(getString(R.string.tit_animo)))
                        .setStyle(new NotificationCompat.BigTextStyle()
                                .bigText(msg))
                        .setContentText(msg);

                mBuilder.setContentIntent(contentIntent);
                mNotificationManager.notify(param, mBuilder.build());
            } else {

                if (param == Config.ALARMA_MEDICAMENTO_MANANA || param == Config.ALARMA_MEDICAMENTO_TARDE ||
                        param == Config.ALARMA_MEDICAMENTO_NOCHE) {
                    Intent notifyIntent = new Intent(getApplicationContext(), AlarmaMedicamento.class);
                    notifyIntent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                    PendingIntent contentIntent = PendingIntent.getActivity(this, 0,
                            notifyIntent, PendingIntent.FLAG_ONE_SHOT);
                    Uri defaultSoundUri = Uri.parse("android.resource://" + getPackageName() + "/assets/raw/sonido.wav");

                    NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(this)
                            .setSmallIcon(R.drawable.notificacion_2)
                            .setLargeIcon(BitmapFactory.decodeResource(getResources(), R.drawable.notificacion))
                            .setAutoCancel(true)
                            .setSound(defaultSoundUri)
                            .setDefaults(Notification.DEFAULT_ALL)
                            .setPriority(Notification.PRIORITY_HIGH)
                            .setContentTitle(Html.fromHtml(getString(R.string.tit_toma_medicamento)))
                            .setStyle(new NotificationCompat.BigTextStyle()
                                    .bigText(msg))
                            .setContentText(msg);
                    mBuilder.setContentIntent(contentIntent);
                    mNotificationManager.notify(param, mBuilder.build());

                } else {
                    // Todo probar: ¿Qué hacer cuando no hay id de notificación reconocido?
                }
            }
        }
    }
}
