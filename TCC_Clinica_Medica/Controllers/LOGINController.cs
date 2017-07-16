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
            if (Session["Usuario"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Login(string login, string senha)
        {
            string senhaCrypto = new Crypto.Crypto().Encrypt(senha);
            USUARIOS uSUARIOS = db.USUARIOS.FirstOrDefault(x => (x.CPF == login || x.EMAIL == login) && x.SENHA == senhaCrypto);

            if (uSUARIOS != null)
            {
                Session["Usuario"] = uSUARIOS;
                Session["Nome"] = uSUARIOS.NOME;
                return RedirectToAction("Index", "HOME"); 
            }

            return RedirectToAction("Index", "LOGIN", new { mensagem = "Usuario ou senha invalidos!"});
       
        }
    }
}