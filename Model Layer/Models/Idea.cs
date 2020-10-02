using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Model_Layer.Models
{
    public class Idea
    {
        public Guid IdeaID { get; set; }
        [Description("What is your idea, try to explain as simply as possible.")]
        public string IdeaExplain { get; set; }
        public decimal ProgressValue { get; set; }
        public IdeaBreakDown IdeaBreakDown { get; set; }
        public AboutYou AboutYou { get; set; }
        public Company Company { get; set; }
        public IdeaSelling IdeaSelling { get; set; }
        public Money Money { get; set; }


    }
    public class IdeaBreakDown
    {
        [Description("What type of start-up is it")]
        public string StartupType { get; set; }
        [Description("What technology will this start-up use")]
        public string StartupTechnology { get; set; }
        [Description("What problem will this solve")]
        public string ProblemSolve { get; set; }
        [Description("Who are you solving the problem for")]
        public string ProblemSolver { get; set; }
        [Description("What feedback have you received about your idea")]
        public string FeedBackReceived { get; set; }
        [Description("Who was the feedback from")]
        public string FeedBackFrom { get; set; }
        [Description("Is there a demand for your product ")]
        public int? ProductDemand { get; set; }
        [Description("Do you have a niche if so what is it")]
        public string Niche { get; set; }
        [Description("Is there something on the market already")]
        public int? InMarketAlready { get; set; }
        [Description("Who already exists in this space")]
        public string SpaceExist { get; set; }
        [Description("Is it scalable - If you got 100 customers on launch and 1000 the following week, could you handle it")]
        public int? Scalable { get; set; }
    }
    public class AboutYou
    {
        [Description("Why do you want to become an entrepreneur")]
        public string Entrepreneur { get; set; }
        [Description("Can you see yourself doing this for years")]
        public string YearsDoing { get; set; }
        [Description("What is your experience why are you the person to make this a reality")]
        public string Experience { get; set; }
        [Description("What are your priorities in life")]
        public string Priorities { get; set; }
        [Description("What is the end goal")]
        public int? EndGoal { get; set; }

    }
    public class Company
    {
        [Description("Do you have a company set up")]
        public int? CompanySetup { get; set; }
        [Description("What is the name of the company  ")]
        public string CompanyName { get; set; }
        [Description("Have you got domain name")]
        public int? HaveGotDomain { get; set; }
        [Description("What is the domain")]
        public string DomainName { get; set; }
        [Description("Have you got a co-founder")]
        public int? Cofounder { get; set; }
        [Description("Have you got someone that can support technically")]
        public int? SupportTechnically { get; set; }
        [Description("Who is going to build it")]
        public int? BuildFrom { get; set; }
        [Description("Have you any thoughts on the brand that you want to create")]
        public string BrandThought { get; set; }
        [Description("Think about company mission what could it be")]
        public string CompanyMission { get; set; }
        [Description("Think about look & feel, how will the user interact with the company")]
        public string CompanyLookFeel { get; set; }

    }
    public class IdeaSelling
    {
        [Description("What are you selling")]
        public string SellType { get; set; }
        [Description("Who is going to buy the product")]
        public string ProductBuy { get; set; }
        [Description("How would you like to charge")]
        public string ProductCharge { get; set; }
        [Description("How much are going to charge")]
        public string ChargeGoing { get; set; }
        [Description("Who are you selling to")]
        public string SellTo { get; set; }
        [Description("How are you planning on finding customers")]
        public string CustomerFindPlan { get; set; }
        [Description("Are you planning on taking on sales staff")]
        public int? SaleStaffPlan { get; set; }


    }
    public class Money
    {
        [Description("How much do you think it will cost to launch your business ")]
        public int? BusinessCost { get; set; }
        [Description("Can you afford that")]
        public string Affort { get; set; }
        [Description("How are you planning to raise the money ")]
        public string MoneyRaisePlan { get; set; }
        [Description("Can you make it profitable")]
        public string ProfitableMake { get; set; }
        [Description("How long before you think you will be profitable")]
        public string ProfitableThinkTime { get; set; }


    }

    public class IdeaViewModel
    {
        public Guid IdeaID { get; set; }
        [Description("What is your idea, try to explain as simply as possible.")]
        public decimal ProgressValue { get; set; }
        public string IdeaExplain { get; set; }
        [Description("What type of start-up is it")]
        public string StartupType { get; set; }
        [Description("What technology will this start-up use")]
        public string StartupTechnology { get; set; }
        [Description("What problem will this solve")]
        public string ProblemSolve { get; set; }
        [Description("Who are you solving the problem for")]
        public string ProblemSolver { get; set; }
        [Description("What feedback have you received about your idea")]
        public string FeedBackReceived { get; set; }
        [Description("Who was the feedback from")]
        public string FeedBackFrom { get; set; }
        [Description("Is there a demand for your product ")]
        public int? ProductDemand { get; set; }
        [Description("Do you have a niche if so what is it")]
        public string Niche { get; set; }
        [Description("Is there something on the market already")]
        public int? InMarketAlready { get; set; }
        [Description("Who already exists in this space")]
        public string SpaceExist { get; set; }
        [Description("Is it scalable - If you got 100 customers on launch and 1000 the following week, could you handle it")]
        public int? Scalable { get; set; }
        [Description("Why do you want to become an entrepreneur")]
        public string Entrepreneur { get; set; }
        [Description("Can you see yourself doing this for years")]
        public string YearsDoing { get; set; }
        [Description("What is your experience why are you the person to make this a reality")]
        public string Experience { get; set; }
        [Description("What are your priorities in life")]
        public string Priorities { get; set; }
        [Description("What is the end goal")]
        public int? EndGoal { get; set; }
        [Description("Do you have a company set up")]
        public int? CompanySetup { get; set; }
        [Description("What is the name of the company  ")]
        public string CompanyName { get; set; }
        [Description("Have you got domain name")]
        public int? HaveGotDomain { get; set; }
        [Description("What is the domain")]
        public string DomainName { get; set; }
        [Description("Have you got a co-founder")]
        public int? Cofounder { get; set; }
        [Description("Have you got someone that can support technically")]
        public int? SupportTechnically { get; set; }
        [Description("Who is going to build it")]
        public int? BuildFrom { get; set; }
        [Description("Have you any thoughts on the brand that you want to create")]
        public string BrandThought { get; set; }
        [Description("Think about company mission what could it be")]
        public string CompanyMission { get; set; }
        [Description("Think about look & feel, how will the user interact with the company")]
        public string CompanyLookFeel { get; set; }
        [Description("What are you selling")]
        public string SellType { get; set; }
        [Description("Who is going to buy the product")]
        public string ProductBuy { get; set; }
        [Description("How would you like to charge")]
        public string ProductCharge { get; set; }
        [Description("How much are going to charge")]
        public string ChargeGoing { get; set; }
        [Description("Who are you selling to")]
        public string SellTo { get; set; }
        [Description("How are you planning on finding customers")]
        public string CustomerFindPlan { get; set; }
        [Description("Are you planning on taking on sales staff")]
        public int? SaleStaffPlan { get; set; }
        [Description("How much do you think it will cost to launch your business ")]
        public int? BusinessCost { get; set; }
        [Description("Can you afford that")]
        public string Affort { get; set; }
        [Description("How are you planning to raise the money ")]
        public string MoneyRaisePlan { get; set; }
        [Description("Can you make it profitable")]
        public string ProfitableMake { get; set; }
        [Description("How long before you think you will be profitable")]
        public string ProfitableThinkTime { get; set; }



    }
}