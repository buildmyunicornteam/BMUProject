using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BuildMyUnicorn_Supplier.Business_Layer;
using Model_Layer.Models;

namespace BuildMyUnicorn_Supplier.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
          
            return View();
        }

        public ActionResult SignupSuccess()
        {
            return View();
        }

        public string AddSupplier(Supplier Model)
        {

            return new SupplierManager().AddNewSupplier(Model);

        }

        public ActionResult ResetPasswordEmailSuccess()
        {
            return View();
        }

        public ActionResult EmailVerification()
        {

            if (Request.QueryString["refid"] != null)
            {


                string[] returnvalue = new string[3];
                returnvalue = new SupplierManager().ConfirmEmail(Request.QueryString["refid"].ToString());

                if (returnvalue[0] == "OK")
                {
                    ViewBag.SupplierID = returnvalue[1];
                    ViewBag.ConfirmationID = returnvalue[2];
                    ViewBag.SupplierName = returnvalue[3];
                    return View();
                }
                else
                {
                    return PartialView("_BadRequest");

                }
            }
            else
            {
                return PartialView("_BadRequest");

            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }


        public ActionResult ResetPassword()
        {

            if (Request.QueryString["refid"] != null)
            {


                string[] returnvalue = new string[3];
                returnvalue = new SupplierManager().ConfirmResetPassword(Request.QueryString["refid"].ToString());

                if (returnvalue[0] == "OK")
                {
                    ViewBag.ClientID = returnvalue[1];
                    ViewBag.ConfirmationID = returnvalue[2];
                    ViewBag.CustomerName = returnvalue[3];
                    return View();
                }
                else
                {
                    return PartialView("_BadRequest");

                }
            }
            else
            {
                return PartialView("_BadRequest");

            }
        }


        public string UpdatePassword(Supplier Model)
        {
            new SupplierManager().UpdateEmailConfirmation(Model.ConfirmationID);
            FormsAuthentication.SetAuthCookie(Model.SupplierID.ToString(), true);
            return new SupplierManager().UpdateSupplierPassword(Model);
        }

        //public string UpdateForgotPassword(Client Model)
        //{
        //    new SupplierManager().UpdateCustomerCustomerForgotPassword(Model);

        //    return new SupplierManager().UpdateCustomerPassword(Model);
        //}


       

        public string SendPasswordResetLink(String Email)
        {

            return new SupplierManager().SendPasswordRestLink(Email);
        }

        public JsonResult GetCountryList()
        {

            IEnumerable<Country> countryList = new CountryManager().GetCountryList();
            return Json(new { country = countryList }, JsonRequestBehavior.AllowGet);
        }
    }
}