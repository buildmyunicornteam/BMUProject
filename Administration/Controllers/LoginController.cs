using Business_Model.Model;
using System.Web.Mvc;
using Administration.Business_Layer;

namespace Administration.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public string ValidateUser(AdminUser Model)
        {
            return new AdminManager().ValidateAdminLogin(Model);
        }
    }
}