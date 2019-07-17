using JoJo.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoJo.Models
{
    public class Total
    {
        public int ProductId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string SpecDetails { get; set; }
        public string TypeDetails { get; set; }
        public string ModelName { get; set; }
        public string ModelType { get; set; }
        public string SeriesName { get; set; }
        public string ManufactureIdName { get; set; }
        public string SubCategoryName { get; set; }
        public string Specs { get; set; }
        public string CategoryName { get; set; }
        public int ManufactureId { get; set; }
        public int SeriesId { get; set; }
        public int ModelId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
    }

}