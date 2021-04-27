using System;
using System.Collections.Generic;
using System.Text;

namespace DataMunging.Models
{
    public class Soccer
    {
        public string Team { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }

        public int Difference { get => Math.Abs(GoalsFor - GoalsAgainst); }

    }
}
