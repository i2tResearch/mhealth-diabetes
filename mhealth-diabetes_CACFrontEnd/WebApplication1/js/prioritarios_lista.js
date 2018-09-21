//$(document).ready(init);
init();

var lastcolor;

function init() {

    $("#atras").click(function (e) {
        e.preventDefault();
        $(".content").html(waitloadingGif);
        $.ajax({
            type: "GET",
            url: "/Admin/Archivos"
        }).done(function (html) {
            $(".content").html(html);
        });
    });
    localStorage.removeItem('cedula');
    localStorage.removeItem("detallePrioritarios");
    var id_file = localStorage.getItem('file-id');

    if (id_file !== null) {
        $("#lista-prioritarios").html("<tr><td>Cargando...</td><td></td><td></td><td></td><td></td></tr>");
        $.ajax({
            url: "/Data/getListaDePrioritarios",
            type: "GET",
            data: { "file_id": id_file }
        }).done(function (respuesta) {
            ToDownload(respuesta);
            addRow(respuesta);
            //dynamicTable(JSON.parse(respuesta));
        });
    }
    else {
        $(".content").html(waitloadingGif);
        $.ajax({
            type: "GET",
            url: "/Admin/Archivos"
        }).done(function (html) {
            $(".content").html(html);
        });
    }
}

function dynamicTable(listjson) {
    $('#tabla-errores').bind('dynatable:preinit', function (e, dynatable) {
        dynatable.utility.textTransform.myNewStyle = function (text) {
            var ts = "AFA";//text.replace(/\s+/, '_').replace(/[A-Z]/, function ($1) { return $1 + $1 });
            //console.log(ts)
            return ts;
        };
    }).dynatable({
        dataset: {
            records: listjson
        },
        table: {
            defaultColumnIdStyle: 'trimDash'
        },
        inputs: {
            queries: null,
            sorts: null,
            multisort: ['ctrlKey', 'shiftKey', 'metaKey'],
            page: null,
            queryEvent: 'blur change',
            recordCountTarget: null,
            recordCountPlacement: 'after',
            paginationLinkTarget: null,
            paginationLinkPlacement: 'after',
            paginationPrev: 'Anterior',
            paginationNext: 'Siguiente',
            paginationGap: [1, 2, 2, 1],
            searchTarget: null,
            searchPlacement: 'before',
            perPageTarget: null,
            perPagePlacement: 'before',
            perPageText: 'Ver: ',
            recordCountText: 'Mostrar ',
            processingText: 'Cargando...'
        },
    });
}

function verDetalle(clicked_id) {

    localStorage.setItem('cedula', clicked_id);

    $(".content").html(waitloadingGif);
    $.ajax({
        type: "GET",
        url: "/Admin/DetallePrioritarios"
    }).done(function (html) {
        $(".content").html(html);
    });
}

function addRow(respuesta) {
    localStorage.setItem("detallePrioritarios", respuesta);
    var prioritarios = JSON.parse(respuesta);
    $("#lista-prioritarios").empty();
    for (var i = 0 ; i < prioritarios.length ; i++) {
        var fila = "<tr class='filaselector' id='" + prioritarios[i].Cedula + "' onClick=\"verDetalle(this.id)\"><td>" + prioritarios[i].Nombres + "</td><td>" + prioritarios[i].Apellidos + "</td><td>" + prioritarios[i].Cedula + "</td><td>" + prioritarios[i].NumContacto + "</td>";
        var aux = "";
        fila += "<td>";
        if (prioritarios[i].ListaVariablesPrioritarias.length)
        {
            var lista_var = prioritarios[i].ListaVariablesPrioritarias;
            for (var j = 0 ; j < lista_var.length ; j++) {
                aux += lista_var[j].NombreVariable + ", ";
            }
            aux = aux.substring(0, aux.length - 2);
        } else {
            var lista_var = prioritarios[i].ListaVariablesDesactualizadas;
            for (var j = 0 ; j < lista_var.length ; j++) {
                aux += lista_var[j].NombreVariable + ", ";
            }
            aux = aux.substring(0, aux.length - 2);
        }
        fila += aux + "</td></tr>";
        $("#lista-prioritarios").append(fila);
    }

    $(".filaselector").mouseenter(function () {
        lastcolor = $(this).css("background-color");
        $(this).css("background-color", "#51B1D0");
    });

    $(".filaselector").mouseleave(function () {
        $(this).css("background-color", lastcolor);
    });
}