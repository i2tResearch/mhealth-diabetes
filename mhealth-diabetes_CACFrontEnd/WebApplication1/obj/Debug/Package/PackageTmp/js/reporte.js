var resp = "[{\"Mes\":\"10\",\"Resultado\":\"10\",\"Numerador\":\"20\",\"Denominador\":\"30\"},{\"Mes\":\"11\",\"Resultado\":\"90\",\"Numerador\":\"44\",\"Denominador\":\"23\"},{\"Mes\":\"12\",\"Resultado\":\"34\",\"Numerador\":\"70\",\"Denominador\":\"25\"}]";
var respu = "[{\"Mes\":\"10\",\"Resultado\":\"33\",\"Numerador\":\"70\",\"Denominador\":\"22\"},{\"Mes\":\"11\",\"Resultado\":\"45\",\"Numerador\":\"66\",\"Denominador\":\"77\"},{\"Mes\":\"12\",\"Resultado\":\"11\",\"Numerador\":\"55\",\"Denominador\":\"88\"}]";

var torta = "[{\"Mes\":\"9\",\"NoMedidos\":\"6\",\"PacientesControlados\":\"1\",\"PacientesEstadios\":\"7\",\"PacientesEstudiados\":\"5\",\"VigentesControlados\":\"8\",\"VigentesDescontrolados\":\"9\"}]";
var torta2 = "[{\"Mes\":\"9\",\"NoMedidos\":\"6\",\"PacientesControlados\":\"1\",\"PacientesEstadios\":\"7\",\"PacientesEstudiados\":\"5\",\"VigentesControlados\":\"8\",\"VigentesDescontrolados\":\"9\"},{\"Mes\":\"10\",\"NoMedidos\":\"2\",\"PacientesControlados\":\"-1\",\"PacientesEstadios\":\"7\",\"PacientesEstudiados\":\"11\",\"VigentesControlados\":\"4\",\"VigentesDescontrolados\":\"17\"},{\"Mes\":\"11\",\"NoMedidos\":6,\"PacientesControlados\":\"16\",\"PacientesEstadios\":\"12\",\"PacientesEstudiados\":\"15\",\"VigentesControlados\":\"19\",\"VigentesDescontrolados\":\"9\"},{\"Mes\":\"12\",\"NoMedidos\":\"23\",\"PacientesControlados\":\"54\",\"PacientesEstadios\":\"33\",\"PacientesEstudiados\":\"12\",\"VigentesControlados\":\"22\",\"VigentesDescontrolados\":\"41\"}]"
//VigentesControlados, VigentesDescontrolados, NoMedidos para la torta
//NoMedidos, PacientesControlados, PacientesEstadios para la barra

var resp2 = "[{\"Mes\":\"1\",\"Resultado\":\"25\"},{\"Mes\": \"2\",\"Resultado\": \"40\"},{\"Mes\": \"3\",\"Resultado\": \"50\"}]";
var resp3 = "[{\"Mes\":\"1\",\"Resultado\":\"60\"},{\"Mes\": \"2\",\"Resultado\": \"35\"},{\"Mes\": \"3\",\"Resultado\": \"15\"}]";

var months = ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"];
var monthsComplete = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];

var tit0 = 'Numero de pacientes con diabetes  o alguna <br/>comorbilidad: con niveles tensionales inferiores 140/90';
var tit1 = 'Numero de pacientes con diabetes o alguna <br/>comorbilidad';
var tit2 = 'Resultado';

var fecha_inicio = '31-03-2017';
var fecha_fin = '31-05-2017';

var nombreReporte = "getControlHipertensionArterial";

var chartColors = {
    red: 'rgb(255, 99, 132)',
    orange: 'rgb(255, 159, 64)',
    yellow: 'rgb(255, 205, 86)',
    green: 'rgb(75, 192, 192)',
    blue: 'rgb(54, 162, 235)',
    purple: 'rgb(153, 102, 255)',
    grey: 'rgb(201, 203, 207)'
};

function clearcanvas1() {
    $('#chart1').remove(); // this is my <canvas> element
    $('#canvBarra').append('<canvas id="chart1" style="height: 100%;"></canvas>');

    $('#chart2').remove(); // this is my <canvas> element
    $('#canvTorta').append('<canvas id="chart2" style="height: 100%;"></canvas>');
}

$(document).ready(init);

function init() {

    //$('#myfirstchart').hide();
    //$('#myfirstchart2').hide();
    //$('#myfirstchart3').hide();
    //$('#titulo1').hide();
    //$('#titulo2').hide();
    //$('#titulo3').hide();


    $("#titulo2").text("");
    $("#titulo3").text("");
    $('#myfirstchart2').empty();
    $('#myfirstchart2').css('display', 'none');
    $('#myfirstchart3').empty();
    $('#myfirstchart3').css('display', 'none');

    $("#titulo").empty();
    $("#tabla-titulos").empty();
    $("#tabla-datos").empty();
    $("#titulo4").empty();
    $("#tabla-titulos4").empty();
    $("#tabla-datos4").empty();
    $("#inicio").text('');
    $("#fin").text('');
    $("#titulo1").empty();
    $("#myfirstchart").empty();
    $("#tituloTorta").empty();
    $("#tortaGrande").empty();


    $('#meses').css('display', 'none');
    $("#inicio").datepicker();
    $("#fin").datepicker();

    cargarSelectorIPS();

}

function cargarSelectorIPS() {
    $.ajax({
        url: "/Data/getEmpresasList",
        type: "GET",
    }).done(function (respuesta) {
        //--------------------------------
        console.log(respuesta);

        var empr = JSON.parse(respuesta);
        console.log(empr);

        $('#company').empty();
        $('#companyTorta').empty();

        for (var key in empr) {
            $('#company').append('<option value="' + empr[key].Id + '">' + empr[key].Nombre + '</option>');
            $('#companyTorta').append('<option value="' + empr[key].Id + '">' + empr[key].Nombre + '</option>');
            console.log(empr[key].Nombre);
        }
    }).fail(function (response) {
        console.log("error");
        console.log(response);
        //alert(response);
    });
}


