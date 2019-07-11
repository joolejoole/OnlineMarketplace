using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoJo;

namespace JoJo.Repo
{
    public partial class SeriesRepository : Repository<Core.Series>, ISeriesRepository
    {
        public SeriesRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}