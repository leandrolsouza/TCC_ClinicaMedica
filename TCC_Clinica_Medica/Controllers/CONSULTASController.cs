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
            return View();
        }

        public ActionResult Marcacao()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            ViewBag.Especialidades =
                 new SelectList(db.ESPECIALIDADES.ToList().Where(x => x.ATIVO).OrderBy(s => s.DESCRICAO).ToList(), "ID", "DESCRICAO");

            ViewBag.Medicos = new List<USUARIOS>();

            ViewBag.Pacientes =
                 new SelectList(db.USUARIOS.ToList().Where(x => x.TIPO_ACESSO == 3 && x.ATIVO).OrderBy(s => s.NOME).ToList(), "ID", "NOME");

            ViewBag.ConsultasAntigas = from s in db.CONSULTAS
                          where s.PACIENTES.ID_USUARIO == 2
                          select s;

            ViewBag.Agenda = new List<CONSULTAS>();
            ViewBag.ConsultasAntigasPaciente = new List<CONSULTAS>();

            return View();
        }

        [HttpPost]
        public ActionResult MudarMedico(int id)
        {
            ViewBag.Medicos = (from u in db.USUARIOS
                              join m in db.MEDICOS on u.ID equals m.ID_USUARIO
                              join me in db.MEDICO_ESPECIALIDADE on m.ID equals me.ID_MEDICO
                              where me.ID_ESPECIALIDADE == id && u.ATIVO && u.TIPO_ACESSO == 2
                              select u).ToList();


            return PartialView("Medicos", ViewBag.Medicos);
        }

        [HttpPost]
        public ActionResult AgendaMedico(int id)
        {
            ViewBag.Agenda = (from c in db.CONSULTAS
                               join m in db.MEDICOS on c.ID_MEDICO equals m.ID
                               join pa in db.PACIENTES on c.ID_PACIENTE equals pa.ID
                               where c.ID_MEDICO == id && !c.REALIZADA && !c.RETORNO
                               select c).ToList();


            return PartialView("Agenda", ViewBag.Agenda);
        }

        [HttpPost]
        public ActionResult AgendaPaciente(int id)
        {
            ViewBag.ConsultasAntigasPaciente = (from c in db.CONSULTAS
                              join m in db.MEDICOS on c.ID_MEDICO equals m.ID
                              join pa in db.PACIENTES on c.ID_PACIENTE equals pa.ID
                              where c.ID_PACIENTE == id && c.REALIZADA && !c.RETORNO
                              select c).ToList();


            return PartialView("ConsultasAntigasPaciente", ViewBag.ConsultasAntigasPaciente);
        }


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
        public async Task<ActionResult> Marcacao(string IDMEDICO,string IDPACIENTE, string DATA, string INICIO, string FIM,string RETORNO,string IDCONSULTARETORNO)
        {
            if (ModelState.IsValid)
            {
                CONSULTAS cONSULTAS = new CONSULTAS();
                cONSULTAS.ID_MEDICO = int.Parse(IDMEDICO);
                cONSULTAS.ID_PACIENTE = int.Parse(IDPACIENTE);
                cONSULTAS.DATA_INICIO = DateTime.Parse(DATA).AddHours(double.Parse(INICIO.Split(':').ToList()[0])).AddMinutes(double.Parse(INICIO.Split(':').ToList()[1]));
                cONSULTAS.DATA_FIM = DateTime.Parse(DATA).AddHours(double.Parse(FIM.Split(':').ToList()[0])).AddMinutes(double.Parse(FIM.Split(':').ToList()[1]));
                if(RETORNO == "1")
                {
                    cONSULTAS.RETORNO = true;
                    cONSULTAS.ID_CONSULTA_RETORNO = int.Parse(IDCONSULTARETORNO);
                }
                else
                {
                    cONSULTAS.RETORNO = false;
                }

                db.CONSULTAS.Add(cONSULTAS);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
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