function cambiarGrafica() {

    /*$('#tortaGrande').empty();
    console.log(tortaGraf(JSON.parse(torta)));
    graficaTortaTriple(tortaGraf(JSON.parse(torta)), 'tortaGrande');*/
    var indicador = $('#indicador').val();
    var fecha_inicio = $('#inicio').val();
    var fecha_fin = $('#fin').val();
    var company = $('#company').val();
    //console.log(fecha_inicio);
    //console.log(fecha_fin);
    // -------> VALIDAR FECHA INICIO MENOR QUE FECHA FIN !!!!!!!!


    if (fecha_inicio.trim() == '' || fecha_fin.trim() == '' || indicador === 0) {
        mostrarMensaje('Seleccione las fechas de consulta');
    } else {
        var d1 = Date.parse(fecha_inicio);
        var d2 = Date.parse(fecha_fin);
        //console.log(d1);
        //console.log(d2);
        if (d2 < d1) {
            mostrarMensaje("¡La fecha final debe ser mayor a la inicial!");
        } else {


            $("#titulo").empty();
            $("#titulo1").empty();
            $("#titulo4").empty();
            $("#tabla-titulos4").empty();
            $("#tabla-datos4").empty();
            $("#tabla-titulos").empty();
            $("#tabla-datos").empty();
            $('#myfirstchart').empty();
            $('#chart1').empty();
            $('#espacio').css('display', 'none');
            $('#myfirstchart').css('display', 'none');
            clearcanvas1();



            //  ----- > TABLA Y GRAFICA DE BARRAS CON URL DE INDICADORES
            if (indicador == 1) { //Hipertension Arterial
                tit0 = 'Pacientes con diabetes y tensioninferiores a 140/90';
                tit1 = 'Numero de pacientes con diabetes mellitus';
                $("#titulo1").text("Hipertensión arterial");
                nombreReporte = "getControlHipertensionArterial";
                llenarTablaUno(fecha_inicio, fecha_fin, 'myfirstchart', 2, company);
                //$('#myfirstchart').css('display', 'block');
                $('#chart1').css('display', 'block');

            }
            if (indicador == 2) { // H1B1C
                var tit = 'HbA1c';
                tit0 = 'Pacientes en ERC 1-4 con DM, con examen HbA1c en los ultimos 6 meses';
                tit1 = 'Pacientes con ERC 1-4 y DM';
                tit2 = 'Pacientes con ERC 1-4 ,DM y Examen HbA1C vigente con resultado menor a 7%';
                $("#titulo1").text(tit);
                nombreReporte = "getMedicionHbA1c";
                llenarTablaDos(fecha_inicio, fecha_fin, "getControlDiabetesMellitus", company);
                //$('#myfirstchart').css('display', 'block');
                $('#chart1').css('display', 'block');

            }
            if (indicador == 3) { // LDL
                tit0 = 'Pacientes con ERC 1-4 y medicion LDL en el ultimo año';
                tit1 = 'Pacientes con ERC 1-4';
                tit2 = 'Pacientes  con ERC 1-4, medicion LDL en el ultimo año con resultados inferiores o iguales a 100mg/dl';
                var tit = 'LDL';
                $("#titulo1").text(tit);
                nombreReporte = "getMedicionLDL";
                llenarTablaDos(fecha_inicio, fecha_fin, "getControlLDL", company);
                var an = JSON.parse(torta);
                //$('#myfirstchart').css('display', 'block');
                $('#chart1').css('display', 'block');
                //barraTotalIndicador(an, 'myfirstchart');                
                /*var tit = 'LDL';
                $("#titulo1").text(tit);
                nombreReporte = "getMedicionLDL";
                llenarTablaDos(fecha_inicio, fecha_fin, "getControlLDL");
                $('#myfirstchart').css('display', 'block');*/
            }
            // Albuminuria Creatinina PTH   ---> TFG???????????????
            if (indicador == 4) { //  Albuminuria
                tit0 = 'Pacientes con ERC 1-4 y con medicion de albuminuria en el ultimo año';
                tit1 = 'Pacientes con ERC 1-4';
                var tit = 'Albuminuria';
                $("#titulo1").text(tit);
                nombreReporte = "getMedicionAlbuminuria";
                llenarTablaUno(fecha_inicio, fecha_fin, 'myfirstchart', 1, company);
                //$('#myfirstchart').css('display', 'block');
                $('#chart1').css('display', 'block');
            }
            if (indicador == 5) { // Creatinina
                tit0 = 'Pacientes con ERC 1-4 y medicion de creatinina en el ultimo año';
                tit1 = 'Pacientes con ERC 1-4';
                var tit = 'Creatinina';
                $("#titulo1").text(tit);
                nombreReporte = "getMedicionCreatina";
                llenarTablaUno(fecha_inicio, fecha_fin, 'myfirstchart', 1, company);
                //$('#myfirstchart').css('display', 'block');
                $('#chart1').css('display', 'block');
            }
            if (indicador == 6) { //  TFG
                tit0 = 'Pacientes con ERC 1-4 Con disminucion de TFG de menos de 5ml/min/1.733m² en 1 año';
                tit1 = 'Pacientes con ERC 1-4';
                var tit = 'TFG';
                $("#titulo1").text(tit);
                nombreReporte = "getProgresionRenal";
                llenarTablaUno(fecha_inicio, fecha_fin, 'myfirstchart', 2, company);
                //$('#myfirstchart').css('display', 'block');
                $('#chart1').css('display', 'block');
            }
            if (indicador == 7) { //  PTH 3
                tit0 = 'Pacientes con ERC 3 y con medicion de PTH en el ultimo año';
                tit1 = 'Pacientes con ERC 3';
                tit2 = 'Pacientes con ERC 3, con medicion de PTH en el ultimo año, entre 35 y 70';
                var tit = 'PTH 3';
                $("#titulo1").text(tit);
                nombreReporte = "getMedicionPTH3";
                llenarTablaDos(fecha_inicio, fecha_fin, "getControlPTH3", company);
                //$('#myfirstchart').css('display', 'block');
                $('#chart1').css('display', 'block');
            }
            if (indicador == 8) { //  PTH 4
                tit0 = 'Pacientes con ERC 4 y con medicion de PTH en el ultimo semestre';
                tit1 = 'Pacientes con ERC 4';
                tit2 = 'Pacientes con ERC 4, con medicion de PTH en el ultimo semestre, entre 70 y 110';
                var tit = 'PTH 4';
                $("#titulo1").text(tit);
                nombreReporte = "getMedicionPTH4";
                llenarTablaDos(fecha_inicio, fecha_fin, "getControlPTH4", company);
                //$('#myfirstchart').css('display', 'block');
                $('#chart1').css('display', 'block');
            }
            if (indicador == 9) { //  PTH 5
                tit0 = 'Pacientes con ERC 5 y con medicion de PTH en el ultimo Trimestre';
                tit1 = 'Pacientes con ERC 5';
                tit2 = 'Pacientes con ERC 5, con medicion de PTH en el ultimo trimestre, entre 150 y 300';
                var tit = 'PTH 5';
                $("#titulo1").text(tit);
                nombreReporte = "getMedicionPTH5";
                llenarTablaDos(fecha_inicio, fecha_fin, "getControlPTH5", company);
                //$('#myfirstchart').css('display', 'block');
                $('#chart1').css('display', 'block');
            }
        }
        //------------->

        /*if (indicador == 3) {
            $("#titulo1").text("Medicion de albuminaria");
            medicionDeAlbuminuria(fecha_inicio, fecha_fin);
            $('#myfirstchart').css('display', 'block');

            $("#titulo2").text("Progresión de la enfermedad renal TFG");
            progresionTFG(fecha_inicio, fecha_fin);
            $('#myfirstchart2').css('display', 'block');

            $("#titulo3").text("Tiempo de creatinina");
            tiempoDeCreatinina(fecha_inicio, fecha_fin);
            $('#myfirstchart3').css('display', 'block');
        }
        if (indicador == 4) {
            $("#titulo1").text("Control PTH Estadio 5");
            PTH5medicion(fecha_inicio, fecha_fin);
            $('#myfirstchart').css('display', 'block');

            $("#titulo2").text("");
            $("#titulo3").text("");

            $('#myfirstchart2').empty();
            $('#myfirstchart2').css('display', 'none');
            $('#myfirstchart3').empty();
            $('#myfirstchart3').css('display', 'none');
        }
        if (indicador == 5) {
            $("#titulo1").text("Control PTH Estadio 4");
            PTH4medicion(fecha_inicio, fecha_fin);
            $('#myfirstchart').css('display', 'block');

            $("#titulo2").text("");
            $("#titulo3").text("");
            $('#myfirstchart2').empty();
            $('#myfirstchart2').css('display', 'none');
            $('#myfirstchart3').empty();
            $('#myfirstchart3').css('display', 'none');
        }
        if (indicador == 6) {
            $("#titulo1").text("Control PTH Estadio 3");
            PTH3medicion(fecha_inicio, fecha_fin);
            $('#myfirstchart').css('display', 'block');

            $("#titulo2").text("");
            $("#titulo3").text("");

            $('#myfirstchart2').empty();
            $('#myfirstchart2').css('display', 'none');
            $('#myfirstchart3').empty();
            $('#myfirstchart3').css('display', 'none');
        }
        /*$('#myfirstchart').empty();
        if (indicador == 1) {

            $("#titulo1").text("Control de hipertensión arterial");
            controlHipertensionArterial(fecha_inicio, fecha_fin);
            $('#myfirstchart').css('display', 'block');

            $("#titulo2").text("Control de HbA1c");
            medicionHBA1C(fecha_inicio, fecha_fin);
            $('#myfirstchart2').css('display', 'block');

            $("#titulo3").text("");
            $('#myfirstchart3').empty();
            $('#myfirstchart3').css('display', 'none');
            

        }
        if (indicador == 2) {
            $("#titulo1").text("Control LDL");
            medicionLDL(fecha_inicio, fecha_fin);
            $('#myfirstchart').css('display', 'block');

            $("#titulo2").text("");
            $("#titulo3").text("");

            $('#myfirstchart2').empty();
            $('#myfirstchart2').css('display', 'none');
            $('#myfirstchart3').empty();
            $('#myfirstchart3').css('display', 'none');

        }
        if (indicador == 3) {
            $("#titulo1").text("Medicion de albuminaria");
            medicionDeAlbuminuria(fecha_inicio, fecha_fin);
            $('#myfirstchart').css('display', 'block');

            $("#titulo2").text("Progresión de la enfermedad renal TFG");
            progresionTFG(fecha_inicio, fecha_fin);
            $('#myfirstchart2').css('display', 'block');

            $("#titulo3").text("Tiempo de creatinina");
            tiempoDeCreatinina(fecha_inicio, fecha_fin);
            $('#myfirstchart3').css('display', 'block');
        }
        if (indicador == 4) {
            $("#titulo1").text("Control PTH Estadio 5");
            PTH5medicion(fecha_inicio, fecha_fin);
            $('#myfirstchart').css('display', 'block');

            $("#titulo2").text("");
            $("#titulo3").text("");

            $('#myfirstchart2').empty();
            $('#myfirstchart2').css('display', 'none');
            $('#myfirstchart3').empty();
            $('#myfirstchart3').css('display', 'none');
        }
        if (indicador == 5) {
            $("#titulo1").text("Control PTH Estadio 4");
            PTH4medicion(fecha_inicio, fecha_fin);
            $('#myfirstchart').css('display', 'block');

            $("#titulo2").text("");
            $("#titulo3").text("");
            $('#myfirstchart2').empty();
            $('#myfirstchart2').css('display', 'none');
            $('#myfirstchart3').empty();
            $('#myfirstchart3').css('display', 'none');
        }
        if (indicador == 6) {
            $("#titulo1").text("Control PTH Estadio 3");
            PTH3medicion(fecha_inicio, fecha_fin);
            $('#myfirstchart').css('display', 'block');

            $("#titulo2").text("");
            $("#titulo3").text("");

            $('#myfirstchart2').empty();
            $('#myfirstchart2').css('display', 'none');
            $('#myfirstchart3').empty();
            $('#myfirstchart3').css('display', 'none');
        }*/
    }
}

