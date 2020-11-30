using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiHumidic.Model
{
    public class Humidity
    {
        public int Level { get; set; }
        public DateTime Date { get; set; }

        public Humidity()
        {

        }

        public Humidity(DateTime date, int level)
        {

            Level = level;
            Date = date;

        }
    }
}


