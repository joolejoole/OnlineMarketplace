using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JoJo.Controllers
{
    public class CompareController : Controller
    {
        // GET: Compare
        public ActionResult Index()
        {
            List<Models.Product> products = new List<Models.Product>
            {
                new Models.Product(1),
                new Models.Product(2),
                new Models.Product(3)
            };

            return View(products);
        }
    }
}