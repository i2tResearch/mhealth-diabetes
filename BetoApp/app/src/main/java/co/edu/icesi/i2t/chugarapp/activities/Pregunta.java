package co.edu.icesi.i2t.chugarapp.activities;

import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.AppCompatButton;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import java.util.HashMap;

import co.edu.icesi.i2t.chugarapp.R;
import co.edu.icesi.i2t.chugarapp.alarm.AlarmReceiver;
import co.edu.icesi.i2t.chugarapp.helpers.Config;
import co.edu.icesi.i2t.chugarapp.helpers.Const;
import co.edu.icesi.i2t.chugarapp.models.UsuarioBeto;

public class Pregunta extends AppCompatActivity {

    private Typeface fuenteTexto;
    private int colorAzulClaro;
    private int colorFondo;

    private AlertDialog dialogoInicio, dialogoRol, dialogoEdad, dialogoCiudad,
            dialogoMedicamento, dialogoEjercicio, dialogoHorarioAlarma, dialogoFin;
    private int dialogoActual;
    private AlarmReceiver gestorAlarmas;
    private SharedPreferences configuracionLocal;
    private DatabaseReference baseDeDatos;
    private UsuarioBeto usuarioActual = new UsuarioBeto();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        fuenteTexto = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");
        colorAzulClaro = getResources().getColor(R.color.azul_claro);
        colorFondo = getResources().getColor(R.color.fondo_tit);

        configuracionLocal = getSharedPreferences(Config.PREFERENCIAS, Context.MODE_PRIVATE);
        dialogoActual = 0;
        gestorAlarmas = new AlarmReceiver();
        baseDeDatos = FirebaseDatabase.getInstance().getReference();

