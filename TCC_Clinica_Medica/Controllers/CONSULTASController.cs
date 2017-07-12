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
    public class CONSULTASController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: CONSULTAS
        public async Task<ActionResult> Index()
        {
            var cONSULTAS = db.CONSULTAS.Include(c => c.CONSULTAS2).Include(c => c.MEDICOS).Include(c => c.PACIENTES);
            return View(await cONSULTAS.ToListAsync());
        }

        // GET: CONSULTAS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONSULTAS cONSULTAS = await db.CONSULTAS.FindAsync(id);
            if (cONSULTAS == null)
            {
                return HttpNotFound();
            }
            return View(cONSULTAS);
        }

        // GET: CONSULTAS/Create
        public ActionResult Create()
        {
            ViewBag.ID_CONSULTA_RETORNO = new SelectList(db.CONSULTAS, "ID", "ID");
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM");
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTES, "ID", "ENDERECO");
            return View();
        }

        // POST: CONSULTAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ID_MEDICO,ID_PACIENTE,DATA_INICIO,DATA_FIM,REALIZADA,RETORNO,ID_CONSULTA_RETORNO")] CONSULTAS cONSULTAS)
        {
            if (ModelState.IsValid)
            {
                db.CONSULTAS.Add(cONSULTAS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CONSULTA_RETORNO = new SelectList(db.CONSULTAS, "ID", "ID", cONSULTAS.ID_CONSULTA_RETORNO);
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM", cONSULTAS.ID_MEDICO);
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTES, "ID", "ENDERECO", cONSULTAS.ID_PACIENTE);
            return View(cONSULTAS);
        }

        // GET: CONSULTAS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONSULTAS cONSULTAS = await db.CONSULTAS.FindAsync(id);
            if (cONSULTAS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CONSULTA_RETORNO = new SelectList(db.CONSULTAS, "ID", "ID", cONSULTAS.ID_CONSULTA_RETORNO);
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM", cONSULTAS.ID_MEDICO);
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTES, "ID", "ENDERECO", cONSULTAS.ID_PACIENTE);
            return View(cONSULTAS);
        }

        // POST: CONSULTAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ID_MEDICO,ID_PACIENTE,DATA_INICIO,DATA_FIM,REALIZADA,RETORNO,ID_CONSULTA_RETORNO")] CONSULTAS cONSULTAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONSULTAS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CONSULTA_RETORNO = new SelectList(db.CONSULTAS, "ID", "ID", cONSULTAS.ID_CONSULTA_RETORNO);
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM", cONSULTAS.ID_MEDICO);
            ViewBag.ID_PACIENTE = new SelectList(db.PACIENTES, "ID", "ENDERECO", cONSULTAS.ID_PACIENTE);
            return View(cONSULTAS);
        }

        // GET: CONSULTAS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONSULTAS cONSULTAS = await db.CONSULTAS.FindAsync(id);
            if (cONSULTAS == null)
            {
                return HttpNotFound();
            }
            return View(cONSULTAS);
        }

        // POST: CONSULTAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CONSULTAS cONSULTAS = await db.CONSULTAS.FindAsync(id);
            db.CONSULTAS.Remove(cONSULTAS);
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
