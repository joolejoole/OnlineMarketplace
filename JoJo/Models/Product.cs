using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JoJo;

namespace JoJo.Models
{
    public class Product : Service.ProductInfo
    {
        public Product(int id) : base(id)
        {
        }
    }
}