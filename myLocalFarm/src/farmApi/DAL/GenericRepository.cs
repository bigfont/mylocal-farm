

using Microsoft.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using farmApi.Models;
using farmApi.DAL.Interfaces;

namespace farmApi.DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : FarmEntity
    {
        internal IFarmContext context;
        internal DbSet<TEntity> dbSet;

        // TODO Consider using an interface with dependency injection
        public GenericRepository(IFarmContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> All()
        {
            return dbSet.Select(x => x);
        }

        public TEntity GetById(int id)
        {
            return dbSet.FirstOrDefault(x => x.Id == id);
        }

        public void Add(TEntity item)
        {
            item.Id = 1 + dbSet.Max(x => (int?)x.Id) ?? 0;
            dbSet.Add(item);
        }

        public bool TryDelete(int id)
        {
            var item = GetById(id);
            if (item == null)
            {
                return false;
            }
            dbSet.Remove(item);
            return true;
        }
    }
}
