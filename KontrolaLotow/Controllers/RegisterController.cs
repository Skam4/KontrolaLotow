using Microsoft.AspNetCore.Mvc;
using KontrolaLotow.Models;
using System.Diagnostics;
using KontrolaLotow.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using KontrolaLotow.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace KontrolaLotow.Controllers
{
    public class RegisterController : Controller
    {

        BazaLotow DataBase = new BazaLotow();

        private readonly IConfiguration _configuration;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Go to register site
        public IActionResult Register()
        {
            RegisterModel model = new RegisterModel();
            return View(model);
        }

        //Go to login site
        public IActionResult Login()
        {
            return View();
        }

        //Redirect to login site
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JWTToken");

            return RedirectToAction("Login");
        }

        //Log in button was clicked
        public IActionResult LoginVerification(string login, string password)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            var existingUser = DataBase.Users.Include(u => u.UserRole).FirstOrDefault(v => v.Login == login);

            if (existingUser == null)
            {
                ModelState.AddModelError("Login", "Niepoprawny login.");
                return View("Login");
            }

            var newPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);

            if (BCrypt.Net.BCrypt.EnhancedVerify(password, existingUser.Password) != true)
            {
                ModelState.AddModelError("Password", "Niepoprawne hasło.");
                return View("Login");
            }
            
            //Generate JWT Token
            var token = GenerateToken(existingUser);

            //Saving token in cookie
            Response.Cookies.Append("JWTToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(10)
            });

            //Console.Write("token: " + token);

            return RedirectToAction("Index", "Home");
        }

        //Register button was clicked
        public IActionResult RegisterVerification(RegisterModel Model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", Model);
            }

            var existingUsername = DataBase.Users.FirstOrDefault(v => v.UserName == Model.Username);
            var existingLogin = DataBase.Users.FirstOrDefault(v => v.Login == Model.Login);
            var existingEmail = DataBase.Users.FirstOrDefault(v => v.Email == Model.Email);

            if (existingLogin != null)
            {
                ModelState.AddModelError("Login", "Ten login już istnieje.");
            }

            if (existingUsername != null)
            {
                ModelState.AddModelError("Username", "Ta nazwa użytkownika już istnieje.");
            }

            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "Ten email już istnieje w bazie.");
            }

            //Creating new user
            User newUser = new User();

            newUser.UserName = Model.Username;
            newUser.Login = Model.Login;
            newUser.Email = Model.Email;
            newUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(Model.Password, 13);

            var userRole = DataBase.Roles.FirstOrDefault(v => v.RoleName == "user");

            if (userRole != null)
            {
                newUser.UserRole = userRole;
            }

            DataBase.Add(newUser);
            DataBase.SaveChanges();

            return RedirectToAction("Login");
        }


        public string GenerateToken(User user)
        {

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];

            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.RoleName)
            };

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                issuer: issuer,
                audience: audience,
                signingCredentials: signingCredentials);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

    }
}