function cambiarGrafica2() {
    /*$('#tortaGrande').empty();
    console.log(tortaGraf(JSON.parse(torta)));
    graficaTortaTriple(tortaGraf(JSON.parse(torta)), 'tortaGrande');*/

    var indicador = $('#indicadorTorta').val();
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var companyTorta = $('#companyTorta').val();
    //console.log(mes);
    //console.log(ano);
    clearcanvas1();
    if (mes == 0 || ano == 0) {
        mostrarMensaje('Seleccione las opciones de consulta');
    } else {
        var fecha = mes + '/01' + '/' + ano;//(new Date().getFullYear().toString());
        //console.log(fecha_inicio);
        //  ----- > TABLA Y GRAFICA DE BARRAS CON URL DE INDICADORES
        if (indicador == 1) { //Hipertension Arterial
            tit0 = 'Pacientes con diabetes y tensión inferiores a 140/90';
            tit1 = "Pacientes con diabetes y tensión superior a 140/90";
            tit2 = "";
            $("#tituloTorta").text("Hipertensión arterial");
            nombreReporte = "getHipertensionArterial";
            graficaTorta(fecha, 2, companyTorta);
            //$('#tortaGrande').css('display', 'block');
            $('#chart2').css('display', 'block');

            $("#lab1").text(tit0);
            $("#lab2").text(tit1);
            $("#lab3").text(tit2);

        }
        if (indicador == 2) { // H1B1C
            tit0 = 'Pacientes  en ERC 1-4 con DM, con Examen HbA1C vencido';
            tit1 = "Pacientes con ERC 1-4 ,DM y Examen HbA1C vigente con resultado menor a 7%";
            tit2 = "Pacientes con ERC 1-4 , DM y Examen HbA1C vigente con resultado mayor a 7%";
            $("#tituloTorta").text("HbA1c");
            nombreReporte = "getHba1c";
            graficaTorta(fecha, 1, companyTorta);
            //$('#tortaGrande').css('display', 'block');
            $('#chart2').css('display', 'block');
        }
        if (indicador == 3) { // LDL 
            tit0 = 'Pacientes con ERC 1-4 y medicion LDL Vencida';
            tit1 = "Pacientes  con ERC 1-4, medicion LDL en el ultimo año con resultados inferiores o iguales a 100mg/dl";
            tit2 = "Pacientes con ERC 1-4 y LDL  en el ultimo año con resultado mayor a 100mg/dl";
            $("#tituloTorta").text("LDL");
            nombreReporte = "getLDL";
            graficaTorta(fecha, 1, companyTorta);
            //$('#tortaGrande').css('display', 'block');
            $('#chart2').css('display', 'block');
        }
        // Albuminuria Creatinina PTH   ---> TFG???????????????
        if (indicador == 4) { //  Albuminuria
            tit0 = 'Pacientes con ERC 1-4 y con medicion de albuminuria en el ultimo año';
            tit1 = "Pacientes con ERC 1-4 y con medicion de albuminuria vencida";
            tit2 = "";
            $("#tituloTorta").text("Albuminuria");
            nombreReporte = "getAlbuminuria";
            graficaTorta(fecha, 3, companyTorta);
            //$('#tortaGrande').css('display', 'block');
            $('#chart2').css('display', 'block');
        }
        if (indicador == 5) { // Creatinina 
            tit0 = 'Creatinina Pacientes con ERC 1-4 y medicion de creatinina en el ultimo año';
            tit1 = "Pacientes con ERC 1-4 con medicion de creatinina vencida";
            tit2 = "";
            $("#tituloTorta").text("Creatinina");
            nombreReporte = "getCreatinina";
            graficaTorta(fecha, 3, companyTorta);
            //$('#tortaGrande').css('display', 'block');
            $('#chart2').css('display', 'block');
        }
        if (indicador == 6) { //  TFG
            tit0 = 'TFG Pacientes con ERC 1-4 Con disminucion de TFG de menos de 5ml/min/1.733m² en 1 año';
            tit1 = "Pacientes con ERC 1-4 Con disminucion de TFG de mayor de 5ml/min/1.733m² en 1 año";
            tit2 = "";
            $("#tituloTorta").text("TFG");
            nombreReporte = "getProgEnfRenal";
            graficaTorta(fecha, 2, companyTorta);
            //$('#tortaGrande').css('display', 'block');
            $('#chart2').css('display', 'block');
        }
        if (indicador == 7) { //  PTH 3
            tit0 = 'Pacientes con ERC 3 y con examen PTH vencido';
            tit1 = "Pacientes con ERC 3, con medicion de PTH en el ultimo año, entre 35 y 70";
            tit2 = "Pacientes con ERC 3, con medicion de PTH en el ultimo año por fuera del rango de 35 Y  70";
            $("#tituloTorta").text("PTH 3");
            nombreReporte = "getPTH3";
            graficaTorta(fecha, 1, companyTorta);
            //$('#tortaGrande').css('display', 'block');
            $('#chart2').css('display', 'block');
        }
        if (indicador == 8) { //  PTH 4
            tit0 = 'Pacientes con ERC 4 y con examen PTH vencido';
            tit1 = "Pacientes con ERC 4, con medicion de PTH en el ultimo semestre, entre 70 y 110";
            tit2 = "Pacientes con ERC 4, con medicion de PTH en el ultimo semestre por fuera del rango de70 Y  110";
            $("#tituloTorta").text("PTH 4");
            nombreReporte = "getPTH4";
            graficaTorta(fecha, 1, companyTorta);
            //$('#tortaGrande').css('display', 'block');
            $('#chart2').css('display', 'block');
        }
        if (indicador == 9) { //  PTH 5
            tit0 = 'Pacientes con ERC 5 y con examen PTH Vencido';
            tit1 = "Pacientes con ERC 5, con medicion de PTH en el ultimo trimestre, entre 150 y 300";
            tit2 = "Pacientes con ERC 5, con medicion de PTH en el ultimo trimestre por fuera del rango de 150 Y  300";
            $("#tituloTorta").text("PTH 5");
            nombreReporte = "getPTH5";
            graficaTorta(fecha, 1, companyTorta);
            //$('#tortaGrande').css('display', 'block');
            $('#chart2').css('display', 'block');
        }
    }
}

