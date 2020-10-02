using BuildMyUnicorn.Business_Layer;
using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Model_Layer.Helper;
using System.Drawing;


namespace BuildMyUnicorn.Controllers
{
    public class IdeaController : WebController
    {
        [Authorize]
        public ActionResult Index(string IdeaID)
        {
         
           
            ViewBag.IsEdit = false;
            if (string.IsNullOrEmpty(IdeaID))
            {
                IdeaViewModel Model = new IdeaManager().GetClientIdea();
                if (Model == null)
                {
                    ViewBag.IsEdit = true;
                }
            }
            else
            {
                ViewBag.IsEdit = true;
            }

            return View();



        }

        public ActionResult Create()
        {
            ViewBag.ProductDemandList = new List<ProductDemand> {
                    new ProductDemand { ID=1, Value="Yes"},
                    new ProductDemand { ID=2, Value="No"},
                    new ProductDemand { ID=3, Value="I dont know"}
                };
            ViewBag.InMarketAlreadyList = new List<InMarketAlready> {
                    new InMarketAlready { ID=1, Value="Something very similar"},
                    new InMarketAlready { ID=2, Value="Nothing at all"},
                    new InMarketAlready { ID=3, Value="Nothing quite like it"},
                    new InMarketAlready { ID=4, Value="Maybe something I have not found"}
                };
            ViewBag.ScalableList = new List<Scalable> {
                    new Scalable { ID=1, Value="scalable to meet a surge in demand"},
                    new Scalable { ID=2, Value="The first version will not be able to scale"},
                    new Scalable { ID=3, Value="To early to talk about scaling"}

                };
            ViewBag.EndGoalList = new List<EndGoal> {
                    new EndGoal { ID=1, Value="To be rich"},
                    new EndGoal { ID=2, Value="To work for myself"},
                    new EndGoal { ID=3, Value="To Change the world"},
                    new EndGoal { ID=4, Value="For the challenge"},
                    new EndGoal { ID=5, Value="Personal"}
                };
            ViewBag.CompanySetupList = new List<CompanySetup> {
                    new CompanySetup { ID=1, Value="Yes"},
                    new CompanySetup { ID=2, Value="No"}

                };
            ViewBag.CofounderList = new List<Cofounder> {
                    new Cofounder { ID=1, Value="Yes"},
                    new Cofounder { ID=2, Value="No"}

                };
            ViewBag.SupportTechnicallyList = new List<SupportTechnically> {
                    new SupportTechnically { ID=1, Value="Yes"},
                    new SupportTechnically { ID=2, Value="No"},
                    new SupportTechnically { ID=3, Value="I am the Tech Support"}

                };
            ViewBag.BuildFromList = new List<BuildFrom> {
                    new BuildFrom { ID=1, Value="I am"},
                    new BuildFrom { ID=2, Value="Get a development company"},
                    new BuildFrom { ID=3, Value="Hire a freelancer"},
                    new BuildFrom { ID=4, Value="Other"}
                };
            ViewBag.SellToList = new List<SellTo> {
                    new SellTo { ID=1, Value="B2B"},
                    new SellTo { ID=2, Value="B2C"},
                    new SellTo { ID=4, Value="Other"}
                };

            ViewBag.SaleStaffPlanList = new List<SaleStaffPlan> {
                    new SaleStaffPlan { ID=1, Value="Yes"},
                    new SaleStaffPlan { ID=2, Value="No"},
                    new SaleStaffPlan { ID=3, Value="Eventually"}

                };
            ViewBag.BusinessCostLList = new List<BusinessCost> {
                    new BusinessCost { ID=1, Value="0 – 1000"},
                    new BusinessCost { ID=2, Value="1001-5000"},
                    new BusinessCost { ID=3, Value="5001- 10000"},
                    new BusinessCost { ID=4, Value="10001 – 20000"},
                    new BusinessCost { ID=5, Value="20001 – 50000"},
                    new BusinessCost { ID=6, Value="50001 – 100000"},
                    new BusinessCost { ID=7, Value="100000 +"}
                };

            ViewBag.MasterStartup = new Master().GetMasterByTableName(new Startup().TableName);
            ViewBag.MasterTechnology = new Master().GetMasterByTableName(new Technology().TableName);
            ViewBag.MasterSelling = new Master().GetMasterByTableName(new Selling().TableName);
            ViewBag.MasterCharge = new Master().GetMasterByTableName(new Charge().TableName);
            ViewBag.MasterMoneyRasie = new Master().GetMasterByTableName(new MoneyRasie().TableName);
            IdeaViewModel Model = new IdeaManager().GetClientIdea();
            Idea obj = new Idea();
            if (Model != null)
            {
                obj.IdeaExplain = Model.IdeaExplain;
                obj.IdeaID = Model.IdeaID;
                obj.ProgressValue = Model.ProgressValue;
                AboutYou Aboutyouobj = new AboutYou();
                Company Companyobj = new Company();
                IdeaBreakDown IdeaBreakDownobj = new IdeaBreakDown();
                IdeaSelling IdeaSellingobj = new IdeaSelling();
                Money Moneyobj = new Money();
                IdeaBreakDownobj.StartupType = Model.StartupType;
                IdeaBreakDownobj.StartupTechnology = Model.StartupTechnology;
                IdeaBreakDownobj.ProblemSolve = Model.ProblemSolve;
                IdeaBreakDownobj.ProblemSolver = Model.ProblemSolver;
                IdeaBreakDownobj.FeedBackReceived = Model.FeedBackReceived;
                IdeaBreakDownobj.FeedBackFrom = Model.FeedBackFrom;
                IdeaBreakDownobj.ProductDemand = Model.ProductDemand;
                IdeaBreakDownobj.Niche = Model.Niche;
                IdeaBreakDownobj.InMarketAlready = Model.InMarketAlready;
                IdeaBreakDownobj.SpaceExist = Model.SpaceExist;
                IdeaBreakDownobj.Scalable = Model.Scalable;

                Aboutyouobj.Entrepreneur = Model.Entrepreneur;
                Aboutyouobj.YearsDoing = Model.YearsDoing;
                Aboutyouobj.Experience = Model.Experience;
                Aboutyouobj.Priorities = Model.Priorities;
                Aboutyouobj.EndGoal = Model.EndGoal;


                Companyobj.CompanySetup = Model.CompanySetup;
                Companyobj.CompanyName = Model.CompanyName;
                Companyobj.DomainName = Model.DomainName;
                Companyobj.HaveGotDomain = Model.HaveGotDomain;
                Companyobj.Cofounder = Model.Cofounder;
                Companyobj.SupportTechnically = Model.SupportTechnically;
                Companyobj.BuildFrom = Model.BuildFrom;
                Companyobj.BrandThought = Model.BrandThought;
                Companyobj.CompanyMission = Model.CompanyMission;
                Companyobj.CompanyLookFeel = Model.CompanyLookFeel;




                IdeaSellingobj.SellType = Model.SellType;
                IdeaSellingobj.ProductBuy = Model.ProductBuy;
                IdeaSellingobj.ProductCharge = Model.ProductCharge;
                IdeaSellingobj.ChargeGoing = Model.ChargeGoing;
                IdeaSellingobj.SellTo = Model.SellTo;
                IdeaSellingobj.CustomerFindPlan = Model.CustomerFindPlan;
                IdeaSellingobj.SaleStaffPlan = Model.SaleStaffPlan;

                Moneyobj.BusinessCost = Model.BusinessCost;
                Moneyobj.Affort = Model.Affort;
                Moneyobj.MoneyRaisePlan = Model.MoneyRaisePlan;
                Moneyobj.ProfitableMake = Model.ProfitableMake;
                Moneyobj.ProfitableThinkTime = Model.ProfitableThinkTime;



                obj.AboutYou = Aboutyouobj;
                obj.Company = Companyobj;
                obj.IdeaBreakDown = IdeaBreakDownobj;
                obj.IdeaSelling = IdeaSellingobj;
                ViewBag.ActionType = "UPDATE";
                obj.Money = Moneyobj;

            }
            else
            {

                obj.AboutYou = new AboutYou();
                obj.Company = new Company();
                obj.IdeaBreakDown = new IdeaBreakDown();
                obj.IdeaSelling = new IdeaSelling();
                obj.Money = new Money();
            }

            return PartialView("_CreateIdeaPartial", obj);
        }

