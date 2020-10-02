using System.Web.Mvc;
using BuildMyUnicorn.Business_Layer;
using Model_Layer.Models;

namespace BuildMyUnicorn.Controllers
{
   
    public class DashboardController : WebController
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            
           
            return View();
        }

        public JsonResult GetClientIdeaProgressData()
        {
            return Json(new Dashboard().GetIdeaProgressData(), JsonRequestBehavior.AllowGet);
        }
    }
}