using System;
using System.Web.Mvc;
using Business_Model.Model;
using BuildMyUnicorn.Business_Layer;

namespace BuildMyUnicorn.Controllers
{
    [Authorize]
    public class WebController : Controller
    {
        protected override void ExecuteCore()
        {
            ViewBag.Client = new ClientManager().GetClient(Guid.Parse(User.Identity.Name));
            base.ExecuteCore();
        }

        protected override bool DisableAsyncSupport
        {
           
            get { return true; }
        }

      

    }
}