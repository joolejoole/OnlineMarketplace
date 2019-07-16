using JoJo.Core;
using JoJo.Models;
using JoJo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JoJo.Controllers
{
    public class SearchController : Controller
    {
        JoJoEntities one = new JoJoEntities();
        Sample s;
        public ActionResult Search()
        {
            CatSubModel two = new CatSubModel();
            List<ProductCategory> prodCat = one.ProductCategory.ToList();
            ViewBag.prodCat = new SelectList(prodCat, "CategoryId", "CategoryName");
            return View(two);
        }

        public JsonResult GetSubCat(int CategoryId)
        {

            s = new Sample(one);
            one.Configuration.ProxyCreationEnabled = false;
            return Json(s.subCategory(CategoryId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getemp(string ename)
        {
            var emp = (from x in one.ProductSubCategory where x.SubCategoryName.StartsWith(ename) select new { label = x.SubCategoryName }).ToList();
            s = new Sample(one);
            return Json(emp);
        }
    }
}