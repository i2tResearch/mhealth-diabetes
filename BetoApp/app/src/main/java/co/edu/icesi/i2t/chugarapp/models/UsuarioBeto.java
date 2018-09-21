package co.edu.icesi.i2t.chugarapp.models;

import java.util.ArrayList;

public class UsuarioBeto {

    private String rol;
    private String edad;
    private String sexo;
    private String ciudad;
    private String barrio;
    private String tomaMedicamento;
    private String ejercicio;
    private String horarioAlarma;
    private ArrayList<AlarmaMedicamento> alarmasMedicamentos;

    public UsuarioBeto() {
        rol = "";
        edad = "";
        sexo = "";
        ciudad = "";
        barrio = "";
        tomaMedicamento = "";
        ejercicio = "";
        horarioAlarma = "";
        alarmasMedicamentos = new ArrayList<AlarmaMedicamento>();
    }

    public String getRol() {
        return rol;
    }

    public void setRol(String rol) {
        this.rol = rol;
    }

    public String getEdad() {
        return edad;
    }

    public void setEdad(String edad) {
        this.edad = edad;
    }

    public String getSexo() {
        return sexo;
    }

    public void setSexo(String sexo) {
        this.sexo = sexo;
    }

    public String getCiudad() {
        return ciudad;
    }

    public void setCiudad(String ciudad) {
        this.ciudad = ciudad;
    }

    public String getBarrio() {
        return barrio;
    }

    public void setBarrio(String barrio) {
        this.barrio = barrio;
    }

    public String getTomaMedicamento() {
        return tomaMedicamento;
    }

    public void setTomaMedicamento(String tomaMedicamento) {
        this.tomaMedicamento = tomaMedicamento;
    }

    public String getEjercicio() {
        return ejercicio;
    }

    public void setEjercicio(String ejercicio) {
        this.ejercicio = ejercicio;
    }

    public String getHorarioAlarma() {
        return horarioAlarma;
    }

    public void setHorarioAlarma(String horarioAlarma) {
        this.horarioAlarma = horarioAlarma;
    }

    public ArrayList<AlarmaMedicamento> getAlarmasMedicamentos() {
        return alarmasMedicamentos;
    }

    public void setAlarmasMedicamentos(ArrayList<AlarmaMedicamento> alarmasMedicamentos) {
        this.alarmasMedicamentos = alarmasMedicamentos;
    }
}
