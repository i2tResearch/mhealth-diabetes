using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CACWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var currentUser = Session["currentUser"];

            if (currentUser != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        //Archivos
        public ActionResult Archivos()
        {
            return PartialView();
        }

        //Cargar Archivos
        public ActionResult CargarArchivos()
        {
            return PartialView();
        }


        public ActionResult GestionUsuarios() {
            return PartialView();
        }


        public ActionResult CargaSatisfactoria() {
            return PartialView();
        }

        public ActionResult AsignarDatosArchivo() {
            return PartialView();
        }

        public ActionResult ListaPrioritarios() {
            return PartialView();
        }

        public ActionResult DetallePrioritarios() {
            return PartialView();
        }

        public ActionResult CrearUsuario() {
            return PartialView();
        }

        public ActionResult Reportes()
        {
            return PartialView();
        }

    }
}
