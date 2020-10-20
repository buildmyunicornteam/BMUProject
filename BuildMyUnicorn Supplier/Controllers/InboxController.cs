using System;
using System.Web.Mvc;
using BuildMyUnicorn_Supplier.Business_Layer;
using Business_Model.Model;

namespace BuildMyUnicorn_Supplier.Controllers
{
    public class InboxController : WebController
    {
        // GET: Inbox
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ClientPackageOrderList()
        {
       
            return PartialView("_OrderPackagePartial", new SupplierManager().GetSupplierClientList());
        }

        public ActionResult Client(Guid ClientID)
        { 
            Client obj = new SupplierManager().GetClient(ClientID);
            ViewBag.QuestionData = new SupplierManager().GetSupplierClientData(ClientID);
            return View(obj);
        }
    }
}