function cambiarVistaBarra() {
    init();
    $('#meses').css('display', 'none');
    $('#fechas').css('display', 'block');
    document.getElementById("tortas").style.background = '#8a8a8a';
    document.getElementById("barra").style.background = '#103d4d';

}

function cambiarVistaTorta() {
    init();
    $('#fechas').css('display', 'none');
    $('#meses').css('display', 'block');
    $("tortas").removeClass("btn-gray");
    document.getElementById("barra").style.background = '#8a8a8a';
    document.getElementById("tortas").style.background = '#103d4d';
}
/*
function llenarTablaUno(fecha_inicio, fecha_fin, chart, tipo) {
    
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        if (respuesta) {
            if (respuesta.status == "SERVER_OK") {
                resp = respuesta.content;
                console.log("respuesta T1 " + respuesta.content);
                var data = datos(JSON.parse(resp));

                //-------

                if (tipo === 1)
                    $("#titulo").text("Medición");
                else
                    $("#titulo").text("Control");
                graficarBarraDoble(resp, chart);

                var tableHeaders = '<tr>';
                tableHeaders += '<th class="centered"> <b>DESCRIPCIÓN  /  MES</b></th>';
                $.each(data[1], function (i, val) {
                    tableHeaders += '<th class="centered">' + val + '</th>';
                });
                tableHeaders += '<tr>';

                $("#tabla-titulos").append(tableHeaders);

                var tableData = "";
                $.each(data[0], function (i, val) {
                    var row = data[0][i];
                    //console.log(row);
                    tableData = '<tr>';
                    if (i === 0) {
                        tableData += '<td><b>' + tit0 + '</b></td>';
                    }
                    if (i === 1) {
                        tableData += '<td ><b>' + tit1 + '</b></td>';
                    }
                    if (i === 2) {
                        tableData += '<td ><b>' + tit2 + '</b></td>';
                    }
                    $.each(row, function (i, val) {
                        tableData += '<td class="centered">' + val + '</td>';
                    });
                    tableData += '</tr>';
                    $("#tabla-datos").append(tableData);
                    //tableHeaders += "<th class="centered">" + val + "</th>";
                });
            } else {
                mostrarMensaje(respuesta.content);
            }
        } else {
            mostrarMensaje("No hay datos en ese periodo de tiempo");
        }           

    });
}

function llenarTablaDos(fecha_inicio, fecha_fin, otroNombre) {
    var respUno;
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte}
    }).done(function (respuesta) {
        if (respuesta) {
            if (respuesta.status == "SERVER_OK") {
                respUno = respuesta.content;                
                nombreReporte = otroNombre;

                $.ajax({
                    url: "/Indicadores/GetReport",
                    type: "GET",
                    data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
                }).done(function (respuesta) {

                    if (respuesta) {
                        if (respuesta.status == "SERVER_OK") {
                            //respUno = respu;
                            var respDos;
                            //respDos = resp;
                            respDos = respuesta.content;

                            var data = datos(JSON.parse(respUno));
                            var data2 = datos(JSON.parse(respDos));

                            //--- Tabla 1
                            $("#titulo").text("Medición");
                            var tableHeaders = '<tr>';
                            tableHeaders += '<th class="centered"> <b>DESCRIPCIÓN  /  MES</b></th>';
                            $.each(data[1], function (i, val) {
                                if (val === '') {

                                } else {
                                    tableHeaders += '<th class="centered">' + val + '</th>';
                                }
                            });
                            tableHeaders += '<tr>';
                            $("#tabla-titulos").append(tableHeaders);

                            var tableData = "";
                            $.each(data[0], function (i, val) {
                                var row = data[0][i];
                                //console.log(row);
                                tableData = '<tr>';
                                if (i === 0) {
                                    tableData += '<td><b>' + tit0 + '</b></td>';
                                }
                                if (i === 1) {
                                    tableData += '<td ><b>' + tit1 + '</b></td>';
                                }
                                if (i === 2) {
                                    tableData += '<td ><b>' + tit2 + '</b></td>';
                                }
                                $.each(row, function (i, val) {
                                    tableData += '<td class="centered">' + val + '</td>';
                                });
                                tableData += '</tr>';
                                $("#tabla-datos").append(tableData);
                                //tableHeaders += "<th class="centered">" + val + "</th>";
                            });

                            //--- Tabla dos
                            $("#titulo4").text("Control");
                            tableHeaders = '<tr>';
                            tableHeaders += '<th class="centered"> <b>DESCRIPCIÓN  /  MES</b></th>';
                            $.each(data2[1], function (i, val) {
                                if (val === '') {

                                } else {
                                    tableHeaders += '<th class="centered">' + val + '</th>';
                                }
                            });
                            tableHeaders += '<tr>';

                            $("#tabla-titulos4").append(tableHeaders);

                            tableData = "";
                            $.each(data2[0], function (i, val) {
                                var row = data2[0][i];
                                //console.log(row);
                                tableData = '<tr>';
                                if (i === 0) {
                                    tableData += '<td><b>' + tit0 + '</b></td>';
                                }
                                if (i === 1) {
                                    tableData += '<td ><b>' + tit1 + '</b></td>';
                                }
                                if (i === 2) {
                                    tableData += '<td ><b>' + tit2 + '</b></td>';
                                }
                                $.each(row, function (i, val) {
                                    tableData += '<td class="centered">' + val + '</td>';
                                });
                                tableData += '</tr>';
                                $("#tabla-datos4").append(tableData);
                                //tableHeaders += "<th class="centered">" + val + "</th>";
                            });

                            var graf = agregarValoresSuperpuesta(JSON.parse(respUno), JSON.parse(respDos));
                            console.log(graf);

                            graficarBarraTriple(graf, 'myfirstchart');
                        } else {
                            mostrarMensaje(respuesta.content);
                        }
                    } else {
                        mostrarMensaje("No hay datos en ese periodo de tiempo");
                    }
                });


            } else {
                mostrarMensaje(respuesta.content);
            }
        } else {
            mostrarMensaje("No hay datos en ese periodo de tiempo");
        }
    });
}
*/ // LLenar tablas con morris

