using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DataMunging.Data
{
    public class DataReader
    {
        private readonly string _filePath;

        public DataReader(string filePath)
        {
            _filePath = filePath;
        }

        public ICollection<T> ReadData<T>(Dictionary<string, FixedWithStruct> fileFormat, bool header = true, char skipChar = '\0') 
        {
            var result = new List<T>();

            if (!File.Exists(_filePath)) throw new FileNotFoundException();

            using(var reader = new StreamReader(_filePath))
            {
                string data = reader.ReadLine();
                if (header)
                    data = reader.ReadLine();

                var iType = typeof(T);

                while (!reader.EndOfStream)
                {
                    if(!data.SkipLine(skipChar))
                    {
                        var item = Activator.CreateInstance<T>();
                        foreach (var key in fileFormat.Keys) {
                            var props = iType.GetProperties();

                            if (props.Any(p => p.Name == key)){
                                var fw = fileFormat[key];
                                var val = data.Substring(fw.StartPoint, fw.Length).Trim();
                                PropertyInfo prop = props.First(p => p.Name == key);
                                if (val.CanConvert(prop.PropertyType))
                                {
                                    prop.SetValue(item, Convert.ChangeType(val, prop.PropertyType));
                                }
                            }
                        }
                        result.Add(item);
                    }
                    data = reader.ReadLine();
                }
            }

            return result;

        }
    }

    public static class DataReaderExtension
    {
        public static bool CanConvert(this object value, Type type)
        {
            try
            {
                var val = Convert.ChangeType(value, type);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static bool SkipLine(this string data, char skip)
        {
            if (string.IsNullOrWhiteSpace(data))
                return true;
            if(skip != '\0')
            {
                string trimData = data.Trim();
                if (trimData.Equals(new string(skip, trimData.Length)))
                    return true;
            }
            return false;
        }
    }

}
