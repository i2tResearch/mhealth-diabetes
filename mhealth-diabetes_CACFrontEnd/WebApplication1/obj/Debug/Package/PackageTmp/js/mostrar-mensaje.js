$(document).ready(function () {
    document.body.innerHTML += '<div id="snackbar">Mensaje Inicial</div>';
});

function mostrarMensaje(mensaje, tiempo) {
    $("#snackbar").css("visibility", "visible");
    if (mensaje) {
        $("#snackbar").text(mensaje);
    }
    $("#snackbar").animate({
        opacity: "1",
        bottom: "60px"
    }, 300);

    if (!tiempo) {
        tiempo = 3000;
    }
    setTimeout(function () {
        $("#snackbar").animate({
            opacity: "0",
            bottom: "5px"
        }, 900);
    }, tiempo);
}