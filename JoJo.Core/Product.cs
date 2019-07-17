//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JoJo.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int ModelId { get; set; }
        public int SeriesId { get; set; }
        public int ManufactureId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string SpecDetails { get; set; }
        public string TypeDetails { get; set; }
    
        public virtual Manufacture Manufacture { get; set; }
        public virtual Model Model { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductSubCategory ProductSubCategory { get; set; }
        public virtual Series Series { get; set; }
    }
}
