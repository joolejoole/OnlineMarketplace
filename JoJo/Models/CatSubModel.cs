using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoJo.Models
{
    public class CatSubModel
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}