package co.edu.icesi.i2t.chugarapp.activities;

import android.app.AlertDialog;
import android.graphics.Typeface;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import co.edu.icesi.i2t.chugarapp.R;

public class AlarmaEjercicio extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //setContentView(R.layout.activity_alarma_ejercicio);

        //getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        LayoutInflater li = LayoutInflater.from(AlarmaEjercicio.this);
        View promptsView = li.inflate(R.layout.dialog_movimiento_caminar, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaEjercicio.this);
        alertDialogBuilder.setCancelable(false);
        Typeface typeFace = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");

        Button recuerda = (Button) promptsView.findViewById(R.id.recuerda);
        Button ahora_no = (Button) promptsView.findViewById(R.id.ahora_no);
        TextView tit = (TextView) promptsView.findViewById(R.id.titulo);
        tit.setText(Html.fromHtml(getString(R.string.tit_caminar)));
        tit.setTypeface(typeFace);
        recuerda.setTypeface(typeFace);
        ahora_no.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo = alertDialogBuilder.create();
        dialogo.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));

        recuerda.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                //// TODO: Empezar a medir el movimiento
                dialogo.dismiss();
                finish();
            }
        });

        ahora_no.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                entiendo();
                dialogo.dismiss();
            }
        });

        dialogo.show();
    }

    public void entiendo() {
        //getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        LayoutInflater li = LayoutInflater.from(AlarmaEjercicio.this);
        View promptsView = li.inflate(R.layout.dialog_movimiento_actividad_fisica, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaEjercicio.this);
        alertDialogBuilder.setCancelable(false);
        Typeface typeFace = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");

        Button ya_lo = (Button) promptsView.findViewById(R.id.ya_lo_hice);
        Button mas_tarde = (Button) promptsView.findViewById(R.id.mas_tarde);
        TextView tit = (TextView) promptsView.findViewById(R.id.tit_actividad);
        tit.setText(Html.fromHtml(getString(R.string.tit_actividad_fisica)));
        tit.setTypeface(typeFace);
        ya_lo.setTypeface(typeFace);
        mas_tarde.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo = alertDialogBuilder.create();
        dialogo.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));

        ya_lo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                yaHice();
                dialogo.dismiss();
            }
        });

        mas_tarde.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                dialogo.dismiss();
                finish();
            }
        });

        dialogo.show();
    }

    public void yaHice() {
        //getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        LayoutInflater li = LayoutInflater.from(AlarmaEjercicio.this);
        View promptsView = li.inflate(R.layout.dialog_movimiento_llevar, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaEjercicio.this);
        alertDialogBuilder.setCancelable(false);
        Typeface typeFace = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");

        Button orgull = (Button) promptsView.findViewById(R.id.orgullo);
        EditText mas_tarde = (EditText) promptsView.findViewById(R.id.ejercicio_no_llevar);
        TextView tit = (TextView) promptsView.findViewById(R.id.tit_no_llevar);
        tit.setText(Html.fromHtml(getString(R.string.tit_no_llevar)));
        tit.setTypeface(typeFace);
        orgull.setTypeface(typeFace);
        mas_tarde.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo = alertDialogBuilder.create();
        dialogo.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));

        orgull.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                cinco();
                dialogo.dismiss();
            }
        });

        dialogo.show();
    }

    public void cinco() {
        //getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        LayoutInflater li = LayoutInflater.from(AlarmaEjercicio.this);
        View promptsView = li.inflate(R.layout.dialog_movimiento_bien, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaEjercicio.this);
        alertDialogBuilder.setCancelable(false);
        Typeface typeFace = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");

        Button orgull = (Button) promptsView.findViewById(R.id.btn_treinta);
        TextView tit = (TextView) promptsView.findViewById(R.id.tit_bien);
        tit.setText(Html.fromHtml(getString(R.string.tit_bien)));
        tit.setTypeface(typeFace);
        orgull.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo = alertDialogBuilder.create();
        dialogo.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));

        orgull.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                veinte();
                dialogo.dismiss();
            }
        });

        dialogo.show();
    }

    public void veinte() {
        //getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        LayoutInflater li = LayoutInflater.from(AlarmaEjercicio.this);
        View promptsView = li.inflate(R.layout.dialog_movimiento_animo, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaEjercicio.this);
        alertDialogBuilder.setCancelable(false);
        Typeface typeFace = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");

        Button orgull = (Button) promptsView.findViewById(R.id.btn_casi);
        TextView tit = (TextView) promptsView.findViewById(R.id.tit_animo);
        tit.setText(Html.fromHtml(getString(R.string.tit_animo_mov)));
        tit.setTypeface(typeFace);
        orgull.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo = alertDialogBuilder.create();
        dialogo.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));

        orgull.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                treinta();
                dialogo.dismiss();
            }
        });

        dialogo.show();
    }

    public void treinta() {
        //getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        LayoutInflater li = LayoutInflater.from(AlarmaEjercicio.this);
        View promptsView = li.inflate(R.layout.dialog_movimiento_muy_bien, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaEjercicio.this);
        alertDialogBuilder.setCancelable(false);
        Typeface typeFace = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");

        Button orgull = (Button) promptsView.findViewById(R.id.fin_orgullo);
        TextView tit = (TextView) promptsView.findViewById(R.id.tit_no_llevar);
        tit.setText(Html.fromHtml(getString(R.string.tit_muy_bien)));
        tit.setTypeface(typeFace);
        orgull.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo = alertDialogBuilder.create();
        dialogo.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));

        orgull.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                dialogo.dismiss();
                finish();
            }
        });

        dialogo.show();
    }
}