function llenarTablaUno(fecha_inicio, fecha_fin, chart, tipo, company) {

    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte, "company": company }
    }).done(function (respuesta) {
        if (respuesta) {
            if (respuesta.status == "SERVER_OK") {
                resp = respuesta.content;
                //console.log("respuesta T1 " + respuesta.content);
                var data = datos(JSON.parse(resp));

                //-------


                var color = Chart.helpers.color;
                var ctx = document.getElementById("chart1").getContext('2d');
                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: data[1],
                        backgroundColor: color(chartColors.red).alpha(0.5).rgbString(),
                        borderColor: chartColors.red,
                        borderWidth: 1,
                        datasets: [{
                            label: '[1]',
                            backgroundColor: color(chartColors.blue).alpha(0.5).rgbString(),
                            borderColor: chartColors.blue,
                            borderWidth: 1,
                            data: data[0][0]
                        }, {
                            label: '[2]',
                            backgroundColor: color(chartColors.red).alpha(0.5).rgbString(),
                            borderColor: chartColors.red,
                            borderWidth: 1,
                            data: data[0][1]
                        }]
                    },
                    options: {
                        responsive: true,
                        legend: {
                            position: 'top',
                        },
                        /*title: {
                            display: true,
                            text: 'Chart.js Bar Chart'
                        },*/
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true
                                }
                            }]
                        }
                    }
                });


                //           graficarBarraDoble(resp, chart);
                if (tipo === 1)
                    $("#titulo").text("Medición");
                else
                    $("#titulo").text("Control");


                var tableHeaders = '<tr>';
                tableHeaders += '<th class="centered"> <b>DESCRIPCIÓN  /  MES</b></th>';
                $.each(data[1], function (i, val) {
                    tableHeaders += '<th class="centered">' + val + '</th>';
                });
                tableHeaders += '<tr>';

                $("#tabla-titulos").append(tableHeaders);

                var tableData = "";
                $.each(data[0], function (i, val) {
                    var row = data[0][i];
                    //console.log(row);
                    tableData = '<tr>';
                    if (i === 0) {
                        tableData += '<td><b>[1] ' + tit0 + '</b></td>';
                    }
                    if (i === 1) {
                        tableData += '<td ><b>[2] ' + tit1 + '</b></td>';
                    }
                    if (i === 2) {
                        tableData += '<td ><b> Resultado </b></td>';
                    }
                    $.each(row, function (i, val) {
                        tableData += '<td class="centered">' + val + '</td>';
                    });
                    tableData += '</tr>';
                    $("#tabla-datos").append(tableData);
                    //tableHeaders += "<th class="centered">" + val + "</th>";
                });
            } else {
                mostrarMensaje(respuesta.content);
            }
        } else {
            mostrarMensaje("No hay datos en ese periodo de tiempo");
        }
    });
}

