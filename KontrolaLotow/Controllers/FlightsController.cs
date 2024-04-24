using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KontrolaLotow.Data;
using KontrolaLotow.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KontrolaLotow.Controllers
{
    public class FlightsController : Controller
    {

        BazaLotowContext DataBase = new BazaLotowContext();

        public FlightsController(BazaLotowContext dataBase)
        {
            DataBase = dataBase;
        }

        public IActionResult Flights()
        {
            var allFlights = DataBase.Flights.ToList();

            var userRole = HttpContext.Request.Cookies["JWTToken"] != null ? GetRoleFromToken(HttpContext.Request.Cookies["JWTToken"]) : null;

            //Console.WriteLine("useRole: " + userRole);

            ViewBag.UserRole = userRole;

            bool ifLogged = Request.Cookies.ContainsKey("JWTToken");

            ViewBag.IfLogged = ifLogged;

            return View(allFlights);
        }

        public IActionResult DeleteFlight(int id) 
        {
            var flight = DataBase.Flights.FirstOrDefault(f => f.IdLotu == id);
            if (flight != null)
            {
                DataBase.Flights.Remove(flight);
                DataBase.SaveChanges();
            }
            return RedirectToAction("Flights");
        }

        public IActionResult CreateFlight(Flight flight)
        {
            Flight newFlight = new Flight();

            newFlight.NumerLotu = flight.NumerLotu;
            newFlight.DataWylotu = flight.DataWylotu;
            newFlight.MiejsceWylotu = flight.MiejsceWylotu;
            newFlight.MiejscePrzylotu = flight.MiejscePrzylotu;
            newFlight.TypSamolotu = flight.TypSamolotu;

            DataBase.Add(newFlight);
            DataBase.SaveChanges();

            return RedirectToAction("Flights");
        }

        public IActionResult EditFlight(int id)
        {
            var flight = DataBase.Flights.FirstOrDefault(f => f.IdLotu == id);
            if (flight == null)
            {
                return RedirectToAction("Flights");
            }

            return View(flight);
        }

        public IActionResult ChangeFlight(Flight flight)
        {
            if (ModelState.IsValid)
            {
                DataBase.Flights.Update(flight);
                DataBase.SaveChanges();
            }
            return RedirectToAction("Flights");
        }

        public IActionResult Details(int id)
        {
            var flight = DataBase.Flights.FirstOrDefault(f => f.IdLotu == id);
            if (flight == null)
            {
                return RedirectToAction("Flights");
            }

            bool ifLogged = Request.Cookies.ContainsKey("JWTToken");

            ViewBag.IfLogged = ifLogged;

            if(ifLogged == false) 
            {
                return RedirectToAction("Login", "Register");
            }

            return View(flight);
        }


        private string GetRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            return roleClaim?.Value;
        }

    }

}
