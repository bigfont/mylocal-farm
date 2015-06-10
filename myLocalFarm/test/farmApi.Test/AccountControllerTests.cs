using Xunit;
using farmApi.Controllers;
using farmApi.Models;
using farmApi.DAL.Interfaces;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace farmApi.Test
{
    public class AccountControllerTests
    {
        public AccountControllerTests()
        {

        }

        [Fact]
        public async void Register_Returns200OnSuccess()
        {
            var controller = GetController();
            var result = await controller.Register(new RegisterViewModel { Email = "", Password = "", ConfirmPassword = "" });
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Register_Returns500OnFailure()
        {
            var controller = GetController();
            var result = await controller.Register(new RegisterViewModel());
            Assert.Equal(500, result.StatusCode);
        }


        [Theory]
        [InlineData("shaun@bigfont.ca", "test123", "test123")]
        public void PostRegister_CreatesANewUnconfirmedUser(string email, string password, string confirm)
        {

        }

        [Fact]
        public void EmailVerification_ConfirmsANewUser()
        {

        }

        [Fact]
        public void Login_MakesUserAuthenticated()
        {

        }

        [Fact]
        public void Logout_MakesUserAnonymous()
        {

        }

        private AccountController GetController()
        {
            // User Manager
            var mockUserManager = new Mock<IUserManager<User>>();

            // CreateAsync
            mockUserManager
                .Setup(x => x.CreateAsync(It.IsNotNull<string>(), It.IsNotNull<string>()))
                .Returns(Task<IdentityResult>.Run(async () =>
                {
                    await Task.Delay(1);
                    return IdentityResult.Success;
                }));

            mockUserManager
                .Setup(x => x.CreateAsync(It.Is<string>(s => s == null), It.Is<string>(s => s == null)))
                .Returns(Task<IdentityResult>.Run(async () =>
                {
                    await Task.Delay(1);
                    return IdentityResult.Failed(null);
                }));

            // Unit Of Work
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // User Manager
            mockUnitOfWork
                .Setup(x => x.UserManager)
                .Returns(mockUserManager.Object);

            // Controller
            var controller = new AccountController(mockUnitOfWork.Object);
            return controller;
        }
    }
}
