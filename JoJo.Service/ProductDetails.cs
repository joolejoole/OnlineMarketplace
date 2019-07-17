
using JoJo.Repo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJo.Service
{
    public class ProductDetails
    {
        private DbContext dbContext;

        private Repo.UnitOfWork uow;

        public ProductDetails(DbContext dbContext)
        {
            this.dbContext = dbContext;
            uow = new Repo.UnitOfWork(this.dbContext);
        }

        public IEnumerable<Models.Total> GetProduct()
        {
            var _ProductCategory = uow.ProductCategoryRepository.GetAll();
            var _ProductSubCategory = uow.ProductSubCategoryRepository.GetAll();
            var _Product = uow.ProductRepository.GetAll();
            var _Model = uow.ModelRepository.GetAll();
            var _Series = uow.SeriesRepository.GetAll();
            var _Manufacture = uow.ManufactureRepository.GetAll();
            var table1 = (from manufacture in _Manufacture
                          join series in _Series on manufacture.ManufactureId equals series.ManufactureId
                          join model in _Model on series.SeriesId equals model.SeriesId
                          select new Models.Total {
                              ManufactureId = manufacture.ManufactureId,
                              ManufactureIdName = manufacture.ManufactureIdName,
                              SeriesId = series.SeriesId,
                              SeriesName = series.SeriesName,
                              ModelId = model.ModelId,
                              ModelName = model.ModelName
                          });

            var table2 = (from productCategory in _ProductCategory
                          join productSubCategory in _ProductSubCategory on productCategory.CategoryId equals productSubCategory.CategoryId
                          select new Models.Total {
                             CategoryId = productCategory.CategoryId,
                             CategoryName = productCategory.CategoryName,
                             SubCategoryId = productSubCategory.SubCategoryId,
                             SubCategoryName = productSubCategory.SubCategoryName,
                             Specs = productSubCategory.SPECS,
                             ModelType = productSubCategory.ModelType
                          });
            var table3 = (from t1 in table1
                          join product in _Product on t1.ModelId equals product.ModelId
                          select new Models.Total
                          {
                              ManufactureId = t1.ManufactureId,
                              ManufactureIdName = t1.ManufactureIdName,
                              SeriesId = t1.SeriesId,
                              SeriesName = t1.SeriesName,
                              ModelId = t1.ModelId,
                              ModelName = t1.ModelName,
                              ProductId = product.ProductId,
                              CategoryId = product.CategoryId,
                              TypeDetails = product.TypeDetails,
                              ProductName = product.ProductName,
                              ProductImage = product.ProductImage,
                              SubCategoryId = product.SubCategoryId,
                              SpecDetails = product.SpecDetails
                          }) ;
            var query = (from t2 in table2
                         join t3 in table3 on t2.SubCategoryId equals t3.SubCategoryId orderby t3.ProductId
                         select new Models.Total {
                             CategoryName = t2.CategoryName,
                             SubCategoryName = t2.SubCategoryName,
                             Specs = t2.Specs,
                             ModelType = t2.ModelType,
                             ManufactureIdName = t3.ManufactureIdName,
                             SeriesName = t3.SeriesName,
                             ModelName = t3.ModelName,
                             ProductId = t3.ProductId,
                             TypeDetails = t3.TypeDetails,
                             ProductName = t3.ProductName,
                             ProductImage = t3.ProductImage,
                             SpecDetails = t3.SpecDetails

                         });
            return query;
        }

        public IEnumerable<Core.ProductCategory> GetCategory()
        {
            return uow.ProductCategoryRepository.GetAll();
        }
    }
}
