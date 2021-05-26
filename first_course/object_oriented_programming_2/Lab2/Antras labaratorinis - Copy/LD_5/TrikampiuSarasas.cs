using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_5
{
    public sealed class TrikampiuSarasas
    {
        private sealed class Mazgas
        {
            public Trikampis Duom { get; set; }
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
            /// <param name="trikampis"></param>
            /// <param name="adr"></param>
            public Mazgas(Trikampis trikampis, Mazgas adr)
            {
                Duom = trikampis;
                Kitas = adr;
            }
        }
        private Mazgas pradzia;
        private Mazgas pabaiga;
        private Mazgas d;
        /// <summary>
        /// Konstruktorius, sudarantis pradžios, pabaigos, darbines rodykles
        /// </summary>
        public TrikampiuSarasas()
        {
            pradzia = null;
            pabaiga = null;
            d = null;
        }
        /// <summary>
        /// Įdeda Trikampis duomenis į sąrašo pradžią
        /// </summary>
        /// <param name="trikampis"></param>
        public void DetiDuomenisIPradzia(Trikampis trikampis)
        {
            pradzia = new Mazgas(trikampis, pradzia);
        }
        /// <summary>
        /// Įdeda Trikampis duomenis į sąrašo pabaigą
        /// </summary>
        /// <param name="trikampis"></param>
        public void DetiDuomenisIPabaiga(Trikampis trikampis)
        {
            Mazgas dd = new Mazgas(trikampis, null);
            if(pradzia != null)
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
        /// Patikrina, ar jau pasiekta sąrašo pabaiga
        /// </summary>
        /// <returns>True arba false</returns>
        public bool Yra()
        {
            return d != null;
        }
        /// <summary>
        /// Grąžina Trikampis duomenis
        /// </summary>
        /// <returns>Trikampis duomenys</returns>
        public Trikampis ImtiDuomenis()
        {
            return d.Duom;
        }
        /// <summary>
        /// Surikiuoja sąrašo duomenis
        /// </summary>
        public void Rikiuoti()
        {
            for (Mazgas d1 = pradzia; d1 != null; d1 = d1.Kitas)
            {
                Mazgas minv = d1;
                for (Mazgas d2 = d1.Kitas; d2!=null; d2 = d2.Kitas)
                {
                    if (d2.Duom <= minv.Duom)
                    {
                        minv = d2;
                    }
                }
                Trikampis trikampis = d1.Duom;
                d1.Duom = minv.Duom;
                minv.Duom = trikampis;
            }
        }
        /// <summary>
        /// Pašalina nurodytų koordinačių trikampį iš trikampių sąrašo
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        public void Salinti(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            Mazgas l = pradzia;
            int i = 0;
            for(Mazgas d = pradzia; d != null; d = d.Kitas)
            {
                if(d.Duom.Pozymis == null)
                {
                    if (d == pradzia)
                    {
                        if (pradzia.Duom.ArLygiosKoordinates(x1, y1, x2, y2, 
                            x3, y3) == true)
                        {
                            Mazgas darbinis = pradzia;
                            pradzia = pradzia.Kitas;
                            darbinis.Kitas = null;
                            break;
                        }
                    }

                }
                if(d.Duom.Pozymis == null)
                {
                    if(pabaiga.Duom.Pozymis == null)
                    {
                        if (pabaiga.Duom.ArLygiosKoordinates(x1, y1, x2, y2,
                            x3, y3) == true)
                        {
                            if (d != pabaiga)
                            {
                                while (d.Kitas != pabaiga)
                                {
                                    d = d.Kitas;
                                }
                                pabaiga = d;
                                d.Kitas = null;
                                break;
                            }
                            if(d == pabaiga)
                            {
                                pabaiga = l;
                                l.Kitas = null;
                                break;
                            }
                        }
                    }
                }
                if (d.Duom.Pozymis == null)
                {
                    if (d.Duom.ArLygiosKoordinates(x1, y1, x2, y2, x3, y3)
                        == true)
                    {
                        Mazgas s = d.Kitas;
                        d.Duom = s.Duom;
                        d.Kitas = s.Kitas;
                        s = null;
                        break;
                    }
                }
                if (i != 0)
                {
                    l = l.Kitas;
                }
                i++;
            }
        }
       
    }
}