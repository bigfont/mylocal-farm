

using farmApi.Models;
using Microsoft.Data.Entity;

namespace farmApi.DAL.Interfaces
{
    public interface IFarmContext
    {
        DbSet<TodoItem> TodoItems { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
