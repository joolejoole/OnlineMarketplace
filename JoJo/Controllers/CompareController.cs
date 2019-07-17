using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JoJo.Controllers
{
    public class CompareController : Controller
    {
        // DummyData Test
        public ActionResult Index()
        {
            List<Models.Product> products = new List<Models.Product>
            {
                new Models.Product(1),
                new Models.Product(5),
                new Models.Product(6)
            };

            if (validateCategories(products))
            {
                return View("Index", products);
            }
            else
            {
                return View("Wrong", products);
            }
        }

        // Get checkedItem from Filter.cshtml
        [HttpGet]
        public ActionResult Compare(List<string> checkedItem)
        {
            List<Models.Product> products = new List<Models.Product>();

            foreach (string id_string in checkedItem)
            {
                int id = Convert.ToInt32(id_string);
                products.Add(new Models.Product(id));
            }

            if (validateCategories(products))
            {
                return View("Index", products);
            }
            else
            {
                return View("Wrong", products);
            }
        }

        // Check if all products in List have same category and subcategory
        private bool validateCategories(List<Models.Product> products)
        {
            string category = products.First().CategoryName;
            string subcategory = products.First().SubCategoryName;

            foreach (Models.Product p in products)
            {
                if (category != p.CategoryName) return false;
                if (subcategory != p.SubCategoryName) return false;
            }

            return true;
        }
    }
}