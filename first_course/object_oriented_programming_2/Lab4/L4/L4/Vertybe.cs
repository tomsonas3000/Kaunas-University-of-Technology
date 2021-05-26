using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L4
{
    public class Vertybe : IComparable<Vertybe>, IEquatable<Vertybe>
    {
        public string Vardas { get; set; }
        public int Kiekis { get; set; }
        public decimal Kaina { get; set; }
        /// <summary>
        /// Konstruktorius be parametrų
        /// </summary>
        public Vertybe()
        {

        }
        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="vardas"></param>
        /// <param name="kiekis"></param>
        /// <param name="kaina"></param>
        public Vertybe(string vardas, int kiekis, decimal kaina)
        {
            Vardas = vardas;
            Kiekis = kiekis;
            Kaina = kaina;
        }
        /// <summary>
        /// Paima užsakymo duomenis iš tekstinio failo
        /// </summary>
        /// <param name="eilute"></param>
        public Vertybe(string eilute)
        {
            PaimtiDuomenis(eilute);
        }
        /// <summary>
        /// Sudeda kintamuosius į savybes objekto
        /// </summary>
        /// <param name="eilute"></param>
        public void PaimtiDuomenis(string eilute)
        {
            string[] reiksmes = eilute.Split(',');
            Vardas = reiksmes[0];
            Kiekis = int.Parse(reiksmes[1]);
            Kaina = decimal.Parse(reiksmes[2]);
        }
        /// <summary>
        /// Grąžina suformatuotą teksto eilutę
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public override string ToString()
        {
            return String.Format("|{0,-20}|{1,7}|{2,15}|", Vardas, 
                Kiekis, Kaina);
        }
        /// <summary>
        /// Grąžina suformatuotą teksto eilutę
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public string Antraste()
        {
            return String.Format("|{0,-20}|{1,-7}|{2,-15}|", "Pavadinimas",
                "Kiekis", "Kaina");
        }
        /// <summary>
        /// Metodas, nurodantis, kad lyginsime Vertybe klasės objektą
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True arba false</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Vertybe);
        }
        /// <summary>
        /// Metodas, patikrinantis, ar objektai yra tos pačios klasės 
        /// </summary>
        /// <param name="kita">Vertybės objektas</param>
        /// <returns>True arba false</returns>
        public bool Equals(Vertybe kita)
        {
            if (Object.ReferenceEquals(kita, null))
                return false;
            if (this.GetType() != kita.GetType())
                return false;
            return kita.Vardas.CompareTo(this.Vardas)==0;
        }
        /// <summary>
        /// Metodas, skirtas palyginimo optimizavimui
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Vardas.GetHashCode();
        }
        /// <summary>
        /// Metodas, reikalingas IComparable realizavimui
        /// </summary>
        /// <param name="kita">Objektas, su kuriuo bus lyginama</param>
        /// <returns>-1 0 1</returns>
        public int CompareTo(Vertybe kita)
        {
            if (kita.Equals(this))
            {
                if (kita.Kiekis > this.Kiekis)
                {
                    return 1;
                }
                else
                    return -1;
            }
            if (kita >= this)
                return 1;
            else
                return -1;
        }
        /// <summary>
        /// Užklotas >= operatorius, palyginantis du objektus
        /// </summary>
        /// <param name="v1">Vertybės objektas</param>
        /// <param name="v2">Vertybės objektas</param>
        /// <returns>true arba false</returns>
        public static bool operator >=(Vertybe v1, Vertybe v2)
        {
            return v1.Vardas.CompareTo(v2.Vardas) < 0 || v1.Equals(v2)
                && v1.Kiekis > v2.Kiekis; 
        }
        /// <summary>
        /// Užklotas >= operatorius, palyginantis du objektus
        /// </summary>
        /// <param name="v1">Vertybės objektas</param>
        /// <param name="v2">Vertybės objektas</param>
        /// <returns>true arba false</returns>
        public static bool operator <=(Vertybe v1, Vertybe v2)
        {
            return v2.Vardas.CompareTo(v1.Vardas) > 0 || v1.Equals(v2) 
                && v2.Kiekis > v1.Kiekis;
        }
    }
}