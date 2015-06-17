// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;
using farmApi.Controllers;
using farmApi.Models;
using farmApi.DAL.Interfaces;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace farmApi.Test
{
    /// <summary>
    /// Tests the Account Controller
    /// </summary>
    /// <remarks>
    /// We are NOT also testing:
    /// 1. The data access layer. So, in this test class, we don't check whether user creation succeeded in the data store.
    /// 2. The emailer service. Neither do we test whether the emailer actually sent an email.
    /// </remarks>
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

        [Fact]
        public void EmailVerification_Returns200OnSuccess()
        {
        }

        [Fact]
        public void EmailVerification_Returns500OnFailure()
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
            var mockUserManager = new Mock<UserManager<User>>();

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
