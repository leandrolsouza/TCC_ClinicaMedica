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
using TCC_Clinica_Medica.App_Start;
using PagedList;
using System.IO;

namespace TCC_Clinica_Medica.Controllers
{
    public class EXAMESController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador })]
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

            var planos = db.EXAMES_SOLICITADOS.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                planos = planos.Where(s => s.EXAMES.DESCRICAO.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    planos = planos.OrderByDescending(s => s.EXAMES.DESCRICAO);
                    break;
                default:
                    planos = planos.OrderBy(s => s.EXAMES.DESCRICAO);
                    break;
            }

            planos = planos.Where(x => x.EXAME_RESULTADO.Count == 0);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(planos.ToPagedList(pageNumber, pageSize));
        }

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador })]
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            EXAMES_SOLICITADOS eXAMES_SOLICITADOS = db.EXAMES_SOLICITADOS.Find(id);
            eXAMES_SOLICITADOS.EXECUTADO = true;
            db.SaveChanges();
            return Json(Url.Action("Index", new { mensagem = "Exame cancelado com sucesso!" }));
        }

        public ActionResult Execucao(int? id)
        {
            var eXAMES_SOLICITADOS = db.EXAMES_SOLICITADOS.Find(id);
            eXAMES_SOLICITADOS.EXECUTADO = true;
            db.SaveChanges();

            EXAME_RESULTADO eXAME_RESULTADO = new EXAME_RESULTADO();
            Random rnd = new Random();

                string[] adjetivos = new string[] { "PESSIMO", "RUIM", "NORMAL", "BOM", "EXCELENTE", "SUPER MAN :)" };
                 
            

            string text = adjetivos[rnd.Next(0, adjetivos.Length)];
    
          

            eXAME_RESULTADO.DESCRICAO = "O RESULTADO DO EXAME FOI: " + text;
            eXAME_RESULTADO.ENTREGUE_PACIENTE = true;
            eXAME_RESULTADO.ID_EXAMES_SOLICITADO = id.Value;
            eXAME_RESULTADO.DATA_CRIACAO = DateTime.Now ;
            eXAME_RESULTADO.GUID = Guid.NewGuid();
            db.EXAME_RESULTADO.Add(eXAME_RESULTADO);
            db.SaveChanges();




            return RedirectToAction("Resultado", new { id = eXAME_RESULTADO.ID });
        }
        
    public ActionResult Resultado(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAME_RESULTADO eXAME_RESULTADO = db.EXAME_RESULTADO.Find(id);
            if (eXAME_RESULTADO == null)
            {
                return HttpNotFound();
            }

            return View(eXAME_RESULTADO);
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
