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
            return dbContext.Set<TEntity>().Find(id);
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

        /*
        public int Edit(TEntity tentity)
        {
            int status = 0;
            try
            {
                dbContext.Entry(tentity).State = EntityState.Modified;
                status = 1;
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return status;
        }
        */

        public bool SaveChanges()
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

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}