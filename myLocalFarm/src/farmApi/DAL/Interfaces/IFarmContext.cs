

using farmApi.Models;
using Microsoft.Data.Entity;

namespace farmApi.DAL
{
    public interface IFarmContext
    {
        DbSet<TodoItem> TodoItems { get; set; }
    }
}
