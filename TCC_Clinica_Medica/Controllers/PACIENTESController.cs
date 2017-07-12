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
    public class PACIENTESController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: PACIENTES
        public async Task<ActionResult> Index()
        {
            var pACIENTES = db.PACIENTES.Include(p => p.PLANO_SAUDE).Include(p => p.USUARIOS);
            return View(await pACIENTES.ToListAsync());
        }

        // GET: PACIENTES/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTES pACIENTES = await db.PACIENTES.FindAsync(id);
            if (pACIENTES == null)
            {
                return HttpNotFound();
            }
            return View(pACIENTES);
        }

        // GET: PACIENTES/Create
        public ActionResult Create()
        {
            ViewBag.ID_PLANO_SAUDE = new SelectList(db.PLANO_SAUDE, "ID", "DESCRICAO");
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID", "NOME");
            return View();
        }

        // POST: PACIENTES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ENDERECO,ID_PLANO_SAUDE,ID_USUARIO,TELEFONE")] PACIENTES pACIENTES)
        {
            if (ModelState.IsValid)
            {
                db.PACIENTES.Add(pACIENTES);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PLANO_SAUDE = new SelectList(db.PLANO_SAUDE, "ID", "DESCRICAO", pACIENTES.ID_PLANO_SAUDE);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID", "NOME", pACIENTES.ID_USUARIO);
            return View(pACIENTES);
        }

        // GET: PACIENTES/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTES pACIENTES = await db.PACIENTES.FindAsync(id);
            if (pACIENTES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PLANO_SAUDE = new SelectList(db.PLANO_SAUDE, "ID", "DESCRICAO", pACIENTES.ID_PLANO_SAUDE);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID", "NOME", pACIENTES.ID_USUARIO);
            return View(pACIENTES);
        }

        // POST: PACIENTES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ENDERECO,ID_PLANO_SAUDE,ID_USUARIO,TELEFONE")] PACIENTES pACIENTES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pACIENTES).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PLANO_SAUDE = new SelectList(db.PLANO_SAUDE, "ID", "DESCRICAO", pACIENTES.ID_PLANO_SAUDE);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID", "NOME", pACIENTES.ID_USUARIO);
            return View(pACIENTES);
        }

        // GET: PACIENTES/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTES pACIENTES = await db.PACIENTES.FindAsync(id);
            if (pACIENTES == null)
            {
                return HttpNotFound();
            }
            return View(pACIENTES);
        }

        // POST: PACIENTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PACIENTES pACIENTES = await db.PACIENTES.FindAsync(id);
            db.PACIENTES.Remove(pACIENTES);
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
