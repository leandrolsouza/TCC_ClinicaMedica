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
    public class CLINICASController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: CLINICAS
        public async Task<ActionResult> Index()
        {
            return View(await db.CLINICAS.ToListAsync());
        }

        // GET: CLINICAS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
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

        // GET: CLINICAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CLINICAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NOME,CNPJ,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] CLINICAS cLINICAS)
        {
            if (ModelState.IsValid)
            {
                db.CLINICAS.Add(cLINICAS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cLINICAS);
        }

        // GET: CLINICAS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
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

        // POST: CLINICAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NOME,CNPJ,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] CLINICAS cLINICAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLINICAS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cLINICAS);
        }

        // GET: CLINICAS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
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

        // POST: CLINICAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CLINICAS cLINICAS = await db.CLINICAS.FindAsync(id);
            db.CLINICAS.Remove(cLINICAS);
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
