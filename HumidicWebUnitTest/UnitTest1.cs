using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading;
using System.Web;
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

        private List<Humidity> PostHumidities()
        {
            Humidity h = new Humidity(DateTime.Now, 85);
            IEnumerable<Humidity> newList = hc.Get().Append(h);

            List<Humidity> postList = newList.ToList();
            return postList;
        }

        private List<Humidity> DeleteHumidities()
        {
            Humidity h = new Humidity(DateTime.Now, 80);
            IEnumerable<Humidity> delete = hc.Get();

            List<Humidity> newList = delete.ToList();
           
           newList.Remove( h);

  
            return newList;

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
        public void PostMethodTest()
        {
            // Act

            var testProducts = GetHumidities();
          
             var post = PostHumidities();
                             
            Xunit.Assert.Equal(testProducts.Count, post.Count -1);
        }

        [Fact]
        public void DeleteMethodTest()
        {
            var testProducts = GetHumidities();
            var result = DeleteHumidities();

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

