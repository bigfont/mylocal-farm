using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using farmApi.Models;
using farmApi.DAL.Interfaces;
using System.Threading.Tasks;

namespace farmApi.Controllers
{
    public class AccountController : Controller
    {
        private IUnitOfWork unitOfWork;

        public AccountController([FromServices] IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<HttpStatusCodeResult> Register(RegisterViewModel vm)
        {
            var result = await unitOfWork.UserManager.CreateAsync(vm.Email, vm.Password);
            if (!result.Succeeded)
            {
                return new HttpStatusCodeResult(500);
            }
            return new HttpStatusCodeResult(200);
        }
    }
}
