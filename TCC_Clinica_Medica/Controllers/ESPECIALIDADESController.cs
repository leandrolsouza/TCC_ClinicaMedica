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
    public class ESPECIALIDADESController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: ESPECIALIDADES
        public async Task<ActionResult> Index()
        {
            return View(await db.ESPECIALIDADES.ToListAsync());
        }

        // GET: ESPECIALIDADES/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESPECIALIDADES eSPECIALIDADES = await db.ESPECIALIDADES.FindAsync(id);
            if (eSPECIALIDADES == null)
            {
                return HttpNotFound();
            }
            return View(eSPECIALIDADES);
        }

        // GET: ESPECIALIDADES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ESPECIALIDADES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DESCRICAO,ATIVO,DATA_CRIACAO,DATA_MODIFICACAO")] ESPECIALIDADES eSPECIALIDADES)
        {
            if (ModelState.IsValid)
            {
                db.ESPECIALIDADES.Add(eSPECIALIDADES);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eSPECIALIDADES);
        }

        // GET: ESPECIALIDADES/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESPECIALIDADES eSPECIALIDADES = await db.ESPECIALIDADES.FindAsync(id);
            if (eSPECIALIDADES == null)
            {
                return HttpNotFound();
            }
            return View(eSPECIALIDADES);
        }

        // POST: ESPECIALIDADES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DESCRICAO,ATIVO,DATA_CRIACAO,DATA_MODIFICACAO")] ESPECIALIDADES eSPECIALIDADES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eSPECIALIDADES).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eSPECIALIDADES);
        }

        // GET: ESPECIALIDADES/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESPECIALIDADES eSPECIALIDADES = await db.ESPECIALIDADES.FindAsync(id);
            if (eSPECIALIDADES == null)
            {
                return HttpNotFound();
            }
            return View(eSPECIALIDADES);
        }

        // POST: ESPECIALIDADES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ESPECIALIDADES eSPECIALIDADES = await db.ESPECIALIDADES.FindAsync(id);
            db.ESPECIALIDADES.Remove(eSPECIALIDADES);
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
