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
    public class MEDICAMENTOSController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            /*
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            */
            ViewBag.CurrentSort = sortOrder;
            ViewBag.GenericoSortParm = String.IsNullOrEmpty(sortOrder) ? "nome_generico" : "";
            ViewBag.FabricaSortParm = String.IsNullOrEmpty(sortOrder) ? "fabrica" : "";
            ViewBag.FabricanteSortParm = String.IsNullOrEmpty(sortOrder) ? "fabricante" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var medicamentos = db.MEDICAMENTOS.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                medicamentos = medicamentos.Where(s => s.NOME_GENERICO.ToUpper().Contains(searchString.ToUpper()));

                if(medicamentos.ToList().Count == 0)
                {
                    medicamentos = db.MEDICAMENTOS.ToList().AsEnumerable();
                    medicamentos = medicamentos.Where(s => s.FABRICA.ToUpper().Contains(searchString.ToUpper()));
                }

                if (medicamentos.ToList().Count == 0)
                {
                    medicamentos = db.MEDICAMENTOS.ToList().AsEnumerable();
                    medicamentos = medicamentos.Where(s => s.FABRICANTE.ToUpper().Contains(searchString.ToUpper()));
                }

            }

            switch (sortOrder)
            {
                case "nome_generico":
                    medicamentos = medicamentos.OrderByDescending(s => s.NOME_GENERICO);
                    break;
                case "fabrica":
                    medicamentos = medicamentos.OrderBy(s => s.FABRICA);
                    break;
                case "fabricante":
                    medicamentos = medicamentos.OrderBy(s => s.FABRICANTE);
                    break;
                default:
                    medicamentos = medicamentos.OrderBy(s => s.NOME_GENERICO);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(medicamentos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        // GET: MEDICAMENTOS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NOME_GENERICO,FABRICA,FABRICANTE")] MEDICAMENTOS mEDICAMENTOS)
        {
            mEDICAMENTOS.DATA_CRIACAO = DateTime.Now;
            mEDICAMENTOS.DATA_MODIFICACAO = DateTime.Now;
            mEDICAMENTOS.ATIVO = true;

            if (ModelState.IsValid)
            {
                db.MEDICAMENTOS.Add(mEDICAMENTOS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "MEDICAMENTOS", new { mensagem = "Registro criado com sucesso!" });
            }

            return View(mEDICAMENTOS);
        }

        // GET: MEDICAMENTOS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICAMENTOS mEDICAMENTOS = await db.MEDICAMENTOS.FindAsync(id);
            if (mEDICAMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(mEDICAMENTOS);
        }

        // POST: MEDICAMENTOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NOME_GENERICO,FABRICA,FABRICANTE,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] MEDICAMENTOS mEDICAMENTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEDICAMENTOS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mEDICAMENTOS);
        }

        // GET: MEDICAMENTOS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICAMENTOS mEDICAMENTOS = await db.MEDICAMENTOS.FindAsync(id);
            if (mEDICAMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(mEDICAMENTOS);
        }

        // POST: MEDICAMENTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MEDICAMENTOS mEDICAMENTOS =  db.MEDICAMENTOS.Find(id);
            db.MEDICAMENTOS.Remove(mEDICAMENTOS);
             db.SaveChangesAsync();
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
