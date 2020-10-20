using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using BuildMyUnicorn_Supplier.Business_Layer;
using System.IO;

namespace BuildMyUnicorn_Supplier.Controllers
{
    public class ProfileController : WebController
    {
        // GET: Profile
        public ActionResult Index()
        {
            Guid SupplierID = Guid.Parse(User.Identity.Name);
            ViewBag.CountryList = new CountryManager().GetCountryList();
            ViewBag.BusinessPlacement = new MasterManager().GetOptionMasterList((int)OptionType.BusinessPlacement);
            ViewBag.WorkLocation = new MasterManager().GetOptionMasterList((int)OptionType.WorkLocation);
            Supplier obj = new SupplierManager().GetSingleSupplier(SupplierID);
            return View(obj);
        }
        public string UpdateProfile(Supplier Model)
        {
            return new SupplierManager().UpdateSupplierProfile(Model);
        }

        public string FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string FileName = System.IO.Path.GetFileName(file.FileName);
                string guid = Guid.NewGuid().ToString();
                string basePath = Server.MapPath("~/Content/file_upload/");
                string filePath = System.IO.Path.Combine(Server.MapPath("~/Content/file_upload/"), FileName);
                file.SaveAs(filePath);
                string fileGuid = guid + Path.GetExtension(filePath);
                var newFilePath = Path.Combine(Path.GetDirectoryName(filePath), fileGuid);
                System.IO.File.Move(filePath, newFilePath);
                return fileGuid;
            }
            else
            {
                return "!OK";
            }

        }
    }
}