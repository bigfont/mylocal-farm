

using farmApi.Models;
using Microsoft.Data.Entity;

namespace farmApi.DAL
{
    public class FarmContext : DbContext, IFarmContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoItem>().Key(e => e.Id);

            base.OnModelCreating(builder);
        }
    }
}
