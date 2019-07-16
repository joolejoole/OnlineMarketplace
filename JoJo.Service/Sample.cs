using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoJo;
using JoJo.Core;

namespace JoJo.Service
{
    public class Sample
    {
        private DbContext dbContext;

        private Repo.UnitOfWork uow;

        public Sample(DbContext dbContext)
        {
            this.dbContext = dbContext;
            uow = new Repo.UnitOfWork(this.dbContext);
        }

        public void TestRelationship()
        {
            Users user = uow.UsersRepository.GetById(10);

            user.UserRole = uow.UserRoleRepository.GetById(4);

            uow.Save();
        }

        public IEnumerable<ProductSubCategory> subCategory(int catID)
        {
            IEnumerable<ProductSubCategory> subCat = uow.ProductSubCategoryRepository.GetAll().Where(x => x.CategoryId == catID).ToList();

            return subCat;
        }

        public List<ProductSubCategory> catcat(string ename)
        {
            var emp = uow.ProductSubCategoryRepository.GetAll().Where(x => x.SubCategoryName.StartsWith(ename)).ToList();

            return emp;
        }

        public void TestInsert()
        {
            UserRole u = new UserRole()
            {
                RoleDescription = "Dummy UserRole Insert"
            };

            uow.UserRoleRepository.Insert(u);

            uow.Save();
        }

        public void TestEdit()
        {
            uow.UserRoleRepository.GetById(4).RoleDescription = "Modified!!!!!";

            uow.Save();
        }

        public void TestDelete()
        {
            uow.UserRoleRepository.Remove(uow.UserRoleRepository.GetById(6));

            uow.Save();
        }
    }
}