using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using BuildMyUnicorn.Business_Layer;
using Business_Model.Model;
using Newtonsoft.Json;

namespace BuildMyUnicorn.Controllers
{
    public class SurveyProfessionalController : WebController
    {
        // GET: Professional
        public ActionResult Index()
        {
            IEnumerable<Supplier> Model = new ProfessionalManager().GetSupplierList();
            return View(Model);
        }

        
        public void AddQuestionData(string SQFID, string SupplierID)
        {
            //  string json = new StreamReader(Request.InputStream).ReadToEnd();
            Stream req = Request.InputStream;
            req.Seek(0, SeekOrigin.Begin);
            string jsonData = new StreamReader(req).ReadToEnd();
            dynamic dyn = JsonConvert.DeserializeObject(jsonData);
            List<_QuestionData> QuestionDataList = new List<_QuestionData>();
            foreach (var item in dyn.data)
            {
                string key = Convert.ToString(item.Name);
                string value = Convert.ToString(item.Value);
                QuestionDataList.Add(new _QuestionData() { KeyField = key, KeyValue = value });
              
            }

            new ProfessionalManager().AddClientQuestionData(QuestionDataList, Guid.Parse(SupplierID), Guid.Parse(SQFID));
            
        }

        public string AddSupplierClient(_supplierclinet Model)
        {
            return new ProfessionalManager().AddSupplierClient(Model);
        }
        public ActionResult ProfessionalProfile(Guid SupplierID)
        {
            ViewBag.PackageModel = new ProfessionalManager().GetSupplierPackageList(SupplierID);
            Supplier SupplierModel = new ProfessionalManager().GetSingleSupplier(SupplierID);
            return View(SupplierModel);
        }
       
        public ActionResult Question(Guid SupplierID)
        {
            ViewBag.Question = new ProfessionalManager().GetSupplierQuestionForm(SupplierID);
            return View();
        }

        
    }
}