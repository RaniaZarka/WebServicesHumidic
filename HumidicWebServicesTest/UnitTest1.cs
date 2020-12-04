using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiHumidic.Controllers;
using WebApiHumidic.Model;

namespace HumidicWebServicesTest
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
        //[TestMethod]
        //public void TestPost()
        //{
        //    var testProducts = GetHumidities();
        //    var controller = new HumidityController();

        //    Humidity h = new Humidity
        //    {
        //        Date = DateTime.Now,
        //        Level = 80
        //    };

        //    var result = hc.Post(h) ;
        //    var data = result.Value as Humidity;
        //   //Assert.NotNull(result);
        //    // Assert
        //   Assert.AreEqual(testProducts.Count, result.Count + 1);
        // }


        // }
        [TestMethod]
        public void PostMethodTest()
        {
            var h = new Humidity(DateTime.Now, 70);
             hc.Post(h);


        }

    }
}
