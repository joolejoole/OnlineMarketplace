using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JoJo.Repo
{
    public partial interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        bool Insert(TEntity entity);

        bool Remove(TEntity entity);

        //int Edit(TEntity entity);

        bool SaveChanges();

        Task SaveChangesAsync();
    }
}