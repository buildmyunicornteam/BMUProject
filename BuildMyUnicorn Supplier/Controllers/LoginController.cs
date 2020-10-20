using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BuildMyUnicorn_Supplier.Business_Layer;
using Business_Model.Model;

namespace BuildMyUnicorn_Supplier.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public string ValidateUser(Supplier Model)
        {
            return new SupplierManager().ValidateLogin(Model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}