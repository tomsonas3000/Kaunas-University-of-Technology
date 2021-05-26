using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Knyga1 = @"..\..\Knyga1.txt";
            const string Knyga2 = @"..\..\Knyga2.txt";
            const string Rodikliai = @"..\..\Rodikliai.txt";
            const string ManoKnyga = @"..\..\ManoKnyga.txt";
            char[] skyrikliai = { ' ', ',', '.', ';', ':' };
            int ilgis = 0;
            ZodziuKonteineris SarasasIsAbiejuFailu = new ZodziuKonteineris(100);
            ZodziuKonteineris SarasasPirmoFailo = new ZodziuKonteineris(100);
            ZodziuKonteineris SarasasAntroFailo = new ZodziuKonteineris(100);
            ZodziuKonteineris Tekstas1 = new ZodziuKonteineris(100);
            ZodziuKonteineris Tekstas2 = new ZodziuKonteineris(100);
            Program p = new Program();
            ilgis = p.RastiIlgiosiuZodziuIlgi(Knyga1, Knyga2, skyrikliai);
            p.AtrinktiZodzius(Knyga1, Knyga2, skyrikliai, ilgis, 
                SarasasPirmoFailo, SarasasAntroFailo, SarasasIsAbiejuFailu);
            p.IlgiausiuZodziuSpausdinimas(Rodikliai, SarasasPirmoFailo, 
                SarasasIsAbiejuFailu);
            p.SudetiIKonteineri(Knyga1, Tekstas1);
            p.SudetiIKonteineri(Knyga2, Tekstas2);
            p.Spausdinti(Tekstas1, Tekstas2, ManoKnyga);
            Console.ReadKey();
        }
        /// <summary>
        /// Randa ilgiausio žodžio ilgį
        /// </summary>
        /// <param name="failas1">pirmo failo kelias</param>
        /// <param name="failas2">antro failo kelias</param>
        /// <param name="skyrikliai">skyrikliai skiriantys žodžius</param>
        /// <returns>ilgiausio žodžio ilgį</returns>
        int RastiIlgiosiuZodziuIlgi(string failas1, string failas2, 
            char[] skyrikliai)
        {
            int ilgis1 = RastiIlgiausiuZodiuIlgiFaile(failas1, skyrikliai);
            int ilgis2 = RastiIlgiausiuZodiuIlgiFaile(failas2, skyrikliai);
            if (ilgis1 > ilgis2)
            {
                return ilgis1;
            }
            else
            {
                return ilgis2;
            }

        }
        /// <summary>
        /// Randa ilgiausią žodžio ilgį faile
        /// </summary>
        /// <param name="failas">failo kelias</param>
        /// <param name="skyrikliai">skyrikliai skiriantys žodžius</param>
        /// <returns>Ilgiausio žodžio ilgį faile</returns>
        int RastiIlgiausiuZodiuIlgiFaile(string failas, char[] skyrikliai)
        {
            string eilute = null;
            int ilgis = 0;
            using (StreamReader skaitytojas = new StreamReader(failas, 
                Encoding.GetEncoding(1257)))
            {
                while ((eilute = skaitytojas.ReadLine()) != null)
                {
                    string[] zodziai = eilute.Split(skyrikliai);
                    foreach (string zodis in zodziai)
                    {
                        if (zodis.Length > ilgis)
                        {
                            ilgis = zodis.Length;
                        }
                    }
                }
            }
            return ilgis;
        }
        /// <summary>
        /// Sudeda žodžius į bendrą sąrašą, panaikina nereikalingus žodžius iš 
        /// pirmojo failo sąrašo
        /// </summary>
        /// <param name="failas1">pirmojo failo kelias</param>
        /// <param name="failas2">antrojo failo kelias</param>
        /// <param name="skyrikliai">skyrikliai skiriantys žodžius</param>
        /// <param name="ilgis">ilgiausio žodžio ilgis</param>
        /// <param name="SarasasIsPirmoFailo">ilgiausių žodžių sąrašas
        /// iš pirmo failo</param>
        /// <param name="SarasasIsAntroFailo">ilgiausių žodžių sąrašas iš
        /// antro failo</param>
        /// <param name="SarasasIsAbiejuFailu">ilgiausių žodžių sąrašas
        /// iš abiejų failų</param>
        void AtrinktiZodzius(string failas1, string failas2, char[] skyrikliai,
            int ilgis, ZodziuKonteineris SarasasIsPirmoFailo,
            ZodziuKonteineris SarasasIsAntroFailo, 
            ZodziuKonteineris SarasasIsAbiejuFailu)
        {
            AtrinktiZodziusIsFailo(failas1, skyrikliai, SarasasIsPirmoFailo, 
                ilgis);
            AtrinktiZodziusIsFailo(failas2, skyrikliai, SarasasIsAntroFailo, 
                ilgis);
            if (SarasasIsPirmoFailo.Skaicius > SarasasIsAntroFailo.Skaicius)
            {
                SudetiIVienaSarasa(SarasasIsAntroFailo, SarasasIsPirmoFailo, 
                    SarasasIsAbiejuFailu, '1');
            }
            else
            {
                SudetiIVienaSarasa(SarasasIsPirmoFailo, SarasasIsAntroFailo, 
                    SarasasIsAbiejuFailu, '2');
            }
            for (int i = 0; i < SarasasIsPirmoFailo.Skaicius; i++)
            {
                for (int j = 0; j < SarasasIsAntroFailo.Skaicius; j++)
                {
                    if (SarasasIsPirmoFailo.PaimtiZodi(i).Pavadinimas.Equals
                        (SarasasIsAntroFailo.PaimtiZodi(j).Pavadinimas, 
                        StringComparison.OrdinalIgnoreCase))
                    {
                        SarasasIsPirmoFailo.PasalintiZodi
                            (SarasasIsPirmoFailo.PaimtiZodi(i));
                    }
                }
            }
        }
        /// <summary>
        /// Atrenka žodžius, kurių ilgiai sutampa su ilgiausio žodžio ilgiu
        /// </summary>
        /// <param name="failas">failo kelias</param>
        /// <param name="skyrikliai">skyrikliai skiriantys žodžius</param>
        /// <param name="ZodziuSarasas">ilgiausių žodžių sąrašas</param>
        /// <param name="ilgis">ilgiausio žodžio ilgis</param>
        void AtrinktiZodziusIsFailo(string failas, char[] skyrikliai, 
            ZodziuKonteineris ZodziuSarasas, int ilgis)
        {
            string eilute = null;
            using (StreamReader skaitytojas = new StreamReader(failas,
                Encoding.GetEncoding(1257)))
            {

                while ((eilute = skaitytojas.ReadLine()) != null)
                {
                    string[] zodziai = eilute.Split(skyrikliai);
                    foreach (string zodis in zodziai)
                    {
                        if (zodis.Length == ilgis)
                        {
                            int skaitliukas = 0;
                            Zodis zodis1 = new Zodis(zodis);
                            for (int i = 0; i < ZodziuSarasas.Skaicius; i++)
                            {
                                if (ZodziuSarasas.PaimtiZodi(i).Pavadinimas.
                                    Equals(zodis1.Pavadinimas, 
                                    StringComparison.OrdinalIgnoreCase))
                                {
                                    skaitliukas++;
                                    if (failas.EndsWith("1.txt"))
                                    {
                                        ZodziuSarasas.PaimtiZodi(i).Kiekis1++;
                                    }
                                    else
                                    {
                                        ZodziuSarasas.PaimtiZodi(i).Kiekis2++;
                                    }
                                }
                            }
                            if (skaitliukas == 0)
                            {
                                ZodziuSarasas.PridetiZodi(zodis1);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Sudeda ilgiausius žodžius, kurie yra abiejuose failuose, į vieną 
        /// sąrašą
        /// </summary>
        /// <param name="PirmasSarasas">ilgiausi žodžiai iš pirmo failo</param>
        /// <param name="AntrasSarasas">ilgiausi žodžiai iš antro failo</param>
        /// <param name="BendrasSarasas">ilgiausi žodžiai iš abiejų failų
        /// </param>
        /// <param name="kelintas"></param>
        void SudetiIVienaSarasa(ZodziuKonteineris PirmasSarasas, 
            ZodziuKonteineris AntrasSarasas, ZodziuKonteineris BendrasSarasas,
            char kelintas)
        {
            for (int i = 0; i < PirmasSarasas.Skaicius; i++)
            {
                for (int j = 0; j < AntrasSarasas.Skaicius; j++)
                {
                    if (PirmasSarasas.PaimtiZodi(i).Pavadinimas.Equals
                        (AntrasSarasas.PaimtiZodi(j).Pavadinimas, 
                        StringComparison.OrdinalIgnoreCase))
                    {
                        if (BendrasSarasas.Skaicius <= 10)
                        {
                            BendrasSarasas.PridetiZodi
                                (PirmasSarasas.PaimtiZodi(i));
                            if (kelintas.Equals('1'))
                            {
                                BendrasSarasas.PaimtiZodi(i).Kiekis1 = 
                                    AntrasSarasas.PaimtiZodi(j).Kiekis1;
                            }
                            else
                            {
                                BendrasSarasas.PaimtiZodi(i).Kiekis2 = 
                                    AntrasSarasas.PaimtiZodi(j).Kiekis2;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Rašo duomenis į duomenų failą
        /// </summary>
        /// <param name="failas">failo kelias</param>
        /// <param name="IlgiausiZodziai1">pirmojo failo ilgiausių žodžių
        /// sąrašas</param>
        /// <param name="IlgiausiZodziaiBendras">ilgiausių žodžių sąrašas
        /// iš abiejų failų</param>
        void IlgiausiuZodziuSpausdinimas(string failas, ZodziuKonteineris 
            IlgiausiZodziai1, ZodziuKonteineris IlgiausiZodziaiBendras)
        {
            using (StreamWriter rasytojas = new StreamWriter(@failas))
            {
                rasytojas.WriteLine("Dažniausių žodžių sąrašas iš pirmo failo");
                IlgiausiuZodziuSpausdinimas2(rasytojas, IlgiausiZodziai1, '1');
                rasytojas.WriteLine
                    ("Dažniausių žodžių sąrašas iš abiejų failų");
                IlgiausiuZodziuSpausdinimas2(rasytojas, IlgiausiZodziaiBendras,
                    '2');

                if (IlgiausiZodziai1.Skaicius == 0)
                {
                    Console.WriteLine("Pirmame faile tokių žodžių nėra");
                }

                if (IlgiausiZodziaiBendras.Skaicius == 0)
                {
                    Console.WriteLine("Per abudu failus tokių žodžių nėra");
                }
            }
        }
        /// <summary>
        /// Rašo duomenis į duomenų failą
        /// </summary>
        /// <param name="rasytojas"></param>
        /// <param name="IlgiausiZodziai">sąrašas ilgiausių žodžių</param>
        /// <param name="kelintas">ar spausdinti kaip pirmą ar kaip
        /// abiejų failų duomenis</param>
        void IlgiausiuZodziuSpausdinimas2(StreamWriter rasytojas,
            ZodziuKonteineris IlgiausiZodziai, char kelintas)
        {
            int a = 0;
            if (IlgiausiZodziai.Skaicius >= 10)
            {
                a = 10;
            }
            else
            {
                a = IlgiausiZodziai.Skaicius;
            }
            if (kelintas == '1')
            {
                rasytojas.WriteLine("------------------------------------" +
                    "-------");
                rasytojas.WriteLine("|{0,-20}|{1,-20}|", "Žodis",
                    "Kiekis pirmam faile");
                for (int i = 0; i < a; i++)
                {
                    rasytojas.WriteLine("--------------------------------" +
                        "-----------");
                    rasytojas.WriteLine("|{0,-20}|{1,20}|",
                        IlgiausiZodziai.PaimtiZodi(i).Pavadinimas,
                        IlgiausiZodziai.PaimtiZodi(i).Kiekis1);
                }
                rasytojas.WriteLine("-----------------------------------" +
                    "--------");
            }
            else
            {
                rasytojas.WriteLine("------------------------------------" +
                    "----------------------------");
                rasytojas.WriteLine("|{0,-20}|{1,-20}|{2,-20}|", "Žodis",
                    "Kiekis pirmam faile", "Kiekis antram faile");
                for (int i = 0; i < a; i++)
                {
                    rasytojas.WriteLine("-------------------------------" +
                        "---------------------------------");
                    rasytojas.WriteLine("|{0,-20}|{1,20}|{2,20}|",
                        IlgiausiZodziai.PaimtiZodi(i).Pavadinimas,
                        IlgiausiZodziai.PaimtiZodi(i).Kiekis1,
                        IlgiausiZodziai.PaimtiZodi(i).Kiekis2);
                }
                    rasytojas.WriteLine("-------------------------------" +
                        "---------------------------------");
            }

        }
        /// <summary>
        /// Sudeda žodžius ir skyriklius į konteinerį
        /// </summary>
        /// <param name="failas">failo kelias</param>
        /// <param name="Tekstas">konteineris, skirtas žodžių ir skyriklių
        /// saugojimui</param>
        void SudetiIKonteineri(string failas, ZodziuKonteineris Tekstas)
        {
            string eilute = null;
            using (StreamReader skaitytojas = new StreamReader(failas, 
                Encoding.GetEncoding(1257)))
            {
                while ((eilute = skaitytojas.ReadLine()) != null)
                {
                    Regex zodziams = new Regex
                        (@"[!(/)a-zA-zĄąČčĘęĖėĮįŠšŲųŪūŽž]+");
                    Regex simboliams = new Regex(@"[\s,.;:\t\r\n]");
                    string[] simboliai = SudetiSimbolius(simboliams, eilute, 
                        out int masyvoIlgis1);
                    string[] zodziai = SudetiZodzius(zodziams, eilute,
                        out int masyvoIlgis2);
                    for (int i = 0; i < masyvoIlgis2 + 1; i++)
                    {
                        Zodis zodis = new Zodis
                        {
                            Simboliai = simboliai[i],
                            Pavadinimas = zodziai[i]
                        };
                        Tekstas.PridetiZodi(zodis);
                    }
                }
            }
            if (Tekstas.Skaicius == 0)
            {
                Console.WriteLine("Teksto nėra");
            }
        }
        /// <summary>
        /// Sudeda skyriklius į konteinerį
        /// </summary>
        /// <param name="simboliams">Regex kintamasis skirtas
        /// atrinkti skyriklius</param>
        /// <param name="eilute">duomenų failo eilutė</param>
        /// <param name="masyvoIlgis1">skyriklių eilutėje skaičius</param>
        /// <returns>skyriklius vienoje eilutėje</returns>
        string[] SudetiSimbolius(Regex simboliams, string eilute,
            out int masyvoIlgis1)
        {
            masyvoIlgis1 = 0;
            string[] simboliai = new string[100];
            for (int i = 0; i < simboliams.Matches(eilute).Count; i++)
            {
                string simboliuSeka = simboliams.Matches(eilute)[i].ToString();
                simboliai[i] = simboliuSeka;
                masyvoIlgis1++;
            }
            simboliai[masyvoIlgis1] = System.Environment.NewLine;
            return simboliai;
        }
        /// <summary>
        /// Sudeda žodžius į konteinerį
        /// </summary>
        /// <param name="zodziams">Regex kintamasis, skirtas
        /// atskirti žodžius</param>
        /// <param name="eilute">duomenų eilutė iš failo</param>
        /// <param name="masyvoIlgis2">žodžių skaičius eilutėje</param>
        /// <returns></returns>
        string[] SudetiZodzius(Regex zodziams, string eilute, 
            out int masyvoIlgis2)
        {
            masyvoIlgis2 = 0;
            string[] zodziai = new string[100];
            for (int i = 0; i < zodziams.Matches(eilute).Count; i++)
            {
                string zodziuSeka = zodziams.Matches(eilute)[i].ToString();
                zodziai[i] = zodziuSeka;
                masyvoIlgis2++;
            }
            return zodziai;
        }
        /// <summary>
        /// Spausdina tekstą pagal sąlygas į duomenų failą
        /// </summary>
        /// <param name="Tekstas1">pirmojo failo konteineris</param>
        /// <param name="Tekstas2">antrojo failo konteineris</param>
        /// <param name="failas">failo kelias</param>
        void Spausdinti(ZodziuKonteineris Tekstas1, ZodziuKonteineris Tekstas2,
            string failas)
        {
            using (StreamWriter rasytojas = new StreamWriter(@failas))
            {
                bool arKartojosi = false;
                ZodziuKonteineris Pasikartojimai = new ZodziuKonteineris(100);
                Spausdinti1(Tekstas1, Tekstas2, rasytojas, 0, 0, arKartojosi);
                
            }
        }
        /// <summary>
        /// Spausdina pirmojo failo tekstą pagal sąlygas
        /// </summary>
        /// <param name="Tekstas1">pirmojo failo konteineris</param>
        /// <param name="Tekstas2">antrojo failo konteineris</param>
        /// <param name="rasytojas"></param>
        /// <param name="indeksas1">indeksas, parodantis nuo kur rašyti
        /// pirmąjį failą</param>
        /// <param name="indeksas3">indeksas, parodantis, nuo kur skaityti
        /// antrąjį failą</param>
        /// <param name="arKartojosi">parodo, ar buvo žodžių, kurie sutapo
        /// per failus</param>
        void Spausdinti1 (ZodziuKonteineris Tekstas1, 
            ZodziuKonteineris Tekstas2, StreamWriter rasytojas,
            int indeksas1, int indeksas3,
            bool arKartojosi)
        {
            int c = 0;
            for (int a = indeksas1; a < Tekstas1.Skaicius; a++)
            {
                c = 0;
                for (int b = 0; b < Tekstas2.Skaicius; b++)
                {
                    if ((Tekstas1.PaimtiZodi(a).Pavadinimas != 
                        Tekstas2.PaimtiZodi(b).Pavadinimas) && c == 0)
                    {
                        rasytojas.Write("{0}{1}", 
                            Tekstas1.PaimtiZodi(a).Pavadinimas, 
                            Tekstas1.PaimtiZodi(a).Simboliai);
                        c++;
                    }
                    if ((Tekstas1.PaimtiZodi(a).Pavadinimas == 
                        Tekstas2.PaimtiZodi(b).Pavadinimas) && 
                        Tekstas1.PaimtiZodi(a).Pavadinimas != null)
                    {
                        arKartojosi = true;
                       rasytojas.Write("\r\n");
                       SpausdintiExtraVariantas(Tekstas1, Tekstas2, a, 
                           rasytojas, indeksas3, 
                           arKartojosi);
                    }
                }
                if (a == Tekstas1.Skaicius - 1 && arKartojosi == false)
                {
                    for (int j = 0; j < Tekstas2.Skaicius; j++)
                    {
                        rasytojas.Write("{0}{1}", 
                            Tekstas2.PaimtiZodi(j).Pavadinimas, 
                            Tekstas2.PaimtiZodi(j).Simboliai);
                    }
                }
            }
        }
        /// <summary>
        /// Spausdina antrojo failo tekstą į rezultatų failą pagal sąlygas
        /// </summary>
        /// <param name="Tekstas1">pirmojo failo konteineris</param>
        /// <param name="Tekstas2">antrojo failo konteineris</param>
        /// <param name="indeksas2">parodo nuo kur skaityti pirmąjį failą
        /// </param>
        /// <param name="rasytojas"></param>
        /// <param name="indeksas3">parodo nuo kur rašyti antrąjį failą</param>
        /// <param name="arKartojosi">parodo, ar buvo pasikartojančių
        /// žodžių per abudu failus</param>
        void SpausdintiExtraVariantas(ZodziuKonteineris Tekstas1,
            ZodziuKonteineris Tekstas2, int indeksas2, StreamWriter rasytojas,
            int indeksas3, 
            bool arKartojosi)
        {
            for (int i = indeksas3; i < Tekstas2.Skaicius; i++)
            {
                int c = 0;
                for(int j = indeksas2+1; j < Tekstas1.Skaicius; j++)
                {
                    if ((Tekstas2.PaimtiZodi(i).Pavadinimas != 
                        Tekstas1.PaimtiZodi(j).Pavadinimas) && c == 0)
                    {
                        rasytojas.Write("{0}{1}", 
                            Tekstas2.PaimtiZodi(i).Pavadinimas,
                            Tekstas2.PaimtiZodi(i).Simboliai);
                        
                        c++;
                    }
                    if((Tekstas2.PaimtiZodi(i).Pavadinimas == 
                        Tekstas1.PaimtiZodi(j).Pavadinimas) && 
                        Tekstas2.PaimtiZodi(i).Pavadinimas != null)
                    {
                        rasytojas.Write("\r\n");
                        Spausdinti1(Tekstas1, Tekstas2, rasytojas, indeksas2+1,
                            i + 1, arKartojosi); 
                    }
                }
            }
        }
    }
}
