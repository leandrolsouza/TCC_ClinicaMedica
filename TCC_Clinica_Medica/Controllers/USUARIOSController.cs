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
using PagedList;

namespace TCC_Clinica_Medica.Controllers
{
    public class USUARIOSController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewBag.CPFSortParm = String.IsNullOrEmpty(sortOrder) ? "cpf_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var usuarios = db.USUARIOS.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(s => s.NOME.ToUpper().Contains(searchString.ToUpper()));

                if (usuarios.ToList().Count == 0)
                {
                    usuarios = db.USUARIOS.ToList().AsEnumerable();
                    usuarios = usuarios.Where(s => s.EMAIL.ToUpper().Contains(searchString.ToUpper()));
                }

                if (usuarios.ToList().Count == 0)
                {
                    usuarios = db.USUARIOS.ToList().AsEnumerable();
                    usuarios = usuarios.Where(s => s.CPF.ToUpper().Contains(searchString.ToUpper()));
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    usuarios = usuarios.OrderByDescending(s => s.NOME);
                    break;
                case "email_desc":
                    usuarios = usuarios.OrderByDescending(s => s.EMAIL);
                    break;
                case "cpf_desc":
                    usuarios = usuarios.OrderByDescending(s => s.CPF);
                    break;
                default:
                    usuarios = usuarios.OrderBy(s => s.NOME);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(usuarios.ToPagedList(pageNumber, pageSize));
        }

        // GET: USUARIOS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USUARIOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NOME,EMAIL,CPF,SENHA,TIPO_ACESSO,FOTO")] USUARIOS uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                db.USUARIOS.Add(uSUARIOS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(uSUARIOS);
        }

        // GET: USUARIOS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = await db.USUARIOS.FindAsync(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // POST: USUARIOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NOME,EMAIL,CPF,SENHA,TIPO_ACESSO,FOTO")] USUARIOS uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIOS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(uSUARIOS);
        }

        // GET: USUARIOS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = await db.USUARIOS.FindAsync(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // POST: USUARIOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            USUARIOS uSUARIOS = await db.USUARIOS.FindAsync(id);
            db.USUARIOS.Remove(uSUARIOS);
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
