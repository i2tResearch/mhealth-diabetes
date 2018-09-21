$(document).ready(init);

function init() {
    document.getElementsByTagName("html")[0].style.visibility = "visible";
    $("#recuperar-password").click(enviarCorreo);
}

function enviarCorreo() {
    var auth = firebase.auth();
    var emailAddress = $("#usuario_recuperar").val();
    
    if (emailAddress.toString().trim() !== '') {
        auth.sendPasswordResetEmail(emailAddress).then(function () {
            $(location).attr("href", "/Login");
            localStorage.setItem("contra","");
        }, function (error) {
            mostrarMensaje("Este correo no está inscrito en el portal", 2000);
        });
    } else {
        mostrarMensaje("Escriba su correo", 2000);
    }

}