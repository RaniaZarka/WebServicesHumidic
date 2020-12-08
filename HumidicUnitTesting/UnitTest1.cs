using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiHumidic.Controllers;
using WebApiHumidic.Model;

namespace HumidicUnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        

        HumidityController hc = new HumidityController();
        private List<Humidity> GetHumidities()
        {
            IEnumerable<Humidity> theList = hc.Get();
            List<Humidity> HumidicList = theList.ToList();

            return HumidicList;
        }




        [TestMethod]
        public void GetAllMethodTest()
        {
            // Arrange
            
            var testProducts = GetHumidities();

            //Act
            var result = hc.Get() as List<Humidity>;
          
            //Assert
            Assert.AreEqual(testProducts.Count, result.Count);
        }



        [TestMethod]
        public void TestPropertyLevel()
        {
            //Arrange
            Humidity h = new Humidity();

            int Level = 50;

            //Act

            h.Level = Level;

            // Assert

            Assert.AreEqual(h.Level, Level);

        }


        [TestMethod]
        public void TestPropertyDate()
        {
            //Arrange
            Humidity h = new Humidity();

            DateTime date = new DateTime(29 / 11 / 2020 / 10 / 15);

            //Act

            h.Date = date;

            // Assert

            Assert.AreEqual(h.Date, date);

        }
    }
}
