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
        private static int gID;
        //JoJoEntities one = new JoJoEntities();
        Sample s;
        public ActionResult Search()
        {
            JoJoEntities one = new JoJoEntities();
            CatSubModel two = new CatSubModel();
            List<ProductCategory> prodCat = one.ProductCategory.ToList();
            ViewBag.prodCat = new SelectList(prodCat, "CategoryId", "CategoryName");
            return View(two);
        }

        public JsonResult GetSubCat(int CategoryId)
        {
            JoJoEntities one = new JoJoEntities();
            s = new Sample(one);
            one.Configuration.ProxyCreationEnabled = false;
            List<ProductSubCategory> subCat = one.ProductSubCategory.Where(x => x.CategoryId == CategoryId).ToList();
            gID = CategoryId;
            //s.subCategory(CategoryId)
            return Json(subCat, JsonRequestBehavior.AllowGet);
            //return Json(s.subCategory(CategoryId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getemp(string ename)
        {
            JoJoEntities one = new JoJoEntities();
            var g = gID;
            //List<ProductSubCategory> subCat = one.ProductSubCategory.Where(x => x.CategoryId == CategoryId).ToList();
            var emp = (from x in one.ProductSubCategory where x.SubCategoryName.StartsWith(ename) && x.CategoryId==g select new { label = x.SubCategoryName }).ToList();
            s = new Sample(one);
            return Json(emp);
        }
    }
}