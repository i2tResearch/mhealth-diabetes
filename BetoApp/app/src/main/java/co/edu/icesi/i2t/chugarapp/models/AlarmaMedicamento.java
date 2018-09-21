package co.edu.icesi.i2t.chugarapp.models;

public class AlarmaMedicamento {
    public static int FRECUENCIA_DIARIA = 1;
    public static int FRECUENCIA_SEMANAL = 2;
    public static int FRECUENCIA_CADA_2_DIAS = 2;

    private int hora;
    private int minuto;
    private int frecuencia;
    private String mensaje;

    public int getHora() {
        return hora;
    }

    public void setHora(int hora) {
        this.hora = hora;
    }

    public int getMinuto() {
        return minuto;
    }

    public void setMinuto(int minuto) {
        this.minuto = minuto;
    }

    public int getFrecuencia() {
        return frecuencia;
    }

    public void setFrecuencia(int frecuencia) {
        this.frecuencia = frecuencia;
    }

    public String getMensaje() {
        return mensaje;
    }

    public void setMensaje(String mensaje) {
        this.mensaje = mensaje;
    }
}

