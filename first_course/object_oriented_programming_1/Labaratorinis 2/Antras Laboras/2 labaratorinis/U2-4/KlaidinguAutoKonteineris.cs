using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_2
{
    class KlaidinguAutoKonteineris
    {
        private KlaidingasAutomobilis[] KlaidingiAutomobiliai;
        public int Skaicius { get; private set; }
        /// <summary>
        /// Konstruktorius
        /// </summary>
        /// <param name="ilgis">Objektų rinkinio dydis</param>
        public KlaidinguAutoKonteineris(int ilgis)
        {
            KlaidingiAutomobiliai = new KlaidingasAutomobilis[ilgis];
            Skaicius = 0;
        }
        /// <summary>
        /// Metodas, į konteinerio konkrečią vietą,
        /// pridedantis automobilio duomenis
        /// </summary>
        /// <param name="automobilis">Automobilis objektas</param>
        /// <param name="indeksas"></param>
        public void PridetiAuto(KlaidingasAutomobilis automobilis, int indeksas)
        {
            KlaidingiAutomobiliai[indeksas] = automobilis;
        }
        /// <summary>
        /// Metodas, pridedantis automobilio duomenis į konteinerį
        /// </summary>
        /// <param name="automobilis">Automobilio objektas</param>
        public void PridetiAuto (KlaidingasAutomobilis automobilis)
        {
            KlaidingiAutomobiliai[Skaicius++] = automobilis;
        }
        /// <summary>
        /// Metodas, paimantis duomenis apie automobilį
        /// </summary>
        /// <param name="indeksas"></param>
        /// <returns>Grąžina Automobilis klasės objektą</returns>
        public KlaidingasAutomobilis PaimtiAuto (int indeksas)
        {
            return KlaidingiAutomobiliai[indeksas];
        }
        /// <summary>
        /// Metodas, patikrinantis, ar objektas jau yra konteineryje
        /// </summary>
        /// <param name="automobilis"></param>
        /// <returns>True arba false</returns>
        public bool Turi (KlaidingasAutomobilis automobilis)
        {
            for (int i = 0; i < Skaicius; i++)
            {
                if (KlaidingiAutomobiliai[i].ValstybiniaiNumeriai == automobilis.ValstybiniaiNumeriai && KlaidingiAutomobiliai[i].Modelis == automobilis.Modelis)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
