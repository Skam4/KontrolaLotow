using KontrolaLotow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KontrolaLotow.Data;

namespace KontrolaLotow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public BazaLotowContext DataBase = new BazaLotowContext();
        public HomeController(ILogger<HomeController> logger, BazaLotowContext dataBase)
        {
            _logger = logger;
            DataBase = dataBase;
        }

        /*        public HomeController(ILogger<HomeController> logger)
                {
                    _logger = logger;
                }*/

        public IActionResult Index()
        {
            PreliminaryData();

            bool ifLogged = Request.Cookies.ContainsKey("JWTToken");

            ViewBag.IfLogged = ifLogged;

            return RedirectToAction("Flights", "Flights");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void PreliminaryData()
        {
            //if admin role does not exists, create admin role
            if (DataBase.Roles.FirstOrDefault(v => v.RoleName == "admin") == null)
            {
                Role newRole = new Role();
                newRole.RoleName = "admin";
                DataBase.Add(newRole);
                DataBase.SaveChanges();
            }

            //if admin does not exists, create admin
            if (DataBase.Users.FirstOrDefault(v => v.Login == "admin") == null)
            {
                User adminUser = new User();
                adminUser.UserName = "admin";
                adminUser.Login = "admin";
                adminUser.Email = "admin@admin.pl";
                adminUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword("admin");
                adminUser.UserRole = DataBase.Roles.FirstOrDefault(v => v.RoleName == "admin");
                DataBase.Add(adminUser);
                DataBase.SaveChanges();
            }

            //if user does not exists, create user role
            if (DataBase.Roles.FirstOrDefault(v => v.RoleName == "user") == null)
            {
                Role newRole = new Role();
                newRole.RoleName = "user";
                DataBase.Add(newRole);
                DataBase.SaveChanges();
            }
        }
    }
}
