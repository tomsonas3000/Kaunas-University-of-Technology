using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_2
{
    class KlaidingasAutomobilis
    {
        public string ValstybiniaiNumeriai { get; set; }
        public string Modelis { get; set; }
        public List<string> Filialai { get; set; }
        /// <summary>
        /// KlaidingasAutomobolis konstruktorius 
        /// </summary>
        /// <param name="valstybiniaiNumeriai"></param>
        /// <param name="modelis"></param>
        public KlaidingasAutomobilis(string valstybiniaiNumeriai,
            string modelis)
        {
            ValstybiniaiNumeriai = valstybiniaiNumeriai;
            Modelis = modelis;
            Filialai = new List<string>();
        }
    }
}
