using Administration.Business_Layer;
using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class ChargeController : Controller
    {
        // GET: Charge
        public ActionResult Index()
        {
            return View();
        }
        public string AddCharge(Charge Model)
        {
            return new Master().AddMaster(5, Model);
        }

        public string EditCharge(Charge Model)
        {
            return new Master().UpdateMaster(5, Model);
        }

        public JsonResult GetAllChargeList()
        {
            Master obj = new Master();
            IEnumerable<MasterCommon> technologyList = obj.GetMasterByTableName(new Charge().TableName);
            return Json(new { msg = new Master().GetMasterByTableName(new Technology().TableName), total = technologyList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCharge(Guid ID)
        {
            return Json(new Master().GetMasterByTableNameAndID(new Charge().TableName, ID), JsonRequestBehavior.AllowGet);
        }
    }
}