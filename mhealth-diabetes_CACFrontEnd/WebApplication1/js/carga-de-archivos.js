$(document).ready(function () {
    $("#file").change(setFileName);
    $("#drag-n-drop").on("dragover", sobreElDiv);
    $("#drag-n-drop").on("dragleave", salirDelDiv);
    $("#drag-n-drop").on("drop", soltarEnDiv);
    $("#siguiente-carga-archivos").click(enviarArchivo);
});

//Metodo de carga-de-archivos.html
function enviarArchivo(event) {
    event.preventDefault();
    if ($("#file").get(0).files.length == 0 && acceptedTypes[$("#file").get(0).type] === true) {
        mostrarMensaje("Debes cargar un archivo para continuar. Arrastre el archivo XLS, CSV, o XLSX a la página", 4000);
    } else {
        doUploadRequest($("#file").get(0).files[0]);
    }
}



//Metodo de carga-de-archivos.html
function setFileName() {
    var filename = $('input[type=file]').val().replace(/C:\\fakepath\\/i, '');
    if (filename != '') {
        $('#file-name').text(filename);
    }
}

//Metodo de carga-de-archivos.html
function doUploadRequest(archivo) {
    mostrarMensaje("Ha comenzado la carga del archivo. Un momento por favor, en breve continuará el proceso de validación", 10000);
    var formData = new FormData();
    formData.append('file', archivo);
    var usuario = JSON.parse(localStorage.getItem("usuario"));
    var user_id = usuario.Id;

    formData.append('user_id', user_id);

    $.ajax({
        url: "/Data/UploadFile",
        cache: false,
        contentType: false,
        processData: false,
        type: 'POST',
        data: formData
    }).done(function (data) {
        if (data) {
            localStorage.setItem("lista_errores", data);
            var obj = JSON.parse(data);
            if (obj.List !== null) {
                gotoAsignarDatosArchivo();
            }
            else if (obj.Archivo !== null) {
                gotoCargaSatisfactoria();
            } else {
                mostrarMensaje("Error DTO", 4000);
            }
        }
    }).fail(function (error) {
        mostrarMensaje(error, 4000);
    });

}

function gotoCargaSatisfactoria() {
    //$(location).attr("href", "/Admin/CargaSatisfactoria");
    $(".content").html(waitloadingGif);
    $.ajax({
        type: "GET",
        url: "/Admin/CargaSatisfactoria"
    }).done(function (html) {
        $(".content").html(html);
    });
}

//Metodo de carga-de-archivos.html
function gotoAsignarDatosArchivo() {
    //$(location).attr("href", "/Admin/AsignarDatosArchivo");
    $(".content").html(waitloadingGif);
    $.ajax({
        type: "GET",
        url: "/Admin/AsignarDatosArchivo"
    }).done(function (html) {
        $(".content").html(html);
    });
}


//Metodo de carga-de-archivos.html
function sobreElDiv(event) {
    event.preventDefault();
    $("html body .layout .content .gray-content").css('background-color', '#d2d2d2');
    $("html body .layout .content .gray-content").css("border", "3px dashed black");
    $("#label-excel").text("Suelte el archivo");
}

//Metodo de carga-de-archivos.html
function salirDelDiv(event) {
    event.preventDefault();
    $("html body .layout .content .gray-content").css('background-color', '#f4f4f5');
    $("html body .layout .content .gray-content").css("border", "0px solid red");
    $("#label-excel").text("Arrastre su archivo a esta zona");
}

//Metodo de carga-de-archivos.html
function soltarEnDiv(event) {
    event.preventDefault();
    var files = event.originalEvent.dataTransfer.files;
    if (files.length === 1 && acceptedTypes[files[0].type] === true) {
        $("html body .layout .content .gray-content").css('background-color', '#f4f4f5');
        $("html body .layout .content .gray-content").css("border", "0px solid red");
        $("#label-excel").text("Cargando...");
        $('#file-name').text(files[0].name);
        doUploadRequest(files[0]);
    } else {
        var mensaje = "";
        if (files.length > 1) {
            mensaje = "No se puede agregar más de 1 archivo. Por favor arrastre sólo 1";
        }else{
            mensaje = "Arrastre un archivo de tipo XLS, CSV, o XLSX a la página";
        }
        mostrarMensaje(mensaje, 4000);
    }

}

var acceptedTypes = {
    'application/vnd.ms-excel': true,
    'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet': true,
    'application/x-zip-compressed': true,
    'xlsx': true,
    'xls': true,
    'csv': true
};
