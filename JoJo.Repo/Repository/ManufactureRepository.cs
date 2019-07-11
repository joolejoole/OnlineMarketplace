using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoJo;

namespace JoJo.Repo
{
    public partial class ManufactureRepository : Repository<Core.Manufacture>, IManufactureRepository
    {
        public ManufactureRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}