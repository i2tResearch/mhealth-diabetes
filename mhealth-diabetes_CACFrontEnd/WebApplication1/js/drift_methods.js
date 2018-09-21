!function () {
    var t;
    if (t = window.driftt = window.drift = window.driftt || [], !t.init) return t.invoked ? void (window.console && console.error && console.error("Drift snippet included twice.")) : (t.invoked = !0,
    t.methods = ["identify", "config", "track", "reset", "debug", "show", "ping", "page", "hide", "off", "on"],
    t.factory = function (e) {
        return function () {
            var n;
            return n = Array.prototype.slice.call(arguments), n.unshift(e), t.push(n), t;
        };
    }, t.methods.forEach(function (e) {
        t[e] = t.factory(e);
    }), t.load = function (t) {
        var e, n, o, i;
        e = 3e5, i = Math.ceil(new Date() / e) * e, o = document.createElement("script"),
        o.type = "text/javascript", o.async = !0, o.crossorigin = "anonymous", o.src = "https://js.driftt.com/include/" + i + "/" + t + ".js",
        n = document.getElementsByTagName("script")[0], n.parentNode.insertBefore(o, n);
    });
}();

drift.SNIPPET_VERSION = '0.3.1';
drift.load('');

drift.on('ready', function (api, payload) {
    var obj = JSON.parse(localStorage.getItem("usuario"));
    if (obj) {
        drift.identify(obj.Id, { email: obj.Email, companyId: obj.Organizacion.Nombre });
    } else {
        drift.config({ backgroundColor: '#0ea8cc' });
    }
    drift.on('message', function (e) {
        if (!e.data.sidebarOpen) {
            api.widget.show();
        }
    });
});

$(document).ready(function () {
    if ($("cerrarsesion")) {
        $("#cerrarsesion").on('click', function () {
            drift.reset();
        });
    }
});