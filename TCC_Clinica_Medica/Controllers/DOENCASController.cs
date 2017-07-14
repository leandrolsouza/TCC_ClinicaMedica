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
    public class DOENCASController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "descricao" : "";
            ViewBag.CidSortParm = String.IsNullOrEmpty(sortOrder) ? "cid" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var doencas = db.DOENCAS.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                doencas = doencas.Where(s => s.DESCRICAO.ToUpper().Contains(searchString.ToUpper()));

                if(doencas == null)
                {
                    doencas = doencas.Where(s => s.CID.ToUpper().Contains(searchString.ToUpper()));
                }
            }

            switch (sortOrder)
            {
                case "descricao":
                    doencas = doencas.OrderByDescending(s => s.DESCRICAO);
                    break;
                case "cid":
                    doencas = doencas.OrderByDescending(s => s.CID);
                    break;
                default:
                    doencas = doencas.OrderBy(s => s.DESCRICAO);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(doencas.ToPagedList(pageNumber, pageSize));
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
        public async Task<ActionResult> Create([Bind(Include = "ID,CID,DESCRICAO")] DOENCAS dOENCAS)
        {
            dOENCAS.DATA_CRIACAO = DateTime.Now;
            dOENCAS.DATA_MODIFICACAO = DateTime.Now;
            dOENCAS.ATIVO = true;
            dOENCAS.DESCRICAO = dOENCAS.DESCRICAO.ToUpper();
            dOENCAS.CID = dOENCAS.CID.ToUpper();

            if (ModelState.IsValid)
            {
                db.DOENCAS.Add(dOENCAS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "DOENCAS", new { mensagem = "Registro criado com sucesso!" });
            }

            return View(dOENCAS);
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

            DOENCAS dOENCAS = await db.DOENCAS.FindAsync(id);
            if (dOENCAS == null)
            {
                return HttpNotFound();
            }
            return View(dOENCAS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CID,DESCRICAO,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] DOENCAS dOENCAS)
        {
            if (ModelState.IsValid)
            {
                dOENCAS.DATA_MODIFICACAO = DateTime.Now;
                dOENCAS.DESCRICAO = dOENCAS.DESCRICAO.ToUpper();
                dOENCAS.CID = dOENCAS.CID.ToUpper();
                db.Entry(dOENCAS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { mensagem = "Registro editado com sucesso!" });
            }
            return View(dOENCAS);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            DOENCAS dOENCAS = db.DOENCAS.Find(id);
            db.DOENCAS.Remove(dOENCAS);
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
