using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U1_3
{
    class Studentas // klasė studento duomenims saugoti
    {
        public string Pavarde { get; set; } //Studento pavardė
        public string Vardas { get; set; } //Studento vardas
        public DateTime GimimoData { get; set; } //Studento gimimo data
        public string StudentoPazymejimoNumeris { get; set; }
        //Studento pažymėjimo numeris
        public string Kursas { get; set; } //Studento kursas
        public int TelefonoNumeris { get; set; } //Studento telefono numeris
        public bool Fuksas { get; set; } 
        //Požymis, nusakantis, ar studentas "fuksas"
        /// <summary>
        /// Klasės Studentas konstruktorius
        /// </summary>
        /// <param name="pavarde"></param>
        /// <param name="vardas"></param>
        /// <param name="gimimoData"></param>
        /// <param name="studentoPazymejimoNumeris"></param>
        /// <param name="kursas"></param>
        /// <param name="telefonoNumeris"></param>
        /// <param name="fuksas"></param>
        public Studentas(string pavarde, string vardas, DateTime gimimoData,
            string studentoPazymejimoNumeris, string kursas,
            int telefonoNumeris, bool fuksas)
        {
            Pavarde = pavarde;
            Vardas = vardas;
            GimimoData = gimimoData;
            StudentoPazymejimoNumeris = studentoPazymejimoNumeris;
            Kursas = kursas;
            TelefonoNumeris = telefonoNumeris;
            Fuksas = fuksas;
        }
    }
}
