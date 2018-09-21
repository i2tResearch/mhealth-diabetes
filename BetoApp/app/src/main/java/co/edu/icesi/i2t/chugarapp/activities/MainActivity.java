package co.edu.icesi.i2t.chugarapp.activities;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;

import co.edu.icesi.i2t.chugarapp.R;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        ((Button)findViewById(R.id.dial)).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                /*LayoutInflater li = LayoutInflater.from(MainActivity.this);
                View promptsView = li.inflate(R.layout.dialog_animo_estado, null);
                AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(MainActivity.this);

                /*final TextInputEditText oldPass = (TextInputEditText) promptsView.findViewById(R.id.contra_old);
                final TextInputEditText newPass = (TextInputEditText) promptsView.findViewById(R.id.contra_new);
                final TextInputEditText confPass = (TextInputEditText) promptsView.findViewById(R.id.contra_confir);

                AppCompatButton guardar = (AppCompatButton)promptsView.findViewById(R.id.btnGuardar);
                AppCompatButton rechazar = (AppCompatButton)promptsView.findViewById(R.id.btnRechazar);*/

                //alertDialogBuilder.setView(promptsView);
                //final AlertDialog dialogo = alertDialogBuilder.create();

                /*rechazar.setOnClickListener(new View.OnClickListener(){
                    @Override
                    public void onClick(View v) {
                        dialogo.dismiss();
                    }
                });*/
                /*dialogo.show();*/

                //SampleAlarmReceiver alarm = new SampleAlarmReceiver();
                //alarm.setAlarm(getApplicationContext());
                startActivity(new Intent(getApplicationContext(), Pregunta.class));
                finish();
            }

        });
    }
}
