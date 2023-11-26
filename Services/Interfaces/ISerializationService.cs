using System.Collections.Generic;
using LiveCharts.Wpf;
using Monefy.Model;

namespace Monefy.Services.Interfaces
{
    internal interface ISerializationService
    {
        public List<SpandingModel> Deserialize();
        public void Serialize(List<SpandingModel> spendings);
    }
}
