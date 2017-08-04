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
using PagedList;

namespace TCC_Clinica_Medica.Controllers
{
    public class PACIENTESController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador })]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CpfSortParm = String.IsNullOrEmpty(sortOrder) ? "cpf_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var pacientes = db.PACIENTES.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                pacientes = pacientes.Where(s => s.USUARIOS.NOME.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    pacientes = pacientes.OrderByDescending(s => s.USUARIOS.NOME);
                    break;
                case "cpf_desc":
                    pacientes = pacientes.OrderByDescending(s => s.USUARIOS.CPF);
                    break;
                default:
                    pacientes = pacientes.OrderBy(s => s.USUARIOS.NOME);
                    break;
            }

            

            pacientes = pacientes.Where(x => x.USUARIOS.ATIVO);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(pacientes.ToPagedList(pageNumber, pageSize));
        }

        // GET: PACIENTES/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.Exames = new List<EXAMES_SOLICITADOS>();

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

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador })]
        [HttpPost]
        public ActionResult MudarExames(int id)
        {
            ViewBag.Exames = (from u in db.EXAMES_SOLICITADOS
                               join c in db.CONSULTAS on u.ID_CONSULTA equals c.ID
                               join e in db.EXAMES on u.ID_EXAME equals e.ID
                               where c.ID == id
                               select u).ToList();


            return PartialView("ExamesPartial", ViewBag.Exames);
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
