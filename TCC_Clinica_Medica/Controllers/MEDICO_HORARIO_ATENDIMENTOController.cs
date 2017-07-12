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
    public class MEDICO_HORARIO_ATENDIMENTOController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: MEDICO_HORARIO_ATENDIMENTO
        public async Task<ActionResult> Index()
        {
            var mEDICO_HORARIO_ATENDIMENTO = db.MEDICO_HORARIO_ATENDIMENTO.Include(m => m.HORARIOS_ATENDIMENTO).Include(m => m.MEDICOS);
            return View(await mEDICO_HORARIO_ATENDIMENTO.ToListAsync());
        }

        // GET: MEDICO_HORARIO_ATENDIMENTO/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICO_HORARIO_ATENDIMENTO mEDICO_HORARIO_ATENDIMENTO = await db.MEDICO_HORARIO_ATENDIMENTO.FindAsync(id);
            if (mEDICO_HORARIO_ATENDIMENTO == null)
            {
                return HttpNotFound();
            }
            return View(mEDICO_HORARIO_ATENDIMENTO);
        }

        // GET: MEDICO_HORARIO_ATENDIMENTO/Create
        public ActionResult Create()
        {
            ViewBag.ID_HORARIO_ATENDIMENTO = new SelectList(db.HORARIOS_ATENDIMENTO, "ID", "DESCRICAO");
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM");
            return View();
        }

        // POST: MEDICO_HORARIO_ATENDIMENTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ID_MEDICO,ID_HORARIO_ATENDIMENTO")] MEDICO_HORARIO_ATENDIMENTO mEDICO_HORARIO_ATENDIMENTO)
        {
            if (ModelState.IsValid)
            {
                db.MEDICO_HORARIO_ATENDIMENTO.Add(mEDICO_HORARIO_ATENDIMENTO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_HORARIO_ATENDIMENTO = new SelectList(db.HORARIOS_ATENDIMENTO, "ID", "DESCRICAO", mEDICO_HORARIO_ATENDIMENTO.ID_HORARIO_ATENDIMENTO);
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM", mEDICO_HORARIO_ATENDIMENTO.ID_MEDICO);
            return View(mEDICO_HORARIO_ATENDIMENTO);
        }

        // GET: MEDICO_HORARIO_ATENDIMENTO/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICO_HORARIO_ATENDIMENTO mEDICO_HORARIO_ATENDIMENTO = await db.MEDICO_HORARIO_ATENDIMENTO.FindAsync(id);
            if (mEDICO_HORARIO_ATENDIMENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_HORARIO_ATENDIMENTO = new SelectList(db.HORARIOS_ATENDIMENTO, "ID", "DESCRICAO", mEDICO_HORARIO_ATENDIMENTO.ID_HORARIO_ATENDIMENTO);
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM", mEDICO_HORARIO_ATENDIMENTO.ID_MEDICO);
            return View(mEDICO_HORARIO_ATENDIMENTO);
        }

        // POST: MEDICO_HORARIO_ATENDIMENTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ID_MEDICO,ID_HORARIO_ATENDIMENTO")] MEDICO_HORARIO_ATENDIMENTO mEDICO_HORARIO_ATENDIMENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEDICO_HORARIO_ATENDIMENTO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_HORARIO_ATENDIMENTO = new SelectList(db.HORARIOS_ATENDIMENTO, "ID", "DESCRICAO", mEDICO_HORARIO_ATENDIMENTO.ID_HORARIO_ATENDIMENTO);
            ViewBag.ID_MEDICO = new SelectList(db.MEDICOS, "ID", "CRM", mEDICO_HORARIO_ATENDIMENTO.ID_MEDICO);
            return View(mEDICO_HORARIO_ATENDIMENTO);
        }

        // GET: MEDICO_HORARIO_ATENDIMENTO/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEDICO_HORARIO_ATENDIMENTO mEDICO_HORARIO_ATENDIMENTO = await db.MEDICO_HORARIO_ATENDIMENTO.FindAsync(id);
            if (mEDICO_HORARIO_ATENDIMENTO == null)
            {
                return HttpNotFound();
            }
            return View(mEDICO_HORARIO_ATENDIMENTO);
        }

        // POST: MEDICO_HORARIO_ATENDIMENTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MEDICO_HORARIO_ATENDIMENTO mEDICO_HORARIO_ATENDIMENTO = await db.MEDICO_HORARIO_ATENDIMENTO.FindAsync(id);
            db.MEDICO_HORARIO_ATENDIMENTO.Remove(mEDICO_HORARIO_ATENDIMENTO);
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
