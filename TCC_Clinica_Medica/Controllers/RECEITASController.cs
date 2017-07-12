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

namespace TCC_Clinica_Medica.Controllers
{
    public class RECEITASController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: RECEITAS
        public async Task<ActionResult> Index()
        {
            var rECEITAS = db.RECEITAS.Include(r => r.CONSULTAS).Include(r => r.MEDICAMENTOS);
            return View(await rECEITAS.ToListAsync());
        }

        // GET: RECEITAS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECEITAS rECEITAS = await db.RECEITAS.FindAsync(id);
            if (rECEITAS == null)
            {
                return HttpNotFound();
            }
            return View(rECEITAS);
        }

        // GET: RECEITAS/Create
        public ActionResult Create()
        {
            ViewBag.ID_CONSULTA = new SelectList(db.CONSULTAS, "ID", "ID");
            ViewBag.ID_MEDICAMENTO = new SelectList(db.MEDICAMENTOS, "ID", "NOME_GENERICO");
            return View();
        }

        // POST: RECEITAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ID_MEDICAMENTO,ID_CONSULTA")] RECEITAS rECEITAS)
        {
            if (ModelState.IsValid)
            {
                db.RECEITAS.Add(rECEITAS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CONSULTA = new SelectList(db.CONSULTAS, "ID", "ID", rECEITAS.ID_CONSULTA);
            ViewBag.ID_MEDICAMENTO = new SelectList(db.MEDICAMENTOS, "ID", "NOME_GENERICO", rECEITAS.ID_MEDICAMENTO);
            return View(rECEITAS);
        }

        // GET: RECEITAS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECEITAS rECEITAS = await db.RECEITAS.FindAsync(id);
            if (rECEITAS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CONSULTA = new SelectList(db.CONSULTAS, "ID", "ID", rECEITAS.ID_CONSULTA);
            ViewBag.ID_MEDICAMENTO = new SelectList(db.MEDICAMENTOS, "ID", "NOME_GENERICO", rECEITAS.ID_MEDICAMENTO);
            return View(rECEITAS);
        }

        // POST: RECEITAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ID_MEDICAMENTO,ID_CONSULTA")] RECEITAS rECEITAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rECEITAS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CONSULTA = new SelectList(db.CONSULTAS, "ID", "ID", rECEITAS.ID_CONSULTA);
            ViewBag.ID_MEDICAMENTO = new SelectList(db.MEDICAMENTOS, "ID", "NOME_GENERICO", rECEITAS.ID_MEDICAMENTO);
            return View(rECEITAS);
        }

        // GET: RECEITAS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECEITAS rECEITAS = await db.RECEITAS.FindAsync(id);
            if (rECEITAS == null)
            {
                return HttpNotFound();
            }
            return View(rECEITAS);
        }

        // POST: RECEITAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RECEITAS rECEITAS = await db.RECEITAS.FindAsync(id);
            db.RECEITAS.Remove(rECEITAS);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
