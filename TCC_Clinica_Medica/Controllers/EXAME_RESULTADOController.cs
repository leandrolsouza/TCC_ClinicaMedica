using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TCC_Clinica_Medica;
using PagedList;
using TCC_Clinica_Medica.App_Start;

namespace TCC_Clinica_Medica.Controllers
{
    public class EXAME_RESULTADOController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador, UserType.Medico })]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var exames = db.EXAME_RESULTADO.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                exames = exames.Where(s => s.EXAMES_SOLICITADOS.CONSULTAS.PACIENTES.USUARIOS.NOME.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    exames = exames.OrderByDescending(s => s.EXAMES_SOLICITADOS.CONSULTAS.PACIENTES.USUARIOS.NOME);
                    break;
                default:
                    exames = exames.OrderBy(s => s.EXAMES_SOLICITADOS.CONSULTAS.PACIENTES.USUARIOS.NOME);
                    break;
            }

            
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(exames.ToPagedList(pageNumber, pageSize));
        }

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador, UserType.Medico })]
        public ActionResult Resultado(int? id)
        {
            return RedirectToAction("Resultado","EXAMES", new { id = id });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
