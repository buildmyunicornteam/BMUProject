using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMyUnicorn.Controllers
{
    public class QuestController : WebController
    {
        // GET: Quest
        public ActionResult Index()
        {
            return View();
        }
    }
}