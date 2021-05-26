using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L4
{
    public class Uzsakymas : IEquatable<Uzsakymas>, IComparable<Uzsakymas>
    {
        public string Vardas { get; set; }
        public int Kiekis { get; set; }
        /// <summary>
        /// Konstruktorius be parametrų
        /// </summary>
        public Uzsakymas()
        {

        }
        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="vardas"></param>
        /// <param name="kiekis"></param>
        public Uzsakymas(string vardas, int kiekis)
        {
            Vardas = vardas;
            Kiekis = kiekis;
        }
        /// <summary>
        /// Grąžina suformatuotą teksto eilutę  
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public override string ToString()
        {
            return string.Format("|{0,-15}|{1,7}|", Vardas, Kiekis);
        }
        /// <summary>
        /// Grąžina suformatuotą teksto eilutę
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public string Antraste()
        {
            return string.Format("|{0,-15}|{1,-7}|", "Vardas", "Kiekis");
        }
        /// <summary>
        /// Paima užsakymo duomenis iš tekstinio failo
        /// </summary>
        /// <param name="eilute"></param>
        public Uzsakymas(string eilute)
        {
            PaimtiDuomenis(eilute);
        }
        /// <summary>
        /// Sudeda kintamuosius į savybes objektox
        /// </summary>
        /// <param name="eilute"></param>
        public void PaimtiDuomenis(string eilute)
        {
            string[] reiksmes = eilute.Split(',');
            Vardas = reiksmes[0];
            Kiekis = int.Parse(reiksmes[1]);
        }
        public int CompareTo(Uzsakymas kitas)
        {
            return 0;
        }
        public bool Equals(Uzsakymas kitas)
        {
            return true;
        }
    }
}