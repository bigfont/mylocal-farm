using Microsoft.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using farmApi.Models;

namespace farmApi.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : FarmEntity
    {
        IEnumerable<TEntity> All();
        TEntity GetById(int id);
        void Add(TEntity item);
        bool TryDelete(int id);
    }
}
