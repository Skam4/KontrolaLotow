using KontrolaLotow;
using KontrolaLotow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KontrolaLotow.Controllers;
using KontrolaLotow.Data;
using System;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateFlight()
        {
            var flight = new Flight
            {
                NumerLotu = 123,
                DataWylotu = DateTime.Now,
                MiejsceWylotu = "TestAirport1",
                MiejscePrzylotu = "TestAirport2",
                TypSamolotu = "TestPlane"
            };

            

            //Assert.AreEqual("Flights", );
        }
    }
}