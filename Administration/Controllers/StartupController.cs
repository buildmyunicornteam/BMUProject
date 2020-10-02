using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administration.Business_Layer;
using Model_Layer.Models;

namespace Administration.Controllers
{
    public class StartupController : Controller
    {
        // GET: Startup
        public ActionResult Index()
        {
         
            return View();
        }

        public string AddStartup(Startup Model)
        {
            return new Master().AddMaster(1, Model);
        }

        public string EditStartup(Startup Model)
        {
            return new Master().UpdateMaster(1, Model);
        }

        public JsonResult GetAllStartupList()
        {
              Master obj = new Master();
             IEnumerable<MasterCommon> startupList = obj.GetMasterByTableName(new Startup().TableName);
             return Json(new { msg = new Master().GetMasterByTableName(new Startup().TableName), total = startupList.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStartup(Guid ID)
        {
           return Json(new Master().GetMasterByTableNameAndID(new Startup().TableName,ID), JsonRequestBehavior.AllowGet);
        }


       
    }
}