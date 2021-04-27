using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataMunging.Data;
using DataMunging.Models;

namespace DataMunging
{
    public class Program
    {
        static void Main(string[] args)
        {
            GetWeather();
            GetSoccer();
        }

        static void GetSoccer()
        {
            var soccerList = LoadSoccer();
            Soccer lowest = null;
            soccerList.ForEach(s => {
                if (lowest == null || s.Difference < lowest.Difference)
                    lowest = s;
            });
            Console.WriteLine($"Team: {lowest.Team}, Difference: {lowest.Difference}");
        }

        static void GetWeather()
        {
            var weatherList = LoadWather();
            Weather lowest = null;
            weatherList.ForEach(w => {
                if (lowest == null || w.TempSpread < lowest.TempSpread)
                    lowest = w;
            });

            Console.WriteLine($"Day: {lowest.Day}, Temperatue Spread: {lowest.TempSpread}");
        }

        static List<Soccer> LoadSoccer()
        {
            string path = @"D:\Development\DataMunging\DataMunging\Data\football.dat";
            var dr = new DataReader(path);
            return dr.ReadData<Soccer>(GetSoccerStruct(),true, '-').ToList();
        }

        static List<Weather> LoadWather()
        {
            string path = @"D:\Development\DataMunging\DataMunging\Data\weather.dat";
            var dr = new DataReader(path);
            return dr.ReadData<Weather>(GetWeatherStruct()).ToList();
        }

        private static Dictionary<string,FixedWithStruct> GetWeatherStruct()
        {
            var dic = new Dictionary<string, FixedWithStruct>();
            dic.Add("Day", new FixedWithStruct { StartPoint = 0, Length = 4 });
            dic.Add("MaxTemp", new FixedWithStruct { StartPoint = 4, Length = 4 });
            dic.Add("MinTemp", new FixedWithStruct { StartPoint = 10, Length = 4 });
            dic.Add("AvgTemp", new FixedWithStruct { StartPoint = 16, Length = 5 });

            return dic;
        }

        private static Dictionary<string, FixedWithStruct> GetSoccerStruct()
        {
            var dic = new Dictionary<string, FixedWithStruct>();
            dic.Add("Team", new FixedWithStruct { StartPoint = 7, Length = 14 });
            dic.Add("Wins", new FixedWithStruct { StartPoint = 29, Length = 3 });
            dic.Add("Losses", new FixedWithStruct { StartPoint = 33, Length = 3 });
            dic.Add("GoalsFor", new FixedWithStruct { StartPoint = 43, Length = 3 });
            dic.Add("GoalsAgainst", new FixedWithStruct { StartPoint = 50, Length = 3 });

            return dic;
        }
    }
}
