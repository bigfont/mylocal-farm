using System.Threading.Tasks;
using farmApi.Models;
using Microsoft.AspNet.Identity;

namespace farmApi.DAL.Interfaces
{
    public interface IUserManager<TEntity> where TEntity : FarmEntity
    {
        Task<IdentityResult> CreateAsync(string username, string password);
    }
}
