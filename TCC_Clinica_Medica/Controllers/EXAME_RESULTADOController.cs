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
    public class EXAME_RESULTADOController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: EXAME_RESULTADO
        public async Task<ActionResult> Index()
        {
            var eXAME_RESULTADO = db.EXAME_RESULTADO.Include(e => e.EXAMES_SOLICITADOS);
            return View(await eXAME_RESULTADO.ToListAsync());
        }

        // GET: EXAME_RESULTADO/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAME_RESULTADO eXAME_RESULTADO = await db.EXAME_RESULTADO.FindAsync(id);
            if (eXAME_RESULTADO == null)
            {
                return HttpNotFound();
            }
            return View(eXAME_RESULTADO);
        }

        // GET: EXAME_RESULTADO/Create
        public ActionResult Create()
        {
            ViewBag.ID_EXAMES_SOLICITADO = new SelectList(db.EXAMES_SOLICITADOS, "ID", "ID");
            return View();
        }

        // POST: EXAME_RESULTADO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DESCRICAO,ID_EXAMES_SOLICITADO,DATA_CRIACAO,ENTREGUE_PACIENTE")] EXAME_RESULTADO eXAME_RESULTADO)
        {
            if (ModelState.IsValid)
            {
                db.EXAME_RESULTADO.Add(eXAME_RESULTADO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EXAMES_SOLICITADO = new SelectList(db.EXAMES_SOLICITADOS, "ID", "ID", eXAME_RESULTADO.ID_EXAMES_SOLICITADO);
            return View(eXAME_RESULTADO);
        }

        // GET: EXAME_RESULTADO/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAME_RESULTADO eXAME_RESULTADO = await db.EXAME_RESULTADO.FindAsync(id);
            if (eXAME_RESULTADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EXAMES_SOLICITADO = new SelectList(db.EXAMES_SOLICITADOS, "ID", "ID", eXAME_RESULTADO.ID_EXAMES_SOLICITADO);
            return View(eXAME_RESULTADO);
        }

        // POST: EXAME_RESULTADO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DESCRICAO,ID_EXAMES_SOLICITADO,DATA_CRIACAO,ENTREGUE_PACIENTE")] EXAME_RESULTADO eXAME_RESULTADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eXAME_RESULTADO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EXAMES_SOLICITADO = new SelectList(db.EXAMES_SOLICITADOS, "ID", "ID", eXAME_RESULTADO.ID_EXAMES_SOLICITADO);
            return View(eXAME_RESULTADO);
        }

        // GET: EXAME_RESULTADO/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAME_RESULTADO eXAME_RESULTADO = await db.EXAME_RESULTADO.FindAsync(id);
            if (eXAME_RESULTADO == null)
            {
                return HttpNotFound();
            }
            return View(eXAME_RESULTADO);
        }

        // POST: EXAME_RESULTADO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EXAME_RESULTADO eXAME_RESULTADO = await db.EXAME_RESULTADO.FindAsync(id);
            db.EXAME_RESULTADO.Remove(eXAME_RESULTADO);
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