function llenarTablaDos(fecha_inicio, fecha_fin, otroNombre, company) {
    var respUno;
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte, "company": company }
    }).done(function (respuesta) {
        if (respuesta) {
            if (respuesta.status == "SERVER_OK") {
                respUno = respuesta.content;
                nombreReporte = otroNombre;

                $.ajax({
                    url: "/Indicadores/GetReport",
                    type: "GET",
                    data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte, "company": company }
                }).done(function (respuesta) {

                    if (respuesta) {
                        if (respuesta.status == "SERVER_OK") {
                            //respUno = respu;
                            var respDos;
                            respDos = resp;
                            respDos = respuesta.content;
                            //console.log("resp1 " + respUno);
                            //console.log("resp2 " + respDos);

                            var data = datos(JSON.parse(respUno));
                            var data2 = datos(JSON.parse(respDos));

                            var color = Chart.helpers.color;
                            var ctx = document.getElementById("chart1").getContext('2d');
                            var myChart = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: data[1],
                                    backgroundColor: color(chartColors.red).alpha(0.5).rgbString(),
                                    borderColor: chartColors.red,
                                    borderWidth: 1,
                                    datasets: [{
                                        label: '[2]',
                                        backgroundColor: color(chartColors.red).alpha(0.5).rgbString(),
                                        borderColor: chartColors.red,
                                        borderWidth: 1,
                                        data: data[0][1]
                                    }, {
                                        label: '[1]',
                                        backgroundColor: color(chartColors.blue).alpha(0.5).rgbString(),
                                        borderColor: chartColors.blue,
                                        borderWidth: 1,
                                        data: data[0][0]
                                    }, {
                                        label: '[3]',
                                        backgroundColor: color(chartColors.yellow).alpha(0.5).rgbString(),
                                        borderColor: chartColors.yellow,
                                        borderWidth: 1,
                                        data: data2[0][0]
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    legend: {
                                        position: 'top',
                                    },
                                    /*title: {
				                        display: true,
				                        text: 'Chart.js Bar Chart'
				                    },*/
                                    scales: {
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    }
                                }
                            });




                            //--- Tabla 1
                            $("#titulo").text("Medición");
                            var tableHeaders = '<tr>';
                            tableHeaders += '<th class="centered"> <b>DESCRIPCIÓN  /  MES</b></th>';
                            $.each(data[1], function (i, val) {
                                if (val === '') {

                                } else {
                                    tableHeaders += '<th class="centered">' + val + '</th>';
                                }
                            });
                            tableHeaders += '<tr>';
                            $("#tabla-titulos").append(tableHeaders);

                            var tableData = "";
                            $.each(data[0], function (i, val) {
                                var row = data[0][i];
                                //console.log(row);
                                tableData = '<tr>';
                                if (i === 0) {
                                    tableData += '<td><b>[1] ' + tit0 + '</b></td>';
                                }
                                if (i === 1) {
                                    tableData += '<td ><b>[2] ' + tit1 + '</b></td>';
                                }
                                if (i === 2) {
                                    tableData += '<td ><b> Resultado  </b></td>';
                                }
                                $.each(row, function (i, val) {
                                    tableData += '<td class="centered">' + val + '</td>';
                                });
                                tableData += '</tr>';
                                $("#tabla-datos").append(tableData);
                                //tableHeaders += "<th class="centered">" + val + "</th>";
                            });

                            // toto poner nuevos labels

                            //--- Tabla dos
                            $("#titulo4").text("Control");
                            tableHeaders = '<tr>';
                            tableHeaders += '<th class="centered"> <b>DESCRIPCIÓN  /  MES</b></th>';
                            $.each(data2[1], function (i, val) {
                                if (val === '') {

                                } else {
                                    tableHeaders += '<th class="centered">' + val + '</th>';
                                }
                            });
                            tableHeaders += '<tr>';

                            $("#tabla-titulos4").append(tableHeaders);

                            tableData = "";
                            $.each(data2[0], function (i, val) {
                                var row = data2[0][i];
                                //console.log(row);
                                tableData = '<tr>';
                                if (i === 0) {
                                    tableData += '<td><b>[3] ' + tit2 + '</b></td>';
                                }
                                if (i === 1) {
                                    tableData += '<td ><b>' + tit0 + '</b></td>';
                                }
                                if (i === 2) {
                                    tableData += '<td ><b> Resultado </b></td>';
                                }
                                $.each(row, function (i, val) {
                                    tableData += '<td class="centered">' + val + '</td>';
                                });
                                tableData += '</tr>';
                                $("#tabla-datos4").append(tableData);
                                //tableHeaders += "<th class="centered">" + val + "</th>";
                            });

                            /*var graf = agregarValoresSuperpuesta(JSON.parse(respUno), JSON.parse(respDos));
                            console.log(graf);
                            graficarBarraTriple(graf, 'myfirstchart');*/


                        } else {
                            mostrarMensaje(respuesta.content);
                        }
                    } else {
                        mostrarMensaje("No hay datos en ese periodo de tiempo");
                    }
                });


            } else {
                mostrarMensaje(respuesta.content);
            }
        } else {
            mostrarMensaje("No hay datos en ese periodo de tiempo");
        }
    });
}

function graficarBarraDoble(respuesta, element) {
    var datos = JSON.parse(respuesta);
    new Morris.Bar({
        // ID of the element in which to draw the chart.
        element: element,
        data: datos,
        // The name of the data record attribute that contains x-values.
        xkey: 'Mes',
        // A list of names of data record attributes that contain y-values.
        ykeys: ['Numerador', 'Denominador'],
        // Labels for the ykeys -- will be displayed when you hover over the
        // chart.
        labels: [tit0, tit1],
        resize: 'true',
        xLabelFormat: function (x) {
            /*console.log(x);
            var start = new Date(x);
            var y = start.getFullYear();
            //console.log(y.toString().substring(2));

            return months[y.toString().substring(2) - 1];*/
            //console.log(monthsComplete[x.src.Mes - 1]);
            return monthsComplete[x.src.Mes - 1];
        }/*,

        yLabelFormat: function (x) {
            return x.toString() + '%';
        }*/
    });
}

function graficarBarraTriple(respuesta, element) {
    //var datos = JSON.parse(respuesta);
    new Morris.Bar({
        // ID of the element in which to draw the chart.
        element: element,
        data: respuesta,
        // The name of the data record attribute that contains x-values.
        xkey: 'Mes',
        // A list of names of data record attributes that contain y-values.
        ykeys: ['Numerador', 'Denominador', 'Proceso'],
        // Labels for the ykeys -- will be displayed when you hover over the
        // chart.
        labels: [tit0, tit1, tit2],
        resize: 'true',

        xLabelFormat: function (x) {
            //console.log(monthsComplete[x.src.Mes - 1]);
            return monthsComplete[x.src.Mes - 1];
        }/*,

        yLabelFormat: function (x) {
            return x.toString();
        }*/
    });
}

function barraTotalIndicador(respuesta, element) {

    new Morris.Bar({
        // ID of the element in which to draw the chart.
        element: element,
        data: respuesta,
        // The name of the data record attribute that contains x-values.
        xkey: 'Mes',
        // A list of names of data record attributes that contain y-values.
        ykeys: ['NoMedidos', 'PacientesControlados', 'PacientesEstadios'],
        // Labels for the ykeys -- will be displayed when you hover over the
        // chart.
        labels: [tit0, tit1, tit2],
        resize: 'true',

        xLabelFormat: function (x) {
            //console.log(monthsComplete[x.src.Mes - 1]);
            return monthsComplete[x.src.Mes - 1];
        }/*,

        yLabelFormat: function (x) {
            return x.toString();
        }*/
    });


    //)};
}

function datos(son) {
    var retorno = [];
    var meses = [];
    var merge = [];
    var nume = [];
    var deno = [];
    var resu = [];
    for (var i = 0; i < son.length; i++) {
        var obj2 = son[i];
        for (var key in obj2) {
            var attrName = key;
            var attrValue = obj2[key];
            if (key === 'Mes') {
                //console.log("mes que llega " + attrValue);
                meses.push(monthsComplete[attrValue - 1] + ' ' + obj2['Year']);
                //$(tabla.column(i).header()).text(months[attrValue-1]);	    	
            }
            if (key === 'Resultado') {
                var resul = attrValue * 100;
                resu.push(resul.toFixed(2) + '%');
            }
            if (key === 'Numerador') {
                nume.push(attrValue);
            }
            if (key === 'Denominador') {
                deno.push(attrValue);
            }

        }
    }
    merge.push(nume);
    merge.push(deno);
    merge.push(resu);
    retorno.push(merge);
    retorno.push(meses);
    //console.log(retorno);
    return retorno;
}

