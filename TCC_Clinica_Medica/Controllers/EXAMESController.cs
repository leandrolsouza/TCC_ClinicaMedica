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

namespace TCC_Clinica_Medica.Controllers
{
    public class EXAMESController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: EXAMES
        public async Task<ActionResult> Index()
        {
            return View(await db.EXAMES.ToListAsync());
        }

        // GET: EXAMES/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAMES eXAMES = await db.EXAMES.FindAsync(id);
            if (eXAMES == null)
            {
                return HttpNotFound();
            }
            return View(eXAMES);
        }

        // GET: EXAMES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EXAMES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DESCRICAO,DATA_CRIAÇÃO,DATA_MODIFICACAO,ATIVO")] EXAMES eXAMES)
        {
            if (ModelState.IsValid)
            {
                db.EXAMES.Add(eXAMES);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eXAMES);
        }

        // GET: EXAMES/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAMES eXAMES = await db.EXAMES.FindAsync(id);
            if (eXAMES == null)
            {
                return HttpNotFound();
            }
            return View(eXAMES);
        }

        // POST: EXAMES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DESCRICAO,DATA_CRIAÇÃO,DATA_MODIFICACAO,ATIVO")] EXAMES eXAMES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eXAMES).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eXAMES);
        }

        // GET: EXAMES/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAMES eXAMES = await db.EXAMES.FindAsync(id);
            if (eXAMES == null)
            {
                return HttpNotFound();
            }
            return View(eXAMES);
        }

        // POST: EXAMES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EXAMES eXAMES = await db.EXAMES.FindAsync(id);
            db.EXAMES.Remove(eXAMES);
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
