using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model_Layer.Models;
using BuildMyUnicorn.Business_Layer;

namespace BuildMyUnicorn.Controllers
{
    public class ProfileController : WebController
    {
        // GET: Profile
        public ActionResult Index(int clientID)
        {
            ViewBag.CountryList = new CountryManager().GetCountryList();
            Client obj = new  ClientManager().GetClient(clientID);
            List<MasterCommon> RoleList = new List<MasterCommon>();
            RoleList.Add(new MasterCommon { ID = Guid.Parse("839f0c8f-b5ce-4e17-bbe9-0b1c3b835dde"), Value = "Founder" });
            RoleList.Add(new MasterCommon { ID = Guid.Parse("8ea0dea8-9c94-454c-a175-3757ea4290f1"), Value = "Co Founder" });
            RoleList.Add(new MasterCommon { ID = Guid.Parse("cdb14b91-05c4-4054-a2e4-73133d879734"), Value = "Sales" });
            RoleList.Add(new MasterCommon { ID = Guid.Parse("f1110994-d667-4bb4-b856-b7220a2d17a2"), Value = "CEO" });
            RoleList.Add(new MasterCommon { ID = Guid.Parse("0c733131-159a-4e16-bf2f-c11de47fe96d"), Value = "COO" });
            RoleList.Add(new MasterCommon { ID = Guid.Parse("3d58636e-17e3-4d58-af56-f2f84f654f47"), Value = "CMO" });
            RoleList.Add(new MasterCommon { ID = Guid.Parse("84c5d93d-6bcf-464c-8784-111df68f81a6"), Value = "CTO" });
            RoleList.Add(new MasterCommon { ID = Guid.Parse("421eaf78-4856-4fd9-8637-4deec149b264"), Value = "Business Development" });
            RoleList.Add(new MasterCommon { ID = Guid.Parse("421eaf78-4856-4fd9-8637-4deec149b264"), Value = "CFO" });

            ViewBag.Role = RoleList;
            return View(obj);
        }
    }
}