function tortaGraf(son, doble) {
    var retorno = [];
    var uno = [];
    var dos = [];
    var tres = [];
    var torta = [];
    var resu = [];

    // Nueva Gráfica
    var labe = [];
    var dat = [];
    var col = [];

    for (var i = 0; i < son.length; i++) {
        var obj2 = son[i];
        for (var key in obj2) {
            var attrName = key;
            var attrValue = obj2[key];
            /*if (key === 'Mes') {
                meses.push(monthsComplete[attrValue - 1]);
                //$(tabla.column(i).header()).text(months[attrValue-1]);	    	
            }*/

            // todo poner en orden los LABELS


            if (doble === 1) { // TRIPLEs
                if (key === 'NoMedidos') {
                    tres["label"] = tit0;
                    tres["value"] = attrValue;
                    dat.push(attrValue);
                    labe.push(tit0);
                    col.push(chartColors.orange);
                }
                if (key === 'VigentesControlados') {
                    uno["label"] = tit1;
                    uno["value"] = attrValue;
                    dat.push(attrValue);
                    //console.log(tit0);
                    labe.push(tit1);
                    col.push(chartColors.green);
                }
                if (key === 'VigentesDescontrolados') {
                    dos["label"] = tit2;
                    dos["value"] = attrValue;
                    dat.push(attrValue);
                    labe.push(tit2);
                    col.push(chartColors.purple);
                }
            }
            if (doble === 2) { // VIGENTES TFG E HIPERTENSION
                if (key === 'VigentesControlados') {
                    uno["label"] = tit0;
                    uno["value"] = attrValue;
                    dat.push(attrValue);
                    labe.push(tit0);
                    col.push(chartColors.green);
                }
                if (key === 'VigentesDescontrolados') {
                    dos["label"] = tit1;
                    dat.push(attrValue);
                    labe.push(tit1);
                    col.push(chartColors.purple);
                }
            }
            if (doble === 3) {// NO MEDIDOS albuminuria y creatinina
                if (key === 'PacientesEstudiados') {
                    uno["label"] = tit0;
                    uno["value"] = attrValue;
                    dat.push(attrValue);
                    labe.push(tit0);
                    col.push(chartColors.orange);
                }
                if (key === 'NoMedidos') {
                    dos["label"] = tit1;
                    dos["value"] = attrValue;
                    dat.push(attrValue);
                    labe.push(tit1);
                    col.push(chartColors.purple);
                }
            }
        }
        if (i === 1)
            break;
    }
    /*retorno.push(uno);
    retorno.push(dos);
    retorno.push(tres);*/
    retorno.push(dat);
    retorno.push(labe);
    retorno.push(col);
    return retorno;
}

function graficaTorta(fechaMes, doble, companyTorta) {

    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fechaMes, "fecha_fin": fechaMes, "nombreReporte": nombreReporte, "company": companyTorta }
    }).done(function (respuesta) {

        if (respuesta) {
            if (respuesta.status == "SERVER_OK") {

                var respu = tortaGraf(JSON.parse(respuesta.content), doble);

                //var respu = tortaGraf(JSON.parse(torta), doble);
                //console.log(respu);
                var color = Chart.helpers.color;
                var ctx = document.getElementById("chart2").getContext('2d');
                var myChart = new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: respu[1],
                        datasets: [{
                            backgroundColor: respu[2],
                            data: respu[0]
                        }]
                    },
                    options: {
                        responsive: true,
                        legend: {
                            position: 'top',
                        },
                        /*title: {
                            display: true,
                            text: 'Chart.js Bar Chart'
                        },
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero:true
                                }
                            }]
                        }*/
                    }
                });


                /*
                new Morris.Donut({
                    // ID of the element in which to draw the chart.
                    element: 'tortaGrande',
                    data: respu
                });*/



            } else {
                mostrarMensaje(respuesta.content);
            }

        } else {
            mostrarMensaje("No hay datos en ese periodo de tiempo");
        }

    });
}

//--------------------------------------------------------------------->

function controlHipertensionArterial(fecha_inicio, fecha_fin) {
    var nombreReporte = "getControlHipertensionArterial";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        //console.log(respuesta);
        respuesta = resp;
        var datos = JSON.parse(respuesta);
        new Morris.Line({
            // ID of the element in which to draw the chart.
            element: 'myfirstchart',

            data: datos,
            // The name of the data record attribute that contains x-values.
            xkey: 'Mes',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['Resultado'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Valor'],
            eventStrokeWidth: 0,
            resize: 'true',
            ymax: 'auto',
            padding: 50,

            xLabelFormat: function (x) {
                //console.log(x.getFullYear().toString().substring(2));

                return months[x.getFullYear().toString().substring(2) - 1];
            },

            yLabelFormat: function (x) {
                return x.toString() + '%';
            }
        });
    });
}

function medicionHBA1C(fecha_inicio, fecha_fin) {
    var nombreReporte = "getMedicionHbA1c";
    var R2;
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        R2 = respuesta;
        conrolDeDiabetesMelitus(R2, fecha_inicio, fecha_fin);
    });
}

function conrolDeDiabetesMelitus(R2, fecha_inicio, fecha_fin) {
    var nombreReporte = "getControlDiabetesMellitus";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        //console.log(respuesta);

        R2 = resp;
        respuesta = resp2;
        //UNIR R2 Y respuesta para graficar la superposición

        var datos = agregarValoresSuperpuesta(JSON.parse(R2), JSON.parse(respuesta));

        //console.log(datos);

        new Morris.Line({
            // ID of the element in which to draw the chart.
            element: 'myfirstchart2',

            data: datos,
            // The name of the data record attribute that contains x-values.
            xkey: 'Mes',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['Resultado', 'Proceso'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Resultado', 'Proceso'],
            eventStrokeWidth: 0,
            resize: 'true',
            ymax: 'auto',
            padding: 50,

            xLabelFormat: function (x) {
                //console.log('---->');
                var indice = parseInt(x.getFullYear().toString().substring(2)) - 1;
                //console.log(indice);
                return months[indice];
            },

            yLabelFormat: function (x) {
                return x + '%';
            }
        });
    });
}

//--------------------------------------------------------------------->

function medicionLDL(fecha_inicio, fecha_fin) {
    var R4;
    var nombreReporte = "getMedicionLDL";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        R4 = respuesta;
        controlLDL(R4, fecha_inicio, fecha_fin);
    });
}

function controlLDL(R4, fecha_inicio, fecha_fin) {
    var nombreReporte = "getControlLDL";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        //console.log(respuesta);

        R4 = resp;
        respuesta = resp2;
        //UNIR R4 Y respuesta para graficar la superposición


        var datos = agregarValoresSuperpuesta(JSON.parse(R4), JSON.parse(respuesta));

        new Morris.Line({
            // ID of the element in which to draw the chart.
            element: 'myfirstchart',

            data: datos,
            // The name of the data record attribute that contains x-values.
            xkey: 'Mes',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['Resultado', 'Proceso'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Resultado', 'Proceso'],
            eventStrokeWidth: 0,
            resize: 'true',
            ymax: 'auto',
            padding: 50,

            xLabelFormat: function (x) {
                //console.log('---->');
                var indice = parseInt(x.getFullYear().toString().substring(2)) - 1;
                //console.log(indice);
                return months[indice];
            },

            yLabelFormat: function (x) {
                return x + '%';
            }
        });
    });
}

