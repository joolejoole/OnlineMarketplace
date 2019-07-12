using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JoJo.Repo
{
    public partial class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext dbContext;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual bool Remove(TEntity entity)
        {
            bool status = false;

            try
            {
                if (dbContext.Entry(entity).State == EntityState.Detached)
                {
                    dbContext.Set<TEntity>().Attach(entity);
                }

                dbContext.Set<TEntity>().Remove(entity);

                status = true;
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return status;
        }

        public virtual TEntity GetById(int id)
        {
            try
            {
                return dbContext.Set<TEntity>().Find(id);
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public virtual bool Insert(TEntity entity)
        {
            bool status = false;

            try
            {
                dbContext.Set<TEntity>().Add(entity);
                status = true;
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return status;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().ToList();
        }

        public void Dispose()
        {
            dbContext.Dispose();
            //throw new NotImplementedException();
        }
    }
}