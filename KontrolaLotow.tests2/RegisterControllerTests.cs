using KontrolaLotow.Controllers;
using KontrolaLotow.Data;
using KontrolaLotow.Models;
using KontrolaLotow.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontrolaLotow.tests2
{
    public class RegisterControllerTests
    {
        [Fact]
        public void LoginVerification_ReturnsCorrectRedirectResult_WhenLoginAndPasswordAreValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotowContext(options))
            {
                var user = new User
                {
                    UserId = 1,
                    UserName = "testuser",
                    Login = "testuser",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("testpassword", 13),
                    UserRole = new Role { RoleId = 1, RoleName = "user" },
                    Email = "test@example.com"
                };

                context.Users.Add(user);
                context.SaveChanges();

                var controller = new RegisterController(context);

                // Act
                var result = controller.LoginVerification("testuser", "testpassword") as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);
                Assert.Equal("Home", result.ControllerName);
            }
        }

        [Fact]
        public void LoginVerification_ReturnsCorrectViewResult_WhenLoginIsInvalid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotowContext(options))
            {
                var user = new User
                {
                    UserId = 1,
                    UserName = "testuser",
                    Login = "testuser",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("testpassword", 13),
                    UserRole = new Role { RoleId = 1, RoleName = "user" },
                    Email = "test@example.com"
                };

                context.Users.Add(user);
                context.SaveChanges();

                var controller = new RegisterController(context);

                // Act
                var result = controller.LoginVerification("invaliduser", "testpassword") as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Login", result.ViewName);
            }
        }

        [Fact]
        public void LoginVerification_ReturnsCorrectViewResult_WhenPasswordIsInvalid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotowContext(options))
            {
                var user = new User
                {
                    UserId = 1,
                    UserName = "testuser",
                    Login = "testuser",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("testpassword", 13),
                    UserRole = new Role { RoleId = 1, RoleName = "user" },
                    Email = "test@example.com"
                };

                context.Users.Add(user);
                context.SaveChanges();

                var controller = new RegisterController(context);

                // Act
                var result = controller.LoginVerification("testuser", "invalidpassword") as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Login", result.ViewName);
            }
        }

        [Fact]
        public void RegisterVerification_AddsNewUser_WhenDataIsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotowContext(options))
            {
                var controller = new RegisterController(context);
                var model = new RegisterModel
                {
                    Username = "testuser",
                    Login = "testuser",
                    Email = "test@example.com",
                    Password = "testpassword"
                };

                // Act
                var result = controller.RegisterVerification(model) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Login", result.ActionName);
            }
        }

        [Fact]
        public void RegisterVerification_ReturnsViewResultWithModelError_WhenLoginIsAlreadyTaken()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotowContext(options))
            {
                var existingUser = new User
                {
                    UserId = 1,
                    UserName = "existinguser",
                    Login = "existinguser",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("existingpassword", 13),
                    UserRole = new Role { RoleId = 1, RoleName = "user" },
                    Email = "existing@example.com"
                };

                context.Users.Add(existingUser);
                context.SaveChanges();

                var controller = new RegisterController(context);
                var model = new RegisterModel
                {
                    Username = "testuser",
                    Login = "existinguser", // Login already exists
                    Email = "test@example.com",
                    Password = "testpassword"
                };

                // Act
                var result = controller.RegisterVerification(model) as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.True(result.ViewData.ModelState.ContainsKey("Login"));
            }
        }

        [Fact]
        public void RegisterVerification_ReturnsViewResultWithModelError_WhenUsernameIsAlreadyTaken()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotowContext(options))
            {
                var existingUser = new User
                {
                    UserId = 1,
                    UserName = "existinguser",
                    Login = "existinguser",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("existingpassword", 13),
                    UserRole = new Role { RoleId = 1, RoleName = "user" },
                    Email = "existing@example.com"
                };

                context.Users.Add(existingUser);
                context.SaveChanges();

                var controller = new RegisterController(context);
                var model = new RegisterModel
                {
                    Username = "existinguser", // Username already exists
                    Login = "testuser",
                    Email = "test@example.com",
                    Password = "testpassword"
                };

                // Act
                var result = controller.RegisterVerification(model) as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.True(result.ViewData.ModelState.ContainsKey("Username"));
            }
        }

        [Fact]
        public void RegisterVerification_ReturnsViewResultWithModelError_WhenEmailIsAlreadyTaken()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotowContext(options))
            {
                var existingUser = new User
                {
                    UserId = 1,
                    UserName = "existinguser",
                    Login = "existinguser",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("existingpassword", 13),
                    UserRole = new Role { RoleId = 1, RoleName = "user" },
                    Email = "existing@example.com"
                };

                context.Users.Add(existingUser);
                context.SaveChanges();

                var controller = new RegisterController(context);
                var model = new RegisterModel
                {
                    Username = "testuser",
                    Login = "testuser",
                    Email = "existing@example.com", // Email already exists
                    Password = "testpassword"
                };

                // Act
                var result = controller.RegisterVerification(model) as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.True(result.ViewData.ModelState.ContainsKey("Email"));
            }
        }
    }
}

