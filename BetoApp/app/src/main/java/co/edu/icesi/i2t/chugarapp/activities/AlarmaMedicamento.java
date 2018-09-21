package co.edu.icesi.i2t.chugarapp.activities;

import android.app.AlertDialog;
import android.content.Intent;
import android.graphics.Typeface;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import java.text.DateFormat;
import java.util.Date;
import java.util.HashMap;

import co.edu.icesi.i2t.chugarapp.R;

public class AlarmaMedicamento extends AppCompatActivity {

    private DatabaseReference mDatabase;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //setContentView(R.layout.activity_alarma_medicamento);
        //getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));

        try {
            FirebaseDatabase.getInstance().setPersistenceEnabled(true);
        } catch (Exception e) {
        }

        mDatabase = FirebaseDatabase.getInstance().getReference();
        LayoutInflater li = LayoutInflater.from(AlarmaMedicamento.this);
        View promptsView = li.inflate(R.layout.dialog_medicamento_toma, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaMedicamento.this);
        alertDialogBuilder.setCancelable(false);
        Typeface typeFace = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");
        Button yaTome = (Button) promptsView.findViewById(R.id.ya_tome);
        Button masTarde = (Button) promptsView.findViewById(R.id.record_en30);

        TextView tit = (TextView) promptsView.findViewById(R.id.titu);
        tit.setText(Html.fromHtml(getString(R.string.tit_toma_medicamento)));
        tit.setTypeface(typeFace);
        masTarde.setTypeface(typeFace);
        yaTome.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo = alertDialogBuilder.create();
        dialogo.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));

        masTarde.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                // ToDo: SETEAR ALARMA MEDICAMENTO PARA 30 MIN DESPUES
                HashMap preguntas = new HashMap();

                preguntas.put("fecha_toma", "Aplazó 30 min");

                String key = mDatabase.child("medicamento").push().getKey();
                mDatabase.child("medicamento").child(key).setValue(preguntas);
                vale();
                dialogo.dismiss();
            }
        });

        yaTome.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                HashMap preguntas = new HashMap();

                preguntas.put("fecha_toma", DateFormat.getDateTimeInstance().format(new Date()));

                String key = mDatabase.child("medicamento").push().getKey();
                mDatabase.child("medicamento").child(key).setValue(preguntas);
                ya();
                dialogo.dismiss();
            }
        });

        dialogo.show();
    }

    public void ya() {
        //getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        LayoutInflater li = LayoutInflater.from(AlarmaMedicamento.this);
        View promptsView = li.inflate(R.layout.dialog_medicamento_trabajo, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaMedicamento.this);
        alertDialogBuilder.setCancelable(false);
        Typeface typeFace = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");
        Button yaTome = (Button) promptsView.findViewById(R.id.nada);

        TextView tit = (TextView) promptsView.findViewById(R.id.titu);
        tit.setText(Html.fromHtml(getString(R.string.tit_trabajo)));
        tit.setTypeface(typeFace);
        yaTome.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo = alertDialogBuilder.create();
        dialogo.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));

        yaTome.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(getApplicationContext(), AlarmaEjercicio.class));
                finish();
            }
        });

        dialogo.show();
    }

    public void vale() {
        //getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        LayoutInflater li = LayoutInflater.from(AlarmaMedicamento.this);
        View promptsView = li.inflate(R.layout.dialog_medicamento_vale, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaMedicamento.this);
        alertDialogBuilder.setCancelable(false);
        Typeface typeFace = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");
        Button yaTome = (Button) promptsView.findViewById(R.id.nada);

        TextView tit = (TextView) promptsView.findViewById(R.id.tit_vale);
        tit.setText(Html.fromHtml(getString(R.string.tit_vale)));
        tit.setTypeface(typeFace);
        yaTome.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo = alertDialogBuilder.create();
        dialogo.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));

        yaTome.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(getApplicationContext(), AlarmaEjercicio.class));
                dialogo.dismiss();
                finish();
            }
        });

        dialogo.show();
    }
}
