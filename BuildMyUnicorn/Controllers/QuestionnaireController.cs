using BuildMyUnicorn.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model_Layer.Models;
using System.IO;
using Newtonsoft.Json;

namespace BuildMyUnicorn.Controllers
{
    public class QuestionnaireController : WebController
    {
        // GET: Questionnaire
        public ActionResult Index()
        {
            string surveyID = "2022fc15-8e34-4216-9271-995e27cd2fc0";
            Survey obj = new ClientManager().GetClientSurveyForm(Guid.Parse(surveyID));
            return View(obj);
        }

        public ActionResult Create()
        {

            return View();
        }


        public ActionResult QuestionnaireList()
        {
            return PartialView("_QuestionnairePartial", new ClientManager().GetClientAllSurveyForm());
        }

        public ActionResult GetSurveyData(string surveyID)
        {
            IEnumerable<SurveyData> modelList = new ClientManager().GetSurveyData(Guid.Parse(surveyID));  
            return View(modelList);
        }

        //public JsonResult GetClientIdeaProgressData()
        //{
        //    string surveyID = "2022fc15-8e34-4216-9271-995e27cd2fc0";
          
        //    return Json(new ClientManager().GetClientSurveyForm(Guid.Parse(surveyID)), JsonRequestBehavior.AllowGet);
        //}

        public string AddSurvey(Survey Model)
        {
            return new ClientManager().AddClientSurvey(Model);
        }
        public string EditSurveyStatus(string SurveyID)
        {
            return new ClientManager().UpdateSurveyStatus(Guid.Parse(SurveyID));
        }
        public string DeleteSurvey(string surveyID)
        {
            return new ClientManager().DeleteSurvey(Guid.Parse(surveyID));
        }


    }
}