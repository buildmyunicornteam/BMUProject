using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using ALMS_DAL;
using Model_Layer.Models;

namespace BuildMyUnicorn.Business_Layer
{
    public class Dashboard
    {
        public IdeaProgress GetIdeaProgressData()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Convert.ToInt16(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            IdeaViewModel  Model =  obj.GetSingle<IdeaViewModel>(CommandType.StoredProcedure, "sp_get_client_idea", parameters);
            IdeaProgress IdeaObj = new IdeaProgress();
            if (Model != null)
            {
                IdeaObj.TotalProgressData = Model.ProgressValue;
                IdeaObj.YourIdeaProgressData = Model.IdeaExplain == null ? 0.00m : 100.00m;
                IdeaObj.YourIdeaProgressData = Model.IdeaExplain == null ? 0.00m : 100.00m;
                IdeaObj.YourIdeaProgressData = Math.Round(IdeaObj.YourIdeaProgressData);
                //-- Let us break down idea
                IdeaObj.IdeaBreakDownProgressData = Model.ProblemSolve == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.ProblemSolver == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.FeedBackReceived == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.Niche == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.SpaceExist == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.StartupType ==null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.StartupTechnology == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.ProductDemand == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.InMarketAlready == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData += Model.Scalable == null ? 0.00m : 10.00m;
                IdeaObj.IdeaBreakDownProgressData = Math.Round(IdeaObj.IdeaBreakDownProgressData);
                //-- About you
                IdeaObj.AboutYouProgressData += Model.Entrepreneur == null ? 0.00m : 20.00m;
                IdeaObj.AboutYouProgressData += Model.YearsDoing == null ? 0.00m : 20.00m;
                IdeaObj.AboutYouProgressData += Model.Experience == null ? 0.00m : 20.00m;
                IdeaObj.AboutYouProgressData += Model.Priorities == null ? 0.00m : 20.00m;
                IdeaObj.AboutYouProgressData += Model.EndGoal == null ? 0.00m : 20.00m;
                IdeaObj.AboutYouProgressData = Math.Round(IdeaObj.AboutYouProgressData);
                //-- The Company
                IdeaObj.CompanyProgressData += Model.CompanyName == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.DomainName == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.BrandThought == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.CompanyMission == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.CompanyLookFeel == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.CompanySetup == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.HaveGotDomain == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.Cofounder == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.SupportTechnically == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData += Model.BuildFrom == null ? 0.00m : 10.00m;
                IdeaObj.CompanyProgressData = Math.Round(IdeaObj.CompanyProgressData);
                //Selling The Idea
                IdeaObj.IdeaSellingProgressData += Model.ProductBuy == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.ChargeGoing == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.CustomerFindPlan == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.SellType == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.ProductCharge == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.SellTo == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData += Model.SaleStaffPlan == null ? 0.00m : 14.28571m;
                IdeaObj.IdeaSellingProgressData = Math.Round(IdeaObj.IdeaSellingProgressData);
                //-- The Money
                IdeaObj.MoneyProgressData += Model.Affort == null ? 0.00m : 20.00m;
                IdeaObj.MoneyProgressData += Model.ProfitableMake == null ? 0.00m : 20.00m;
                IdeaObj.MoneyProgressData += Model.ProfitableThinkTime == null ? 0.00m : 20.00m;
                IdeaObj.MoneyProgressData += Model.BusinessCost == null ? 0.00m : 20.00m;
                IdeaObj.MoneyProgressData += Model.MoneyRaisePlan == null ? 0.00m : 20.00m;
                IdeaObj.MoneyProgressData = Math.Round(IdeaObj.MoneyProgressData);
            }
            return IdeaObj;

        }
    }
}