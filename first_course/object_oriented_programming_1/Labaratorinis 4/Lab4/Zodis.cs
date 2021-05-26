using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Zodis
    {
        public string Pavadinimas { get; set; }
        public int Kiekis1 { get; set; }
        public int Kiekis2 { get; set; }
        public string Simboliai { get; set; }
        public int EilutesNumeris { get; set; }
        public int PozicijaEiluteje { get; set; }
        public int[] EilutesPabaigosNumeris { get; set; }
        /// <summary>
        /// Konstruktorius be parametrų
        /// </summary>
        public Zodis()
        {

        }
        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="pavadinimas"></param>
        public Zodis(string pavadinimas)
        {
            Pavadinimas = pavadinimas;
            Kiekis1 = 1;
            Kiekis2 = 1;
        }
        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="pavadinimas"></param>
        /// <param name="simboliai"></param>
        /// <param name="eilutesNumeris"></param>
        /// <param name="pozicijaEiluteje"></param>
        public Zodis(string pavadinimas, string simboliai, int eilutesNumeris,
            int pozicijaEiluteje)
        {
            Pavadinimas = pavadinimas;
            Simboliai = simboliai;
            EilutesNumeris = eilutesNumeris;
            PozicijaEiluteje = pozicijaEiluteje;
        }
    }
}
