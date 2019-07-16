using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoJo;

namespace JoJo.Repo
{
    public class UnitOfWork : IDisposable
    {
        private static DbContext dbContext = null;

        public UnitOfWork()
        {
            dbContext = new Core.JoJoEntities();
        }

        public UnitOfWork(DbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        // Add all the repository handles here
        private static IRepository<Core.Users> usersRepository = null;

        private static IRepository<Core.UserRole> userRoleRepository = null;

        private static IRepository<Core.Manufacture> manufactureRepository = null;
        private static IRepository<Core.Series> seriesRepository = null;
        private static IRepository<Core.Model> modelRepository = null;

        private static IRepository<Core.Product> productRepository = null;
        private static IRepository<Core.ProductCategory> productCategoryRepository = null;
        private static IRepository<Core.ProductSubCategory> productSubCategoryRepository = null;

        // Add all the repository getters here

        public IRepository<Core.Users> UsersRepository
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new UsersRepository(dbContext);
                }
                return usersRepository;
            }
        }

        public IRepository<Core.UserRole> UserRoleRepository
        {
            get
            {
                if (userRoleRepository == null)
                {
                    userRoleRepository = new UserRoleRepository(dbContext);
                }
                return userRoleRepository;
            }
        }

        public IRepository<Core.Manufacture> ManufactureRepository
        {
            get
            {
                if (manufactureRepository == null)
                {
                    manufactureRepository = new ManufactureRepository(dbContext);
                }
                return manufactureRepository;
            }
        }

        public IRepository<Core.Series> SeriesRepository
        {
            get
            {
                if (seriesRepository == null)
                {
                    seriesRepository = new SeriesRepository(dbContext);
                }
                return seriesRepository;
            }
        }

        public IRepository<Core.Model> ModelRepository
        {
            get
            {
                if (modelRepository == null)
                {
                    modelRepository = new ModelRepository(dbContext);
                }
                return modelRepository;
            }
        }

        public IRepository<Core.Product> ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(dbContext);
                }
                return productRepository;
            }
        }

        public IRepository<Core.ProductCategory> ProductCategoryRepository
        {
            get
            {
                if (productCategoryRepository == null)
                {
                    productCategoryRepository = new ProductCategoryRepository(dbContext);
                }
                return productCategoryRepository;
            }
        }

        public IRepository<Core.ProductSubCategory> ProductSubCategoryRepository
        {
            get
            {
                if (productSubCategoryRepository == null)
                {
                    productSubCategoryRepository = new ProductSubCategoryRepository(dbContext);
                }
                return productSubCategoryRepository;
            }
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public bool Save()
        {
            bool status = false;

            try
            {
                dbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return status;
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
