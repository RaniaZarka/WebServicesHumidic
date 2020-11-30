using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHumidic.Model;

namespace WebApiHumidic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumidityController : ControllerBase
    {
        public const string conn = "Server=tcp:humidity.database.windows.net,1433;Initial Catalog=Humidity;Persist Security Info=False;User ID=team;Password=Humidity!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //public const string conn = "Data Source=humidity.database.windows.net;Initial Catalog=Humidity;User ID=team;Password=Humidity!;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: api/Humidity
        [HttpGet]
        public IEnumerable<Humidity> Get()
        {
            var humidityList = new List<Humidity>();

            string selectall = "select * from HumidityLevel";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectall, databaseConnection))
                {
                    databaseConnection.Open();

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime date = reader.GetDateTime(0);
                            int level = reader.GetInt32(1);

                            humidityList.Add(new Humidity(date, level));

                        }
                    }
                }
            }
            return humidityList;


        }
        //// GET: api/Humidity
        //[HttpGet(("byDate/{date}"), Name = "GetByDay")]
        //public IEnumerable<Humidity> Get1Day()
        //{
        //    var humidityList = new List<Humidity>();

        //    string selectall = "select * from HumidityLevel WHERE date < NOW() - INTERVAL 1 DAY";

        //    using (SqlConnection databaseConnection = new SqlConnection(conn))
        //    {
        //        using (SqlCommand selectCommand = new SqlCommand(selectall, databaseConnection))
        //        {
        //            databaseConnection.Open();

        //            using (SqlDataReader reader = selectCommand.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    DateTime date = reader.GetDateTime(0);
        //                    int level = reader.GetInt32(1);

        //                    humidityList.Add(new Humidity(date, level));

        //                }
        //            }
        //        }
        //    }
        //    return humidityList;
        //    //SELECT* from Results WHERE date < NOW() - INTERVAL 30 DAY;

        //}
        // GET: api/Humidity/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Humidity
        [HttpPost]
        public void Post([FromBody] Humidity value)
        {
            string insertHumiditySql = "insert into HumidityLevel (level, date) values (@level, @date)";
            using (SqlConnection databaseconnection = new SqlConnection(conn))
            {
                databaseconnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertHumiditySql, databaseconnection))
                {
                    insertCommand.Parameters.AddWithValue("@date", value.Date);
                    insertCommand.Parameters.AddWithValue("@level", value.Level);
                    
                    int rowaffected = insertCommand.ExecuteNonQuery();
                    Console.WriteLine($"rows affected: {rowaffected}");
                }

            }

        }

        // PUT: api/Humidity/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(DateTime date)
        {
            //DELETE FROM Table_name
            // WHERE Date_column<GETDATE() -7
            //...or this:
            //DELETE FROM Table_name
            //WHERE Date_column < DATEADD(dd, -7, GETDATE())

            string deleteHumidity = "delete from humidityLevel where date = @date";
            using (SqlConnection databaseConnection = new SqlConnection())
            {
                databaseConnection.Open();
                using (SqlCommand deleteCommand = new SqlCommand(deleteHumidity, databaseConnection))
                {
                    deleteCommand.Parameters.AddWithValue("@date", date);
                    int rowAffected = deleteCommand.ExecuteNonQuery();
                }
            }

        }
    }
}
