using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Security.Principal;
using System.Web.Routing;
using System.Web.Security;

namespace TCC_Clinica_Medica.App_Start
{
        // Custom property
        [Serializable]
        //  When Enum mark with “Flags“ attribute it will work as bit field
        public enum UserType
        {
            Administrador = 1,
            Medico = 2,
            Paciente = 3
        }

        // Propriedade para setar os tipos de erro de acesso
        [Serializable]
        [Flags]
        public enum ErrorAccess
        {
            Permission = 2
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
        public class CustomAuthorizeAttribute : AuthorizeAttribute
        {
            public new UserType[] Roles;

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                if (httpContext == null)
                {
                    throw new ArgumentNullException("httpContext");
                }

                if (httpContext.Session["Usuario"] != null)
                {
                    bool bFlag = true;
                    var x = (USUARIOS)httpContext.Session["Usuario"];
                    UserType role = (UserType)x.TIPO_ACESSO;

                    foreach (var item in Roles.ToList())
                    {
                        if (item.ToString() == role.ToString())
                        {
                            bFlag = false;
                        }
                    }

                    if (bFlag)
                    {
                        httpContext.Session["AccessError"] = "2";
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                if (HttpContext.Current.Session["AccessError"] != null)
                {
                    int codigoAcesso = int.Parse(HttpContext.Current.Session["AccessError"].ToString());

                    if (codigoAcesso == (int)ErrorAccess.Permission)
                    {
                        HttpContext.Current.Session.Remove("AccessError");

                        filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                    }
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }
    }