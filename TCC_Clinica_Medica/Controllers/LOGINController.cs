using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCC_Clinica_Medica.Controllers
{
    public class LOGINController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: LOGIN
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string login, string senha)
        {
            USUARIOS uSUARIOS = db.USUARIOS.FirstOrDefault(x => (x.CPF == login || x.EMAIL == login) && x.SENHA == senha);

            if (uSUARIOS == null)
            {
                return RedirectToAction("Index", "HOME");
            }

            return View();
        }
    }
}