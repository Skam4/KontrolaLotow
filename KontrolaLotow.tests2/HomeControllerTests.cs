using KontrolaLotow.Controllers;
using KontrolaLotow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NuGet;
using Moq;
using KontrolaLotow.Models;

namespace KontrolaLotow.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void PreliminaryData_AddsAdminRole_WhenRoleDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (var context = new BazaLotowContext(options))
            {
                context.Roles.Add(new Role { RoleName = "user" });
                context.SaveChanges();

                var controller = new HomeController(Mock.Of<ILogger<HomeController>>(), context);

                // Act
                controller.PreliminaryData();

                // Assert
                var adminRole = context.Roles.FirstOrDefault(r => r.RoleName == "admin");
                Assert.NotNull(adminRole);
            }
        }

        [Fact]
        public void PreliminaryData_AddsUserRole_WhenRoleDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (var context = new BazaLotowContext(options))
            {
                var controller = new HomeController(Mock.Of<ILogger<HomeController>>(), context);

                // Act
                controller.PreliminaryData();

                // Assert
                var userRole = context.Roles.FirstOrDefault(r => r.RoleName == "user");
                Assert.NotNull(userRole);
            }
        }

        [Fact]
        public void PreliminaryData_AddsAdminAccount_WhenRoleDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (var context = new BazaLotowContext(options))
            {
                var controller = new HomeController(Mock.Of<ILogger<HomeController>>(), context);

                // Act
                controller.PreliminaryData();

                // Assert
                var adminAccount = context.Users.FirstOrDefault(u => u.UserName == "admin");
                Assert.NotNull(adminAccount);
            }
        }
    }
}
