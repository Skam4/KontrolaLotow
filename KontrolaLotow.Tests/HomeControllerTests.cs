using KontrolaLotow.Controllers;
using KontrolaLotow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NuGet;

namespace KontrolaLotow.Tests
{
    public class HomeControllerTests
    {
/*        [Fact]
        public void PreliminaryData_AddsAdminRole_WhenRoleDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotow>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotow(options))
            {
                var controller = new HomeController(null);
                controller.DataBase = context;

                // Act
                controller.PreliminaryData();

                // Assert
                var adminRole = context.Roles.FirstOrDefault(r => r.RoleName == "admin");
                Assert.NotNull(adminRole);
            }
        }*/

/*        [Fact]
        public void PreliminaryData_AddsAdminUser_WhenUserDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotow>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotow(options))
            {
                var controller = new HomeController(null);
                controller.DataBase = context;

                // Act
                controller.PreliminaryData();

                // Assert
                var adminUser = context.Users.FirstOrDefault(u => u.Login == "admin");
                Assert.NotNull(adminUser);
            }
        }*/
    }
}
