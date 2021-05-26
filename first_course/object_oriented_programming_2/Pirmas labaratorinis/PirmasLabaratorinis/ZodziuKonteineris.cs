using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PirmasLabaratorinis
{
    public class ZodziuKonteineris
    {
        public int Skaicius { get; private set; }
        public Zodis[] Zodziai;

        public ZodziuKonteineris(int skaicius)
        {
            Zodziai = new Zodis[skaicius];
            Skaicius = 0;
        }
        public void PridetiZodi(Zodis zodis)
        {
            Zodziai[Skaicius++] = zodis;
        }
        public Zodis PaimtiZodi(int indeksas)
        {
            return Zodziai[indeksas];
        }
        public bool ArYra(Zodis zodis)
        {
            return Zodziai.Contains(zodis);
        }
        public void PasalintiZodi(Zodis zodis)
        {
            int i = 0;
            while (i < Skaicius)
            {
                if (Zodziai[i].Equals(zodis))
                {
                    Skaicius--;
                }
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