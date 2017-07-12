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
    public class HORARIOS_ATENDIMENTOController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: HORARIOS_ATENDIMENTO
        public async Task<ActionResult> Index()
        {
            return View(await db.HORARIOS_ATENDIMENTO.ToListAsync());
        }

        // GET: HORARIOS_ATENDIMENTO/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORARIOS_ATENDIMENTO hORARIOS_ATENDIMENTO = await db.HORARIOS_ATENDIMENTO.FindAsync(id);
            if (hORARIOS_ATENDIMENTO == null)
            {
                return HttpNotFound();
            }
            return View(hORARIOS_ATENDIMENTO);
        }

        // GET: HORARIOS_ATENDIMENTO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HORARIOS_ATENDIMENTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DESCRICAO,HORA_INICIO,HORA_FIM,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] HORARIOS_ATENDIMENTO hORARIOS_ATENDIMENTO)
        {
            if (ModelState.IsValid)
            {
                db.HORARIOS_ATENDIMENTO.Add(hORARIOS_ATENDIMENTO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hORARIOS_ATENDIMENTO);
        }

        // GET: HORARIOS_ATENDIMENTO/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORARIOS_ATENDIMENTO hORARIOS_ATENDIMENTO = await db.HORARIOS_ATENDIMENTO.FindAsync(id);
            if (hORARIOS_ATENDIMENTO == null)
            {
                return HttpNotFound();
            }
            return View(hORARIOS_ATENDIMENTO);
        }

        // POST: HORARIOS_ATENDIMENTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DESCRICAO,HORA_INICIO,HORA_FIM,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] HORARIOS_ATENDIMENTO hORARIOS_ATENDIMENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hORARIOS_ATENDIMENTO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hORARIOS_ATENDIMENTO);
        }

        // GET: HORARIOS_ATENDIMENTO/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HORARIOS_ATENDIMENTO hORARIOS_ATENDIMENTO = await db.HORARIOS_ATENDIMENTO.FindAsync(id);
            if (hORARIOS_ATENDIMENTO == null)
            {
                return HttpNotFound();
            }
            return View(hORARIOS_ATENDIMENTO);
        }

        // POST: HORARIOS_ATENDIMENTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HORARIOS_ATENDIMENTO hORARIOS_ATENDIMENTO = await db.HORARIOS_ATENDIMENTO.FindAsync(id);
            db.HORARIOS_ATENDIMENTO.Remove(hORARIOS_ATENDIMENTO);
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
