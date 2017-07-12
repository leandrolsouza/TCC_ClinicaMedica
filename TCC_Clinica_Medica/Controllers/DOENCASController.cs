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
    public class DOENCASController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: DOENCAS
        public async Task<ActionResult> Index()
        {
            return View(await db.DOENCAS.ToListAsync());
        }

        // GET: DOENCAS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
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

        // GET: DOENCAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DOENCAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,CODIGO,DESCRICAO,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] DOENCAS dOENCAS)
        {
            if (ModelState.IsValid)
            {
                db.DOENCAS.Add(dOENCAS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(dOENCAS);
        }

        // GET: DOENCAS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
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

        // POST: DOENCAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CODIGO,DESCRICAO,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] DOENCAS dOENCAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dOENCAS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dOENCAS);
        }

        // GET: DOENCAS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
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

        // POST: DOENCAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DOENCAS dOENCAS = await db.DOENCAS.FindAsync(id);
            db.DOENCAS.Remove(dOENCAS);
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
