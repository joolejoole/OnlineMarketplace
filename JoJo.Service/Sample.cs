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