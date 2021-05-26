using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_5
{
    public class Spalv
    {
        public string Spalva { get; set; }
        public bool ArGalima { get; set; }
        /// <summary>
        /// Konstruktorius be parametrų
        /// </summary>
        public Spalv()
        {

        }
        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="spalva"></param>
        /// <param name="arGalima">Požymis, ar reikia sudarinėti
        /// tos spalvos trikampius</param>
        public Spalv(string spalva, bool arGalima)
        {
            Spalva = spalva;
            ArGalima = arGalima;
        }
        /// <summary>
        /// Grąžina suformatuotą teksto eilutę
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public string Antraste()
        {
            return String.Format(" {0,-15} {1,-20} ", "Spalva", 
                "Ar galima spalva?");
        }
        /// <summary>
        /// Grąžina suformatuotą teksto eilutę
        /// </summary>
        /// <returns>Suformatuota teksto eilutė</returns>
        public override string ToString()
        {
            string arGalima;
            if (ArGalima)
                arGalima = "Taip";
            else
                arGalima = "Ne";
            return String.Format("|{0,-15}|{1,-20}|", Spalva, arGalima);
        }
    }
}