        if (validarFormularioDiligenciado()) {
            startActivity(new Intent(this, AlarmaAnimo.class));
            finish();
        } else {
            mostrarDialogoInicio();
        }
    }

    private boolean validarFormularioDiligenciado() {
        String complete = configuracionLocal.getString(Config.COMPLETO, null);
        return complete != null && complete.equals("Si");
    }

    @Override
    public void onDestroy() {
        super.onDestroy();

        switch (dialogoActual) {
            case 1:
                dismissDialog(dialogoInicio);
                break;
            case 2:
                dismissDialog(dialogoRol);
                break;
            case 3:
                dismissDialog(dialogoEdad);
                break;
            case 4:
                dismissDialog(dialogoCiudad);
                break;
            case 5:
                dismissDialog(dialogoMedicamento);
                break;
            case 6:
                dismissDialog(dialogoEjercicio);
                break;
            case 7:
                dismissDialog(dialogoHorarioAlarma);
                break;
            case 8:
                dismissDialog(dialogoFin);
                break;
        }
    }

    private void dismissDialog(AlertDialog dialog) {
        if (dialog != null) {
            dialog.dismiss();
        }
    }

    private void mostrarDialogoInicio() {
        dialogoActual = 1;

        LayoutInflater li = LayoutInflater.from(Pregunta.this);
        View promptsView = li.inflate(R.layout.pregunta_inicio, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(Pregunta.this);
        alertDialogBuilder.setCancelable(false);
        AppCompatButton sig = (AppCompatButton) promptsView.findViewById(R.id.siguiente);
        TextView textView = (TextView) promptsView.findViewById(R.id.titulo);
        textView.setText(Html.fromHtml(getString(R.string.txt_inicio)));
        sig.setText(Html.fromHtml(getString(R.string.sigui)));
        textView.setTypeface(fuenteTexto);
        sig.setTypeface(fuenteTexto);
        alertDialogBuilder.setView(promptsView);

        dialogoInicio = alertDialogBuilder.create();

        sig.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mostrarDialogoRol();
                dialogoInicio.dismiss();
            }
        });

        dialogoInicio.show();
    }

    private void mostrarDialogoRol() {
        dialogoActual = 2;

        LayoutInflater li = LayoutInflater.from(Pregunta.this);
        View promptsView = li.inflate(R.layout.pregunta_rol, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(Pregunta.this);
        alertDialogBuilder.setCancelable(false);
        AppCompatButton sig = (AppCompatButton) promptsView.findViewById(R.id.siguiente2);
        final AppCompatButton pac = (AppCompatButton) promptsView.findViewById(R.id.paciente);
        final AppCompatButton cui = (AppCompatButton) promptsView.findViewById(R.id.cuidador);
        pac.setText(Html.fromHtml(getString(R.string.btn_pac)));
        cui.setText(Html.fromHtml(getString(R.string.btn_cuida)));
        sig.setText(Html.fromHtml(getString(R.string.sigui)));
        pac.setTypeface(fuenteTexto);
        cui.setTypeface(fuenteTexto);
        sig.setTypeface(fuenteTexto);
        alertDialogBuilder.setView(promptsView);

        dialogoRol = alertDialogBuilder.create();

        pac.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                pac.setTextColor(Color.WHITE);
                pac.setBackgroundColor(colorAzulClaro);
                cui.setTextColor(colorFondo);
                cui.setBackgroundColor(Color.WHITE);
                usuarioActual.setRol(Const.ROL_PACIENTE);
            }
        });

        cui.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                cui.setTextColor(Color.WHITE);
                cui.setBackgroundColor(colorAzulClaro);
                pac.setTextColor(colorFondo);
                pac.setBackgroundColor(Color.WHITE);
                usuarioActual.setRol(Const.ROL_CUIDADOR);
            }
        });

        sig.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (!usuarioActual.getRol().isEmpty()) {
                    mostrarDialogoEdad();
                    dialogoRol.dismiss();
                } else {
                    Toast.makeText(getApplicationContext(), R.string.sel, Toast.LENGTH_LONG).show();
                }
            }
        });

        dialogoRol.show();
    }

    private void mostrarDialogoEdad() {
        dialogoActual = 3;

        LayoutInflater li = LayoutInflater.from(Pregunta.this);
        View promptsView = li.inflate(R.layout.pregunta_edad, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(Pregunta.this);
        alertDialogBuilder.setCancelable(false);
        AppCompatButton sig = (AppCompatButton) promptsView.findViewById(R.id.siguiente3);
        final AppCompatButton masc = (AppCompatButton) promptsView.findViewById(R.id.masculino);
        final AppCompatButton fem = (AppCompatButton) promptsView.findViewById(R.id.femenino);
        final EditText d = (EditText) promptsView.findViewById(R.id.dia);
        final EditText m = (EditText) promptsView.findViewById(R.id.mes);
        final EditText y = (EditText) promptsView.findViewById(R.id.ano);
        TextView lse = (TextView) promptsView.findViewById(R.id.lsexo);
        TextView led = (TextView) promptsView.findViewById(R.id.ledad);
        lse.setText(Html.fromHtml(getString(R.string.txt_sexo)));
        led.setText(Html.fromHtml(getString(R.string.txt_edad)));
        sig.setText(Html.fromHtml(getString(R.string.sigui)));
        lse.setTypeface(fuenteTexto);
        led.setTypeface(fuenteTexto);
        masc.setTypeface(fuenteTexto);
        fem.setTypeface(fuenteTexto);
        d.setTypeface(fuenteTexto);
        m.setTypeface(fuenteTexto);
        y.setTypeface(fuenteTexto);
        sig.setTypeface(fuenteTexto);
        alertDialogBuilder.setView(promptsView);

        dialogoEdad = alertDialogBuilder.create();

        masc.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                masc.setTextColor(Color.WHITE);
                masc.setBackgroundColor(colorAzulClaro);
                fem.setTextColor(colorFondo);
                fem.setBackgroundColor(Color.WHITE);
                usuarioActual.setSexo(Const.GENERO_MASCULINO);
            }
        });

        fem.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                fem.setTextColor(Color.WHITE);
                fem.setBackgroundColor(colorAzulClaro);
                masc.setTextColor(colorFondo);
                masc.setBackgroundColor(Color.WHITE);
                usuarioActual.setSexo(Const.GENERO_FEMENINO);
            }
        });

        sig.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (d.getText().toString().equals("") || m.getText().toString().equals("") || y.getText().toString().equals("")
                        || usuarioActual.getSexo().equals("")) {
                    Toast.makeText(getApplicationContext(), R.string.txt_llenar, Toast.LENGTH_LONG).show();
                } else {
                    String edad = d.getText().toString() + "/" + m.getText().toString() + "/" + y.getText().toString();
                    usuarioActual.setEdad(edad);
                    mostrarDialogoCiudad();
                    dialogoEdad.dismiss();
                }
            }
        });

        dialogoEdad.show();
    }

    private void mostrarDialogoCiudad() {
        dialogoActual = 4;

        LayoutInflater li = LayoutInflater.from(Pregunta.this);
        View promptsView = li.inflate(R.layout.pregunta_ciudad, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(Pregunta.this);
        alertDialogBuilder.setCancelable(false);
        AppCompatButton sig = (AppCompatButton) promptsView.findViewById(R.id.siguiente4);
        final EditText ciu = (EditText) promptsView.findViewById(R.id.ciudad);
        TextView lciu = (TextView) promptsView.findViewById(R.id.lciu);
        final EditText barr = (EditText) promptsView.findViewById(R.id.barrio);
        TextView lbarr = (TextView) promptsView.findViewById(R.id.lbarrio);
        lciu.setText(Html.fromHtml(getString(R.string.txt_ciudad)));
        lbarr.setText(Html.fromHtml(getString(R.string.txt_barrio)));
        sig.setText(Html.fromHtml(getString(R.string.sigui)));
        ciu.setTypeface(fuenteTexto);
        barr.setTypeface(fuenteTexto);
        lbarr.setTypeface(fuenteTexto);
        lciu.setTypeface(fuenteTexto);
        sig.setTypeface(fuenteTexto);
        alertDialogBuilder.setView(promptsView);

        dialogoCiudad = alertDialogBuilder.create();

        sig.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (ciu.getText().toString().equals("") || barr.getText().toString().equals("")) {
                    Toast.makeText(getApplicationContext(), R.string.txt_llenar, Toast.LENGTH_LONG).show();
                } else {
                    usuarioActual.setCiudad(ciu.getText().toString());
                    usuarioActual.setBarrio(barr.getText().toString());
                    mostrarDialogoMedicamento();
                    dialogoCiudad.dismiss();
                }
            }
        });

        dialogoCiudad.show();
    }

    private void mostrarDialogoMedicamento() {
        dialogoActual = 5;

        LayoutInflater li = LayoutInflater.from(Pregunta.this);
        View promptsView = li.inflate(R.layout.pregunta_medicamento, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(Pregunta.this);
        alertDialogBuilder.setCancelable(false);
        AppCompatButton sig = (AppCompatButton) promptsView.findViewById(R.id.siguiente5);
        final AppCompatButton masc = (AppCompatButton) promptsView.findViewById(R.id.masculino);
        final AppCompatButton fem = (AppCompatButton) promptsView.findViewById(R.id.femenino);
        final Button uno = (Button) promptsView.findViewById(R.id.uno);
        final Button dos = (Button) promptsView.findViewById(R.id.dos);
        final Button tres = (Button) promptsView.findViewById(R.id.tres);
        TextView lse = (TextView) promptsView.findViewById(R.id.prim);
        TextView led = (TextView) promptsView.findViewById(R.id.seg);
        lse.setText(Html.fromHtml(getString(R.string.txt_terminamos)));
        led.setText(Html.fromHtml(getString(R.string.txt_medicamento)));
        sig.setText(Html.fromHtml(getString(R.string.sigui)));
        lse.setTypeface(fuenteTexto);
        led.setTypeface(fuenteTexto);
        uno.setTypeface(fuenteTexto);
        dos.setTypeface(fuenteTexto);
        tres.setTypeface(fuenteTexto);
        sig.setTypeface(fuenteTexto);
        alertDialogBuilder.setView(promptsView);

        dialogoMedicamento = alertDialogBuilder.create();

        uno.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                uno.setTextColor(Color.WHITE);
                uno.setBackgroundColor(colorAzulClaro);
                dos.setTextColor(colorFondo);
                dos.setBackgroundColor(Color.WHITE);
                tres.setTextColor(colorFondo);
                tres.setBackgroundColor(Color.WHITE);
                usuarioActual.setTomaMedicamento(Const.MEDICAMENTO_UNO);
            }
        });

        dos.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dos.setTextColor(Color.WHITE);
                dos.setBackgroundColor(colorAzulClaro);
                uno.setTextColor(colorFondo);
                uno.setBackgroundColor(Color.WHITE);
                tres.setTextColor(colorFondo);
                tres.setBackgroundColor(Color.WHITE);
                usuarioActual.setTomaMedicamento(Const.MEDICAMENTO_DOS);
            }
        });

        tres.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                tres.setTextColor(Color.WHITE);
                tres.setBackgroundColor(colorAzulClaro);
                uno.setTextColor(colorFondo);
                uno.setBackgroundColor(Color.WHITE);
                dos.setTextColor(colorFondo);
                dos.setBackgroundColor(Color.WHITE);
                usuarioActual.setTomaMedicamento(Const.MEDICAMENTO_TRES);
            }
        });

        sig.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (usuarioActual.getTomaMedicamento().equals("")) {
                    Toast.makeText(getApplicationContext(), R.string.sel, Toast.LENGTH_LONG).show();
                } else {
                    mostrarDialogoEjercicio();
                    establecerAlarmaMedicamento();
                    dialogoMedicamento.dismiss();
                }
            }
        });

        dialogoMedicamento.show();
    }

    private void establecerAlarmaMedicamento() {
        switch (usuarioActual.getTomaMedicamento()) {
            case Const.MEDICAMENTO_UNO:
                gestorAlarmas.setAlarm(getApplicationContext(), Config.ALARMA_MEDICAMENTO_MANANA);
                break;
            case Const.MEDICAMENTO_DOS:
                gestorAlarmas.setAlarm(getApplicationContext(), Config.ALARMA_MEDICAMENTO_MANANA);
                gestorAlarmas.setAlarm(getApplicationContext(), Config.ALARMA_MEDICAMENTO_TARDE);
                break;
            case Const.MEDICAMENTO_TRES:
                gestorAlarmas.setAlarm(getApplicationContext(), Config.ALARMA_MEDICAMENTO_MANANA);
                gestorAlarmas.setAlarm(getApplicationContext(), Config.ALARMA_MEDICAMENTO_TARDE);
                gestorAlarmas.setAlarm(getApplicationContext(), Config.ALARMA_MEDICAMENTO_NOCHE);
                break;
        }
    }

    private void mostrarDialogoEjercicio() {
        dialogoActual = 6;

        LayoutInflater li = LayoutInflater.from(Pregunta.this);
        View promptsView = li.inflate(R.layout.pregunta_ejercicio, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(Pregunta.this);
        alertDialogBuilder.setCancelable(false);
        AppCompatButton sig = (AppCompatButton) promptsView.findViewById(R.id.siguiente6);
        final Button si = (Button) promptsView.findViewById(R.id.si);
        final Button no = (Button) promptsView.findViewById(R.id.no);
        final Button gim = (Button) promptsView.findViewById(R.id.gimnasio);
        final Button cami = (Button) promptsView.findViewById(R.id.caminar);
        final Button bic = (Button) promptsView.findViewById(R.id.bicicleta);
        final Button otr = (Button) promptsView.findViewById(R.id.otro);
        TextView lse = (TextView) promptsView.findViewById(R.id.hace_ejer);
        final TextView led = (TextView) promptsView.findViewById(R.id.tipo_ejer);
        lse.setText(Html.fromHtml(getString(R.string.txt_ejerc)));
        led.setText(Html.fromHtml(getString(R.string.txt_tipo)));
        sig.setText(Html.fromHtml(getString(R.string.sigui)));
        lse.setTypeface(fuenteTexto);
        led.setTypeface(fuenteTexto);
        si.setTypeface(fuenteTexto);
        no.setTypeface(fuenteTexto);
        gim.setTypeface(fuenteTexto);
        cami.setTypeface(fuenteTexto);
        bic.setTypeface(fuenteTexto);
        otr.setTypeface(fuenteTexto);
        sig.setTypeface(fuenteTexto);
        gim.setVisibility(View.GONE);
        cami.setVisibility(View.GONE);
        bic.setVisibility(View.GONE);
        otr.setVisibility(View.GONE);
        led.setVisibility(View.GONE);
        alertDialogBuilder.setView(promptsView);

        dialogoEjercicio = alertDialogBuilder.create();

        si.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                si.setTextColor(Color.WHITE);
                si.setBackgroundColor(colorAzulClaro);
                no.setTextColor(colorFondo);
                no.setBackgroundColor(Color.WHITE);
                gim.setVisibility(View.VISIBLE);
                cami.setVisibility(View.VISIBLE);
                bic.setVisibility(View.VISIBLE);
                otr.setVisibility(View.VISIBLE);
                led.setVisibility(View.VISIBLE);
                usuarioActual.setEjercicio(Const.EJERCICIO_HACE);
            }
        });

        no.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                no.setTextColor(Color.WHITE);
                no.setBackgroundColor(colorAzulClaro);
                si.setTextColor(colorFondo);
                si.setBackgroundColor(Color.WHITE);
                usuarioActual.setEjercicio(Const.EJERCICIO_NO_HACE);
                gim.setVisibility(View.GONE);
                cami.setVisibility(View.GONE);
                led.setVisibility(View.GONE);
                bic.setVisibility(View.GONE);
                otr.setVisibility(View.GONE);
            }
        });

        cami.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                cami.setTextColor(Color.WHITE);
                cami.setBackgroundColor(colorAzulClaro);
                si.setTextColor(Color.WHITE);
                si.setBackgroundColor(colorAzulClaro);
                otr.setTextColor(colorFondo);
                otr.setBackgroundColor(Color.WHITE);
                bic.setTextColor(colorFondo);
                bic.setBackgroundColor(Color.WHITE);
                gim.setTextColor(colorFondo);
                gim.setBackgroundColor(Color.WHITE);
                usuarioActual.setEjercicio(Const.EJERCICIO_CAMINAR);
            }
        });

        otr.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                otr.setTextColor(Color.WHITE);
                otr.setBackgroundColor(colorAzulClaro);
                si.setTextColor(Color.WHITE);
                si.setBackgroundColor(colorAzulClaro);
                cami.setTextColor(colorFondo);
                cami.setBackgroundColor(Color.WHITE);
                bic.setTextColor(colorFondo);
                bic.setBackgroundColor(Color.WHITE);
                gim.setTextColor(colorFondo);
                gim.setBackgroundColor(Color.WHITE);
                usuarioActual.setEjercicio(Const.EJERCICIO_OTRO);
            }
        });

        bic.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                bic.setTextColor(Color.WHITE);
                bic.setBackgroundColor(colorAzulClaro);
                si.setTextColor(Color.WHITE);
                si.setBackgroundColor(colorAzulClaro);
                cami.setTextColor(colorFondo);
                cami.setBackgroundColor(Color.WHITE);
                otr.setTextColor(colorFondo);
                otr.setBackgroundColor(Color.WHITE);
                gim.setTextColor(colorFondo);
                gim.setBackgroundColor(Color.WHITE);
                usuarioActual.setEjercicio(Const.EJERCICIO_BICICLETA);
            }
        });

        gim.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                gim.setTextColor(Color.WHITE);
                gim.setBackgroundColor(colorAzulClaro);
                si.setTextColor(Color.WHITE);
                si.setBackgroundColor(colorAzulClaro);
                cami.setTextColor(colorFondo);
                cami.setBackgroundColor(Color.WHITE);
                otr.setTextColor(colorFondo);
                otr.setBackgroundColor(Color.WHITE);
                bic.setTextColor(colorFondo);
                bic.setBackgroundColor(Color.WHITE);
                usuarioActual.setEjercicio(Const.EJERCICIO_GIMNASIO);
            }
        });

        sig.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (usuarioActual.getEjercicio().equals("")) {
                    Toast.makeText(getApplicationContext(), R.string.sel, Toast.LENGTH_LONG).show();
                } else {
                    mostrarDialogoHorarioAlarma();
                    dialogoEjercicio.dismiss();
                }
            }
        });

        dialogoEjercicio.show();
    }

    private void mostrarDialogoHorarioAlarma() {
        dialogoActual = 7;

        LayoutInflater li = LayoutInflater.from(Pregunta.this);
        View promptsView = li.inflate(R.layout.pregunta_alarma, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(Pregunta.this);
        alertDialogBuilder.setCancelable(false);
        AppCompatButton sig = (AppCompatButton) promptsView.findViewById(R.id.siguiente7);
        final Button manana = (Button) promptsView.findViewById(R.id.manana);
        final Button medio = (Button) promptsView.findViewById(R.id.medio_dia);
        final Button tarde = (Button) promptsView.findViewById(R.id.tarde);
        final Button noche = (Button) promptsView.findViewById(R.id.noche);
        final Button no_molestar = (Button) promptsView.findViewById(R.id.noMolestar);
        TextView lse = (TextView) promptsView.findViewById(R.id.seg);
        lse.setText(Html.fromHtml(getString(R.string.txt_alarm)));
        sig.setText(Html.fromHtml(getString(R.string.btn_final)));
        lse.setTypeface(fuenteTexto);
        manana.setTypeface(fuenteTexto);
        medio.setTypeface(fuenteTexto);
        tarde.setTypeface(fuenteTexto);
        noche.setTypeface(fuenteTexto);
        no_molestar.setTypeface(fuenteTexto);
        sig.setTypeface(fuenteTexto);
        alertDialogBuilder.setView(promptsView);

        dialogoHorarioAlarma = alertDialogBuilder.create();

        manana.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                manana.setTextColor(Color.WHITE);
                manana.setBackgroundColor(colorAzulClaro);
                medio.setTextColor(colorFondo);
                medio.setBackgroundColor(Color.WHITE);
                tarde.setTextColor(colorFondo);
                tarde.setBackgroundColor(Color.WHITE);
                noche.setTextColor(colorFondo);
                noche.setBackgroundColor(Color.WHITE);
                no_molestar.setTextColor(colorFondo);
                no_molestar.setBackgroundColor(Color.WHITE);
                usuarioActual.setHorarioAlarma(Const.HORARIO_ALARMA_MANANA);
            }
        });

        medio.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                medio.setTextColor(Color.WHITE);
                medio.setBackgroundColor(colorAzulClaro);
                manana.setTextColor(colorFondo);
                manana.setBackgroundColor(Color.WHITE);
                tarde.setTextColor(colorFondo);
                tarde.setBackgroundColor(Color.WHITE);
                noche.setTextColor(colorFondo);
                noche.setBackgroundColor(Color.WHITE);
                no_molestar.setTextColor(colorFondo);
                no_molestar.setBackgroundColor(Color.WHITE);
                usuarioActual.setHorarioAlarma(Const.HORARIO_ALARMA_MEDIO_DIA);
            }
        });

        tarde.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                tarde.setTextColor(Color.WHITE);
                tarde.setBackgroundColor(colorAzulClaro);
                manana.setTextColor(colorFondo);
                manana.setBackgroundColor(Color.WHITE);
                medio.setTextColor(colorFondo);
                medio.setBackgroundColor(Color.WHITE);
                noche.setTextColor(colorFondo);
                noche.setBackgroundColor(Color.WHITE);
                no_molestar.setTextColor(colorFondo);
                no_molestar.setBackgroundColor(Color.WHITE);
                usuarioActual.setHorarioAlarma(Const.HORARIO_ALARMA_TARDE);
            }
        });

        noche.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                noche.setTextColor(Color.WHITE);
                noche.setBackgroundColor(colorAzulClaro);
                manana.setTextColor(colorFondo);
                manana.setBackgroundColor(Color.WHITE);
                medio.setTextColor(colorFondo);
                medio.setBackgroundColor(Color.WHITE);
                tarde.setTextColor(colorFondo);
                tarde.setBackgroundColor(Color.WHITE);
                no_molestar.setTextColor(colorFondo);
                no_molestar.setBackgroundColor(Color.WHITE);
                usuarioActual.setHorarioAlarma(Const.HORARIO_ALARMA_NOCHE);
            }
        });

        no_molestar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                no_molestar.setTextColor(Color.WHITE);
                no_molestar.setBackgroundColor(colorAzulClaro);
                manana.setTextColor(colorFondo);
                manana.setBackgroundColor(Color.WHITE);
                medio.setTextColor(colorFondo);
                medio.setBackgroundColor(Color.WHITE);
                tarde.setTextColor(colorFondo);
                tarde.setBackgroundColor(Color.WHITE);
                noche.setTextColor(colorFondo);
                noche.setBackgroundColor(Color.WHITE);
                usuarioActual.setHorarioAlarma(Const.HORARIO_ALARMA_NO_MOLESTAR);
            }
        });

        sig.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (usuarioActual.getHorarioAlarma().equals("")) {
                    Toast.makeText(getApplicationContext(), R.string.sel, Toast.LENGTH_LONG).show();
                } else {
                    //TODO VALIDAR SEGÚN LA OPCIÓN LAS DIFERENTES HORAS
                    establecerAlarmaAnimo();
                    mostrarDialogoFin();
                    dialogoHorarioAlarma.dismiss();
                }

            }
        });

        dialogoHorarioAlarma.show();
    }

    private void establecerAlarmaAnimo() {
        switch (usuarioActual.getHorarioAlarma()) {
            case Const.HORARIO_ALARMA_NO_MOLESTAR:
                break;
            case Const.HORARIO_ALARMA_MANANA:
                gestorAlarmas.setAlarm(getApplicationContext(), Config.ALARMA_ESTADO_ANIMO_MANANA);
                break;
            case Const.HORARIO_ALARMA_MEDIO_DIA:
                gestorAlarmas.setAlarm(getApplicationContext(), Config.ALARMA_ESTADO_ANIMO_MEDIO_DIA);
                break;
            case Const.HORARIO_ALARMA_TARDE:
                gestorAlarmas.setAlarm(getApplicationContext(), Config.ALARMA_ESTADO_ANIMO_TARDE);
                break;
            case Const.HORARIO_ALARMA_NOCHE:
                gestorAlarmas.setAlarm(getApplicationContext(), Config.ALARMA_ESTADO_ANIMO_NOCHE);
                break;
        }
    }

    private void mostrarDialogoFin() {
        dialogoActual = 8;

        LayoutInflater li = LayoutInflater.from(Pregunta.this);
        View promptsView = li.inflate(R.layout.pregunta_fin, null);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(Pregunta.this);
        alertDialogBuilder.setCancelable(false);
        AppCompatButton sig = (AppCompatButton) promptsView.findViewById(R.id.siguiente8);
        TextView tit = (TextView) promptsView.findViewById(R.id.tit);
        TextView txt = (TextView) promptsView.findViewById(R.id.texto);
        tit.setText(Html.fromHtml(getString(R.string.txt_ahora)));
        txt.setText(Html.fromHtml(getString(R.string.txt_fin)));
        sig.setText(Html.fromHtml(getString(R.string.txt_empezamos)));
        tit.setTypeface(fuenteTexto);
        txt.setTypeface(fuenteTexto);
        sig.setTypeface(fuenteTexto);
        alertDialogBuilder.setView(promptsView);

        dialogoFin = alertDialogBuilder.create();

        sig.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SharedPreferences.Editor editor = configuracionLocal.edit();

                editor.putString(Config.ROL, usuarioActual.getRol());
                editor.putString(Config.EDAD, usuarioActual.getEdad());
                editor.putString(Config.SEXO, usuarioActual.getSexo());
                editor.putString(Config.CIUDAD, usuarioActual.getCiudad());
                editor.putString(Config.BARRIO, usuarioActual.getBarrio());
                editor.putString(Config.CANTIDAD_MEDICAMENTO_ALARMA, usuarioActual.getTomaMedicamento());
                editor.putString(Config.ACTIVIDAD_FISICA, usuarioActual.getEjercicio());
                editor.putString(Config.HORARIO_ALARMA_MEDICAMENTO, usuarioActual.getHorarioAlarma());
                editor.putString(Config.COMPLETO, "Si");
                editor.commit();

                HashMap preguntas = new HashMap();

                preguntas.put(Config.ROL, usuarioActual.getRol());
                preguntas.put(Config.EDAD, usuarioActual.getEdad());
                preguntas.put(Config.SEXO, usuarioActual.getSexo());
                preguntas.put(Config.CIUDAD, usuarioActual.getCiudad());
                preguntas.put(Config.BARRIO, usuarioActual.getBarrio());
                preguntas.put(Config.CANTIDAD_MEDICAMENTO_ALARMA, usuarioActual.getTomaMedicamento());
                preguntas.put(Config.ACTIVIDAD_FISICA, usuarioActual.getEjercicio());
                preguntas.put(Config.HORARIO_ALARMA_MEDICAMENTO, usuarioActual.getHorarioAlarma());

                String key = baseDeDatos.child("preguntas").push().getKey();
                baseDeDatos.child("preguntas").child(key).setValue(preguntas);

                dialogoFin.dismiss();
                finish();
            }
        });

        dialogoFin.show();
    }
}
