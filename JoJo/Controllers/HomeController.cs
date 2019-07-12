using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JoJo;

namespace JoJo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (Core.JoJoEntities jo = new Core.JoJoEntities())
            {
                Service.Sample sample = new Service.Sample(jo);
                //sample.TestRelationship();
            }

            return View();
        }
    }
}