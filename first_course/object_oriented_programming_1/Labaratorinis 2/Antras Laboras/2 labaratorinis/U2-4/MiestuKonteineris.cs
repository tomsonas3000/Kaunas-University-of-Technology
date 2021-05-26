using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_2
{
    class MiestuKonteineris
    {
        private Miestas[] Miestai;
        public int Skaicius { get; private set; }
        /// <summary>
        /// Nurodo konteinerio ilgį
        /// </summary>
        /// <param name="ilgis">konteinerio ilgis</param>
        public MiestuKonteineris(int ilgis)
        {
            Miestai = new Miestas[ilgis];
            Skaicius = 0;
        }
        /// <summary>
        /// Prideda miesto objektą į nurodytą vietą konteineryje
        /// </summary>
        /// <param name="miestas"></param>
        /// <param name="indeksas"></param>
        public void PridetiMiesta (Miestas miestas, int indeksas)
        {
            Miestai[indeksas] = miestas;
        }
        /// <summary>
        /// Prideda miestą
        /// </summary>
        /// <param name="miestas"></param>
        public void PridetiMiesta(Miestas miestas)
        {
            Miestai[Skaicius++] = miestas;
        }
        /// <summary>
        /// Paima miestą iš nurodytos konteinerio vietos
        /// </summary>
        /// <param name="indeksas"></param>
        /// <returns></returns>
        public Miestas PaimtiMiesta (int indeksas)
        {
            return Miestai[indeksas];
        }
    }
}
