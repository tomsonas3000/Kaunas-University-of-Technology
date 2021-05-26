using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_2
{
    class Automobilis // klasė automobilio duomenims saugoti
    {
        public string ValstybiniaiNumeriai { get; set; }
        public string Gamintojas { get; set; }
        public string Modelis { get; set; }
        public int PagaminimoMetai { get; set; }
        public int PagaminimoMenuo { get; set; }
        public DateTime TechninesApziurosGaliojimoData { get; set; }
        public int Kuras { get; set; }
        public double VidutinesKuroSanaudos { get; set; }
        public string Skubiai { get; set; } //Požymis, nusakantis,
        // ar automobilio techninė apžiūra baigėsi
        /// <summary>
        /// Klasės Automobilis Konstruktorius
        /// </summary>
        /// <param name="valstybiniaiNumeriai"></param>
        /// <param name="gamintojas"></param>
        /// <param name="modelis"></param>
        /// <param name="pagaminimoMetai"></param>
        /// <param name="pagaminimoMenuo"></param>
        /// <param name="techninesApziurosGaliojimoData"></param>
        /// <param name="kuras"></param>
        /// <param name="vidutinesKuroSanaudos"></param>
        public Automobilis(string valstybiniaiNumeriai, string gamintojas, 
            string modelis, int pagaminimoMetai, int pagaminimoMenuo,
            DateTime techninesApziurosGaliojimoData, int kuras,
            double vidutinesKuroSanaudos)
        {
            ValstybiniaiNumeriai = valstybiniaiNumeriai;
            Gamintojas = gamintojas;
            Modelis = modelis;
            PagaminimoMetai = pagaminimoMetai;
            PagaminimoMenuo = pagaminimoMenuo;
            TechninesApziurosGaliojimoData = techninesApziurosGaliojimoData;
            Kuras = kuras;
            VidutinesKuroSanaudos = vidutinesKuroSanaudos;
        }
        /// <summary>
        /// Klasės automobilis konstruktorius
        /// </summary>
        /// <param name="gamintojas"></param>
        /// <param name="modelis"></param>
        /// <param name="valstybiniaiNumeriai"></param>
        /// <param name="techininesApziurosGaliojimoData"></param>
        public Automobilis(string gamintojas, string modelis,
            string valstybiniaiNumeriai,
            DateTime techininesApziurosGaliojimoData)
        {
            Gamintojas = gamintojas;
            Modelis = modelis;
            ValstybiniaiNumeriai = valstybiniaiNumeriai;
            TechninesApziurosGaliojimoData = techininesApziurosGaliojimoData;
        }
        /// <summary>
        /// Metodas, grąžinantis suformatuotą duomenų eilutę
        /// </summary>
        /// <returns>Grąžina suformatuotą duomenų eilutę</returns>
        public override string ToString()
        {
            return String.Format("|{0,-21}|{1,-15}|{2,-10}" +
                "|{3,20}|{4,19}" +
                "|{5,32}|{6,6}|{7,23}|", ValstybiniaiNumeriai, Gamintojas,
                Modelis, PagaminimoMetai, PagaminimoMenuo,
                TechninesApziurosGaliojimoData, Kuras,
                VidutinesKuroSanaudos);
        }
        /// <summary>
        /// Metodas, nurodantis, kad lyginsime Automobilis klasės objektus
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals (object obj)
        {
            return this.Equals(obj as Automobilis);
        }
        /// <summary>
        /// Metodas, patikrinantis ar du objektai yra tos pačios klasės
        /// </summary>
        /// <param name="automobilis">Automobilis objektas</param>
        /// <returns>Grąžina reikšmę, ar objektų klasės sutampa</returns>
        public bool Equals (Automobilis automobilis)
        {
            if(Object.ReferenceEquals(automobilis, null))
            {
                return false;
            }
            if (this.GetType() != automobilis.GetType())
            {
                return false;
            }
            return (ValstybiniaiNumeriai == automobilis.ValstybiniaiNumeriai &&
                Gamintojas == automobilis.Gamintojas &&
                Modelis == automobilis.Modelis);
        }
        /// <summary>
        /// Metodas skirtas palyginimo optimizavimui
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ValstybiniaiNumeriai.GetHashCode() ^
                Gamintojas.GetHashCode() ^
                Modelis.GetHashCode();

        }
        /// <summary>
        /// Užklotas == operatorius, skirtas patikrinti, ar objektai yra lygūs
        /// </summary>
        /// <param name="kp">Pirmas Automobilis objektas</param>
        /// <param name="dp">Antras Automobilis objektas</param>
        /// <returns></returns>
        public static bool operator ==(Automobilis kp, Automobilis dp)
        {
            if(Object.ReferenceEquals(kp, null))
            {
                if (Object.ReferenceEquals(dp, null))
                {
                    return true;
                }
                return false;
            }
            return kp.Equals(dp);
        }
        /// <summary>
        /// Užklotas != operatorius, skirtas patikrinti, ar objektai yra vienodi
        /// </summary>
        /// <param name="kp">Pirmas Automobilis objektas</param>
        /// <param name="dp">Antras Automobilis objektas</param>
        /// <returns></returns>
        public static bool operator !=(Automobilis kp, Automobilis dp)
        {
            return !(dp == kp);
        }
    }
}
