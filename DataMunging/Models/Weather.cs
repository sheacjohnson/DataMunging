using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunging.Models
{
    public class Weather
    {
        public int Day { get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
        public int AvgTemp { get; set; }

        public int? TempSpread { get => MaxTemp - MinTemp; }
    }
}
