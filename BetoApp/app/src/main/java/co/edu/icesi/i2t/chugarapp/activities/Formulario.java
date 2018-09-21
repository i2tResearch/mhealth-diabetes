package co.edu.icesi.i2t.chugarapp.activities;

import android.Manifest;
import android.app.AlertDialog;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.graphics.Typeface;
import android.graphics.drawable.ColorDrawable;
import android.media.MediaPlayer;
import android.media.MediaRecorder;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.text.Html;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.StorageReference;
import com.google.firebase.storage.UploadTask;

import java.io.File;
import java.text.DateFormat;
import java.util.Date;
import java.util.HashMap;

import co.edu.icesi.i2t.chugarapp.R;

public class Formulario extends AppCompatActivity {

    private static final String LOG_TAG = "AudioRecordTest";
    private static final int REQUEST_RECORD_AUDIO_PERMISSION = 200;
    private static String mFileName = null;

    private ImageButton mRecordButton = null;
    private MediaRecorder mRecorder = null;

    private ImageButton mPlayButton = null;
    private MediaPlayer mPlayer = null;
    private boolean mStartRecording = true;
    private boolean mStartPlaying = true;

    private StorageReference storageRef;

    // Requesting permission to RECORD_AUDIO
    private boolean permissionToRecordAccepted = false;
    private String[] permissions = {Manifest.permission.RECORD_AUDIO};

