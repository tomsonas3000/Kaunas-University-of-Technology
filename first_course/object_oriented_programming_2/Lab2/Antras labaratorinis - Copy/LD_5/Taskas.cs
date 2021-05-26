using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_5
{
    public class Taskas
    {
        public int XKoord { get; set; }
        public int YKoord { get; set; }
        public string Spalva { get; set; }
        /// <summary>
        /// Konstruktorius be parametrų
        /// </summary>
        public Taskas()
        {

        }
        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="spalva"></param>
        public Taskas(int x, int y, string spalva)
        {
            XKoord = x;
            YKoord = y;
            Spalva = spalva;
        }
        /// <summary>
        /// Grąžina suformatuotą teksto eilutę
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public string Antraste()
        {
            return String.Format(" {0,-2} {1,-2} {2,-15}", "X", "Y", "Spalva");
        }
        /// <summary>
        /// Grąžina suformatuota teksto eilutę
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public string ToString1()
        {
            return String.Format("{0,2}{1,2}", XKoord, YKoord);
        }
        /// <summary>
        /// Grąžina suformatuota teksto eilutę
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public override string ToString()
        {
            return String.Format("|{0,2}|{1,2}|{2,-15}|", XKoord,
                YKoord, Spalva);
        }
        /// <summary>
        /// Užklotas >= operatorius, palyginantis du objektus
        /// </summary>
        /// <param name="t1">Taškas objektas</param>
        /// <param name="t2">Taškas objejktas</param>
        /// <returns>Reikšmę, ar objektas didesnis, ar maženis</returns>
        public static bool operator >= (Taskas t1, Taskas t2)
        {
            return (t1.XKoord >= t2.XKoord) || (t1.XKoord == t2.XKoord 
                && t1.YKoord >= t2.YKoord);
        }
        /// <summary>
        /// Užklotas <= operatorius, palyginantis du objektus
        /// </summary>
        /// <param name="t1">Taškas objektas</param>
        /// <param name="t2">Taškas objektas</param>
        /// <returns>Reikšmę, ar objektas didesnis, ar mažesnis</returns>
        public static bool operator <=(Taskas t1, Taskas t2)
        {
            return (t1.XKoord <= t2.XKoord) || (t1.XKoord == t2.XKoord 
                && t1.YKoord <= t2.YKoord);
        }
    }
}