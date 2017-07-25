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
using TCC_Clinica_Medica.App_Start;

namespace TCC_Clinica_Medica.Controllers
{
    public class CONSULTASController : Controller
    {
        private TCC_CLINICA_MEDICAEntities db = new TCC_CLINICA_MEDICAEntities();

        // GET: CONSULTAS
        public ActionResult Agendadas(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

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

            var consultas = db.CONSULTAS.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                consultas = consultas.Where(s => s.DATA_INICIO.ToShortDateString() == searchString);

                if (consultas.ToList().Count == 0)
                {
                    consultas = db.CONSULTAS.ToList().AsEnumerable();
                    consultas = consultas.Where(s => s.DATA_INICIO.ToLongTimeString() == searchString);
                }

                if (consultas.ToList().Count == 0)
                {
                    consultas = db.CONSULTAS.ToList().AsEnumerable();
                    consultas = consultas.Where(s => s.MEDICOS.USUARIOS.NOME.ToUpper().Contains(searchString.ToUpper()));
                }

                if (consultas.ToList().Count == 0)
                {
                    consultas = db.CONSULTAS.ToList().AsEnumerable();
                    consultas = consultas.Where(s => s.PACIENTES.USUARIOS.NOME.ToUpper().Contains(searchString.ToUpper()));
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    consultas = consultas.OrderByDescending(s => s.DATA_INICIO);
                    break;
                default:
                    consultas = consultas.OrderBy(s => s.DATA_INICIO);
                    break;
            }

            consultas = consultas.Where(x => !x.CANCELADA.Value && !x.REALIZADA);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(consultas.ToPagedList(pageNumber, pageSize));
        }

        [CustomAuthorize(Roles = new UserType[] { UserType.Medico })]
        public ActionResult ConsultasMedico(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

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

            var consultas = db.CONSULTAS.ToList().AsEnumerable().Where(x => x.ID_MEDICO == ((USUARIOS)Session["Usuario"]).MEDICOS.ToList()[0].ID);

            if (!String.IsNullOrEmpty(searchString))
            {
                consultas = consultas.Where(s => s.DATA_INICIO.ToShortDateString() == searchString);

                if (consultas.ToList().Count == 0)
                {
                    db.CONSULTAS.ToList().AsEnumerable().Where(x => x.ID_MEDICO == ((USUARIOS)Session["Usuario"]).MEDICOS.ToList()[0].ID);
                    consultas = consultas.Where(s => s.DATA_INICIO.ToLongTimeString() == searchString);
                }

                if (consultas.ToList().Count == 0)
                {
                    db.CONSULTAS.ToList().AsEnumerable().Where(x => x.ID_MEDICO == ((USUARIOS)Session["Usuario"]).MEDICOS.ToList()[0].ID);
                    consultas = consultas.Where(s => s.MEDICOS.USUARIOS.NOME.ToUpper().Contains(searchString.ToUpper()));
                }

                if (consultas.ToList().Count == 0)
                {
                    db.CONSULTAS.ToList().AsEnumerable().Where(x => x.ID_MEDICO == ((USUARIOS)Session["Usuario"]).MEDICOS.ToList()[0].ID);
                    consultas = consultas.Where(s => s.PACIENTES.USUARIOS.NOME.ToUpper().Contains(searchString.ToUpper()));
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    consultas = consultas.OrderByDescending(s => s.DATA_INICIO);
                    break;
                default:
                    consultas = consultas.OrderBy(s => s.DATA_INICIO);
                    break;
            }

            consultas = consultas.Where(x => !x.CANCELADA.Value && !x.REALIZADA);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(consultas.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Finalizadas(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

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

            var consultas = db.CONSULTAS.ToList().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                consultas = consultas.Where(s => s.DATA_INICIO.ToShortDateString() == searchString);

                if (consultas.ToList().Count == 0)
                {
                    consultas = db.CONSULTAS.ToList().AsEnumerable();
                    consultas = consultas.Where(s => s.DATA_INICIO.ToLongTimeString() == searchString);
                }

                if (consultas.ToList().Count == 0)
                {
                    consultas = db.CONSULTAS.ToList().AsEnumerable();
                    consultas = consultas.Where(s => s.MEDICOS.USUARIOS.NOME.ToUpper().Contains(searchString.ToUpper()));
                }

                if (consultas.ToList().Count == 0)
                {
                    consultas = db.CONSULTAS.ToList().AsEnumerable();
                    consultas = consultas.Where(s => s.PACIENTES.USUARIOS.NOME.ToUpper().Contains(searchString.ToUpper()));
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    consultas = consultas.OrderByDescending(s => s.DATA_INICIO);
                    break;
                default:
                    consultas = consultas.OrderBy(s => s.DATA_INICIO);
                    break;
            }

            consultas = consultas.Where(x => x.CANCELADA.Value || x.REALIZADA);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(consultas.ToPagedList(pageNumber, pageSize));
        }

        [CustomAuthorize(Roles = new UserType[] { UserType.Medico })]
        public ActionResult Consulta()
        {
            ViewBag.Exames = db.EXAMES.ToList().Where(x=> x.ATIVO).ToList();
            ViewBag.Doencas = db.DOENCAS.ToList().Where(x => x.ATIVO).ToList();
            ViewBag.Medicamentos = db.MEDICAMENTOS.ToList().Where(x => x.ATIVO).ToList();

            return View();
        }

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador })]
        public ActionResult Marcacao()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            ViewBag.Especialidades =
                 new SelectList(db.ESPECIALIDADES.ToList().Where(x => x.ATIVO).OrderBy(s => s.DESCRICAO).ToList(), "ID", "DESCRICAO");

            ViewBag.Medicos = new List<USUARIOS>();

            ViewBag.Pacientes = new SelectList((from p in db.PACIENTES
                                 join usu in db.USUARIOS on p.ID_USUARIO equals usu.ID
                                 where usu.ATIVO
                                 select new { NOME = usu.NOME, ID = p.ID }
                                 ).ToList(), "ID", "NOME");

            ViewBag.ConsultasAntigas = from s in db.CONSULTAS
                          where s.PACIENTES.ID_USUARIO == 2
                          select s;

            ViewBag.Agenda = new List<CONSULTAS>();
            ViewBag.ConsultasAntigasPaciente = new List<CONSULTAS>();

            return View();
        }

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador })]
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

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador })]
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

         [CustomAuthorize(Roles = new UserType[] {UserType.Administrador})]
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

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador })]
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
                    cONSULTAS.ID_CONSULTA_RETORNO = null;
                    cONSULTAS.RETORNO = false;
                }

                db.CONSULTAS.Add(cONSULTAS);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
        }

        [CustomAuthorize(Roles = new UserType[] { UserType.Administrador })]
        [HttpPost]
        public ActionResult Cancelar(int id)
        {
            CONSULTAS consulta = db.CONSULTAS.Find(id);
            consulta.CANCELADA = true;
            db.SaveChanges();
            return Json(Url.Action("Index", new { mensagem = "Registro cancelado com sucesso!" }));
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
