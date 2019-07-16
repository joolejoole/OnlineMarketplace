using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JoJo.Controllers
{
    public class ProductSummaryController : Controller
    {
        // GET: ProductSummary
        public ActionResult ShowProductSummary()
        {
            Models.Product SummaryData = new Models.Product(4);
            //string ManuName = SummaryData.ManufactureIdName;
            //ViewBag.ManufactureName = ManuName;
            SummaryData.ToString();
            return View(SummaryData);
        }
    }
}