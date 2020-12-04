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
        // GET: api/Humidity
        [HttpGet(("byDate/{date}"), Name = "GetByToday")]
        public IEnumerable<Humidity> Get1Day()
        {
            var humidityList = new List<Humidity>();

            string selectall = "SELECT * FROM HumidityLevel WHERE Date >= DATEADD(day, -1, GETDATE())";

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
            //SELECT* from Results WHERE date < NOW() - INTERVAL 30 DAY;

        }

       

        // GET: api/Humidity
        [HttpGet(("byDate/{date}"), Name = "GetBy3days")]
        public IEnumerable<Humidity> Get3Days()
        {
            var humidityList = new List<Humidity>();

            string selectall = "SELECT * FROM HumidityLevel WHERE Date >= DATEADD(day, -3, GETDATE())";

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

        // GET: api/Humidity
        [HttpGet(("byDate/{date}"), Name = "GetBy7days")]
        public IEnumerable<Humidity> Get7Day()
        {
            var humidityList = new List<Humidity>();

            string selectall = "SELECT * FROM HumidityLevel WHERE Date >= DATEADD(day, -7, GETDATE())";

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


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{date}")]
        public void Delete(DateTime date)
        {
         
           string deleteHumidity = "DELETE FROM HumidityLevel WHERE Date < DATEADD(dd, -7, GETDATE())";
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
