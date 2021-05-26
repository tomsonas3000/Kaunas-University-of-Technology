using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_2
{
    class Miestas
    {
        public const int MaksimalusAutomobiliuSkaicius = 20;
        public string Pavadinimas { get; set; }
        public string Adresas { get; set; }
        public string Telefonas { get; set; }
        public DateTime AutomobiliuAmziausVidurkis { get; set; }
        public AutoKonteineris Automobiliai { get; private set; }
        /// <summary>
        /// Klasės miestas konstruktorius
        /// </summary>
        /// <param name="pavadinimas"></param>
        /// <param name="adresas"></param>
        /// <param name="telefonas"></param>
        public Miestas(string pavadinimas, string adresas, string telefonas)
        {
            Pavadinimas = pavadinimas;
            Adresas = adresas;
            Telefonas = telefonas;
            Automobiliai = new AutoKonteineris(MaksimalusAutomobiliuSkaicius);
        }
    }
}
