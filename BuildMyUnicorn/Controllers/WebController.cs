
using System.Web.Mvc;
using Model_Layer.Models;
using BuildMyUnicorn.Business_Layer;

namespace BuildMyUnicorn.Controllers
{
    [Authorize]
    public class WebController : Controller
    {
        protected override void ExecuteCore()
        {
            ViewBag.Client = new ClientManager().GetClient(int.Parse(User.Identity.Name));
            base.ExecuteCore();
        }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

      

    }
}