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


namespace TCC_Clinica_Medica.Controllers
{
    public class ESPECIALIDADESController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

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

            var especialidades = db.ESPECIALIDADES.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                especialidades = especialidades.Where(s => s.DESCRICAO.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    especialidades = especialidades.OrderByDescending(s => s.DESCRICAO);
                    break;
                default:
                    especialidades = especialidades.OrderBy(s => s.DESCRICAO);
                    break;
            }

            especialidades = especialidades.Where(x => x.ATIVO);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(especialidades.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Create()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DESCRICAO")] ESPECIALIDADES eSPECIALIDADES)
        {
            eSPECIALIDADES.DATA_CRIACAO = DateTime.Now;
            eSPECIALIDADES.DATA_MODIFICACAO = DateTime.Now;
            eSPECIALIDADES.ATIVO = true;
            eSPECIALIDADES.DESCRICAO = eSPECIALIDADES.DESCRICAO.ToUpper();

            if (ModelState.IsValid)
            {
                db.ESPECIALIDADES.Add(eSPECIALIDADES);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "ESPECIALIDADES", new { mensagem = "Registro criado com sucesso!" });
            }

            return View(eSPECIALIDADES);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ESPECIALIDADES eSPECIALIDADE = await db.ESPECIALIDADES.FindAsync(id);

            if (eSPECIALIDADE == null)
            {
                return HttpNotFound();
            }
            return View(eSPECIALIDADE);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DESCRICAO,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] ESPECIALIDADES eSPECIALIDADES)
        {
            if (ModelState.IsValid)
            {
                eSPECIALIDADES.DATA_MODIFICACAO = DateTime.Now;
                eSPECIALIDADES.DESCRICAO = eSPECIALIDADES.DESCRICAO.ToUpper();
                db.Entry(eSPECIALIDADES).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { mensagem = "Registro editado com sucesso!" });
            }
            return View(eSPECIALIDADES);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            ESPECIALIDADES eSPECIALIDADE = db.ESPECIALIDADES.Find(id);
            eSPECIALIDADE.ATIVO = false;
            db.SaveChanges();
            return Json(Url.Action("Index", new { mensagem = "Registro apagado com sucesso!" }));
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
