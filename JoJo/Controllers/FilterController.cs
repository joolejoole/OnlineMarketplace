using JoJo.Core;
using JoJo.Models;
using JoJo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Xml.Linq;
using Cache = JoJo.Service.Cache;

namespace JoJo.Controllers
{
    public class FilterController : Controller
    {
        [HttpGet]
        public ActionResult Filter(string CategoryID, string SubCategoryName)
        {
            int category = Convert.ToInt32(CategoryID);
            string subcategory = SubCategoryName;
            Core.JoJoEntities jo = new Core.JoJoEntities();
            Service.ProductDetails productDetails = new Service.ProductDetails(jo);
            var ProductList = productDetails.GetProduct();
            var result = (from p in ProductList where p.CategoryId == category && p.SubCategoryName == subcategory select p);
            ViewBag.type = subcategory;
            TempData["type"] = subcategory;
            ViewBag.result = result;

            var Categories = productDetails.GetCategory();
            ViewBag.categories = Categories;

            return View();
        }

        [HttpGet]
        public ActionResult FilterForm(string projectType, int minYear, int maxYear, int minAir, int maxAir, int minPower, int maxPower, int minSound, int maxSound, int minSweep, int maxSweep, string sub)
        {
            Core.JoJoEntities jo = new JoJoEntities();
            Service.ProductDetails productDetails = new Service.ProductDetails(jo);
            var ProductList = productDetails.GetProduct();
            var result = (from p in ProductList
                          where
                            p.CategoryName == projectType &&
                            p.SubCategoryName == sub &&
                            Int32.Parse(XElement.Parse(p.TypeDetails).Elements("item").ToList()[4].Value) >= minYear &&
                            Int32.Parse(XElement.Parse(p.TypeDetails).Elements("item").ToList()[4].Value) <= maxYear &&
                            Int32.Parse(XElement.Parse(p.SpecDetails).Elements("item").ToList()[0].Value) >= minAir &&
                            Int32.Parse(XElement.Parse(p.SpecDetails).Elements("item").ToList()[0].Value) <= maxAir &&
                            Int32.Parse(XElement.Parse(p.SpecDetails).Elements("item").ToList()[5].Value) >= minSound &&
                            Int32.Parse(XElement.Parse(p.SpecDetails).Elements("item").ToList()[5].Value) <= maxSound &&
                            Double.Parse(XElement.Parse(p.SpecDetails).Elements("item").ToList()[7].Value.Split(',')[0].Substring(1)) >= minSweep &&
                            Double.Parse(XElement.Parse(p.SpecDetails).Elements("item").ToList()[7].Value.Split(',')[0].Substring(1)) <= maxSweep &&
                            Double.Parse(XElement.Parse(p.SpecDetails).Elements("item").ToList()[1].Value.Split(',')[0].Substring(1)) >= minPower &&
                            Double.Parse(XElement.Parse(p.SpecDetails).Elements("item").ToList()[1].Value.Split(',')[0].Substring(1)) <= maxPower
                          select p);
            ViewBag.type = "Fans";
            ViewBag.result = result;

            var Categories = productDetails.GetCategory();
            ViewBag.categories = Categories;

            return View("Filter");
        }

        [HttpGet]
        public ActionResult Test(List<string> checkedItem)
        {
            ViewBag.c = checkedItem;
            return View();
        }

        public ActionResult Click(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}