        public ActionResult Detail()
        {

            /// Temporary List
            ViewBag.ProductDemandList = new List<ProductDemand> {
                    new ProductDemand { ID=1, Value="Yes"},
                    new ProductDemand { ID=2, Value="No"},
                    new ProductDemand { ID=3, Value="I dont know"}
                };
            ViewBag.InMarketAlreadyList = new List<InMarketAlready> {
                    new InMarketAlready { ID=1, Value="Something very similar"},
                    new InMarketAlready { ID=2, Value="Nothing at all"},
                    new InMarketAlready { ID=3, Value="Nothing quite like it"},
                    new InMarketAlready { ID=4, Value="Maybe something I have not found"}
                };
            ViewBag.ScalableList = new List<Scalable> {
                    new Scalable { ID=1, Value="scalable to meet a surge in demand"},
                    new Scalable { ID=2, Value="The first version will not be able to scale"},
                    new Scalable { ID=3, Value="To early to talk about scaling"}

                };
            ViewBag.EndGoalList = new List<EndGoal> {
                    new EndGoal { ID=1, Value="To be rich"},
                    new EndGoal { ID=2, Value="To work for myself"},
                    new EndGoal { ID=3, Value="To Change the world"},
                    new EndGoal { ID=4, Value="For the challenge"},
                    new EndGoal { ID=5, Value="Personal"}
                };
            ViewBag.CompanySetupList = new List<CompanySetup> {
                    new CompanySetup { ID=1, Value="Yes"},
                    new CompanySetup { ID=2, Value="No"}

                };
            ViewBag.CofounderList = new List<Cofounder> {
                    new Cofounder { ID=1, Value="Yes"},
                    new Cofounder { ID=2, Value="No"}

                };
            ViewBag.SupportTechnicallyList = new List<SupportTechnically> {
                    new SupportTechnically { ID=1, Value="Yes"},
                    new SupportTechnically { ID=2, Value="No"},
                    new SupportTechnically { ID=3, Value="I am the Tech Support"}

                };
            ViewBag.BuildFromList = new List<BuildFrom> {
                    new BuildFrom { ID=1, Value="I am"},
                    new BuildFrom { ID=2, Value="Get a development company"},
                    new BuildFrom { ID=3, Value="Hire a freelancer"},
                    new BuildFrom { ID=4, Value="Other"}
                };
            ViewBag.SellToList = new List<SellTo> {
                    new SellTo { ID=1, Value="B2B"},
                    new SellTo { ID=2, Value="B2C"},
                    new SellTo { ID=4, Value="Other"}
                };
            ViewBag.SaleStaffPlanList = new List<SaleStaffPlan> {
                    new SaleStaffPlan { ID=1, Value="Yes"},
                    new SaleStaffPlan { ID=2, Value="No"},
                    new SaleStaffPlan { ID=3, Value="Eventually"}

                };
            ViewBag.BusinessCostLList = new List<BusinessCost> {
                    new BusinessCost { ID=1, Value="0 – 1000"},
                    new BusinessCost { ID=2, Value="1001-5000"},
                    new BusinessCost { ID=3, Value="5001- 10000"},
                    new BusinessCost { ID=4, Value="10001 – 20000"},
                    new BusinessCost { ID=5, Value="20001 – 50000"},
                    new BusinessCost { ID=6, Value="50001 – 100000"},
                    new BusinessCost { ID=7, Value="100000 +"}
                };

            /// Temporary List
            /// Temporary Checkbox Vales
            ViewBag.MasterStartup = new Master().GetMasterByTableName(new Startup().TableName);
            ViewBag.MasterTechnology = new Master().GetMasterByTableName(new Technology().TableName);
            ViewBag.MasterSelling = new Master().GetMasterByTableName(new Selling().TableName);
            ViewBag.MasterCharge = new Master().GetMasterByTableName(new Charge().TableName);
            ViewBag.MasterMoneyRasie = new Master().GetMasterByTableName(new MoneyRasie().TableName);
            ViewBag.ProgressData = new Dashboard().GetIdeaProgressData();
            /// Temporary Checkbox Vales
            IdeaViewModel Model = new IdeaManager().GetClientIdea();
            Idea obj = new Idea();
            obj.IdeaExplain = Model.IdeaExplain;
            obj.IdeaID = Model.IdeaID;
            AboutYou Aboutyouobj = new AboutYou();
            Company Companyobj = new Company();
            IdeaBreakDown IdeaBreakDownobj = new IdeaBreakDown();
            IdeaSelling IdeaSellingobj = new IdeaSelling();
            Money Moneyobj = new Money();
            IdeaBreakDownobj.StartupType = Model.StartupType;
            IdeaBreakDownobj.StartupTechnology = Model.StartupTechnology;
            IdeaBreakDownobj.ProblemSolve = Model.ProblemSolve;
            IdeaBreakDownobj.ProblemSolver = Model.ProblemSolver;
            IdeaBreakDownobj.FeedBackReceived = Model.FeedBackReceived;
            IdeaBreakDownobj.FeedBackFrom = Model.FeedBackFrom;
            IdeaBreakDownobj.ProductDemand = Model.ProductDemand;
            IdeaBreakDownobj.Niche = Model.Niche;
            IdeaBreakDownobj.InMarketAlready = Model.InMarketAlready;
            IdeaBreakDownobj.SpaceExist = Model.SpaceExist;
            IdeaBreakDownobj.Scalable = Model.Scalable;

            Aboutyouobj.Entrepreneur = Model.Entrepreneur;
            Aboutyouobj.YearsDoing = Model.YearsDoing;
            Aboutyouobj.Experience = Model.Experience;
            Aboutyouobj.Priorities = Model.Priorities;
            Aboutyouobj.EndGoal = Model.EndGoal;


            Companyobj.CompanySetup = Model.CompanySetup;
            Companyobj.CompanyName = Model.CompanyName;
            Companyobj.DomainName = Model.DomainName;
            Companyobj.Cofounder = Model.Cofounder;
            Companyobj.SupportTechnically = Model.SupportTechnically;
            Companyobj.BuildFrom = Model.BuildFrom;
            Companyobj.BrandThought = Model.BrandThought;
            Companyobj.CompanyMission = Model.CompanyMission;
            Companyobj.CompanyLookFeel = Model.CompanyLookFeel;




            IdeaSellingobj.SellType = Model.SellType;
            IdeaSellingobj.ProductBuy = Model.ProductBuy;
            IdeaSellingobj.ProductCharge = Model.ProductCharge;
            IdeaSellingobj.ChargeGoing = Model.ChargeGoing;
            IdeaSellingobj.SellTo = Model.SellTo;
            IdeaSellingobj.CustomerFindPlan = Model.CustomerFindPlan;
            IdeaSellingobj.SaleStaffPlan = Model.SaleStaffPlan;

            Moneyobj.BusinessCost = Model.BusinessCost;
            Moneyobj.Affort = Model.Affort;
            Moneyobj.MoneyRaisePlan = Model.MoneyRaisePlan;
            Moneyobj.ProfitableMake = Model.ProfitableMake;
            Moneyobj.ProfitableThinkTime = Model.ProfitableThinkTime;



            obj.AboutYou = Aboutyouobj;
            obj.Company = Companyobj;
            obj.IdeaBreakDown = IdeaBreakDownobj;
            obj.IdeaSelling = IdeaSellingobj;
            obj.Money = Moneyobj;

            return PartialView("_DetailIdeaPartial", obj);
        }

        public string AddNewIdea(Idea model)
        {

            return new IdeaManager().AddNewIdea(model);
        }

        public string UpdateIdea(Idea model)
        {

            return new IdeaManager().UpdateIdea(model);
        }


