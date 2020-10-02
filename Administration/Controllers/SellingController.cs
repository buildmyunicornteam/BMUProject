using Administration.Business_Layer;
using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class SellingController : Controller
    {
        // GET: Selling
        public ActionResult Index()
        {
            return View();
        }
        public string AddSelling(Startup Model)
        {
            return new Master().AddMaster(3, Model);
        }

        public string EditSelling(Startup Model)
        {
            return new Master().UpdateMaster(3, Model);
        }

        public JsonResult GetAllSellingList()
        {
            Master obj = new Master();
            IEnumerable<MasterCommon> sellingList = obj.GetMasterByTableName(new Selling().TableName);
            return Json(new { msg = new Master().GetMasterByTableName(new Selling().TableName), total = sellingList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSelling(Guid ID)
        {
            return Json(new Master().GetMasterByTableNameAndID(new Selling().TableName, ID), JsonRequestBehavior.AllowGet);
        }
    }
}