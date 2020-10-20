using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildMyUnicorn_Supplier.Business_Layer;
using Business_Model.Model;


namespace BuildMyUnicorn_Supplier.Controllers
{
    public class PackageController : WebController
    {
        // GET: Package
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult PackageList()
        {
            
            return PartialView("_PackagePartial", new PackageManager().GetSupplierPackageList());
        }

        public ActionResult Add()
        {
            ViewBag.Currency = new SupplierManager().GetCurrencyList();
            return View();
        }


        public ActionResult Edit(Guid SupplierPackageID)
        {

            ViewBag.Currency = new SupplierManager().GetCurrencyList();
            Package Model = new PackageManager().GetSinglePackage(SupplierPackageID);
            return View(Model);
        }
        public string AddPackage(Package Model)
        {
            return new PackageManager().AddNewPackage(Model);
        }

        public ActionResult GetPackgae(Guid SupplierPackageID)
        {
            return PartialView("_PackagePartial");
        }

        public string DeletePackage(Guid SupplierPackageID)
        {
            return new PackageManager().DeletePackage(SupplierPackageID);
        }
    }
}