        public JsonResult DownloadPDF()
        {
            IdeaViewModel Model = new IdeaManager().GetClientIdea();
            Idea obj = new Idea();
            obj.IdeaExplain = Model.IdeaExplain;
            obj.IdeaID = Model.IdeaID;
            obj.ProgressValue = Model.ProgressValue;
            AboutYou Aboutyouobj = new AboutYou();
            Company Companyobj = new Company();
            IdeaBreakDown IdeaBreakDownobj = new IdeaBreakDown();
            IdeaSelling IdeaSellingobj = new IdeaSelling();
            Money Moneyobj = new Money();
            IdeaBreakDownobj.StartupType = Model.StartupType;
            IdeaBreakDownobj.StartupTechnology = Model.StartupTechnology;
            IdeaBreakDownobj.ProblemSolve = Model.ProblemSolve;
            IdeaBreakDownobj.ProblemSolver = Model.ProblemSolver;
            IdeaBreakDownobj.FeedBackReceived = Model.FeedBackReceived;
            IdeaBreakDownobj.FeedBackFrom = Model.FeedBackFrom;
            IdeaBreakDownobj.ProductDemand = Model.ProductDemand;
            IdeaBreakDownobj.Niche = Model.Niche;
            IdeaBreakDownobj.InMarketAlready = Model.InMarketAlready;
            IdeaBreakDownobj.SpaceExist = Model.SpaceExist;
            IdeaBreakDownobj.Scalable = Model.Scalable;

            Aboutyouobj.Entrepreneur = Model.Entrepreneur;
            Aboutyouobj.YearsDoing = Model.YearsDoing;
            Aboutyouobj.Experience = Model.Experience;
            Aboutyouobj.Priorities = Model.Priorities;
            Aboutyouobj.EndGoal = Model.EndGoal;


            Companyobj.CompanySetup = Model.CompanySetup;
            Companyobj.CompanyName = Model.CompanyName;
            Companyobj.DomainName = Model.DomainName;
            Companyobj.Cofounder = Model.Cofounder;
            Companyobj.SupportTechnically = Model.SupportTechnically;
            Companyobj.BuildFrom = Model.BuildFrom;
            Companyobj.BrandThought = Model.BrandThought;
            Companyobj.CompanyMission = Model.CompanyMission;
            Companyobj.CompanyLookFeel = Model.CompanyLookFeel;




            IdeaSellingobj.SellType = Model.SellType;
            IdeaSellingobj.ProductBuy = Model.ProductBuy;
            IdeaSellingobj.ProductCharge = Model.ProductCharge;
            IdeaSellingobj.ChargeGoing = Model.ChargeGoing;
            IdeaSellingobj.SellTo = Model.SellTo;
            IdeaSellingobj.CustomerFindPlan = Model.CustomerFindPlan;
            IdeaSellingobj.SaleStaffPlan = Model.SaleStaffPlan;

            Moneyobj.BusinessCost = Model.BusinessCost;
            Moneyobj.Affort = Model.Affort;
            Moneyobj.MoneyRaisePlan = Model.MoneyRaisePlan;
            Moneyobj.ProfitableMake = Model.ProfitableMake;
            Moneyobj.ProfitableThinkTime = Model.ProfitableThinkTime;



            obj.AboutYou = Aboutyouobj;
            obj.Company = Companyobj;
            obj.IdeaBreakDown = IdeaBreakDownobj;
            obj.IdeaSelling = IdeaSellingobj;
            obj.Money = Moneyobj;
            //IList<TimeDetails> Model = db.GetTimeDetails(TimesheetParameter).ToList();

            ViewBag.MasterStartup = new Master().GetMasterByTableName(new Startup().TableName);
            ViewBag.MasterTechnology = new Master().GetMasterByTableName(new Technology().TableName);
            ViewBag.MasterSelling = new Master().GetMasterByTableName(new Selling().TableName);
            ViewBag.MasterCharge = new Master().GetMasterByTableName(new Charge().TableName);
            ViewBag.MasterMoneyRasie = new Master().GetMasterByTableName(new MoneyRasie().TableName);

            
                List<string> IdeaoutofmyheadList = new List<string>();

       
            IdeaoutofmyheadList.Add("Your Idea");
            IdeaoutofmyheadList.Add("Lets break down the idea");
            IdeaoutofmyheadList.Add("About You");
            IdeaoutofmyheadList.Add("The Company");
            IdeaoutofmyheadList.Add("Selling the Idea");
            IdeaoutofmyheadList.Add("The Money");

            ViewBag.ProductDemandList = new List<ProductDemand> {
                    new ProductDemand { ID=1, Value="Yes"},
                    new ProductDemand { ID=2, Value="No"},
                    new ProductDemand { ID=3, Value="I dont know"}
                };
            ViewBag.InMarketAlreadyList = new List<InMarketAlready> {
                    new InMarketAlready { ID=1, Value="Something very similar"},
                    new InMarketAlready { ID=2, Value="Nothing at all"},
                    new InMarketAlready { ID=3, Value="Nothing quite like it"},
                    new InMarketAlready { ID=4, Value="Maybe something I have not found"}
                };
            ViewBag.ScalableList = new List<Scalable> {
                    new Scalable { ID=1, Value="scalable to meet a surge in demand"},
                    new Scalable { ID=2, Value="The first version will not be able to scale"},
                    new Scalable { ID=3, Value="To early to talk about scaling"}

                };
            ViewBag.EndGoalList = new List<EndGoal> {
                    new EndGoal { ID=1, Value="To be rich"},
                    new EndGoal { ID=2, Value="To work for myself"},
                    new EndGoal { ID=3, Value="To Change the world"},
                    new EndGoal { ID=4, Value="For the challenge"},
                    new EndGoal { ID=5, Value="Personal"}
                };
            ViewBag.CompanySetupList = new List<CompanySetup> {
                    new CompanySetup { ID=1, Value="Yes"},
                    new CompanySetup { ID=2, Value="No"}

                };
            ViewBag.CofounderList = new List<Cofounder> {
                    new Cofounder { ID=1, Value="Yes"},
                    new Cofounder { ID=2, Value="No"}

                };
            ViewBag.SupportTechnicallyList = new List<SupportTechnically> {
                    new SupportTechnically { ID=1, Value="Yes"},
                    new SupportTechnically { ID=2, Value="No"},
                    new SupportTechnically { ID=3, Value="I am the Tech Support"}

                };
            ViewBag.BuildFromList = new List<BuildFrom> {
                    new BuildFrom { ID=1, Value="I am"},
                    new BuildFrom { ID=2, Value="Get a development company"},
                    new BuildFrom { ID=3, Value="Hire a freelancer"},
                    new BuildFrom { ID=4, Value="Other"}
                };
            ViewBag.SellToList = new List<SellTo> {
                    new SellTo { ID=1, Value="B2B"},
                    new SellTo { ID=2, Value="B2C"},
                    new SellTo { ID=4, Value="Other"}
                };
            ViewBag.SaleStaffPlanList = new List<SaleStaffPlan> {
                    new SaleStaffPlan { ID=1, Value="Yes"},
                    new SaleStaffPlan { ID=2, Value="No"},
                    new SaleStaffPlan { ID=3, Value="Eventually"}

                };
            ViewBag.BusinessCostLList = new List<BusinessCost> {
                    new BusinessCost { ID=1, Value="0 – 1000"},
                    new BusinessCost { ID=2, Value="1001-5000"},
                    new BusinessCost { ID=3, Value="5001- 10000"},
                    new BusinessCost { ID=4, Value="10001 – 20000"},
                    new BusinessCost { ID=5, Value="20001 – 50000"},
                    new BusinessCost { ID=6, Value="50001 – 100000"},
                    new BusinessCost { ID=7, Value="100000 +"}
                };

            string fileName = string.Empty;
            string status = "!OK";
            if (!(obj == null))
            {
                DateTime fileCreationDatetime = DateTime.Now;
                string InvoiceNumber = fileCreationDatetime.ToString(@"yyyyMMdd") + "" + fileCreationDatetime.ToString(@"HHmmss");
                fileName = string.Format("{0}.pdf", InvoiceNumber);

                string pdfPath = Server.MapPath(@"~\Content\") + fileName;
                using (FileStream msReport = new FileStream(pdfPath, FileMode.Create))
                {
                    using (Document pdfDoc = new Document(PageSize.A4, 10f, 15f, 120f, 80f))
                    {
                        try
                        {
                            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                            iTextSharp.text.Font SmallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
                            iTextSharp.text.Font FontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
                            iTextSharp.text.Font FontAnswer = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                            iTextSharp.text.Font FontNormalRed = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED);
                            iTextSharp.text.Font FontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
                            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);
                            pdfWriter.PageEvent = new ITextEvents(obj.ProgressValue);
                          //  pdfWriter.PageEvent = new ITextEvents();
                          //  pdfDoc.SetMargins(50, 50, 10, 40);
                           // pdfDoc.LeftMargin = 50;
                            pdfDoc.Open();

                            //------ STEP-1---------->
                            string Step1Heading = "1. " + IdeaoutofmyheadList[0].ToString();
                            string Step1_Q1 = "1.1 " +"What is your idea, try to explain as simply as possible?";
                            string Step1_Q1_Ans = obj.IdeaExplain == null ? "_______________" : obj.IdeaExplain;

                            Paragraph Step1HeadingPara = new Paragraph(new Phrase(Step1Heading, baseFontBig));
                            Step1HeadingPara.Font = baseFontBig;
                            Step1HeadingPara.IndentationLeft = 30f;
                            Step1HeadingPara.Alignment = Element.ALIGN_LEFT;


                            Chunk C_Step1_Q1 = new Chunk(Step1_Q1);
                          
                            Paragraph Step1_Q1Para = new Paragraph(C_Step1_Q1);
                            Step1_Q1Para.IndentationLeft = 40f;
                            Step1_Q1Para.SpacingBefore = 10f;
                            Step1_Q1Para.SpacingAfter = 10f;
                            Step1_Q1Para.Font = FontBold;
                            Step1_Q1Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step1_Q1_Ans = new Chunk(Step1_Q1_Ans);
                            //C_Step1_Q1_Ans.setLineHeight(16f);
                            Paragraph Step1_Q1_AnsPara = new Paragraph(new Phrase(Step1_Q1_Ans, baseFontNormal));
                            Step1_Q1_AnsPara.IndentationLeft = 60f;
                            Step1_Q1_AnsPara.Font = baseFontNormal;
                            Step1_Q1_AnsPara.Alignment = Element.ALIGN_LEFT;


                            //------ STEP-2---------->
                            string TempText = null;
                            string Step2Heading = "2. " + IdeaoutofmyheadList[1].ToString();
                            string Step2_Q = "2.1 " + "What type of start-up is it?";
                            var arrayStartupType = String.IsNullOrEmpty(obj.IdeaBreakDown.StartupType) == false ? obj.IdeaBreakDown.StartupType.Split(',').ToArray() : new string[0];
                            foreach (var item in ViewBag.MasterStartup)
                            {
                                if (Array.Exists(arrayStartupType, x => x == Convert.ToString(@item.ID)) == true)
                                TempText += item.Value + "\n"; 
                            }
                            string Step2_Q_Ans = TempText == null ? "_______________" : TempText;

                            Paragraph Step2HeadingPara = new Paragraph(new Phrase(Step2Heading, baseFontBig));
                            Step2HeadingPara.IndentationLeft = 30f;
                            Step2HeadingPara.Font = baseFontNormal;
                            Step2HeadingPara.Alignment = Element.ALIGN_LEFT;


                            Chunk C_Step2_Q1 = new Chunk(Step2_Q);
                            Paragraph Step2_Q1Para = new Paragraph(C_Step2_Q1);
                            Step2_Q1Para.IndentationLeft = 40f;
                            Step2_Q1Para.SpacingBefore = 10f;
                            Step2_Q1Para.SpacingAfter = 10f;
                            Step2_Q1Para.Font = FontBold;
                            Step2_Q1Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step2_Q1_Ans = new Chunk(Step2_Q_Ans);
                            //C_Step2_Q1_Ans.setLineHeight(3f);
                            Paragraph Step2_Q1_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q1_AnsPara.IndentationLeft = 60f;
                            Step2_Q1_AnsPara.Font = FontAnswer;
                            Step2_Q1_AnsPara.Alignment = Element.ALIGN_LEFT;


                            TempText = null;
                            Step2_Q = "2.2 " + "What technology will this start-up use?";
                            var arrayStartupTechnology = String.IsNullOrEmpty(obj.IdeaBreakDown.StartupTechnology) == false ? obj.IdeaBreakDown.StartupTechnology.Split(',').ToArray() : new string[0];
                            foreach (var item in ViewBag.MasterTechnology)
                            {
                                if (Array.Exists(arrayStartupTechnology, x => x == Convert.ToString(@item.ID)) == true)
                                 TempText += item.Value + "\n";
                                 
                            }
                            Step2_Q_Ans = TempText == null ? "_______________" : TempText;

                           

                            Chunk C_Step2_Q2 = new Chunk(Step2_Q);
                            Paragraph Step2_Q2Para = new Paragraph(C_Step2_Q2);
                            Step2_Q2Para.IndentationLeft = 40f;
                            Step2_Q2Para.SpacingBefore = 10f;
                            Step2_Q2Para.SpacingAfter = 10f;
                            Step2_Q2Para.Font = FontBold;
                            Step2_Q2Para.Alignment = Element.ALIGN_LEFT;

                            Chunk C_Step2_Q2_Ans = new Chunk(Step2_Q_Ans);
                            C_Step2_Q2_Ans.setLineHeight(3f);
                            Paragraph Step2_Q2_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q2_AnsPara.IndentationLeft = 60f;
                            Step2_Q2_AnsPara.Font = FontAnswer;
                            Step2_Q2_AnsPara.Alignment = Element.ALIGN_LEFT;

                         
                            Step2_Q = "2.3 " + "What problem will this solve?";
                            Step2_Q_Ans = obj.IdeaBreakDown.ProblemSolve == null ? "_______________" : obj.IdeaBreakDown.ProblemSolve;

                            Chunk C_Step2_Q3 = new Chunk(Step2_Q);
                            Paragraph Step2_Q3Para = new Paragraph(C_Step2_Q3);
                            Step2_Q3Para.IndentationLeft = 40f;
                            Step2_Q3Para.SpacingBefore = 10f;
                            Step2_Q3Para.SpacingAfter = 10f;
                            Step2_Q3Para.Font = FontBold;
                            Step2_Q3Para.Alignment = Element.ALIGN_LEFT;

                            Chunk C_Step2_Q3_Ans = new Chunk(Step2_Q_Ans);
                            C_Step2_Q3_Ans.setLineHeight(3f);
                            Paragraph Step2_Q3_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q3_AnsPara.IndentationLeft = 60f;
                            Step2_Q3_AnsPara.Font = FontAnswer;
                            Step2_Q3_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step2_Q = "2.4 " + "Who are you solving the problem for?";
                            Step2_Q_Ans = obj.IdeaBreakDown.ProblemSolver == null ? "_______________" : obj.IdeaBreakDown.ProblemSolver;

                            Chunk C_Step2_Q4 = new Chunk(Step2_Q);
                            Paragraph Step2_Q4Para = new Paragraph(C_Step2_Q4);
                            Step2_Q4Para.IndentationLeft = 40f;
                            Step2_Q4Para.SpacingBefore = 10f;
                            Step2_Q4Para.SpacingAfter = 10f;
                            Step2_Q4Para.Font = FontBold;
                            Step2_Q4Para.Alignment = Element.ALIGN_LEFT;

                            Chunk C_Step2_Q4_Ans = new Chunk(Step2_Q_Ans);
                            C_Step2_Q4_Ans.setLineHeight(3f);
                            Paragraph Step2_Q4_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q4_AnsPara.IndentationLeft = 60f;
                            Step2_Q4_AnsPara.Font = FontAnswer;
                            Step2_Q4_AnsPara.Alignment = Element.ALIGN_LEFT;

                            Step2_Q = "2.5 " + "What feedback have you received about your idea?";
                            Step2_Q_Ans = obj.IdeaBreakDown.FeedBackReceived == null ? "_______________" : obj.IdeaBreakDown.FeedBackReceived;

                            Chunk C_Step2_Q5 = new Chunk(Step2_Q);
                            Paragraph Step2_Q5Para = new Paragraph(C_Step2_Q5);
                            Step2_Q5Para.IndentationLeft = 40f;
                            Step2_Q5Para.SpacingBefore = 10f;
                            Step2_Q5Para.SpacingAfter = 10f;
                            Step2_Q5Para.Font = FontBold;
                            Step2_Q5Para.Alignment = Element.ALIGN_LEFT;

                            Chunk C_Step2_Q5_Ans = new Chunk(Step2_Q_Ans);
                            C_Step2_Q5_Ans.setLineHeight(3f);
                            Paragraph Step2_Q5_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q5_AnsPara.IndentationLeft = 60f;
                            Step2_Q5_AnsPara.Font = FontAnswer;
                            Step2_Q5_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step2_Q = "2.6 " + "Who was the feedback from?";
                            Step2_Q_Ans = obj.IdeaBreakDown.FeedBackFrom == null ? "_______________" : obj.IdeaBreakDown.FeedBackFrom;

                            Chunk C_Step2_Q6 = new Chunk(Step2_Q);
                            Paragraph Step2_Q6Para = new Paragraph(C_Step2_Q6);
                            Step2_Q6Para.IndentationLeft = 60f;
                            Step2_Q6Para.SpacingBefore = 10f;
                            Step2_Q6Para.SpacingAfter = 10f;
                            Step2_Q6Para.Font = FontBold;
                            Step2_Q6Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step2_Q6_Ans = new Chunk(Step2_Q_Ans);
                            //C_Step2_Q6_Ans.setLineHeight(3f);
                            Paragraph Step2_Q6_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q6_AnsPara.IndentationLeft = 60f;
                            Step2_Q6_AnsPara.Font = FontAnswer;
                            Step2_Q6_AnsPara.Alignment = Element.ALIGN_LEFT;


                            TempText = null;
                            Step2_Q = "2.7 " + "Is there a demand for your product?";
                            foreach (var item in ViewBag.ProductDemandList)
                            {
                                if (item.ID == obj.IdeaBreakDown.ProductDemand)
                                    TempText += item.Value;

                            }
                            Step2_Q_Ans = TempText == null ? "_______________" : TempText;



                            Chunk C_Step2_Q7 = new Chunk(Step2_Q);
                            Paragraph Step2_Q7Para = new Paragraph(C_Step2_Q7);
                            Step2_Q7Para.IndentationLeft = 40f;
                            Step2_Q7Para.SpacingBefore = 10f;
                            Step2_Q7Para.SpacingAfter = 10f;
                            Step2_Q7Para.Font = FontBold;
                            Step2_Q7Para.Alignment = Element.ALIGN_LEFT;

                            Chunk C_Step2_Q7_Ans = new Chunk(Step2_Q_Ans);
                            C_Step2_Q7_Ans.setLineHeight(3f);
                            Paragraph Step2_Q7_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q7_AnsPara.IndentationLeft = 60f;
                            Step2_Q7_AnsPara.Font = FontAnswer;
                            Step2_Q7_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step2_Q = "2.8 " + "Do you have a niche if so what is it?";
                            Step2_Q_Ans = obj.IdeaBreakDown.Niche == null ? "_______________" : obj.IdeaBreakDown.Niche;

                            Chunk C_Step2_Q8 = new Chunk(Step2_Q);
                            Paragraph Step2_Q8Para = new Paragraph(C_Step2_Q8);
                            Step2_Q8Para.IndentationLeft = 40f;
                            Step2_Q8Para.SpacingBefore = 10f;
                            Step2_Q8Para.SpacingAfter = 10f;
                            Step2_Q8Para.Font = FontBold;
                            Step2_Q8Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step2_Q8_Ans = new Chunk(Step2_Q_Ans);
                            //C_Step2_Q8_Ans.setLineHeight(3f);
                            Paragraph Step2_Q8_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q8_AnsPara.IndentationLeft = 60f;
                            Step2_Q8_AnsPara.Font = FontAnswer;
                            Step2_Q8_AnsPara.Alignment = Element.ALIGN_LEFT;

                            TempText = null;
                            Step2_Q = "2.9 " + "Is there something on the market already?";
                            foreach (var item in ViewBag.InMarketAlreadyList)
                            {
                                if (item.ID == obj.IdeaBreakDown.InMarketAlready)
                                    TempText += item.Value;

                            }
                            Step2_Q_Ans = TempText == null ? "_______________" : TempText;



                            Chunk C_Step2_Q9 = new Chunk(Step2_Q);
                            Paragraph Step2_Q9Para = new Paragraph(C_Step2_Q9);
                            Step2_Q9Para.IndentationLeft = 40f;
                            Step2_Q9Para.SpacingBefore = 10f;
                            Step2_Q9Para.SpacingAfter = 10f;
                            Step2_Q9Para.Font = FontBold;
                            Step2_Q9Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step2_Q9_Ans = new Chunk(Step2_Q_Ans);
                            //C_Step2_Q9_Ans.setLineHeight(3f);
                            Paragraph Step2_Q9_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q9_AnsPara.IndentationLeft = 60f;
                            Step2_Q9_AnsPara.Font = FontAnswer;
                            Step2_Q9_AnsPara.Alignment = Element.ALIGN_LEFT;

                            Step2_Q = "2.10 " + "Who already exists in this space?";
                            Step2_Q_Ans = obj.IdeaBreakDown.SpaceExist == null ? "_______________" : obj.IdeaBreakDown.SpaceExist;

                            Chunk C_Step2_Q10 = new Chunk(Step2_Q);
                            Paragraph Step2_Q10Para = new Paragraph(C_Step2_Q10);
                            Step2_Q10Para.IndentationLeft = 40f;
                            Step2_Q10Para.SpacingBefore = 10f;
                            Step2_Q10Para.SpacingAfter = 10f;
                            Step2_Q10Para.Font = FontBold;
                            Step2_Q10Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step2_Q10_Ans = new Chunk(Step2_Q_Ans);
                            //C_Step2_Q10_Ans.setLineHeight(3f);
                            Paragraph Step2_Q10_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q10_AnsPara.IndentationLeft = 60f;
                            Step2_Q10_AnsPara.Font = FontAnswer;
                            Step2_Q10_AnsPara.Alignment = Element.ALIGN_LEFT;

                            TempText = null;
                            Step2_Q = "2.11 " + "Is it scalable - If you got 100 customers on launch and 1000 the following week, could you handle it?";
                            foreach (var item in ViewBag.ScalableList)
                            {
                                if (item.ID == obj.IdeaBreakDown.Scalable)
                                    TempText += item.Value;

                            }
                            Step2_Q_Ans = TempText == null ? "_______________" : TempText;



                            Chunk C_Step2_Q11 = new Chunk(Step2_Q);
                            Paragraph Step2_Q11Para = new Paragraph(C_Step2_Q11);
                            Step2_Q11Para.IndentationLeft = 40f;
                            Step2_Q11Para.SpacingBefore = 10f;
                            Step2_Q11Para.SpacingAfter = 10f;
                            Step2_Q11Para.Font = FontBold;
                            Step2_Q11Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step2_Q11_Ans = new Chunk(Step2_Q_Ans);
                            //C_Step2_Q11_Ans.setLineHeight(3f);
                            Paragraph Step2_Q11_AnsPara = new Paragraph(new Phrase(Step2_Q_Ans, baseFontNormal));
                            Step2_Q11_AnsPara.IndentationLeft = 60f;
                            Step2_Q11_AnsPara.Font = FontAnswer;
                            Step2_Q11_AnsPara.Alignment = Element.ALIGN_LEFT;

                            //----------- STEP-3----------->
                      
                            string Step3Heading = "3. " + IdeaoutofmyheadList[2].ToString();
                            string Step3_Q = "3.1 " + "Why do you want to become an entrepreneur?";
                            string Step3_Ans = obj.AboutYou.Entrepreneur == null ? "_______________" : obj.AboutYou.Entrepreneur;

                            Paragraph Step3HeadingPara = new Paragraph(new Phrase(Step3Heading, baseFontBig));
                            Step3HeadingPara.Font = FontBold;
                            Step3HeadingPara.IndentationLeft = 30f;
                            Step3HeadingPara.Alignment = Element.ALIGN_LEFT;


                            Chunk C_Step3_Q1 = new Chunk(Step3_Q);
                            Paragraph Step3_Q1Para = new Paragraph(C_Step3_Q1);
                            Step3_Q1Para.IndentationLeft = 40f;
                            Step3_Q1Para.SpacingBefore = 10f;
                            Step3_Q1Para.SpacingAfter = 10f;
                            Step3_Q1Para.Font = FontBold;
                            Step3_Q1Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step3_Q1_Ans = new Chunk(Step3_Ans);
                            //C_Step3_Q1_Ans.setLineHeight(3f);
                            Paragraph Step3_Q1_AnsPara = new Paragraph(new Phrase(Step3_Ans, baseFontNormal));
                            Step3_Q1_AnsPara.IndentationLeft = 60f;
                            Step3_Q1_AnsPara.Font = FontAnswer;
                            Step3_Q1_AnsPara.Alignment = Element.ALIGN_LEFT;

                             Step3_Q = "3.2 " + "Can you see yourself doing this for years?";
                             Step3_Ans = obj.AboutYou.YearsDoing == null ? "_______________" : obj.AboutYou.YearsDoing;

                           
                            Chunk C_Step3_Q2 = new Chunk(Step3_Q);
                            Paragraph Step3_Q2Para = new Paragraph(C_Step3_Q2);
                            Step3_Q2Para.IndentationLeft = 40f;
                            Step3_Q2Para.SpacingBefore = 10f;
                            Step3_Q2Para.SpacingAfter = 10f;
                            Step3_Q2Para.Font = FontBold;
                            Step3_Q2Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step3_Q2_Ans = new Chunk(Step3_Ans);
                            //C_Step3_Q2_Ans.setLineHeight(3f);
                            Paragraph Step3_Q2_AnsPara = new Paragraph(new Phrase(Step3_Ans, baseFontNormal));
                            Step3_Q2_AnsPara.IndentationLeft = 60f;
                            Step3_Q2_AnsPara.Font = FontAnswer;
                            Step3_Q2_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step3_Q = "3.3 " + "What is your experience why are you the person to make this a reality?";
                            Step3_Ans = obj.AboutYou.Experience == null ? "_______________" : obj.AboutYou.Experience;


                            Chunk C_Step3_Q3 = new Chunk(Step3_Q);
                            Paragraph Step3_Q3Para = new Paragraph(C_Step3_Q3);
                            Step3_Q3Para.IndentationLeft = 40f;
                            Step3_Q3Para.SpacingBefore = 10f;
                            Step3_Q3Para.SpacingAfter = 10f;
                            Step3_Q3Para.Font = FontBold;
                            Step3_Q3Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step3_Q3_Ans = new Chunk(Step3_Ans);
                            //C_Step3_Q3_Ans.setLineHeight(3f);
                            Paragraph Step3_Q3_AnsPara = new Paragraph(new Phrase(Step3_Ans, baseFontNormal));
                            Step3_Q3_AnsPara.IndentationLeft = 60f;
                            Step3_Q3_AnsPara.Font = FontAnswer;
                            Step3_Q3_AnsPara.Alignment = Element.ALIGN_LEFT;

                            Step3_Q = "3.4 " + "What are your priorities in life?";
                            Step3_Ans = obj.AboutYou.Priorities == null ? "_______________" : obj.AboutYou.Priorities;


                            Chunk C_Step3_Q4 = new Chunk(Step3_Q);
                            Paragraph Step3_Q4Para = new Paragraph(C_Step3_Q4);
                            Step3_Q4Para.IndentationLeft = 40f;
                            Step3_Q4Para.SpacingBefore = 10f;
                            Step3_Q4Para.SpacingAfter = 10f;
                            Step3_Q4Para.Font = FontBold;
                            Step3_Q4Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step3_Q4_Ans = new Chunk(Step3_Ans);
                            //C_Step3_Q4_Ans.setLineHeight(3f);
                            Paragraph Step3_Q4_AnsPara = new Paragraph(new Phrase(Step3_Ans, baseFontNormal));
                            Step3_Q4_AnsPara.IndentationLeft = 60f;
                            Step3_Q4_AnsPara.Font = FontAnswer;
                            Step3_Q4_AnsPara.Alignment = Element.ALIGN_LEFT;


                            TempText = null;
                            Step3_Q = "3.5 " + "What is the end goal?";
                            foreach (var item in ViewBag.EndGoalList)
                            {
                                if (item.ID == obj.AboutYou.EndGoal)
                                    TempText += item.Value;

                            }
                            Step3_Ans = TempText == null ? "_______________" : TempText;

                            Chunk C_Step3_Q5 = new Chunk(Step3_Q);
                            Paragraph Step3_Q5Para = new Paragraph(C_Step3_Q5);
                            Step3_Q5Para.IndentationLeft = 40f;
                            Step3_Q5Para.SpacingBefore = 10f;
                            Step3_Q5Para.SpacingAfter = 10f;
                            Step3_Q5Para.Font = FontBold;
                            Step3_Q5Para.Alignment = Element.ALIGN_LEFT;

                            //Chunk C_Step3_Q5_Ans = new Chunk(Step3_Ans);
                            //C_Step3_Q5_Ans.setLineHeight(3f);
                            Paragraph Step3_Q5_AnsPara = new Paragraph(new Phrase(Step3_Ans, baseFontNormal));
                            Step3_Q5_AnsPara.IndentationLeft = 60f;
                            Step3_Q5_AnsPara.Font = FontAnswer;
                            Step3_Q5_AnsPara.Alignment = Element.ALIGN_LEFT;



                            //----------- STEP-4----------->

                            string Step4Heading = "4. " + IdeaoutofmyheadList[3].ToString();
                            string Step4_Q = "4.1 " + "Do you have a company set up?";
                          
                            TempText = null;
                            foreach (var item in ViewBag.CompanySetupList)
                            {
                                if (item.ID == obj.Company.CompanySetup)
                                    TempText += item.Value;

                            }
                            string Step4_Ans = TempText == null ? "_______________" : TempText;

                            Paragraph Step4HeadingPara = new Paragraph(new Phrase(Step4Heading, baseFontBig));
                            Step4HeadingPara.Font = FontBold;
                            Step4HeadingPara.IndentationLeft = 30f;
                            Step4HeadingPara.Alignment = Element.ALIGN_LEFT;


                            Chunk C_Step4_Q1 = new Chunk(Step4_Q);
                            Paragraph Step4_Q1Para = new Paragraph(C_Step4_Q1);
                            Step4_Q1Para.IndentationLeft = 40f;
                            Step4_Q1Para.SpacingBefore = 10f;
                            Step4_Q1Para.SpacingAfter = 10f;
                            Step4_Q1Para.Font = FontBold;
                            Step4_Q1Para.Alignment = Element.ALIGN_LEFT;


                            //Chunk C_Step4_Q1_Ans = new Chunk(Step4_Ans);
                            //C_Step4_Q1_Ans.setLineHeight(3f);
                            Paragraph Step4_Q1_AnsPara = new Paragraph(new Phrase(Step4_Ans, baseFontNormal));
                            Step4_Q1_AnsPara.IndentationLeft = 60f;
                            Step4_Q1_AnsPara.Font = FontAnswer;
                            Step4_Q1_AnsPara.Alignment = Element.ALIGN_LEFT;


                             
                             Step4_Q = "4.2 " + "What is the name of the company?";
                             Step4_Ans = obj.Company.CompanyName == null ? "_______________" : obj.Company.CompanyName;
                          
                            Chunk C_Step4_Q2 = new Chunk(Step4_Q);
                            Paragraph Step4_Q2Para = new Paragraph(C_Step4_Q2);
                            Step4_Q2Para.IndentationLeft = 40f;
                            Step4_Q2Para.SpacingBefore = 10f;
                            Step4_Q2Para.SpacingAfter = 10f;
                            Step4_Q2Para.Font = FontBold;
                            Step4_Q2Para.Alignment = Element.ALIGN_LEFT;


                            //Chunk C_Step4_Q2_Ans = new Chunk(Step4_Ans);
                            //C_Step4_Q2_Ans.setLineHeight(3f);
                            Paragraph Step4_Q2_AnsPara = new Paragraph(new Phrase(Step4_Ans, baseFontNormal));
                            Step4_Q2_AnsPara.IndentationLeft = 60f;
                            Step4_Q2_AnsPara.Font = FontAnswer;
                            Step4_Q2_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step4_Q = "4.3 " + "Have you got the domain name?";
                            
                            TempText = null;
                            foreach (var item in ViewBag.CompanySetupList)
                            {
                                if (item.ID == obj.Company.HaveGotDomain)
                                    TempText = item.Value;

                            }

                            Step4_Ans = TempText == null ? "_______________" : TempText;
                            Chunk C_Step4_Q3 = new Chunk(Step4_Q);
                            Paragraph Step4_Q3Para = new Paragraph(C_Step4_Q3);
                            Step4_Q3Para.IndentationLeft = 40f;
                            Step4_Q3Para.SpacingBefore = 10f;
                            Step4_Q3Para.SpacingAfter = 10f;
                            Step4_Q3Para.Font = FontBold;
                            Step4_Q3Para.Alignment = Element.ALIGN_LEFT;


                            //Chunk C_Step4_Q3_Ans = new Chunk(Step4_Ans);
                            //C_Step4_Q3_Ans.setLineHeight(3f);
                            Paragraph Step4_Q3_AnsPara = new Paragraph(new Phrase(Step4_Ans, baseFontNormal));
                            Step4_Q3_AnsPara.IndentationLeft = 60f;
                            Step4_Q3_AnsPara.Font = FontAnswer;
                            Step4_Q3_AnsPara.Alignment = Element.ALIGN_LEFT;

                            Step4_Q = "4.4 " + "What is the domain ? ";
                            Step4_Ans = obj.Company.DomainName == null ? "_______________" : obj.Company.DomainName;

                            Chunk C_Step4_Q4 = new Chunk(Step4_Q);
                            Paragraph Step4_Q4Para = new Paragraph(C_Step4_Q4);
                            Step4_Q4Para.IndentationLeft = 40f;
                            Step4_Q4Para.SpacingBefore = 10f;
                            Step4_Q4Para.SpacingAfter = 10f;
                            Step4_Q4Para.Font = FontBold;
                            Step4_Q4Para.Alignment = Element.ALIGN_LEFT;


                            //Chunk C_Step4_Q4_Ans = new Chunk(Step4_Ans);
                            //C_Step4_Q4_Ans.setLineHeight(3f);
                            Paragraph Step4_Q4_AnsPara = new Paragraph(new Phrase(Step4_Ans, baseFontNormal));
                            Step4_Q4_AnsPara.IndentationLeft = 60f;
                            Step4_Q4_AnsPara.Font = FontAnswer;
                            Step4_Q4_AnsPara.Alignment = Element.ALIGN_LEFT;

                            Step4_Q = "4.5 " + "Have you got a co-founder?";
                           
                            TempText = null;
                            foreach (var item in ViewBag.CofounderList)
                            {
                                if (item.ID == obj.Company.Cofounder)
                                    TempText = item.Value;

                            }

                            Step4_Ans = TempText == null ? "_______________" : TempText;
                            Chunk C_Step4_Q5 = new Chunk(Step4_Q);
                            Paragraph Step4_Q5Para = new Paragraph(C_Step4_Q5);
                            Step4_Q5Para.IndentationLeft = 40f;
                            Step4_Q5Para.SpacingBefore = 10f;
                            Step4_Q5Para.SpacingAfter = 10f;
                            Step4_Q5Para.Font = FontBold;
                            Step4_Q5Para.Alignment = Element.ALIGN_LEFT;


                            //Chunk C_Step4_Q5_Ans = new Chunk(Step4_Ans);
                            //C_Step4_Q5_Ans.setLineHeight(3f);
                            Paragraph Step4_Q5_AnsPara = new Paragraph(new Phrase(Step4_Ans, baseFontNormal));
                            Step4_Q5_AnsPara.IndentationLeft = 60f;
                            Step4_Q5_AnsPara.Font = FontAnswer;
                            Step4_Q5_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step4_Q = "4.6 " + "Have you got someone that can support technically ?";

                            TempText = null;
                            foreach (var item in ViewBag.SupportTechnicallyList)
                            {
                                if (item.ID == obj.Company.SupportTechnically)
                                    TempText += item.Value;

                            }

                            Step4_Ans = TempText == null ? "_______________" : TempText;
                            Chunk C_Step4_Q6 = new Chunk(Step4_Q);
                            Paragraph Step4_Q6Para = new Paragraph(C_Step4_Q6);
                            Step4_Q6Para.IndentationLeft = 40f;
                            Step4_Q6Para.SpacingBefore = 10f;
                            Step4_Q6Para.SpacingAfter = 10f;
                            Step4_Q6Para.Font = FontBold;
                            Step4_Q6Para.Alignment = Element.ALIGN_LEFT;


                            //Chunk C_Step4_Q6_Ans = new Chunk(Step4_Ans);
                            //C_Step4_Q6_Ans.setLineHeight(3f);
                            Paragraph Step4_Q6_AnsPara = new Paragraph(new Phrase(Step4_Ans, baseFontNormal));
                            Step4_Q6_AnsPara.IndentationLeft = 60f;
                            Step4_Q6_AnsPara.Font = FontAnswer;
                            Step4_Q6_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step4_Q = "4.7 " + "Who is going to build it?";

                            TempText = null;
                            foreach (var item in ViewBag.BuildFromList)
                            {
                                if (item.ID == obj.Company.BuildFrom)
                                    TempText += item.Value;

                            }

                            Step4_Ans = TempText == null ? "_______________" : TempText;
                            Chunk C_Step4_Q7 = new Chunk(Step4_Q);
                            Paragraph Step4_Q7Para = new Paragraph(C_Step4_Q7);
                            Step4_Q7Para.IndentationLeft = 40f;
                            Step4_Q7Para.SpacingBefore = 10f;
                            Step4_Q7Para.SpacingAfter = 10f;
                            Step4_Q7Para.Font = FontBold;
                            Step4_Q7Para.Alignment = Element.ALIGN_LEFT;


                            //Chunk C_Step4_Q7_Ans = new Chunk(Step4_Ans);
                            //C_Step4_Q7_Ans.setLineHeight(3f);
                            Paragraph Step4_Q7_AnsPara = new Paragraph(new Phrase(Step4_Ans, baseFontNormal));
                            Step4_Q7_AnsPara.IndentationLeft = 60f;
                            Step4_Q7_AnsPara.Font = FontAnswer;
                            Step4_Q7_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step4_Q = "4.8 " + "Have you any thoughts on the brand that you want to create?";
                            Step4_Ans = TempText == obj.Company.BrandThought ? "_______________" : obj.Company.BrandThought;
                            Chunk C_Step4_Q8 = new Chunk(Step4_Q);
                            Paragraph Step4_Q8Para = new Paragraph(C_Step4_Q8);
                            Step4_Q8Para.IndentationLeft = 40f;
                            Step4_Q8Para.SpacingBefore = 10f;
                            Step4_Q8Para.SpacingAfter = 10f;
                            Step4_Q8Para.Font = FontBold;
                            Step4_Q8Para.Alignment = Element.ALIGN_LEFT;


                            //Chunk C_Step4_Q8_Ans = new Chunk(Step4_Ans);
                            //C_Step4_Q8_Ans.setLineHeight(3f);
                            Paragraph Step4_Q8_AnsPara = new Paragraph(new Phrase(Step4_Ans, baseFontNormal));
                            Step4_Q8_AnsPara.IndentationLeft = 60f;
                            Step4_Q8_AnsPara.Font = FontAnswer;
                            Step4_Q8_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step4_Q = "4.9 " + "Think about company mission what could it be?";
                            Step4_Ans = TempText == obj.Company.CompanyMission ? "_______________" : obj.Company.CompanyMission;
                            Chunk C_Step4_Q9 = new Chunk(Step4_Q);
                            Paragraph Step4_Q9Para = new Paragraph(C_Step4_Q9);
                            Step4_Q9Para.IndentationLeft = 40f;
                            Step4_Q9Para.SpacingBefore = 10f;
                            Step4_Q9Para.SpacingAfter = 10f;
                            Step4_Q9Para.Font = FontBold;
                            Step4_Q9Para.Alignment = Element.ALIGN_LEFT;


                            //Chunk C_Step4_Q9_Ans = new Chunk(Step4_Ans);
                            //C_Step4_Q9_Ans.setLineHeight(3f);
                            Paragraph Step4_Q9_AnsPara = new Paragraph(new Phrase(Step4_Ans, baseFontNormal));
                            Step4_Q9_AnsPara.IndentationLeft = 60f;
                            Step4_Q9_AnsPara.Font = FontAnswer;
                            Step4_Q9_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step4_Q = "4.10 " + "Think about look & feel, how will the user interact with the company?";
                            Step4_Ans = TempText == obj.Company.CompanyLookFeel ? "_______________" : obj.Company.CompanyLookFeel;
                            Chunk C_Step4_Q10 = new Chunk(Step4_Q);
                            Paragraph Step4_Q10Para = new Paragraph(C_Step4_Q10);
                            Step4_Q10Para.IndentationLeft = 40f;
                            Step4_Q10Para.SpacingBefore = 10f;
                            Step4_Q10Para.SpacingAfter = 10f;
                            Step4_Q10Para.Font = FontBold;
                            Step4_Q10Para.Alignment = Element.ALIGN_LEFT;


                            //Chunk C_Step4_Q10_Ans = new Chunk(Step4_Ans);
                            //C_Step4_Q10_Ans.setLineHeight(3f);
                            Paragraph Step4_Q10_AnsPara = new Paragraph(new Phrase(Step4_Ans, baseFontNormal));
                            Step4_Q10_AnsPara.IndentationLeft = 60f;
                            Step4_Q10_AnsPara.Font = FontAnswer;
                            Step4_Q10_AnsPara.Alignment = Element.ALIGN_LEFT;
                            //-------STEP-5------


                            string Step5Heading = "5. " + IdeaoutofmyheadList[4].ToString();
                            string Step5_Q = "5.1 " + "What are you selling?";
                            TempText = null;
                            var arraySellType = String.IsNullOrEmpty(obj.IdeaSelling.SellType) == false ? obj.IdeaSelling.SellType.Split(',').ToArray() : new string[0];
                            foreach (var item in ViewBag.MasterSelling)
                            {
                                if (Array.Exists(arraySellType, x => x == Convert.ToString(@item.ID)) == true)
                                    TempText += item.Value + "\n";
                            }
                            string Step5_Ans = TempText == null ? "_______________" : TempText;

                            Paragraph Step5HeadingPara = new Paragraph(new Phrase(Step5Heading, baseFontBig));
                            Step5HeadingPara.IndentationLeft = 30f;
                            Step5HeadingPara.Font = baseFontNormal;
                            Step5HeadingPara.Alignment = Element.ALIGN_LEFT;


                            Chunk C_Step5_Q1 = new Chunk(Step5_Q);
                            Paragraph Step5_Q1Para = new Paragraph(C_Step5_Q1);
                            Step5_Q1Para.IndentationLeft = 40f;
                            Step5_Q1Para.SpacingBefore = 10f;
                            Step5_Q1Para.SpacingAfter = 10f;
                            Step5_Q1Para.Font = FontBold;
                            Step5_Q1Para.Alignment = Element.ALIGN_LEFT;


                            Paragraph Step5_Q1_AnsPara = new Paragraph(new Phrase(Step5_Ans, baseFontNormal));
                            Step5_Q1_AnsPara.IndentationLeft = 60f;
                            Step5_Q1_AnsPara.Font = FontAnswer;
                            Step5_Q1_AnsPara.Alignment = Element.ALIGN_LEFT;

                            Step5_Q = "5.2 " + "Who is going to buy the product?";
                            Step5_Ans = TempText == obj.IdeaSelling.ProductBuy ? "_______________" : obj.IdeaSelling.ProductBuy;
                            Chunk C_Step5_Q2 = new Chunk(Step5_Q);
                            Paragraph Step5_Q2Para = new Paragraph(C_Step5_Q2);
                            Step5_Q2Para.IndentationLeft = 40f;
                            Step5_Q2Para.SpacingBefore = 10f;
                            Step5_Q2Para.SpacingAfter = 10f;
                            Step5_Q2Para.Font = FontBold;
                            Step5_Q2Para.Alignment = Element.ALIGN_LEFT;



                            Paragraph Step5_Q2_AnsPara = new Paragraph(new Phrase(Step5_Ans, baseFontNormal));
                            Step5_Q2_AnsPara.IndentationLeft = 60f;
                            Step5_Q2_AnsPara.Font = FontAnswer;
                            Step5_Q2_AnsPara.Alignment = Element.ALIGN_LEFT;


                            Step5_Q = "5.3 " + "How would you like to charge?";
                            TempText = null;
                            var arrayProductCharge = String.IsNullOrEmpty(obj.IdeaSelling.ProductCharge) == false ? obj.IdeaSelling.ProductCharge.Split(',').ToArray() : new string[0];
                            foreach (var item in ViewBag.MasterCharge)
                            {
                                if (Array.Exists(arraySellType, x => x == Convert.ToString(@item.ID)) == true)
                                    TempText += item.Value + "\n";
                            }
                            Step5_Ans = TempText == null ? "_______________" : TempText;

                            Chunk C_Step5_Q3 = new Chunk(Step5_Q);
                            Paragraph Step5_Q3Para = new Paragraph(C_Step5_Q3);
                            Step5_Q3Para.IndentationLeft = 40f;
                            Step5_Q3Para.SpacingBefore = 10f;
                            Step5_Q3Para.SpacingAfter = 10f;
                            Step5_Q3Para.Font = FontBold;
                            Step5_Q3Para.Alignment = Element.ALIGN_LEFT;


                            Paragraph Step5_Q3_AnsPara = new Paragraph(new Phrase(Step5_Ans, baseFontNormal));
                            Step5_Q3_AnsPara.IndentationLeft = 60f;
                            Step5_Q3_AnsPara.Font = FontAnswer;
                            Step5_Q3_AnsPara.Alignment = Element.ALIGN_LEFT;

                            Step5_Q = "5.4 " + "How much are going to charge?";
                            Step5_Ans = TempText == obj.IdeaSelling.ChargeGoing ? "_______________" : obj.IdeaSelling.ChargeGoing;
                            Chunk C_Step5_Q4 = new Chunk(Step5_Q);
                            Paragraph Step5_Q4Para = new Paragraph(C_Step5_Q2);
                            Step5_Q4Para.IndentationLeft = 40f;
                            Step5_Q4Para.SpacingBefore = 10f;
                            Step5_Q4Para.SpacingAfter = 10f;
                            Step5_Q4Para.Font = FontBold;
                            Step5_Q4Para.Alignment = Element.ALIGN_LEFT;



                            Paragraph Step5_Q4_AnsPara = new Paragraph(new Phrase(Step5_Ans, baseFontNormal));
                            Step5_Q4_AnsPara.IndentationLeft = 60f;
                            Step5_Q4_AnsPara.Font = FontAnswer;
                            Step5_Q4_AnsPara.Alignment = Element.ALIGN_LEFT;

                            //Step5_Q = "5.5 " + "Who are you selling to?";
                            //TempText = null;

                            //foreach (var item in ViewBag.SellToList)
                            //{
                            //    if (item.ID == obj.IdeaSelling.SellTo)
                            //        TempText = item.Values;
                            //}
                            //Step5_Ans = TempText == null ? "_______________" : TempText;

                            //Chunk C_Step5_Q5 = new Chunk(Step5_Q);
                            //Paragraph Step5_Q5Para = new Paragraph(C_Step5_Q5);
                            //Step5_Q5Para.IndentationLeft = 40f;
                            //Step5_Q5Para.SpacingBefore = 10f;
                            //Step5_Q5Para.SpacingAfter = 10f;
                            //Step5_Q5Para.Font = FontBold;
                            //Step5_Q5Para.Alignment = Element.ALIGN_LEFT;


                            //Paragraph Step5_Q5_AnsPara = new Paragraph(new Phrase(Step5_Ans, baseFontNormal));
                            //Step5_Q5_AnsPara.IndentationLeft = 60f;
                            //Step5_Q5_AnsPara.Font = FontAnswer;
                            //Step5_Q5_AnsPara.Alignment = Element.ALIGN_LEFT;
                            //Step5_Q = "5.6 " + "How are you planning on finding customers?";
                            //Step5_Ans = TempText == obj.IdeaSelling.CustomerFindPlan ? "_______________" : obj.IdeaSelling.CustomerFindPlan;
                            //Chunk C_Step5_Q6 = new Chunk(Step5_Q);
                            //Paragraph Step5_Q6Para = new Paragraph(C_Step5_Q6);
                            //Step5_Q6Para.IndentationLeft = 40f;
                            //Step5_Q6Para.SpacingBefore = 10f;
                            //Step5_Q6Para.SpacingAfter = 10f;
                            //Step5_Q6Para.Font = FontBold;
                            //Step5_Q6Para.Alignment = Element.ALIGN_LEFT;



                            //Paragraph Step5_Q6_AnsPara = new Paragraph(new Phrase(Step5_Ans, baseFontNormal));
                            //Step5_Q6_AnsPara.IndentationLeft = 60f;
                            //Step5_Q6_AnsPara.Font = FontAnswer;
                            //Step5_Q6_AnsPara.Alignment = Element.ALIGN_LEFT;



                            //--- STEP-1---
                            pdfDoc.Add(Step1HeadingPara);
                            pdfDoc.Add(Step1_Q1Para);
                            pdfDoc.Add(Step1_Q1_AnsPara);
                            //--- STEP-2---
                            pdfDoc.Add(Step2HeadingPara);
                            pdfDoc.Add(Step2_Q1Para);
                            pdfDoc.Add(Step2_Q1_AnsPara);

                            pdfDoc.Add(Step2_Q2Para);
                            pdfDoc.Add(Step2_Q2_AnsPara);

                            pdfDoc.Add(Step2_Q3Para);
                            pdfDoc.Add(Step2_Q3_AnsPara);

                            pdfDoc.Add(Step2_Q4Para);
                            pdfDoc.Add(Step2_Q4_AnsPara);

                            pdfDoc.Add(Step2_Q5Para);
                            pdfDoc.Add(Step2_Q5_AnsPara);

                            pdfDoc.Add(Step2_Q6Para);
                            pdfDoc.Add(Step2_Q6_AnsPara);

                            pdfDoc.Add(Step2_Q7Para);
                            pdfDoc.Add(Step2_Q7_AnsPara);

                            pdfDoc.Add(Step2_Q8Para);
                            pdfDoc.Add(Step2_Q8_AnsPara);

                            pdfDoc.Add(Step2_Q9Para);
                            pdfDoc.Add(Step2_Q9_AnsPara);

                            pdfDoc.Add(Step2_Q10Para);
                            pdfDoc.Add(Step2_Q10_AnsPara);

                            pdfDoc.Add(Step2_Q11Para);
                            pdfDoc.Add(Step2_Q11_AnsPara);

                            //-----STEP-3------->

                            pdfDoc.Add(Step3HeadingPara);
                            pdfDoc.Add(Step3_Q1Para);
                            pdfDoc.Add(Step3_Q1_AnsPara);


                            pdfDoc.Add(Step3_Q2Para);
                            pdfDoc.Add(Step3_Q2_AnsPara);

                            
                            pdfDoc.Add(Step3_Q3Para);
                            pdfDoc.Add(Step3_Q3_AnsPara);


                            pdfDoc.Add(Step3_Q4Para);
                            pdfDoc.Add(Step3_Q4_AnsPara);


                            pdfDoc.Add(Step3_Q5Para);
                            pdfDoc.Add(Step3_Q5_AnsPara);

                            //-----STEP-4------->

                            pdfDoc.Add(Step4HeadingPara);
                            pdfDoc.Add(Step4_Q1Para);
                            pdfDoc.Add(Step4_Q1_AnsPara);

                            pdfDoc.Add(Step4_Q2Para);
                            pdfDoc.Add(Step4_Q2_AnsPara);

                            pdfDoc.Add(Step4_Q3Para);
                            pdfDoc.Add(Step4_Q3_AnsPara);

                            pdfDoc.Add(Step4_Q4Para);
                            pdfDoc.Add(Step4_Q4_AnsPara);

                            pdfDoc.Add(Step4_Q5Para);
                            pdfDoc.Add(Step4_Q5_AnsPara);

                            pdfDoc.Add(Step4_Q6Para);
                            pdfDoc.Add(Step4_Q6_AnsPara);

                            pdfDoc.Add(Step4_Q7Para);
                            pdfDoc.Add(Step4_Q7_AnsPara);

                            pdfDoc.Add(Step4_Q8Para);
                            pdfDoc.Add(Step4_Q8_AnsPara);

                            pdfDoc.Add(Step4_Q9Para);
                            pdfDoc.Add(Step4_Q9_AnsPara);

                            pdfDoc.Add(Step4_Q10Para);
                            pdfDoc.Add(Step4_Q10_AnsPara);

                            //-----STEP-5------->

                            pdfDoc.Add(Step5HeadingPara);
                            pdfDoc.Add(Step5_Q1Para);
                            pdfDoc.Add(Step5_Q1_AnsPara);

                            pdfDoc.Add(Step5_Q2Para);
                            pdfDoc.Add(Step5_Q2_AnsPara);

                            pdfDoc.Add(Step5_Q3Para);
                            pdfDoc.Add(Step5_Q3_AnsPara);

                            pdfDoc.Add(Step5_Q4Para);
                            pdfDoc.Add(Step5_Q4_AnsPara);


                            //pdfDoc.Add(Step5_Q5Para);
                            //pdfDoc.Add(Step5_Q5_AnsPara);
                           

                            pdfDoc.Close();
                            status = "OK";

                        }
                        catch (Exception e)
                        {
                            status = e.Message.ToString();
                        }

                    }
                }
            }
              return Json(new { status = status, fileName = fileName }, JsonRequestBehavior.AllowGet);
        }


    }

}
