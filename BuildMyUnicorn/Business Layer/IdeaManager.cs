using ALMS_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model_Layer.Models;
using System.Data;
using System.Configuration;

namespace BuildMyUnicorn.Business_Layer
{
    public class IdeaManager
    {
        public string AddNewIdea(Idea Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@IdeaID", ParamterValue = Guid.NewGuid(), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Convert.ToInt16(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@IdeaExplain", ParamterValue = Model.IdeaExplain, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProgressValue", ParamterValue = Model.ProgressValue, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@StartupType", ParamterValue = Model.IdeaBreakDown.StartupType, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@StartupTechnology", ParamterValue = Model.IdeaBreakDown.StartupTechnology, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProblemSolve", ParamterValue = Model.IdeaBreakDown.ProblemSolve, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProblemSolver", ParamterValue = Model.IdeaBreakDown.ProblemSolver, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedBackReceived", ParamterValue = Model.IdeaBreakDown.FeedBackReceived, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedBackFrom", ParamterValue = Model.IdeaBreakDown.FeedBackFrom, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProductDemand", ParamterValue = Model.IdeaBreakDown.ProductDemand, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Niche", ParamterValue = Model.IdeaBreakDown.Niche, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@InMarketAlready", ParamterValue = Model.IdeaBreakDown.InMarketAlready, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SpaceExist", ParamterValue = Model.IdeaBreakDown.SpaceExist, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Scalable", ParamterValue = Model.IdeaBreakDown.Scalable, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Entrepreneur", ParamterValue = Model.AboutYou.Entrepreneur, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EndGoal", ParamterValue = Model.AboutYou.EndGoal, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Experience", ParamterValue = Model.AboutYou.Experience, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@YearsDoing", ParamterValue = Model.AboutYou.YearsDoing, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Priorities", ParamterValue = Model.AboutYou.Priorities, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanySetup", ParamterValue = Model.Company.CompanySetup, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyName", ParamterValue = Model.Company.CompanyName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyMission", ParamterValue = Model.Company.CompanyMission, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyLookFeel", ParamterValue = Model.Company.CompanyLookFeel, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Cofounder", ParamterValue = Model.Company.Cofounder, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SupportTechnically", ParamterValue = Model.Company.SupportTechnically, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BuildFrom", ParamterValue = Model.Company.BuildFrom, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BrandThought", ParamterValue = Model.Company.BrandThought, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyMission", ParamterValue = Model.Company.CompanyMission, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@DomainName", ParamterValue = Model.Company.DomainName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@HaveGotDomain", ParamterValue = Model.Company.HaveGotDomain, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SellType", ParamterValue = Model.IdeaSelling.SellType, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SellTo", ParamterValue = Model.IdeaSelling.SellTo, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProductBuy", ParamterValue = Model.IdeaSelling.ProductBuy, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProductCharge", ParamterValue = Model.IdeaSelling.ProductCharge, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SaleStaffPlan", ParamterValue = Model.IdeaSelling.SaleStaffPlan, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CustomerFindPlan", ParamterValue = Model.IdeaSelling.CustomerFindPlan, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ChargeGoing", ParamterValue = Model.IdeaSelling.ChargeGoing, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessCost", ParamterValue = Model.Money.BusinessCost, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MoneyRaisePlan", ParamterValue = Model.Money.MoneyRaisePlan, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Affort", ParamterValue = Model.Money.Affort, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProfitableMake", ParamterValue = Model.Money.ProfitableMake, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProfitableThinkTime", ParamterValue = Model.Money.ProfitableThinkTime, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client_idea", parameters);
            if (result > 0) return "OK"; else return "Client Idea Exists";
        }

        public string UpdateIdea(Idea Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@IdeaID", ParamterValue = Model.IdeaID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Convert.ToInt16(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@IdeaExplain", ParamterValue = Model.IdeaExplain, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@StartupType", ParamterValue = Model.IdeaBreakDown.StartupType, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProgressValue", ParamterValue = Model.ProgressValue, ParamterType = DbType.Decimal, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@StartupTechnology", ParamterValue = Model.IdeaBreakDown.StartupTechnology, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProblemSolve", ParamterValue = Model.IdeaBreakDown.ProblemSolve, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProblemSolver", ParamterValue = Model.IdeaBreakDown.ProblemSolver, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedBackReceived", ParamterValue = Model.IdeaBreakDown.FeedBackReceived, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FeedBackFrom", ParamterValue = Model.IdeaBreakDown.FeedBackFrom, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProductDemand", ParamterValue = Model.IdeaBreakDown.ProductDemand, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Niche", ParamterValue = Model.IdeaBreakDown.Niche, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@InMarketAlready", ParamterValue = Model.IdeaBreakDown.InMarketAlready, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SpaceExist", ParamterValue = Model.IdeaBreakDown.SpaceExist, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Entrepreneur", ParamterValue = Model.AboutYou.Entrepreneur, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Scalable", ParamterValue = Model.IdeaBreakDown.Scalable, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EndGoal", ParamterValue = Model.AboutYou.EndGoal, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Experience", ParamterValue = Model.AboutYou.Experience, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@YearsDoing", ParamterValue = Model.AboutYou.YearsDoing, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Priorities", ParamterValue = Model.AboutYou.Priorities, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanySetup", ParamterValue = Model.Company.CompanySetup, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyName", ParamterValue = Model.Company.CompanyName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyMission", ParamterValue = Model.Company.CompanyMission, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyLookFeel", ParamterValue = Model.Company.CompanyLookFeel, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Cofounder", ParamterValue = Model.Company.Cofounder, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SupportTechnically", ParamterValue = Model.Company.SupportTechnically, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BuildFrom", ParamterValue = Model.Company.BuildFrom, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BrandThought", ParamterValue = Model.Company.BrandThought, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyMission", ParamterValue = Model.Company.CompanyMission, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@DomainName", ParamterValue = Model.Company.DomainName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@HaveGotDomain", ParamterValue = Model.Company.HaveGotDomain, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SellType", ParamterValue = Model.IdeaSelling.SellType, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SellTo", ParamterValue = Model.IdeaSelling.SellTo, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProductBuy", ParamterValue = Model.IdeaSelling.ProductBuy, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProductCharge", ParamterValue = Model.IdeaSelling.ProductCharge, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SaleStaffPlan", ParamterValue = Model.IdeaSelling.SaleStaffPlan, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CustomerFindPlan", ParamterValue = Model.IdeaSelling.CustomerFindPlan, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ChargeGoing", ParamterValue = Model.IdeaSelling.ChargeGoing, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@BusinessCost", ParamterValue = Model.Money.BusinessCost, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MoneyRaisePlan", ParamterValue = Model.Money.MoneyRaisePlan, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Affort", ParamterValue = Model.Money.Affort, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProfitableMake", ParamterValue = Model.Money.ProfitableMake, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ProfitableThinkTime", ParamterValue = Model.Money.ProfitableThinkTime, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },


            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_client_idea", parameters);
            if (result > 0) return "OK"; else return "Error in Udating Idea";

        }

        public IdeaViewModel GetClientIdea()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Convert.ToInt16(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<IdeaViewModel>(CommandType.StoredProcedure, "sp_get_client_idea", parameters);

        }
    }
}