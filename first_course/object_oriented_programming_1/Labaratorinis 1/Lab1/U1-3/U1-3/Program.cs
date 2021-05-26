using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace U1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();   
            List<Studentas> studentai = p.NuskaitytiStudentoDuomenis();
            p.IrasytiPradiniusDuomenisItekstiniFaila
                (studentai, studentai.Count);
            int fuksuSkaicius = p.FuksuSkaiciausNustatymas(studentai);
            int[] gimtadieniuKiekisMenesyje =
                p.MenesiuKadaVykstaGimtadieniai(studentai);
            int menesisSuDaugiausiaGimtadieniu =
                p.MenesioKadaDaugiausiaiGimtadieniuGavimas
                (gimtadieniuKiekisMenesyje);
            List<Studentas> gimeDazniausiaMenesi =
                p.SarasasKurieGimeDazniausiaMenesi
                (studentai, menesisSuDaugiausiaGimtadieniu);
            List<Studentas> pirmakursiai = 
                p.IeskomoKursoRadimas(studentai, "pirmakursis");
            // Randu pirmakursius
            List<Studentas> ketvirtakursiai = 
                p.IeskomoKursoRadimas(studentai, "ketvirtakursis"); 
            // Randu ketvirtakursius
            Console.WriteLine("Fuksu skaicius: {0} ", fuksuSkaicius);
            // Į konsolę išspausdinu fuksų skaičių
            Console.WriteLine("Menesis, kurio metu gime daugiausia studentu: " +
                "{0}",
                p.MenesioPavadinimoNustatymas(menesisSuDaugiausiaGimtadieniu)); 
            // Į konsolę išspausdinu mėnesį, kurio metu gimė daugiausia studentų
            p.SpausdintiIKonsole(gimeDazniausiaMenesi);
            // Į konsole išspausdinu studentus, kurie gimė mėnesį,
            // kurio metu gimė daugiausia studentų
            if (pirmakursiai.Count == 0)
            {
                Console.WriteLine("Pirmakursių nėra");
            }
            else
            {
                p.SpausdintiPirmakursiusITekstiniFaila(pirmakursiai);
            }
            //Įrašau pirmakursių duomenis į tekstinį failą
            if (ketvirtakursiai.Count == 0)
            {
                Console.WriteLine("Ketvirtakursių nėra");
            }
            else
            {
                p.SpausdintiKetvirtakursiusITekstiniFaila(ketvirtakursiai);
            }
            // Įrašau pirmakursių duomenis į tekstinį failą
            Console.ReadKey();


        }
        /// <summary>
        /// Įrašau pradinius duomenis į tekstinį failą
        /// </summary>
        /// <param name="studentai">studentų sąrašas</param>
        /// <param name="studentuSkaicius">studentų skaičius</param>
        void IrasytiPradiniusDuomenisItekstiniFaila 
            (List<Studentas> studentai, int studentuSkaicius)
        {
            using (StreamWriter spausdintojas = 
                new StreamWriter(@"PradiniaiDuomenys.txt"))
            {
                spausdintojas.WriteLine("Pradiniai duomenys: ");
                spausdintojas.WriteLine("--------------------------------" +
                    "----------------------------------------------------" +
                    "----------------------------------------------------" +
                    "------------");
                spausdintojas.WriteLine("|{0,-15}|{1,-15}|{2,-30}|{3,-30}|" +
                    "{4,-15}|{5,-20}|{6,-15}|", "Pavardė",
                    "Vardas", "Gimimo data",
                    "Studento pažymėjimo numeris", "Kursas",
                    "Telefono numeris", "Požymis Fuksas");
                spausdintojas.WriteLine("--------------------------------" +
                    "----------------------------------------------------" +
                    "----------------------------------------------------" +
                    "------------");
                for (int i = 0; i < studentuSkaicius; i++)
                {
                    spausdintojas.WriteLine("|{0,-15}|{1,-15}|{2,-30}" +
                        "|{3,-30}|{4,-15}|{5,20}|{6,-15}|",
                        studentai[i].Pavarde, studentai[i].Vardas,
                        studentai[i].GimimoData,
                        studentai[i].StudentoPazymejimoNumeris,
                        studentai[i].Kursas, studentai[i].TelefonoNumeris,
                        studentai[i].Fuksas);
                    spausdintojas.WriteLine("--------------------------------" +
                    "----------------------------------------------------" +
                    "----------------------------------------------------" +
                    "------------");
                }
            }
        }
        /// <summary>
        /// Nuskaitau duomenis iš tekstinio failo.
        /// </summary>
        /// <returns>Studentų sąrašą</returns>
        List<Studentas> NuskaitytiStudentoDuomenis()
        {
            List<Studentas> studentai = new List<Studentas>();
            string[] linijos = File.ReadAllLines(@"F1.csv");
            foreach(string linija in linijos)
            {
                string[] reiksmes = linija.Split(';');
                string pavarde = reiksmes[0];
                string vardas = reiksmes[1];
                DateTime gimimoData = DateTime.Parse(reiksmes[2]);
                string studentoPazymejimoNumeris = reiksmes[3];
                string kursas = reiksmes[4];
                int telefonoNumeris = int.Parse(reiksmes[5]);
                bool fuksas = bool.Parse(reiksmes[6]);

                Studentas studentas = new Studentas(pavarde, vardas,
                    gimimoData, studentoPazymejimoNumeris,
                    kursas, telefonoNumeris, fuksas);
                studentai.Add(studentas);
            }
            return studentai;
        }
        /// <summary>
        /// Spausdina studentų vardą, pavardę ir gimimo datą.
        /// </summary>
        /// <param name="studentai">studentų sąrašas</param>
        void SpausdintiIKonsole(List<Studentas> studentai)
        {
            Console.WriteLine("{0,-20} {1,-20} {2,-20}", "Vardas",
                "Pavardė", "Gimimo data");
            foreach (Studentas studentas in studentai)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,20}", studentas.Vardas,
                    studentas.Pavarde, studentas.GimimoData);
            }
        }
        /// <summary>
        /// Įrašo pirmakursių duomenis į tekstinį failą pirmakursiai.csv.
        /// </summary>
        /// <param name="studentai">studentų sąrašas</param>
        void SpausdintiPirmakursiusITekstiniFaila(List<Studentas> studentai)
        {
            string[] eilutes = new string[studentai.Count];
            for (int i = 0; i < studentai.Count; i++)
            {
                eilutes[i] = String.Format("{0,-15} {1,-15} {2,-25} " +
                    "{3,-15} {4,-15} {5,15} {6,-15}", 
                    studentai[i].Pavarde, studentai[i].Vardas, 
                    studentai[i].GimimoData, 
                    studentai[i].StudentoPazymejimoNumeris, 
                    studentai[i].Kursas, studentai[i].TelefonoNumeris,
                    studentai[i].Fuksas, 
                    "---------------------------------------");
            }
            File.WriteAllLines(@"Pirmakursiai.csv", eilutes);
        }
        /// <summary>
         /// Įrašo ketvirtakursių duomenis į tekstinį failą ketvirtakursiai.csv.
         /// </summary>
         /// <param name="studentai">studentų sąrašas</param>
        void SpausdintiKetvirtakursiusITekstiniFaila(List<Studentas> studentai)
        {
            string[] eilutes = new string[studentai.Count];
            for (int i = 0; i < studentai.Count; i++)
            {
                eilutes[i] = String.Format("{0,-15} {1,-15} {2,-25} " +
                    "{3,-15} {4,-15} {5,15} {6,-15}",
                    studentai[i].Pavarde, 
                    studentai[i].Vardas, 
                    studentai[i].GimimoData, 
                    studentai[i].StudentoPazymejimoNumeris, 
                    studentai[i].Kursas, studentai[i].TelefonoNumeris,
                    studentai[i].Fuksas);
            }
            File.WriteAllLines(@"Ketvirtakursiai.csv", eilutes);
        }
       
        int FuksuSkaiciausNustatymas(List<Studentas> studentai)
        {
            int fuksuSkaicius = 0;
            foreach(Studentas studentas in studentai)
            {
                if (studentas.Fuksas == true)
                {
                    fuksuSkaicius++;
                }
            }
            return fuksuSkaicius;
        }
        /// <summary>
        /// Suranda mėnesius, kurių metu vyksta gimtadieniai.
        /// </summary>
        /// <param name="studentai">studentų sąrašas</param>
        /// <returns>Mėnesius, kada vyksta gimtadieniai</returns>
       int[] MenesiuKadaVykstaGimtadieniai (List<Studentas> studentai)
        {
            int[] gimtadieniuKiekis = new int[13];
            foreach (Studentas studentas in studentai)
                //Tikrinamas kiekvieno studento gimimo mėnesis 
                //ir tada padidinima to mėnesio reikšmė
            {
                switch (studentas.GimimoData.Month) 
                {
                    case 1:
                        gimtadieniuKiekis[1]++;
                        break;
                    case 2:
                        gimtadieniuKiekis[2]++;
                        break;
                    case 3:
                        gimtadieniuKiekis[3]++;
                        break;
                    case 4:
                        gimtadieniuKiekis[4]++;
                        break;
                    case 5:
                        gimtadieniuKiekis[5]++;
                        break;
                    case 6:
                        gimtadieniuKiekis[6]++;
                        break;
                    case 7:
                        gimtadieniuKiekis[7]++;
                        break;
                    case 8:
                        gimtadieniuKiekis[8]++;
                        break;
                    case 9:
                        gimtadieniuKiekis[9]++;
                        break;
                    case 10:
                        gimtadieniuKiekis[10]++;
                        break;
                    case 11:
                        gimtadieniuKiekis[11]++;
                        break;
                    case 12:
                        gimtadieniuKiekis[12]++;
                        break;
                }
            }
            return gimtadieniuKiekis;
        }
        /// <summary>
        /// Surandamas mėnesis, kurio metu vyksta daugiausiai gimtadienių.
        /// </summary>
        /// <param name="menesiai">mėnesių masyvas</param>
        /// <returns>mėnesį, kada daugiausia gimtadienių</returns>
        int MenesioKadaDaugiausiaiGimtadieniuGavimas (int[] menesiai)
        {
            int menesisSuDaugiausiaGimtadieniu = 0;
            int k = 0;
            for (int i = 1; i <=12; i++) //i<=12, nes yra 12 mėnesių.
            {
                if (menesiai[i] > k)
                {
                    menesisSuDaugiausiaGimtadieniu = i;
                    k = menesiai[i];
                }
            }
            return menesisSuDaugiausiaGimtadieniu;
        }
        /// <summary>
        /// Sudaromas sąrašas studentų, kurie gimė mėnesį, 
        /// kurio metu vyksta daugiausia gimtadienių.
        /// </summary>
        /// <param name="studentai">studentų sąrašas</param>
        /// <param name="dazniausias">mėnesis, kada vyksta
        /// daugiausia gimtadienių</param>
        /// <returns>studenų sąrašą gimusių mėnesį, kada daugiausia gimtadienių
        /// </returns>
        List<Studentas> SarasasKurieGimeDazniausiaMenesi 
            (List<Studentas> studentai, int dazniausias)
        {
            List<Studentas> gimeDazniausiaMenesi = new List<Studentas>();
            foreach (Studentas studentas in studentai)
            {
                if (dazniausias == studentas.GimimoData.Month)
                {
                    gimeDazniausiaMenesi.Add(studentas);
                }
            }
            return gimeDazniausiaMenesi;
        }
        /// <summary>
        /// Pagal mėnesio numerį nustatomas mėnesio pavadinimas.
        /// </summary>
        /// <param name="menesioNumeris">Mėnesio numeris</param>
        /// <returns>Mėnesio pavadinimą</returns>
        string MenesioPavadinimoNustatymas (int menesioNumeris)
        {
            string menesioPavadinimas = null;
            switch (menesioNumeris)
            {
                case 1:
                    menesioPavadinimas = "Sausis";
                    break;
                case 2:
                    menesioPavadinimas = "Vasaris";
                    break;
                case 3:
                    menesioPavadinimas = "Kovas";
                    break;
                case 4:
                    menesioPavadinimas = "Balandis";
                    break;
                case 5:
                    menesioPavadinimas = "Gegužė";
                    break;
                case 6:
                    menesioPavadinimas = "Birželis";
                    break;
                case 7:
                    menesioPavadinimas = "Liepa";
                    break;
                case 8:
                    menesioPavadinimas = "Rugpjūtis";
                    break;
                case 9:
                    menesioPavadinimas = "Rugsėjis";
                    break;
                case 10:
                    menesioPavadinimas = "Spalis";
                    break;
                case 11:
                    menesioPavadinimas = "Lapkritis";
                    break;
                case 12:
                    menesioPavadinimas = "Gruodis";
                    break;
            }
            return menesioPavadinimas;
        }
        /// <summary>
        /// Randa ieškomo kurso studentus iš studentų sąrašo.
        /// </summary>
        /// <param name="studentai">studentų sąrašas</param>
        /// <returns>Ieškomo kurso studentų sąrašą</returns>
        List<Studentas> IeskomoKursoRadimas
            (List<Studentas> studentai, string ieskomasKursas)
        {
            List<Studentas> IeskomoKursoStudentai = new List<Studentas>();
            foreach (Studentas studentas in studentai)
            {
                if (studentas.Kursas == ieskomasKursas)
                {
                    IeskomoKursoStudentai.Add(studentas);
                }
            }
            return IeskomoKursoStudentai;
        }
    }
}

