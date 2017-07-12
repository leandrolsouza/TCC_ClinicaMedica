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
    public class MEDICO_CLINICAController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: MEDICO_CLINICA
        public async Task<ActionResult> Index()
        {
            var mEDICO_CLINICA = db.MEDICO_CLINICA.Include(m => m.CLINICAS).Include(m => m.MEDICOS);
            return View(await mEDICO_CLINICA.ToListAsync());
        }

        // GET: MEDICO_CLINICA/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICO_CLINICA mEDICO_CLINICA = await db.MEDICO_CLINICA.FindAsync(id);
            if (mEDICO_CLINICA == null)
            {
                return HttpNotFound();
            }
            return View(mEDICO_CLINICA);
        }

        // GET: MEDICO_CLINICA/Create
        public ActionResult Create()
        {
            ViewBag.ID_CLINICA = new SelectList(db.CLINICAS, "ID", "NOME");
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM");
            return View();
        }

        // POST: MEDICO_CLINICA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ID_MEDICO,ID_CLINICA")] MEDICO_CLINICA mEDICO_CLINICA)
        {
            if (ModelState.IsValid)
            {
                db.MEDICO_CLINICA.Add(mEDICO_CLINICA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CLINICA = new SelectList(db.CLINICAS, "ID", "NOME", mEDICO_CLINICA.ID_CLINICA);
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM", mEDICO_CLINICA.ID_MEDICO);
            return View(mEDICO_CLINICA);
        }

        // GET: MEDICO_CLINICA/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICO_CLINICA mEDICO_CLINICA = await db.MEDICO_CLINICA.FindAsync(id);
            if (mEDICO_CLINICA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CLINICA = new SelectList(db.CLINICAS, "ID", "NOME", mEDICO_CLINICA.ID_CLINICA);
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM", mEDICO_CLINICA.ID_MEDICO);
            return View(mEDICO_CLINICA);
        }

        // POST: MEDICO_CLINICA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ID_MEDICO,ID_CLINICA")] MEDICO_CLINICA mEDICO_CLINICA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEDICO_CLINICA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CLINICA = new SelectList(db.CLINICAS, "ID", "NOME", mEDICO_CLINICA.ID_CLINICA);
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM", mEDICO_CLINICA.ID_MEDICO);
            return View(mEDICO_CLINICA);
        }

        // GET: MEDICO_CLINICA/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICO_CLINICA mEDICO_CLINICA = await db.MEDICO_CLINICA.FindAsync(id);
            if (mEDICO_CLINICA == null)
            {
                return HttpNotFound();
            }
            return View(mEDICO_CLINICA);
        }

        // POST: MEDICO_CLINICA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MEDICO_CLINICA mEDICO_CLINICA = await db.MEDICO_CLINICA.FindAsync(id);
            db.MEDICO_CLINICA.Remove(mEDICO_CLINICA);
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
