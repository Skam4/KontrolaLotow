using KontrolaLotow.Controllers;
using KontrolaLotow.Data;
using KontrolaLotow.Models;
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
    public class FlightsControllerTests
    {
        [Fact]
        public void Flights_ReturnsViewResult_WithListOfFlights()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotowContext(options))
            {
                var flights = new List<Flight>
                {
                    new Flight { IdLotu = 1, NumerLotu = "FL001", DataWylotu = DateTime.Now, MiejsceWylotu = "City A", MiejscePrzylotu = "City B", TypSamolotu = "Boeing 737" },
                    new Flight { IdLotu = 2, NumerLotu = "FL002", DataWylotu = DateTime.Now, MiejsceWylotu = "City C", MiejscePrzylotu = "City D", TypSamolotu = "Airbus A320" }
                };
                context.Flights.AddRange(flights);
                context.SaveChanges();

                var controller = new FlightsController(context);

                // Act
                var result = controller.Flights() as ViewResult;
                var model = result.ViewData.Model as List<Flight>;

                // Assert
                Assert.NotNull(result);
                Assert.Equal(flights.Count, model.Count);
            }
        }

        [Fact]
        public void DeleteFlight_ReturnsRedirectToActionResult_WhenFlightExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BazaLotowContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new BazaLotowContext(options))
            {
                var flight = new Flight { IdLotu = 1, NumerLotu = "FL001", DataWylotu = DateTime.Now, MiejsceWylotu = "City A", MiejscePrzylotu = "City B", TypSamolotu = "Boeing 737" };
                context.Flights.Add(flight);
                context.SaveChanges();

                var controller = new FlightsController(context);

                // Act
                var result = controller.DeleteFlight(1) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Flights", result.ActionName);
            }
        }
    }
}
