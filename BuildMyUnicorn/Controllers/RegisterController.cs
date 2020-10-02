using BuildMyUnicorn.Business_Layer;
using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BuildMyUnicorn.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Signup
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignupSuccess()
        {
            return View();
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
                returnvalue = new ClientManager().ConfirmEmail(Request.QueryString["refid"].ToString());

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


        public ActionResult ForgotPassword()
        {
            return View();
        }


        public ActionResult ResetPassword()
        {

            if (Request.QueryString["refid"] != null)
            {


                string[] returnvalue = new string[3];
                returnvalue = new ClientManager().ConfirmResetPassword(Request.QueryString["refid"].ToString());

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


        public string UpdatePassword(Client Model)
        {
            new ClientManager().UpdateCustomerEmailConfirmation(Model);
            FormsAuthentication.SetAuthCookie(Model.ClientID.ToString(), true);
            return new ClientManager().UpdateCustomerPassword(Model);
        }

        public string UpdateForgotPassword(Client Model)
        {
            new ClientManager().UpdateCustomerCustomerForgotPassword(Model);

            return new ClientManager().UpdateCustomerPassword(Model);
        }


        public string AddCustomer(Client Model)
        {

            return new ClientManager().AddNewClient(Model);

        }
        public string SendPasswordResetLink(String Email)
        {

            return new ClientManager().SendPasswordRestLink(Email);
        }

        public JsonResult GetCountryList()
        {

            IEnumerable<Country> countryList = new CountryManager().GetCountryList();
            return Json(new { country = countryList }, JsonRequestBehavior.AllowGet);
        }
    }
}