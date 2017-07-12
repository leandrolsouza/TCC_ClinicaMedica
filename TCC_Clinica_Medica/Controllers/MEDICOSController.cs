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
    public class MEDICOSController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: MEDICOS
        public async Task<ActionResult> Index()
        {
            var mEDICOS = db.MEDICOS.Include(m => m.USUARIOS);
            return View(await mEDICOS.ToListAsync());
        }

        // GET: MEDICOS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICOS mEDICOS = await db.MEDICOS.FindAsync(id);
            if (mEDICOS == null)
            {
                return HttpNotFound();
            }
            return View(mEDICOS);
        }

        // GET: MEDICOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID", "NOME");
            return View();
        }

        // POST: MEDICOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,CRM,ID_USUARIO")] MEDICOS mEDICOS)
        {
            if (ModelState.IsValid)
            {
                db.MEDICOS.Add(mEDICOS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID", "NOME", mEDICOS.ID_USUARIO);
            return View(mEDICOS);
        }

        // GET: MEDICOS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICOS mEDICOS = await db.MEDICOS.FindAsync(id);
            if (mEDICOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID", "NOME", mEDICOS.ID_USUARIO);
            return View(mEDICOS);
        }

        // POST: MEDICOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CRM,ID_USUARIO")] MEDICOS mEDICOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEDICOS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID", "NOME", mEDICOS.ID_USUARIO);
            return View(mEDICOS);
        }

        // GET: MEDICOS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICOS mEDICOS = await db.MEDICOS.FindAsync(id);
            if (mEDICOS == null)
            {
                return HttpNotFound();
            }
            return View(mEDICOS);
        }

        // POST: MEDICOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MEDICOS mEDICOS = await db.MEDICOS.FindAsync(id);
            db.MEDICOS.Remove(mEDICOS);
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
