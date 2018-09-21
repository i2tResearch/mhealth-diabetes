package co.edu.icesi.i2t.chugarapp.activities;

import android.app.AlertDialog;
import android.content.Intent;
import android.graphics.Typeface;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;

import co.edu.icesi.i2t.chugarapp.R;

public class AlarmaAnimo extends AppCompatActivity {

    private Typeface typeFace;
    private String estado;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        LayoutInflater li = LayoutInflater.from(AlarmaAnimo.this);
        View promptsView = li.inflate(R.layout.dialog_animo_estado, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaAnimo.this);
        alertDialogBuilder.setCancelable(false);
        typeFace = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");
        final Button noMolestar = (Button) promptsView.findViewById(R.id.no_molestar);
        final Button masTarde = (Button) promptsView.findViewById(R.id.mas_tarde);
        ImageButton bien = (ImageButton) promptsView.findViewById(R.id.bien);
        ImageButton normal = (ImageButton) promptsView.findViewById(R.id.normal);
        ImageButton mal = (ImageButton) promptsView.findViewById(R.id.mal);

        TextView tit = (TextView) promptsView.findViewById(R.id.titulo);
        tit.setText(Html.fromHtml(getString(R.string.tit_animo)));
        tit.setTypeface(typeFace);
        masTarde.setTypeface(typeFace);
        noMolestar.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo = alertDialogBuilder.create();
        //dialogo.getWindow().setBackgroundDrawable(new ColorDrawable(android.graphics.Color.TRANSPARENT));

        noMolestar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //startActivity(new Intent(getApplicationContext(), Formulario.class));
                noMolestar();
                dialogo.dismiss();
            }
        });

        masTarde.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //// TODO: 31/05/2017 setear recordatorio para despues, definir cuanto.
                dialogo.dismiss();
                finish();
            }
        });

        bien.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                estado = "bien";
                Toast.makeText(getApplicationContext(), estado, Toast.LENGTH_LONG).show();
                Intent in = new Intent(getApplicationContext(), Formulario.class);
                in.putExtra("feel", "bien");
                startActivity(in);
                dialogo.dismiss();
                finish();
            }
        });

        normal.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                estado = "normal";
                Toast.makeText(getApplicationContext(), estado, Toast.LENGTH_LONG).show();
                Intent in = new Intent(getApplicationContext(), Formulario.class);
                in.putExtra("feel", "normal");
                startActivity(in);
                dialogo.dismiss();
                finish();
            }
        });

        mal.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                estado = "mal";
                Toast.makeText(getApplicationContext(), estado, Toast.LENGTH_LONG).show();
                Intent in = new Intent(getApplicationContext(), Formulario.class);
                in.putExtra("feel", "mal");
                startActivity(in);
                dialogo.dismiss();
                finish();
            }
        });

        dialogo.show();
    }

    public void noMolestar() {
        LayoutInflater li = LayoutInflater.from(AlarmaAnimo.this);
        View promptsView = li.inflate(R.layout.dialog_animo_no_molestar, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(AlarmaAnimo.this);
        alertDialogBuilder.setCancelable(false);
        Button sig = (Button) promptsView.findViewById(R.id.no_mo);

        TextView tit = (TextView) promptsView.findViewById(R.id.tit_mole);
        sig.setText(Html.fromHtml(getString(R.string.btn_no_molesto)));
        tit.setText(Html.fromHtml(getString(R.string.tit_molesto)));
        tit.setTypeface(typeFace);
        sig.setTypeface(typeFace);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo_fin = alertDialogBuilder.create();

        sig.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dialogo_fin.dismiss();
                finish();
            }
        });

        dialogo_fin.show();
    }
}
