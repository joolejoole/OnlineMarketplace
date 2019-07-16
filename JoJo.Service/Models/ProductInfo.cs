using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace JoJo.Service
{
    public class ProductInfo
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string SPECS { get; set; }   // XML
        public Nullable<int> SpecsAmt { get; set; }
        public string SpecDetails { get; set; } // XML
        public string TypeDetails { get; set; } // XML
        public string ModelType { get; set; }   // XML
        public Nullable<int> TypeAmt { get; set; }

        public int ManufactureId { get; set; }
        public string ManufactureIdName { get; set; }

        public int SeriesId { get; set; }
        public string SeriesName { get; set; }

        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public Dictionary<string, string> TypeValues { get; set; }
        public Dictionary<string, string> SpecValues { get; set; }
        public Dictionary<string, string> SpecLabels { get; set; }

        public ProductInfo(int id)
        {
            ProductId = id;

            Repo.UnitOfWork uow = new Repo.UnitOfWork();

            // Info from Product table
            Core.Product product = uow.ProductRepository.GetById(id);
            this.ProductName = product.ProductName;
            this.ProductImage = product.ProductImage;
            this.CategoryId = product.CategoryId;
            this.SubCategoryId = product.SubCategoryId;
            this.ManufactureId = product.ManufactureId;
            this.SeriesId = product.SeriesId;
            this.ModelId = product.ModelId;
            this.SpecDetails = product.SpecDetails;
            this.TypeDetails = product.TypeDetails;

            // Info from Manufacture table
            this.ManufactureIdName = uow.ManufactureRepository
                                               .GetById(this.ManufactureId)
                                               .ManufactureIdName;

            // Info from Series table
            this.SeriesName = uow.SeriesRepository
                                        .GetById(this.SeriesId)
                                        .SeriesName;

            // Info from Model table
            Core.Model model = uow.ModelRepository.GetById(this.ModelId);
            this.ModelName = model.ModelName;

            // Info from ProductCategory table
            this.CategoryName = uow.ProductCategoryRepository
                                          .GetById(this.CategoryId)
                                          .CategoryName;

            // Info from ProductSubCategory table
            Core.ProductSubCategory productSub = uow.ProductSubCategoryRepository
                                                    .GetById(this.SubCategoryId);
            this.SubCategoryName = productSub.SubCategoryName;
            this.SPECS = productSub.SPECS;
            this.SpecsAmt = productSub.SpecsAmt;
            this.ModelType = productSub.ModelType;
            this.TypeAmt = productSub.TypeAmt;

            // Use XmlHelpers to convert data in Xml format into Dictionary<string,string>
            this.SpecValues = XmlHelper(this.SPECS, this.SpecDetails);
            this.TypeValues = XmlHelper(this.ModelType, this.TypeDetails);
            this.SpecLabels = XmlHelper_SpecLabels(this.SPECS);
        }

        // Convert Xml formart data into Dictionary<string,string>,matching based on id attribute.
        // - des_xml_string: [Xml format] descriptions (sample at the end of this file)
        // - val_xml_string: [Xml format] values (sample at the end of this file)
        private Dictionary<string, string> XmlHelper(string des_xml_string, string val_xml_string)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            // Get descriptions from Xml string
            XElement descriptions_xml = XElement.Parse(des_xml_string);

            var descriptions =
                from description in descriptions_xml.Descendants("item")
                select description;

            Dictionary<int, string> des_pair = new Dictionary<int, string>();

            foreach (var des in descriptions)
            {
                int id = Convert.ToInt32(des.Attribute("id").Value);
                string description = des.Value;
                des_pair.Add(id, description);
            }

            // Get values from Xml string
            XElement values_xml = XElement.Parse(val_xml_string);

            var values =
                from value in values_xml.Descendants("item")
                select value;

            Dictionary<int, string> val_pair = new Dictionary<int, string>();

            foreach (var val in values)
            {
                int id = Convert.ToInt32(val.Attribute("id").Value);
                string value = val.Value;
                val_pair.Add(id, value);
            }

            // Match descriptions and values with id,
            // and put them into return object ( Dictionary<string,string> )
            foreach (var pair in des_pair)
            {
                int id = pair.Key;
                ret.Add(pair.Value, val_pair[id]);
            }

            return ret;
        }

        private Dictionary<string, string> XmlHelper_SpecLabels(string des_xml_string)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            // Get descriptions from Xml string
            XElement xml = XElement.Parse(des_xml_string);

            var descriptions =
                from description in xml.Descendants("item")
                select description;

            Dictionary<int, string> des_pair = new Dictionary<int, string>();

            foreach (var des in descriptions)
            {
                int id = Convert.ToInt32(des.Attribute("id").Value);
                string description = des.Value;
                des_pair.Add(id, description);
            }

            // Get labels from Xml string
            var labels =
                from label in xml.Descendants("label")
                select label;

            Dictionary<int, string> lbl_pair = new Dictionary<int, string>();

            foreach (var lbl in labels)
            {
                int id = Convert.ToInt32(lbl.Attribute("id").Value);
                string label = lbl.Value;
                lbl_pair.Add(id, label);
            }

            // Match descriptions and labels with id,
            // and put them into return object
            foreach (var pair in lbl_pair)
            {
                int id = pair.Key;
                ret.Add(des_pair[id], pair.Value);
            }

            return ret;
        }
    }
}

/*** Sample: Xml descriptions (SPEC)
 *
 * <spec>
 *   <item id="1" type="scaler">Sound Driver</item>
 *   <item id="2" type="scaler">Waterproof</item>
 *   <item id="3" type="scaler">Bluetooth Version</item>
 *   <item id="4" type="scaler">Play Time</item>
 *   <item id="5" type="scaler">Weight</item>
 * </spec>
 *
***/

/*** Sample: Xml values (SpecDetails)
 *
 * <sp_detail>
 *   <item id="1">20</item>
 *   <item id="2">5</item>
 *   <item id="3">4.2</item>
 *   <item id="4">12</item>
 *   <item id="5">1.23</item>
 * </sp_detail>
 *
***/