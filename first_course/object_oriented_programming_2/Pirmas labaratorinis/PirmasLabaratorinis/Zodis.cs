using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PirmasLabaratorinis
{
    public class Zodis
    {
        public string Pavadinimas { get; set; }
        public int Kiekis { get; set; }
        public Zodis()
        {

        }
        public Zodis(string pavadinimas, int kiekis)
        {
            Pavadinimas = pavadinimas;
            Kiekis = kiekis;
        }
    }
}