    private Typeface typeface;
    private String feel;
    private DatabaseReference mDatabase;

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        switch (requestCode) {
            case REQUEST_RECORD_AUDIO_PERMISSION:
                permissionToRecordAccepted = grantResults[0] == PackageManager.PERMISSION_GRANTED;
                break;
        }
        if (!permissionToRecordAccepted) finish();

    }

    private void onRecord(boolean start) {
        if (start) {
            startRecording();
        } else {
            stopRecording();
        }
    }

    private void onPlay(boolean start) {
        if (start) {
            startPlaying();
        } else {
            stopPlaying();
        }
    }

    private void startPlaying() {
        mPlayer = new MediaPlayer();

        try {
            mPlayer.setDataSource(mFileName);
            mPlayer.prepare();
            mPlayer.start();
            mPlayer.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {

                public void onCompletion(MediaPlayer mp) {

                    Log.i("Completion Listener", "Song Complete");
                    Toast.makeText(getApplicationContext(), "Media Completed", Toast.LENGTH_SHORT).show();
                    mPlayButton.setBackground(getDrawable(R.drawable.play_1));
                }
            });
        } catch (Exception e) {
            Log.e(LOG_TAG, "prepare() failed for file (" + mFileName + ")");
            e.printStackTrace();
        }
    }

    private void stopPlaying() {
        mPlayer.release();
        mPlayer = null;
    }

    private void startRecording() {
        mRecorder = new MediaRecorder();
        mRecorder.setAudioSource(MediaRecorder.AudioSource.MIC);
        mRecorder.setOutputFormat(MediaRecorder.OutputFormat.THREE_GPP);
        //mFileName = ""+ SystemClock.elapsedRealtime();
        mRecorder.setOutputFile(mFileName);// CAMBIAR NOMBRE DEPENDIENDO DEL ARCHIVO
        mRecorder.setAudioEncoder(MediaRecorder.AudioEncoder.AMR_NB);

        try {
            mRecorder.prepare();
        } catch (Exception e) {
            Log.e(LOG_TAG, "prepare() failed");
            e.printStackTrace();
        }

        mRecorder.start();
    }

    private void stopRecording() {
        mRecorder.stop();
        mRecorder.release();
        mRecorder = null;
    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //this.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
        //setContentView(R.layout.dialog_animo_formulario);
        //FirebaseDatabase.getInstance().setPersistenceEnabled(true);
        mDatabase = FirebaseDatabase.getInstance().getReference();
        LayoutInflater li = LayoutInflater.from(Formulario.this);
        View promptsView = li.inflate(R.layout.dialog_animo_formulario, null);
        typeface = Typeface.createFromAsset(getAssets(), "fonts/Raleway-Regular.ttf");
        // Record to the external cache directory for visibility
        mFileName = getExternalCacheDir().getAbsolutePath();

        mFileName += "/" + android.text.format.DateFormat.format("yyyy-MM-dd", new java.util.Date());

        ImageView ima = (ImageView) promptsView.findViewById(R.id.imagen_barra);
        storageRef = FirebaseStorage.getInstance().getReference();

        feel = getIntent().getStringExtra("feel");
        if (feel != null) {
            if (feel.equals("bien"))
                ima.setBackground(getResources().getDrawable(R.drawable.chugar_03));
            if (feel.equals("mal"))
                ima.setBackground(getResources().getDrawable(R.drawable.chugar_04));
            if (feel.equals("normal"))
                ima.setBackground(getResources().getDrawable(R.drawable.chugar_02));
        }

        ActivityCompat.requestPermissions(this, permissions, REQUEST_RECORD_AUDIO_PERMISSION);
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(Formulario.this);
        mRecordButton = (ImageButton) promptsView.findViewById(R.id.grabar_toma);
        mPlayButton = (ImageButton) promptsView.findViewById(R.id.reproducir);
        final EditText queja = (EditText) promptsView.findViewById(R.id.pqrs);
        alertDialogBuilder.setCancelable(false);
        Button sig = (Button) promptsView.findViewById(R.id.atencion_medica);
        final CheckBox aten = (CheckBox) promptsView.findViewById(R.id.check_atenci);

        TextView tit = (TextView) promptsView.findViewById(R.id.tit_form);
        sig.setText(Html.fromHtml(getString(R.string.btn_listo)));
        tit.setText(Html.fromHtml(getString(R.string.tit_form)));
        aten.setText(Html.fromHtml(getString(R.string.btn_pqrs)));
        tit.setTypeface(typeface);
        aten.setTypeface(typeface);
        sig.setTypeface(typeface);

        alertDialogBuilder.setView(promptsView);
        final AlertDialog dialogo_fin = alertDialogBuilder.create();
        dialogo_fin.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));

        mPlayButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onPlay(mStartPlaying);
                if (mStartPlaying) {
                    mPlayButton.setBackground(getDrawable(R.drawable.play_2));
                    Toast.makeText(getApplicationContext(), "Reproduciendo...", Toast.LENGTH_LONG).show();
                    //setText("Stop playing");
                } else {
                    //setText("Start playing");
                    mPlayButton.setBackground(getDrawable(R.drawable.play_1));
                    Toast.makeText(getApplicationContext(), "Stop", Toast.LENGTH_SHORT).show();
                }
                mStartPlaying = !mStartPlaying;
            }
        });

        mRecordButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onRecord(mStartRecording);
                if (mStartRecording) {
                    mRecordButton.setBackground(getDrawable(R.drawable.mic_2));
                    Toast.makeText(getApplicationContext(), "Grabando...", Toast.LENGTH_LONG).show();
                    //setText("Stop recording");
                } else {
                    mRecordButton.setBackground(getDrawable(R.drawable.mic));
                    Toast.makeText(getApplicationContext(), "Stop", Toast.LENGTH_SHORT).show();
                    //setText("Start recording");
                }
                mStartRecording = !mStartRecording;
            }
        });

        sig.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (new File(mFileName).exists()) {// todo problema de identificador por usuario de las respuestas que suba
                    Uri file = Uri.fromFile(new File(mFileName));
                    StorageReference riversRef = storageRef.child("audios/" + file.getLastPathSegment());
                    UploadTask uploadTask = riversRef.putFile(file);

                    // Register observers to listen for when the download is done or if it fails
                    uploadTask.addOnFailureListener(new OnFailureListener() {
                        @Override
                        public void onFailure(@NonNull Exception exception) {
                            // Handle unsuccessful uploads
                            Toast.makeText(getApplicationContext(), R.string.no_archi, Toast.LENGTH_LONG).show();
                        }
                    }).addOnSuccessListener(new OnSuccessListener<UploadTask.TaskSnapshot>() {
                        @Override
                        public void onSuccess(UploadTask.TaskSnapshot taskSnapshot) {
                            // taskSnapshot.getMetadata() contains file metadata such as size, content-type, and download URL.
                            //Uri downloadUrl = taskSnapshot.getDownloadUrl();
                            startActivity(new Intent(getApplicationContext(), AlarmaEjercicio.class));
                            dialogo_fin.dismiss();
                            finish();
                            new File(mFileName).delete();
                            // todo subir respuesta de texto y si es por pqrs o no, estado de animo, linkiar con el audio

                            HashMap preguntas = new HashMap();

                            preguntas.put("estado_animo", feel);
                            preguntas.put("atencion_medica", aten.isChecked());
                            preguntas.put("sentimiento", queja.getText().toString());
                            preguntas.put("fecha_archivo", mFileName);

                            String key = mDatabase.child("animo").push().getKey();
                            mDatabase.child("animo").child(key).setValue(preguntas);
                        }
                    });
                } else {
                    Toast.makeText(getApplicationContext(), R.string.llenar_form, Toast.LENGTH_LONG).show();
                }
            }
        });

        dialogo_fin.show();
    }

    @Override
    public void onStop() {
        super.onStop();

        if (mRecorder != null) {
            mRecorder.release();
            mRecorder = null;
        }

        if (mPlayer != null) {
            mPlayer.release();
            mPlayer = null;
        }
    }
}
