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
using System.IO;
using System.Data.Entity.Validation;

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


            usuarios = usuarios.Where(x => x.ATIVO);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(usuarios.ToPagedList(pageNumber, pageSize));
        }

        // GET: USUARIOS/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            ViewBag.Planos = db.PLANO_SAUDE.ToList().AsEnumerable().Select(x =>
                      new SelectListItem
                      {
                          Value = x.ID.ToString(),
                          Text = x.DESCRICAO
                      }); ;

            ViewBag.Clinicas = db.CLINICAS.ToList().AsEnumerable().Select(x =>
                     new SelectListItem
                     {
                         Value = x.ID.ToString(),
                         Text = x.NOME
                     }); ;

            ViewBag.Especialidades = db.ESPECIALIDADES.ToList().AsEnumerable().Select(x =>
                    new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.DESCRICAO
                    }); ;

            ViewBag.Horarios = db.HORARIOS_ATENDIMENTO.ToList().AsEnumerable().Select(x =>
                    new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.HORA_INICIO.Value.ToShortTimeString() + " - " +x.HORA_FIM.Value.ToShortTimeString()
                    }); ;



            return View();
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NOME,EMAIL,CPF,SENHA,TIPO_ACESSO,FOTO")] USUARIOS uSUARIOS, 
            string ENDERECO, string TELEFONE, string PLANO, string NUMERO_PLANO, string CRM, string CLINICA, string ESPECIALIDADE, string HORARIO)
        {
            try
            {
                uSUARIOS.DATA_CRIACAO = DateTime.Now;
                uSUARIOS.DATA_MODIFICACAO = DateTime.Now;
                uSUARIOS.ATIVO = true;
                uSUARIOS.SENHA = new Crypto.Crypto().Encrypt("123");
                uSUARIOS.FOTO = ImageToByteArray(Resources.user);
                uSUARIOS.EMAIL = uSUARIOS.EMAIL.ToLower();
                uSUARIOS.NOME = uSUARIOS.NOME.ToUpper();

                if (ModelState.IsValid)
                {
                    db.USUARIOS.Add(uSUARIOS);
                    await db.SaveChangesAsync();

                    if(uSUARIOS.TIPO_ACESSO == 3)
                    {
                        PACIENTES pACIENTES = new PACIENTES();
                        pACIENTES.ENDERECO = ENDERECO;
                        pACIENTES.ID_USUARIO = uSUARIOS.ID;
                        pACIENTES.NUMERO_PLANO = NUMERO_PLANO;
                        pACIENTES.TELEFONE = TELEFONE;
                        pACIENTES.ID_PLANO_SAUDE = int.Parse(PLANO);
                        db.PACIENTES.Add(pACIENTES);
                        await db.SaveChangesAsync();
                    }

                    if (uSUARIOS.TIPO_ACESSO == 2)
                    {
                        MEDICOS mEDICOS = new MEDICOS();
                        mEDICOS.CRM = CRM;
                        mEDICOS.ID_USUARIO = uSUARIOS.ID;
                        db.MEDICOS.Add(mEDICOS);
                        await db.SaveChangesAsync();
                        
                        MEDICO_CLINICA mc = new MEDICO_CLINICA();
                        mc.ID_CLINICA = int.Parse(CLINICA);
                        mc.ID_MEDICO = mEDICOS.ID;
                        db.MEDICO_CLINICA.Add(mc);

                        MEDICO_ESPECIALIDADE mc2 = new MEDICO_ESPECIALIDADE();
                        mc2.ID_ESPECIALIDADE = int.Parse(ESPECIALIDADE);
                        mc2.ID_MEDICO = mEDICOS.ID;
                        db.MEDICO_ESPECIALIDADE.Add(mc2);

                        MEDICO_HORARIO_ATENDIMENTO mc3 = new MEDICO_HORARIO_ATENDIMENTO();
                        mc3.ID_HORARIO_ATENDIMENTO = int.Parse(HORARIO);
                        mc3.ID_MEDICO = mEDICOS.ID;
                        db.MEDICO_HORARIO_ATENDIMENTO.Add(mc3);

                        await db.SaveChangesAsync();

                    }

                    return RedirectToAction("Index", "USUARIOS", new { mensagem = "Registro criado com sucesso!" });
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }


            return View(uSUARIOS);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "LOGIN");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            USUARIOS uSUARIOS = await db.USUARIOS.FindAsync(id);

            ViewBag.Planos = db.PLANO_SAUDE.ToList().AsEnumerable().Select(x =>
                             new SelectListItem
                             {
                                 Value = x.ID.ToString(),
                                 Text = x.DESCRICAO
                             }); ;

            if(uSUARIOS.TIPO_ACESSO == 2)
            {
                ViewBag.Clinicas =
                     new SelectList(db.CLINICAS.ToList().OrderBy(s => s.NOME).ToList(), "ID", "NOME", uSUARIOS.MEDICOS.ToList()[0].MEDICO_CLINICA.ToList()[0].ID_CLINICA);
            }

            ViewBag.Especialidades = db.ESPECIALIDADES.ToList().AsEnumerable().Select(x =>
                    new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.DESCRICAO
                    }); ;

            ViewBag.Horarios = db.HORARIOS_ATENDIMENTO.ToList().AsEnumerable().Select(x =>
                    new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = x.HORA_INICIO.Value.ToShortTimeString() + " - " + x.HORA_FIM.Value.ToShortTimeString()
                    }); ;

          
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NOME,EMAIL,CPF,SENHA,TIPO_ACESSO,FOTO,DATA_CRIACAO,DATA_MODIFICACAO,ATIVO")] USUARIOS uSUARIOS,
            string ENDERECO, string TELEFONE, string PLANO, string NUMERO_PLANO, string CRM, string CLINICA, string ESPECIALIDADE, string HORARIO)
        {
            uSUARIOS.DATA_MODIFICACAO = DateTime.Now;
            uSUARIOS.EMAIL = uSUARIOS.EMAIL.ToLower();
            uSUARIOS.NOME = uSUARIOS.NOME.ToUpper();
            
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIOS).State = EntityState.Modified;
                await db.SaveChangesAsync();

                if (uSUARIOS.TIPO_ACESSO == 3)
                {
                    var pac = db.PACIENTES.FirstOrDefault(x => x.ID_USUARIO == uSUARIOS.ID);
                    var pACIENTES = db.PACIENTES.Find(uSUARIOS.PACIENTES.ToList()[0].ID);
                    pACIENTES.ENDERECO = ENDERECO;
                    pACIENTES.NUMERO_PLANO = NUMERO_PLANO;
                    pACIENTES.TELEFONE = TELEFONE;
                    pACIENTES.ID_PLANO_SAUDE = int.Parse(PLANO);
                    await db.SaveChangesAsync();
                }

                if (uSUARIOS.TIPO_ACESSO == 2)
                {

                }


                    return RedirectToAction("Index", new { mensagem = "Registro editado com sucesso!" });
            }
            return View(uSUARIOS);
        }



        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            USUARIOS uSUARIO = db.USUARIOS.Find(id);
            uSUARIO.ATIVO = false;
            db.SaveChanges();

            return Json(Url.Action("Index", new { mensagem = "Registro desativado com sucesso!" }));
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
