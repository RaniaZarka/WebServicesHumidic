using System;
using System.Collections.Generic;
using System.Linq;
using WebApiHumidic.Controllers;
using WebApiHumidic.Model;
using Xunit;

namespace HumidicWebUnitTest
{
    public class UnitTest1
    {
        HumidityController hc = new HumidityController();


        private List<Humidity> GetHumidities()
        {
            IEnumerable<Humidity> theList = hc.Get(); 
           List<Humidity> HumidicList = theList.ToList();

            return HumidicList;
        }

     


        [Fact]
        public void GetAllMethodTest()
        {
            // Act
            var testProducts = GetHumidities();
            
            var result = hc.Get() as List<Humidity>;

            // Assert
            Xunit.Assert.NotNull(result);
          
            Xunit.Assert.Equal(testProducts.Count, result.Count);
        }

       

        [Fact]
        public void TestPropertyLevel()
        {
            //Arrange
            Humidity h = new Humidity();

            int Level = 50;

            //Act

            h.Level = Level;

            // Assert

            Xunit.Assert.Equal(h.Level, Level);

        }


        [Fact]
        public void TestPropertyDate()
        {
            //Arrange
            Humidity h = new Humidity();

            DateTime date = new DateTime(29 / 11 / 2020 / 10 / 15);

            //Act

            h.Date = date;

            // Assert

            Xunit.Assert.Equal(h.Date, date);

        }



    }
}

