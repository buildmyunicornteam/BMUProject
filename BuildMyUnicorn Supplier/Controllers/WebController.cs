using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildMyUnicorn_Supplier.Business_Layer;

namespace BuildMyUnicorn_Supplier.Controllers
{
    [Authorize]
    public class WebController : Controller
    {
        protected override void ExecuteCore()
        {
            ViewBag.Supplier = new SupplierManager().GetSingleSupplier(Guid.Parse(User.Identity.Name));
            base.ExecuteCore();
        }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }



    }
}