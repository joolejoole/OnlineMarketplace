using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoJo;

namespace JoJo.Repo
{
    public partial class ModelRepository : Repository<Core.Model>, IModelRepository
    {
        public ModelRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}