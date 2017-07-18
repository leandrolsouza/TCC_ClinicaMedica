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
    public class CLINICASController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nome" : "";
            ViewBag.CnpjSortParm = String.IsNullOrEmpty(sortOrder) ? "cnpj" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var clinicas = db.CLINICAS.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                clinicas = clinicas.Where(s => s.NOME.ToUpper().Contains(searchString.ToUpper()));

                if (clinicas.ToList().Count == 0)
                {
                    clinicas = db.CLINICAS.ToList().AsEnumerable();
                    clinicas = clinicas.Where(s => s.CNPJ.ToUpper().Contains(searchString.ToUpper()));
                }
            }

            switch (sortOrder)
            {
                case "descricao":
                    clinicas = clinicas.OrderByDescending(s => s.NOME);
                    break;
                case "cnpj":
                    clinicas = clinicas.OrderByDescending(s => s.CNPJ);
                    break;
                default:
                    clinicas = clinicas.OrderBy(s => s.NOME);
                    break;
            }

            clinicas = clinicas.Where(x => x.ATIVO);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(clinicas.ToPagedList(pageNumber, pageSize));
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
        public async Task<ActionResult> Create([Bind(Include = "ID,NOME,CNPJ,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] CLINICAS cLINICAS)
        {
            if (ModelState.IsValid)
            {
                cLINICAS.DATA_CRIACAO = DateTime.Now;
                cLINICAS.DATA_MODIFICACAO = DateTime.Now;
                cLINICAS.ATIVO = true;
                cLINICAS.NOME = cLINICAS.NOME.ToUpper();
                db.CLINICAS.Add(cLINICAS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { mensagem = "Registro criado com sucesso!" });
            }

            return View(cLINICAS);
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

            CLINICAS cLINICAS = await db.CLINICAS.FindAsync(id);
            if (cLINICAS == null)
            {
                return HttpNotFound();
            }
            return View(cLINICAS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NOME,CNPJ,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] CLINICAS cLINICAS)
        {
            if (ModelState.IsValid)
            {
                cLINICAS.DATA_CRIACAO = DateTime.Now;
                cLINICAS.DATA_MODIFICACAO = DateTime.Now;
                cLINICAS.ATIVO = true;
                cLINICAS.NOME = cLINICAS.NOME.ToUpper();

                db.Entry(cLINICAS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { mensagem = "Registro editado com sucesso!" });
            }
            return View(cLINICAS);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CLINICAS cLINICAS = await db.CLINICAS.FindAsync(id);
            cLINICAS.ATIVO = false;
            await db.SaveChangesAsync();
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
