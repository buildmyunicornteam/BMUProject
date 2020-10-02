using Administration.Business_Layer;
using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class TechnologyController : Controller
    {
        // GET: Technology
        public ActionResult Index()
        {
            return View();
        }
        public string AddTechnology(Technology Model)
        {
            return new Master().AddMaster(2, Model);
        }

        public string EditTechnology(Technology Model)
        {
            return new Master().UpdateMaster(2, Model);
        }

        public JsonResult GetAllTechnologyList()
        {
            Master obj = new Master();
            IEnumerable<MasterCommon> technologyList = obj.GetMasterByTableName(new Technology().TableName);
            return Json(new { msg = new Master().GetMasterByTableName(new Technology().TableName), total = technologyList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTechnology(Guid ID)
        {
            return Json(new Master().GetMasterByTableNameAndID(new Technology().TableName, ID), JsonRequestBehavior.AllowGet);
        }
    }
}