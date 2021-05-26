using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_5
{
    public sealed class TaskuSarasas
    {
        private sealed class Mazgas
        {
            public Taskas Duom { get; set; }
            public Mazgas Kitas { get; set; }
            /// <summary>
            /// Konstruktorius be parametrų
            /// </summary>
            public Mazgas()
            {

            }
            /// <summary>
            /// Konstruktorius su parametrais
            /// </summary>
            /// <param name="taskas"></param>
            /// <param name="adr"></param>
            public Mazgas(Taskas taskas, Mazgas adr)
            {
                Duom = taskas;
                Kitas = adr;
            }
        }
        private Mazgas pradzia;
        private Mazgas pabaiga;
        private Mazgas d;
        /// <summary>
        /// Konstruktorius, sukuriantis pradžios, pabaigos, darbines rodykles
        /// </summary>
        public TaskuSarasas()
        {
            this.pradzia = null;
            this.pabaiga = null;
            this.d = null;
        }
        /// <summary>
        /// Įdeda duomenis į sąrašo pradžią
        /// </summary>
        /// <param name="taskas">Taškas objektas</param>
        public void DetiDuomenisIPradzia(Taskas taskas)
        {
            pradzia = new Mazgas(taskas, pradzia);
        }
        /// <summary>
        /// Įdeda duomenis į sąrašo pabaigąą
        /// </summary>
        /// <param name="taskas">Taškas objektas</param>
        public void DetiDuomenisIPabaiga(Taskas taskas)
        {
            Mazgas dd = new Mazgas(taskas, null);
            if (pradzia != null)
            {
                pabaiga.Kitas = dd;
                pabaiga = dd;
            }
            else
            {
                pradzia = dd;
                pabaiga = dd;
            }
        }
        /// <summary>
        /// Nukreipia darbinę rodyklę į sąrašo pradžią
        /// </summary>
        public void Pradzia()
        {
            d = pradzia;
        }
        /// <summary>
        /// Nukreipia darbinę rodyklę į sekantį elementą
        /// </summary>
        public void Kitas()
        {
            d = d.Kitas;
        }
        /// <summary>
        /// Grąžina pirmojo taško duomenis
        /// </summary>
        /// <returns>Pirmojo taško duomenys</returns>
        public Taskas GrazintiPirmaTaska()
        {
            return d.Duom;
        }
        /// <summary>
        /// Grąžina true arba false, ar sąrašo pabaiga
        /// </summary>
        /// <returns></returns>
        public bool Yra()
        {
            return d != null;
        }
        /// <summary>
        /// Grąžina Taškas duomenis
        /// </summary>
        /// <returns>Taškas duomenys</returns>
        public Taskas ImtiDuomenis()
        {
            return d.Duom;
        }
        /// <summary>
        /// Sudaro didžiausią vienos spalvos lygiašonį trikampį
        /// </summary>
        /// <param name="Spalva">Spalva</param>
        /// <returns>Didžiausią lygiašonį trikampį</returns>
        public Trikampis RastiDidziausiaTrikampi(string Spalva)
        {
            Trikampis didziausiasTrikampis = new Trikampis(0, Spalva);
            for(Mazgas d = pradzia; d != null; d = d.Kitas)
            {
                for(Mazgas g = d.Kitas; g != null; g = g.Kitas)
                {
                    for(Mazgas f = g.Kitas; f != null; f = f.Kitas)
                    {
                        decimal ilgis1 = (decimal)Math.Sqrt(
                            Math.Pow(d.Duom.XKoord - g.Duom.XKoord, 2) 
                            + Math.Pow(d.Duom.YKoord - g.Duom.YKoord, 2));
                        decimal ilgis2 = (decimal)Math.Sqrt(
                            Math.Pow(d.Duom.XKoord - f.Duom.XKoord, 2) 
                            + Math.Pow(d.Duom.YKoord - f.Duom.YKoord, 2));
                        decimal ilgis3 = (decimal)Math.Sqrt(
                            Math.Pow(g.Duom.XKoord - f.Duom.XKoord, 2) 
                            + Math.Pow(g.Duom.YKoord - f.Duom.YKoord, 2));
                        
                        if(g.Duom.Spalva == d.Duom.Spalva && f.Duom.Spalva
                            == g.Duom.Spalva && f.Duom.Spalva == Spalva)
                        {
                            if(ilgis1 == ilgis2 || ilgis2 == ilgis3 || 
                                ilgis1 == ilgis3)
                            {
                                if (ilgis1 + ilgis2 > ilgis3 ||
                                    ilgis2 + ilgis3 > ilgis1 || 
                                    ilgis3 + ilgis1 > ilgis2)
                                {
                                    decimal perimetras = ilgis1 
                                        + ilgis2 + ilgis3;
                                    if (perimetras >= 
                                        didziausiasTrikampis.Perimetras)
                                    {
                                        didziausiasTrikampis.Perimetras
                                            = perimetras;
                                        didziausiasTrikampis.T1 = d.Duom;
                                        didziausiasTrikampis.T2 = g.Duom;
                                        didziausiasTrikampis.T3 = f.Duom;
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return didziausiasTrikampis;
        }
        /// <summary>
        /// Surikiuoja sąrašo duomenis
        /// </summary>
        public void Rikiuoti()
        {
            for (Mazgas d1 = pradzia; d1 != null; d1 = d1.Kitas)
            {
                Mazgas minv = d1;
                for (Mazgas d2 = d1.Kitas; d2 != null; d2 = d2.Kitas)
                {
                    if (d2.Duom <= minv.Duom)
                    {
                        minv = d2;
                    }
                }
                Taskas taskas = d1.Duom;
                d1.Duom = minv.Duom;
                minv.Duom = taskas;
            }
        }
    }
}