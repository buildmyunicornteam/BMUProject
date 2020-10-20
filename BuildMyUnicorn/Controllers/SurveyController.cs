using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using BuildMyUnicorn.Business_Layer;
using System.IO;
using Newtonsoft.Json;

namespace BuildMyUnicorn.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey
        public ActionResult Index(string SurveyID)
        {

            ViewBag.obj = new ClientManager().GetClientSurveyForm(Guid.Parse(SurveyID));
            if (ViewBag.obj != null)
            return View();
            else
            return PartialView("_BadRequest");

        }

        public void AddSurveyData(string SurveyID)
        {
            //  string json = new StreamReader(Request.InputStream).ReadToEnd();
            Stream req = Request.InputStream;
            req.Seek(0, SeekOrigin.Begin);
            string jsonData = new StreamReader(req).ReadToEnd();
            dynamic dyn = JsonConvert.DeserializeObject(jsonData);
            List<SurveyData> SurveyDataList = new List<SurveyData>();
           // Dictionary<string, string> sData = new Dictionary<string, string>();
            // var jsonDatas = JsonObjectAttribute.Parse(jsonData);
            //for (int i = 0; (JArray)jsonDatas["data"].Count; i++)
            //{
            //    var data = jsonData[i - 1];
            //}
            //var jss = new JavaScriptSerializer();
            //string json = new StreamReader(context.Request.InputStream).ReadToEnd();
            //Dictionary<string, string> sData = jss.Deserialize<Dictionary<string, string>>(json);

            //var facebookFriends = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<jsonData>(json);

            foreach (var item in dyn.data)
            {
                string key = Convert.ToString(item.Name);
                string value = Convert.ToString(item.Value);
                SurveyDataList.Add(new SurveyData() { KeyField = key, KeyValue = value });
                //sData.Add(key, value);
                //Console.WriteLine("id: {0}, name: {1}", item.id, item.name);
            }

            new ClientManager().AddSurveyData(SurveyDataList, Guid.Parse(SurveyID));
            //SurveyData
            //return new ClientManager().AddClientSurvey(Model);
        }

       
    }
}