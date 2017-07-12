﻿using System;
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
    public class PLANO_SAUDEController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();
        
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            /*
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            */
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var planos = db.PLANO_SAUDE.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                planos = planos.Where(s => s.DESCRICAO.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    planos = planos.OrderByDescending(s => s.DESCRICAO);
                    break;
                default:
                    planos = planos.OrderBy(s => s.DESCRICAO);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(planos.ToPagedList(pageNumber, pageSize));
        }

        // GET: PLANO_SAUDE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PLANO_SAUDE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,DESCRICAO")] PLANO_SAUDE pLANO_SAUDE)
        {
            pLANO_SAUDE.DATA_CRIACAO = DateTime.Now;
            pLANO_SAUDE.DATA_MODIFICACAO = DateTime.Now;
            pLANO_SAUDE.ATIVO = true;

            if (ModelState.IsValid)
            {
                db.PLANO_SAUDE.Add(pLANO_SAUDE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "PLANO_SAUDE", new { mensagem = "Registro criado com sucesso!" });
            }

            return View(pLANO_SAUDE);
        }

        // GET: PLANO_SAUDE/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLANO_SAUDE pLANO_SAUDE = await db.PLANO_SAUDE.FindAsync(id);
            if (pLANO_SAUDE == null)
            {
                return HttpNotFound();
            }
            return View(pLANO_SAUDE);
        }

        // POST: PLANO_SAUDE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,DESCRICAO,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] PLANO_SAUDE pLANO_SAUDE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pLANO_SAUDE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pLANO_SAUDE);
        }

        // GET: PLANO_SAUDE/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLANO_SAUDE pLANO_SAUDE = await db.PLANO_SAUDE.FindAsync(id);
            if (pLANO_SAUDE == null)
            {
                return HttpNotFound();
            }
            return View(pLANO_SAUDE);
        }

        // POST: PLANO_SAUDE/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            PLANO_SAUDE pLANO_SAUDE = db.PLANO_SAUDE.Find(id);
            db.PLANO_SAUDE.Remove(pLANO_SAUDE);
            db.SaveChangesAsync();
            return Json(Url.Action("Index", new { mensagem = "Registro apagado com sucesso!" }));
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
