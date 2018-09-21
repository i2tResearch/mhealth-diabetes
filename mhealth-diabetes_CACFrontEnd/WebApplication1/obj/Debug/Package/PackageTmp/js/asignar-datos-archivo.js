$(document).ready(init);

function init() {
    $("#cargar_archivo_nuevamente").click(function (e) {
        e.preventDefault();
        $(".content").html(waitloadingGif);
        $.ajax({
            type: "GET",
            url: "/Admin/CargarArchivos"
        }).done(function (html) {
            $(".content").html(html);
        });
    });

    var lista_errores = localStorage.getItem("lista_errores");
    localStorage.removeItem("lista_errores");

    var json = JSON.parse(lista_errores);

    if (json !== null) {
        $("#page_asignar_datos").css("visibility", "visible");
        ToDownload(JSON.stringify(json.List));
        for (var i = 0 ; i < json.List.length ; i++) {
            $("#tabla-errores").append("<tr><td>" + json.List[i].Celda + "</td><td>" + json.List[i].Descripcion + "</td><td>" + json.List[i].Valor + "</td></tr>");
        }
        var listjson = json.List;
        $('#tabla-errores').dynatable({
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
    } else {
        $(location).attr("href", "/Admin/CargarArchivos");
    }
}
