using Administration.Business_Layer;
using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllChargeList()
        {
            Master obj = new Master();
            IEnumerable<MasterCommon> chargeList = obj.GetMasterByTableName(new Startup().TableName);
            return Json(new { msg = new Master().GetMasterByTableName(new Charge().TableName), total = chargeList.Count() }, JsonRequestBehavior.AllowGet);
        }
    }
}