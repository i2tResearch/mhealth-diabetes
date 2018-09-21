init();

function init() {

    $("#atras").click(function (e) {
        e.preventDefault();
        backPage();
    });

    var cedula = localStorage.getItem('cedula');
    var id_file = localStorage.getItem('file-id');
    if (cedula !== null && id_file !== null) {
        $("#lista-prioritarios").html("<tr><td>Cargando...</td><td></td><td></td><td></td></tr>");
        $("#lista-desactualizadas").html("<tr><td>Cargando...</td><td></td><td></td><td></td></tr>");
        var respuesta = localStorage.getItem("detallePrioritarios", respuesta);
        if (respuesta) {
            localStorage.setItem("detallePrioritarios", respuesta);
            addDetails(respuesta, cedula);
        } else {
            $.ajax({
                url: "/Data/getListaDePrioritarios",
                type: "GET",
                data: { "file_id": id_file }
            }).done(function (respuesta) {
                localStorage.setItem("detallePrioritarios", respuesta);
                addDetails(respuesta, cedula);
            });
        }
    } else {
        if (id_file !== null) {
            backPage();
        } else {
            backPage("/Admin/Archivos");
        }
    }
}

function backPage(backURL) {
    $(".content").html(waitloadingGif);
    var _URL = "/Admin/ListaPrioritarios";

    if (backURL) {
        _URL = backURL
    }

    $.ajax({
        type: "GET",
        url: _URL
    }).done(function (html) {
        $(".content").html(html);
    });
}

function addDetails(respuesta, cedula) {
    $("#lista-prioritarios").empty();
    $("#lista-desactualizadas").empty();
    var prioritarios = JSON.parse(respuesta);
    var paciente;
    for (var i = 0 ; i < prioritarios.length ; i++) {
        if (prioritarios[i].Cedula === cedula) {
            paciente = prioritarios[i];
            $("#records_items").html("Paciente prioritario " + (i + 1) + " de " + prioritarios.length);
            break;
        }
    }
    if (paciente) {
        var list = [paciente];
        var print = JSON.stringify(list);
        ToDownload(print);

        var texto = "<h3>Las siguientes variables corresponden a:</h3><h1><b>" + paciente.Nombres + " " + paciente.Apellidos + "</b><h1>";
        $("#descripcion").html(texto);

        var listaVariables = paciente.ListaVariablesPrioritarias;
        $("#lista-prioritarios").empty;
        for (var i = 0 ; i < listaVariables.length ; i++) {
            $("#lista-prioritarios").append("<tr><td>" + listaVariables[i].NombreVariable + "</td><td>" + listaVariables[i].ValorVariable + "</td><td>" + listaVariables[i].ValorUmbral + "</td></tr>");
        }

        var listaVariables = paciente.ListaVariablesDesactualizadas;
        $("#lista-desactualizadas").empty;
        for (var i = 0 ; i < listaVariables.length ; i++) {
            $("#lista-desactualizadas").append("<tr><td>" + listaVariables[i].NombreVariable + "</td><td>" + listaVariables[i].ValorVariable + "</td><td>" + listaVariables[i].MesesDesactualizados + "</td></tr>");
        }

    }
}

function nextObject() {
    var lastOne = false;
    $("#descripcion").html("<h3>Cargando</h3>");
    $("#lista-prioritarios").html("<tr><td>Cargando...</td><td></td><td></td><td></td></tr>");
    $("#lista-desactualizadas").html("<tr><td>Cargando...</td><td></td><td></td><td></td></tr>");
    var detallePrioritarios = localStorage.getItem('detallePrioritarios');
    var cedula = localStorage.getItem('cedula');
    if (cedula !== null && detallePrioritarios !== null) {
        var objJSON = JSON.parse(detallePrioritarios);
        if (objJSON) {
            var paciente;
            var nextIndex;

            for (var i = 0 ; i < objJSON.length ; i++) {
                if (objJSON[i].Cedula === cedula) {
                    nextIndex = i + 1;
                    if (nextIndex < objJSON.length) {
                        paciente = objJSON[nextIndex];
                    } else {
                        paciente = objJSON[i];
                        lastOne = true;
                    }
                    break;
                }
            }
            if (paciente) {
                addDetails(detallePrioritarios, paciente.Cedula);
                localStorage.setItem('cedula', paciente.Cedula);
                //if (lastOne) {
                //    $("#prevItem").css("display", "inline-block");
                //    $("#nextItem").css("display", "none");
                //} else {
                //    $("#nextItem").css("display", "inline-block");
                //    $("#prevItem").css("display", "inline-block");
                //}
            }
        }
    }
}

function prevObject() {
    var firstOne = false;
    $("#descripcion").html("<h3>Cargando</h3>");
    $("#lista-prioritarios").html("<tr><td>Cargando...</td><td></td><td></td><td></td></tr>");
    $("#lista-desactualizadas").html("<tr><td>Cargando...</td><td></td><td></td><td></td></tr>");
    var detallePrioritarios = localStorage.getItem('detallePrioritarios');
    var cedula = localStorage.getItem('cedula');
    if (cedula !== null && detallePrioritarios !== null) {
        var objJSON = JSON.parse(detallePrioritarios);
        if (objJSON) {
            var paciente;
            var prevIndex;

            for (var i = 0 ; i < objJSON.length ; i++) {
                if (objJSON[i].Cedula === cedula) {
                    prevIndex = i - 1;
                    if (prevIndex >= 0) {
                        paciente = objJSON[prevIndex];
                    } else {
                        paciente = objJSON[i];
                        firstOne = true;
                    }
                    break;
                }
            }
            if (paciente) {
                addDetails(detallePrioritarios, paciente.Cedula);
                localStorage.setItem('cedula', paciente.Cedula);
                //if (firstOne) {
                //    $("#prevItem").css("display", "none");
                //    $("#nextItem").css("display", "inline-block");
                //} else {
                //    $("#nextItem").css("display", "inline-block");
                //    $("#prevItem").css("display", "inline-block");
                //}
            }
        }
    }
}