using Administration.Business_Layer;
using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class MoneyRaiseController : Controller
    {
        // GET: MoneyRaise
        public ActionResult Index()
        {
            return View();
        }
        public string AddMoneyRaise(MoneyRasie Model)
        {
            return new Master().AddMaster(4, Model);
        }

        public string EditMoneyRaise(MoneyRasie Model)
        {
            return new Master().UpdateMaster(4, Model);
        }

        public JsonResult GetAllMoneyRaiseList()
        {
            Master obj = new Master();
            IEnumerable<MasterCommon> moneyList = obj.GetMasterByTableName(new MoneyRasie().TableName);
            return Json(new { msg = new Master().GetMasterByTableName(new MoneyRasie().TableName), total = moneyList.Count() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMoneyRaise(Guid ID)
        {
            return Json(new Master().GetMasterByTableNameAndID(new MoneyRasie().TableName, ID), JsonRequestBehavior.AllowGet);
        }
    }
}