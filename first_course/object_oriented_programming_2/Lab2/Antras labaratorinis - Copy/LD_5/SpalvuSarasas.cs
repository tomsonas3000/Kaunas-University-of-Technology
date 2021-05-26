using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LD_5
{
    public sealed class SpalvuSarasas
    {
        private sealed class Mazgas
        {
            public Spalv Duom { get; private set; }
            public Mazgas Kitas { get; set; }
            /// <summary>
            /// Tuščias konstruktorius
            /// </summary>
            public Mazgas()
            {

            }
            /// <summary>
            /// Konstruktorius su parametrais
            /// </summary>
            /// <param name="spalva"></param>
            /// <param name="adr"></param>
            public Mazgas(Spalv spalva, Mazgas adr)
            {
                Duom = spalva;
                Kitas = adr;
            }
        }
        private Mazgas pradzia;
        private Mazgas pabaiga;
        private Mazgas d;
        /// <summary>
        /// Konstruktorius, sukuriantis pradžios, pabaigos ir darbines rodykles
        /// </summary>
        public SpalvuSarasas()
        {
            this.pradzia = null;
            this.pabaiga = null;
            this.d = null;
        }
        /// <summary>
        /// Įdeda duomenis į sąrašo pradžią
        /// </summary>
        /// <param name="spalva">Spalv klasės objektas</param>
        public void DetiDuomenisIPradzia(Spalv spalva)
        {
            pradzia = new Mazgas(spalva, pradzia);
        }
        /// <summary>
        /// Įdeda duomenis į sąrašo pabaigą
        /// </summary>
        /// <param name="spalva">Spalv klasės objektas</param>
        public void DetiDuomenisIPabaiga(Spalv spalva)
        {
            Mazgas dd = new Mazgas(spalva, null);
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
        /// Nukreipia darbinę rodyklę į pradinę rodyklę
        /// </summary>
        public void Pradzia()
        {
            d = pradzia;
        }
        /// <summary>
        /// Nukreipia darbinę rodyklę į sekančią rodyklę
        /// </summary>
        public void Kitas()
        {
            d = d.Kitas;
        }
        /// <summary>
        /// Grąžina true arba false, ar jau pasiekta sąrašo pabaiga
        /// </summary>
        /// <returns>True arba false</returns>
        public bool Yra()
        {
            return d != null;
        }
        /// <summary>
        /// Grąžina Spalv objekto duomenis
        /// </summary>
        /// <returns>Spalv objekto duomenys</returns>
        public Spalv ImtiDuomenis()
        {
            return d.Duom;
        }
    }
}