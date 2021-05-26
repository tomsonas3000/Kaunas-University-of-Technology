using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_2
{
    class AutoKonteineris
    {
        private Automobilis[] Automobiliai;
        public int Skaicius { get; private set; }
        /// <summary>
        /// Nurodo konteinerio ilgį
        /// </summary>
        /// <param name="ilgis">konteinerio ilgis</param>
        public AutoKonteineris(int ilgis)
        {
            Automobiliai = new Automobilis[ilgis];
            Skaicius = 0;
        }
        /// <summary>
        /// Prideda automobilio objektą į nurodytą vietą konteineryje
        /// </summary>
        /// <param name="automobilis"></param>
        /// <param name="indeksas">vietą, į kurią bus 
        /// įdėtas automobilio objektas</param>
        public void PridetiAuto (Automobilis automobilis, int indeksas)
        {
            Automobiliai[indeksas] = automobilis;
        }
        /// <summary>
        /// Prideda automobilio objektą į konteinerį
        /// </summary>
        /// <param name="automobolis"></param>
        public void PridetiAuto (Automobilis automobolis)
        {
            Automobiliai[Skaicius++] = automobolis;
        }
        /// <summary>
        /// Paima automobilio objektą iš konteinerio
        /// </summary>
        /// <param name="indeksas">nurodo</param>
        /// <returns></returns>
        public Automobilis PaimtiAuto (int indeksas)
        {
            return Automobiliai[indeksas];
        }

    }
}
