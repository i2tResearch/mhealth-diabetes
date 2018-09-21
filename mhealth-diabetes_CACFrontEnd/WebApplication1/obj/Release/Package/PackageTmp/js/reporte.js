﻿var resp = "[{\"Mes\":\"10\",\"Resultado\":\"10\"},{\"Mes\": \"11\",\"Resultado\": \"40\"},{\"Mes\": \"12\",\"Resultado\": \"25\"}]";
var resp2 = "[{\"Mes\":\"10\",\"Resultado\":\"30\"},{\"Mes\": \"11\",\"Resultado\": \"40\"},{\"Mes\": \"12\",\"Resultado\": \"25\"}]";
var resp3 = "[{\"Mes\":\"10\",\"Resultado\":\"20\"},{\"Mes\": \"11\",\"Resultado\": \"40\"},{\"Mes\": \"12\",\"Resultado\": \"25\"}]";

var months = ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"];

var fecha_inicio = '31-03-2017';
var fecha_fin = '31-05-2017';

function cambiarGrafica() {
    var indicador = $('#indicador').val();
    var fecha_inicio = $('#inicio').val();    
    var fecha_fin = $('#fin').val();
    console.log(fecha_inicio);
    if (fecha_inicio.trim() == '' || fecha_fin.trim() == '') {
        alert('Seleccione las fechas de consulta');
    }else{
        $('#myfirstchart').empty();
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
        }
}


}

$(function () {
    // Function para cargar el combobox. Obtener de api la lista y llenar el combo
    var json = {
        administrador: "Admin",
        eps: "EPS",
        ips: "IPS"
    }
    for (var i = 0; json.length; i++) {
        $('#rol').append('<option value="' + (i + 10) + '">' + (i + 10) + '</option>');
    }

    /*function getResults(str) {
	  $.ajax({
	        url:'suggest.html',
	        type:'POST',
	        data: 'q=' + str,
	        dataType: 'json',
	        success: function( json ) {
	           $.each(json, function(i, optionHtml){
	              $('#myselect').append(optionHtml);
	           });
	        }
	    });
};*/
});

function controlHipertensionArterial(fecha_inicio, fecha_fin) {
    var nombreReporte = "getControlHipertensionArterial";
    $.ajax({
        url: "/Indicadores/GetReport",
        type: "GET",
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
    }).done(function (respuesta) {
        console.log(respuesta);
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
                console.log(x.getFullYear().toString().substring(2));

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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
    }).done(function (respuesta) {
        console.log(respuesta);

        R2 = resp;
        respuesta = resp2;
        //UNIR R2 Y respuesta para graficar la superposición

        var datos = agregarValoresSuperpuesta(JSON.parse(R2), JSON.parse(respuesta));

        console.log(datos);

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
                console.log('---->');
                var indice = parseInt(x.getFullYear().toString().substring(2)) - 1;
                console.log(indice);
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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
    }).done(function (respuesta) {
        console.log(respuesta);

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
                console.log('---->');
                var indice = parseInt(x.getFullYear().toString().substring(2)) - 1;
                console.log(indice);
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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
    }).done(function (respuesta) {
        console.log(respuesta);
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
                console.log(x.getFullYear().toString().substring(2));

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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
    }).done(function (respuesta) {
        console.log(respuesta);
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
                console.log(x.getFullYear().toString().substring(2));

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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
    }).done(function (respuesta) {
        console.log(respuesta);
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
                console.log(x.getFullYear().toString().substring(2));

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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
    }).done(function (respuesta) {
        console.log(respuesta);
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
                console.log(x.getFullYear().toString().substring(2));

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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
    }).done(function (respuesta) {
        console.log(respuesta);
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
                console.log(x.getFullYear().toString().substring(2));

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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
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
        data: { "fecha_inicio": fecha_inicio, "fecha_fin": fecha_fin, "nombreReporte":  nombreReporte }
    }).done(function (respuesta) {
        console.log(respuesta);
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
                console.log(x.getFullYear().toString().substring(2));

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
            if (key === 'Resultado') {
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

$(document).ready(init);

function init() {
    $("#inicio").datepicker();
    $("#fin").datepicker();
    /*$("#titulo1").text("Control de hipertensión arterial");
    controlHipertensionArterial(fecha_inicio, fecha_fin);
    $('#myfirstchart').css('display', 'block');

    $("#titulo2").text("Control de HbA1c");
    medicionHBA1C(fecha_inicio, fecha_fin);
   
    $("#titulo3").text("");
    $('#myfirstchart3').empty();
    $('#myfirstchart3').css('display', 'none');*/
}