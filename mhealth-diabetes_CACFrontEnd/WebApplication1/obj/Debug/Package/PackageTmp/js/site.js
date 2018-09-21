var waitloadingGif = '<img src="images/loader.gif" width="128" height="128" style="display:block; margin:auto;"/>';

firebase.auth().onAuthStateChanged(function (user) {
    if (user) {        
        var usuario = JSON.parse(localStorage.getItem("usuario"));
        if (usuario) {
            $("#nombre_usuario").text(usuario.Nombres + " " + usuario.Apellidos);
            $("#nombre_usuario_menu").text(usuario.Nombres + " " + usuario.Apellidos);
        }
    } else {
        mostrarMensaje("La sesión se está cerrando", 4000);
        localStorage.removeItem("usuario");
        $(location).attr("href", "/Login");
    }
});

$(document).ready(init);


function init() {
    
    $("#cerrarsesion").click(cerrar_sesion);
    
    //drag-n-drop
    $(document).on("dragover", function (event) {
        event.preventDefault();
    });

    $(document).on("dragenter", function (event) {
        event.preventDefault();
    });

    $(document).on("dragleave", function (event) {
        event.preventDefault();
    });

    $(document).on("drop", function (event) {
        event.preventDefault();
    });
    //Comportamiento del side-menu

    $(window).resize(function () {
        if ($(window).width() > 767) {
            closeSideMenu();
        }
    });

    $(".menu-link").click(function () {
        closeSideMenu();
    });
    $("#backdrop").click(function () {
        closeSideMenu();
    });    

    $("#archivos").click(function (e) {
        e.preventDefault();
        $(".content").html(waitloadingGif);
        $.ajax({
            type: "GET",
            url: url_archivos
        }).done(function (html) {
            $(".content").hide().html(html).fadeIn('slow');
            $("#archivos").addClass("active");
            $("#cargararchivo").removeClass("active");
            $("#gestionusuarios").removeClass("active");
            $("#gestionroles").removeClass("active");
            $("#reportes").removeClass("active");
        });
    });

    $("#cargararchivo").click(function (e) {
        e.preventDefault();
        $(".content").html(waitloadingGif);
        $.ajax({
            type: "GET",
            url: url_cargararchivos
        }).done(function (html) {
            $(".content").hide().html(html).fadeIn('slow');
            $("#archivos").removeClass("active");
            $("#cargararchivo").addClass("active");
            $("#gestionusuarios").removeClass("active");
            $("#gestionroles").removeClass("active");
            $("#reportes").removeClass("active");
        });
    });

    $("#gestionusuarios").click(function (e) {
        e.preventDefault();
        $(".content").html(waitloadingGif);
        $.ajax({
            type: "GET",
            url: url_gestionarchivos
        }).done(function (html) {
            $(".content").hide().html(html).fadeIn('slow');

            $("#archivos").removeClass("active");
            $("#cargararchivo").removeClass("active");
            $("#gestionusuarios").addClass("active");
            $("#gestionroles").removeClass("active");
            $("#reportes").removeClass("active");
        });
    });

    $("#reportes").click(function (e) {
        e.preventDefault();
        $(".content").html(waitloadingGif);
        $.ajax({
            type: "GET",
            url: url_reportes
        }).done(function (html) {
            $(".content").hide().html(html).fadeIn('slow');
            $("#archivos").removeClass("active");
            $("#cargararchivo").removeClass("active");
            $("#gestionusuarios").removeClass("active");
            $("#gestionroles").removeClass("active");
            $("#reportes").addClass("active");
        });
    });

    mostrarMensaje("Bienvenido", 4000);
}

function cerrar_sesion(evt) {    
    evt.preventDefault();
    firebase.auth().signOut().then(function () {
        // Sign-out successful.
    }, function (error) {
        mostrarMensaje(error, 4000);
    });
}

function closeSideMenu() {
    $('#side-menu').removeClass('side-menu-open');
    $("#backdrop").removeClass('bd-show');
}

function openSideMenu() {
    event.preventDefault();
    $('#side-menu').addClass('side-menu-open');
    $("#backdrop").addClass('bd-show');
}

String.format = function () {
    // The string containing the format items (e.g. "{0}")
    // will and always has to be the first argument.
    var theString = arguments[0];

    // start with the second argument (i = 1)
    for (var i = 1; i < arguments.length; i++) {
        // "gm" = RegEx options for Global search (more than one instance)
        // and for Multiline search
        var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        theString = theString.replace(regEx, arguments[i]);
    }

    return theString;
}