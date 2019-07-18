using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoJo;
using JoJo.Core;
using JoJo.Service;

namespace JoJo.Service
{
    public class Services
    {
        private DbContext dbContext;

        private Repo.UnitOfWork uow;

        public Services(DbContext dbContext)
        {
            this.dbContext = dbContext;
            uow = new Repo.UnitOfWork(this.dbContext);
        }

        public bool isEmail(string email)
        {
            var v = uow.UsersRepository.GetAll().Where(a => a.Email == email).FirstOrDefault();
            return v != null;
        }

        public bool compareEmail(string email)
        {
            var v = uow.UsersRepository.GetAll().Where(a => a.Email == email).FirstOrDefault();
            return v != null;
        }

        public void saveUserReg(UserModel uu, string d, int v)
        {
            Users user = new Users();
            user.UserID = uu.UserID;
            user.UserName = uu.UserName;
            user.Email = uu.Email;
            user.Password = uu.ConfirmPassword;
            user.UserRoleId = v;
            user.UserPicture = d;

            uow.UsersRepository.Insert(user);
            uow.Save();
        }

        public void TestRelationship()
        {
            Users user = uow.UsersRepository.GetById(10);

            user.UserRole = uow.UserRoleRepository.GetById(4);

            uow.Save();
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