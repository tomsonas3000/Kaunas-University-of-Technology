using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class ZodziuKonteineris
    {
        private Zodis[] Zodziai;
        public int Skaicius { get; private set; }
        /// <summary>
        /// Konstruktorius
        /// </summary>
        /// <param name="skaicius">konteinerio dydis</param>
        public ZodziuKonteineris(int skaicius)
        {
            Zodziai = new Zodis[skaicius];
            Skaicius = 0;
        }
        /// <summary>
        /// Metodas, į konteinerį pridedantis žodžio duomenis
        /// </summary>
        /// <param name="zodis">klasės Zodis objektas</param>
        public void PridetiZodi(Zodis zodis)
        {
                Zodziai[Skaicius++] = zodis;
        }
        /// <summary>
        /// Metodas, skirtas paimi duomenis apie žodį
        /// </summary>
        /// <param name="indeksas">indeksas</param>
        /// <returns>grąžina Zodis klasės objektą</returns>
        public Zodis PaimtiZodi(int indeksas)
        {
            return Zodziai[indeksas];
        }
        /// <summary>
        /// Metodas, kuris tikrina ar objektas jau yra konteineryje
        /// </summary>
        /// <param name="zodis">klasės Zodis objektas</param>
        /// <returns>True arba false</returns>
        public bool Contains (Zodis zodis)
        {
            return Zodziai.Contains(zodis);
        }
        /// <summary>
        /// Metodas, šalinantis Zodis klasės objektą iš konteinerio
        /// </summary>
        /// <param name="zodis">klases zodis objektas</param>
        public void PasalintiZodi(Zodis zodis)
        {
            int i = 0;
            while(i < Skaicius)
            {
                if (Zodziai[i].Equals(zodis))
                {
                    Skaicius--;
                    for (int j = i; j < Skaicius; j++)
                    {
                        Zodziai[j] = Zodziai[j + 1];
                    }
                    break;
                }
                i++;
            }
        }
    }
}
