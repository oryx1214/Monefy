using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monefy.Model
{
    class SpandingModel : IData
    {
        public string Categorie { get; set; }
        public int Value { get; set; }
        public Brush Color { get; set; }
        public SpandingModel()
        {
            Value = 0;
        }
        public SpandingModel(int value, string categorie, Brush color)
        {
            Value = value;
            Categorie = categorie;
            Color = color;
        }
    }
}
