using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoJo;

namespace JoJo.Repo
{
    public partial class UsersRepository : Repository<Core.User>, IUsersRepository
    {
        public UsersRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}