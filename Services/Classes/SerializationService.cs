using System.Collections.Generic;
using System.IO;
using LiveCharts.Wpf;
using Monefy.Model;
using Monefy.Services.Interfaces;
using Newtonsoft.Json;

namespace Monefy.Services.Classes
{
    internal class SerializationService : ISerializationService
    {
        private readonly string _fileName = "spendings.json";

        public List<SpandingModel> Deserialize()
        {
            if (File.Exists(_fileName))
            {
                using FileStream stream = new(_fileName, FileMode.Open);
                using StreamReader reader = new(stream);
                string json = reader.ReadToEnd();
                var res = JsonConvert.DeserializeObject<List<SpandingModel>>(json);
                return res;
            }
            else
            {
                return null;
            }
        }

        public void Serialize(List<SpandingModel> spendings)
        {
            using FileStream stream = new(_fileName, FileMode.OpenOrCreate);
            using StreamWriter writer = new(stream);
            string json = JsonConvert.SerializeObject(spendings, Formatting.Indented);
            writer.Write(json);
        } 
    }
}
    