//--------------------------------------------------------------------->

function medicionDeAlbuminuria(fecha_inicio, fecha_fin) {
    var nombreReporte = "getMedicionAlbuminuria";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        //console.log(respuesta);
        respuesta = resp;
        var datos = JSON.parse(respuesta);
        new Morris.Line({
            // ID of the element in which to draw the chart.
            element: 'myfirstchart',

            data: datos,
            // The name of the data record attribute that contains x-values.
            xkey: 'Mes',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['Resultado'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Valor'],
            eventStrokeWidth: 0,
            resize: 'true',
            ymax: 'auto',
            padding: 50,

            xLabelFormat: function (x) {
                //console.log(x.getFullYear().toString().substring(2));

                return months[x.getFullYear().toString().substring(2) - 1];
            },

            yLabelFormat: function (x) {
                return x.toString() + '%';
            }
        });
    });
}

function progresionTFG(fecha_inicio, fecha_fin) {
    var nombreReporte = "getProgresionRenal";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        //console.log(respuesta);
        respuesta = resp;
        var datos = JSON.parse(respuesta);
        new Morris.Line({
            // ID of the element in which to draw the chart.
            element: 'myfirstchart2',

            data: datos,
            // The name of the data record attribute that contains x-values.
            xkey: 'Mes',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['Resultado'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Valor'],
            eventStrokeWidth: 0,
            resize: 'true',
            ymax: 'auto',
            padding: 50,

            xLabelFormat: function (x) {
                //console.log(x.getFullYear().toString().substring(2));

                return months[x.getFullYear().toString().substring(2) - 1];
            },

            yLabelFormat: function (x) {
                return x.toString() + '%';
            }
        });
    });
}

function tiempoDeCreatinina(fecha_inicio, fecha_fin) {
    var nombreReporte = "getMedicionCreatina";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        //console.log(respuesta);
        respuesta = resp;
        var datos = JSON.parse(respuesta);
        new Morris.Line({
            // ID of the element in which to draw the chart.
            element: 'myfirstchart3',

            data: datos,
            // The name of the data record attribute that contains x-values.
            xkey: 'Mes',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['Resultado'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Valor'],
            eventStrokeWidth: 0,
            resize: 'true',
            ymax: 'auto',
            padding: 50,

            xLabelFormat: function (x) {
                //console.log(x.getFullYear().toString().substring(2));

                return months[x.getFullYear().toString().substring(2) - 1];
            },

            yLabelFormat: function (x) {
                return x.toString() + '%';
            }
        });
    });
}

//--------------------------------------------------------------------->
function PTH5medicion(fecha_inicio, fecha_fin) {
    var R9;
    var nombreReporte = "getrMedicionPTH5";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        R9 = respuesta;
        PTH5control(R9, fecha_inicio, fecha_fin);
    });
}

function PTH5control(R9, fecha_inicio, fecha_fin) {
    var nombreReporte = "getControlPTH5";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        //console.log(respuesta);
        R9 = resp;
        respuesta = resp2;
        //UNIR R9 Y respuesta para graficar la superposición

        var datos = agregarValoresSuperpuesta(JSON.parse(R9), JSON.parse(respuesta));
        new Morris.Line({
            // ID of the element in which to draw the chart.
            element: 'myfirstchart',

            data: datos,
            // The name of the data record attribute that contains x-values.
            xkey: 'Mes',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['Resultado', 'Proceso'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Resultado', 'Proceso'],
            eventStrokeWidth: 0,
            resize: 'true',
            ymax: 'auto',
            padding: 50,

            xLabelFormat: function (x) {
                //console.log(x.getFullYear().toString().substring(2));

                return months[x.getFullYear().toString().substring(2) - 1];
            },

            yLabelFormat: function (x) {
                return x.toString() + '%';
            }
        });
    });
}

//--------------------------------------------------------------------->
function PTH4medicion(fecha_inicio, fecha_fin) {
    var R11;
    var nombreReporte = "getrMedicionPTH4";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        R11 = respuesta;
        PTH4control(R11, fecha_inicio, fecha_fin);
    });
}

function PTH4control(R11, fecha_inicio, fecha_fin) {
    var nombreReporte = "getControlPTH4";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        //console.log(respuesta);
        R11 = resp;
        respuesta = resp2;
        //UNIR R11 Y respuesta para graficar la superposición

        var datos = agregarValoresSuperpuesta(JSON.parse(R11), JSON.parse(respuesta));
        new Morris.Line({
            // ID of the element in which to draw the chart.
            element: 'myfirstchart',

            data: datos,
            // The name of the data record attribute that contains x-values.
            xkey: 'Mes',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['Resultado', 'Proceso'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Resultado', 'Proceso'],
            eventStrokeWidth: 0,
            resize: 'true',
            ymax: 'auto',
            padding: 50,

            xLabelFormat: function (x) {
                //console.log(x.getFullYear().toString().substring(2));

                return months[x.getFullYear().toString().substring(2) - 1];
            },

            yLabelFormat: function (x) {
                return x.toString() + '%';
            }
        });
    });
}

//--------------------------------------------------------------------->
function PTH3medicion(fecha_inicio, fecha_fin) {
    var R13;
    var nombreReporte = "getMedicionPTH3";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        R13 = respuesta;
        PTH3control(R13, fecha_inicio, fecha_fin);
    });
}

function PTH3control(R13, fecha_inicio, fecha_fin) {
    var nombreReporte = "getControlPTH3";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte": nombreReporte }
    }).done(function (respuesta) {
        //console.log(respuesta);
        R13 = resp;
        respuesta = resp2;

        //UNIR R13 Y respuesta para graficar la superposición

        var datos = agregarValoresSuperpuesta(JSON.parse(R13), JSON.parse(respuesta));
        new Morris.Line({
            // ID of the element in which to draw the chart.
            element: 'myfirstchart',

            data: datos,
            // The name of the data record attribute that contains x-values.
            xkey: 'Mes',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['Resultado', 'Proceso'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Resultado', 'Proceso'],
            eventStrokeWidth: 0,
            resize: 'true',
            ymax: 'auto',
            padding: 50,

            xLabelFormat: function (x) {
                //console.log(x.getFullYear().toString().substring(2));

                return months[x.getFullYear().toString().substring(2) - 1];
            },

            yLabelFormat: function (x) {
                return x.toString() + '%';
            }
        });
    });
}

//--------------------------------------------------------------------->

function datosSuperpuesta(son) {
    var objeto = [];
    for (var i = 0; i < son.length; i++) {
        var obj2 = son[i];
        for (var key in obj2) {
            var attrName = key;
            var attrValue = obj2[key];
            if (key === 'Denominador') {
                objeto.push(attrValue);
            }
        }
    }
    return objeto;
}

function agregarValoresSuperpuesta(quieta, nueva) {
    var nuevas = datosSuperpuesta(nueva);
    for (var i = 0; i < quieta.length; i++) {
        quieta[i]['Proceso'] = nuevas[i];
    }
    return quieta;
}

