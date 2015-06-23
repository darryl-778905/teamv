using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurveyGenerator.Controllers
{
    public class SurveyController : Controller
    {
        //
        // GET: /Survey/
        public ActionResult CreateSurvey()
        {
            return View();
        }
	}
}