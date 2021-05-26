using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_5
{
    public class Trikampis
    {
        public Taskas T1 { get; set; }
        public Taskas T2 { get; set; }
        public Taskas T3 { get; set; }

        public string Spalva { get; set; }

        public decimal Perimetras { get; set; }

        public string Pozymis { get; set; }
        /// <summary>
        /// Konstruktorius be parametrų
        /// </summary>
        public Trikampis()
        {

        }
        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="perimetras"></param>
        /// <param name="spalva"></param>
        public Trikampis(decimal perimetras, string spalva)
        {
            Perimetras = perimetras;
            Spalva = spalva;
            Pozymis = null;
        }
        /// <summary>
        /// Grąžina suformatuotą teksto eilutę
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public string Antraste()
        {
            return string.Format(" {0,-15} {1,2} {2,2} {3,2} {4,2} {5,2} " +
                "{6,2} ",
                "Spalva", "X1", "Y1", "X2", "Y2", "X3", "Y3");
        }
        /// <summary>
        /// Grąžina suformatuotą teksto eilutę
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public override string ToString()
        {
            return String.Format("|{0,-15}|{1,2}|{2,2}|{3,2}|{4,2}|{5,2}" +
                "|{6,2}|",
                Spalva, T1.XKoord, T1.YKoord, T2.XKoord, T2.YKoord, T3.XKoord, 
                T3.YKoord);
        }
        /// <summary>
        /// Metodas, nurodantis, kad lyginsime Trikampis klasės objektus
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Trikampis);
        }
        /// <summary>
        /// Metodas, patikrinantis, ar du objektai yra tos pačios klasės
        /// </summary>
        /// <param name="t">Trikampis objektas</param>
        /// <returns>Grąžina reikšmę, ar objektų klasės sutampa</returns>
        public bool Equals(Trikampis t)
        {
            if (Object.ReferenceEquals(t, null))
            {
                return false;
            }
            if (this.GetType() != t.GetType())
            {
                return false;
            }
            return t.Perimetras == Perimetras && t.Spalva == Spalva;
        }
        /// <summary>
        /// Metodas skirtas palyginimo optimizavimui
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Perimetras.GetHashCode() ^ Spalva.GetHashCode();
        }
        /// <summary>
        /// Užklotas == operatorius, skirtas patikrinti, ar objektai yra vienodi
        /// </summary>
        /// <param name="t1">Pirmas Trikampis objektas</param>
        /// <param name="t2"Antras Trikampis objektas></param>
        /// <returns></returns>
        public static bool operator == (Trikampis t1, Trikampis t2)
        {
            if (Object.ReferenceEquals(t1, null))
            {
                if (Object.ReferenceEquals(t2, null))
                {
                    return true;
                }
                return false;
            }
            return t1.Equals(t2);
        }
        /// <summary>
        /// Užklotas != operatorius, skirtas patikrinti, ar objektai yra vienodi
        /// </summary>
        /// <param name="t1">Pirmas Trikampis objektas</param>
        /// <param name="t2">Antras Trikampis objektas</param>
        /// <returns></returns>
        public static bool operator != (Trikampis t1, Trikampis t2)
        {
            return !t1.Equals(t2);
        }
        /// <summary>
        /// Užklotas >= operatorius, skirtas palyginti objektus
        /// </summary>
        /// <param name="t1">Pirmas Trikampis objektas</param>
        /// <param name="t2">Antras Trikampis objektas</param>
        /// <returns></returns>
        public static bool operator >=(Trikampis t1, Trikampis t2)
        {
            t1.RikiuotiTaskus();
            t2.RikiuotiTaskus();
            int tikrinimas = 0;
            tikrinimas = t1.Spalva.CompareTo(t2.Spalva);
            if (t1.Pozymis != null || t2.Pozymis != null)
            {
                return tikrinimas > 0 || tikrinimas == 0 && t1.T3 >= t2.T3;
            }
            else
            {
                return tikrinimas >= 0;
            }
        }
        /// <summary>
        /// Užklotas <= operatorius, skirtas palyginti objektus
        /// </summary>
        /// <param name="t1">Pirmas Trikampis objektas</param>
        /// <param name="t2">Antras Trikampis objektas</param>
        /// <returns></returns>
        public static bool operator <=(Trikampis t1, Trikampis t2)
        {
            if (t1.Pozymis == null && t2.Pozymis == null)
            {
                t1.RikiuotiTaskus();
                t2.RikiuotiTaskus();
            }
            int tikrinimas = 0;
            tikrinimas = t1.Spalva.CompareTo(t2.Spalva);
            if (t1.Pozymis != null && t2.Pozymis != null)
            {
                return tikrinimas < 0 || tikrinimas == 0 && t1.T3 >= t2.T3;
            }
            else
            {
                return tikrinimas <= 0;
            }
        }
        /// <summary>
        /// Metodas surikiuojantis taškus
        /// </summary>
        public void RikiuotiTaskus()
        {
            if (T1 <= T2 && T1 <= T3)
            {
                if (T3 <= T2)
                {
                    Taskas L = T2;
                    T2 = T3;
                    T3 = L;
                }
            }
            if (T2 <= T1 && T2 <= T3)
            {
                Taskas L = T1;
                T1 = T2;
                T2 = L;
            }
            if (T3 <= T1 && T3 <= T2)
            {
                Taskas L = T1;
                T1 = T3;
                T3 = L;
            }
            if (T3 <= T2)
            {
                Taskas L = T2;
                T2 = T3;
                T3 = L;
            }
        }
        /// <summary>
        /// Patikrina, ar sutampa koordinatės su duotomis koordinatėmis
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns>True arba False</returns>
        public bool ArLygiosKoordinates(int x1, int y1, int x2, int y2, 
            int x3, int y3)
        {
            if (x1 == T1.XKoord && y1 == T1.YKoord && x2 == T2.XKoord && y2
                == T2.YKoord && x3 == T3.XKoord && y3 == T3.YKoord)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}