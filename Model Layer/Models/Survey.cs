using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Model_Layer.Models
{
    public class Survey : Common
    {
        public Guid SurveyID { get; set; }
        public Guid ClientID { get; set; }
        public string SurveyTitle { get; set; }
        [AllowHtml]
        public string SurveyForm { get; set; }
        public string ClientName { get; set; }
        public int SurveyReceived { get; set; }
    }

    public class SurveyData
    {
        public Guid SurveyDataID { get; set; }
        public Guid SurveyID { get; set; }
        public int SurveyUnitID { get; set; } 
        public string KeyField { get; set; }
        public string KeyValue { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}