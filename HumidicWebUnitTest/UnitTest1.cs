using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading;
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
        public void GetAllHumidity()
        {
            // Act
            var testProducts = GetHumidities();
            var controller = new HumidityController();

            var result = controller.Get() as List<Humidity>;
            Assert.NotNull(result);
            // Assert
            Assert.Equal(testProducts.Count, result.Count);
        }

        //[Fact]
        //public void PostTest()
        //{
        //    // Act
        //    Humidity h = new Humidity(DateTime.Now, 80);
        //    var testProducts = GetHumidities();
        //    var controller = new HumidityController();

        //    var result = controller.Post(h) as ;
        //    //Assert.NotNull(result);
        //    // Assert
        //    Assert.Equal(testProducts.Count, result.Count + 1);
        //}


    }
    }

