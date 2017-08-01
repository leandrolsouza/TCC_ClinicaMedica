using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC_Clinica_Medica.App_Start;

namespace TCC_Clinica_Medica.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador, UserType.Medico })]
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            return View();
        }

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador, UserType.Medico })]
        public ActionResult Exit()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "LOGIN");
        }

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador, UserType.Medico })]
        public FileContentResult GetImage()
        {
            var image = ((USUARIOS)Session["Usuario"]).FOTO;

            if (image != null)
            {
                return File(image, "image/png");
            }
            else
                return null;
        }
    }
}