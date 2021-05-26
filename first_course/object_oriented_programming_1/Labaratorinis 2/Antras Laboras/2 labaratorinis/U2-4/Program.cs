using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace U2_2
{
    class Program
    {
        public const int MiestuSkaicius = 3;
        public const int MaksimalusAutomobiliuSkaicius = 100;
        static void Main(string[] args)
        {
            Program p = new Program();
            Miestas[] miestai = new Miestas[MiestuSkaicius];
            int seniausioFilialoIndeksas;
            string populiariausiasGamintojas;
            int populiariausioGamintojoAutomobiliuKiekis;
            p.SkaitytiMiestuDuomenis(out miestai);
            p.PradiniuDuomenuPateikimasLentele(miestai);
            p.VidurkiuRadimas(miestai);
            seniausioFilialoIndeksas = p.SeniausioFilialoRadimas(miestai);
            Console.WriteLine("Seniausio filialo duomenys: ");
            p.SpausdintiIKonsole(miestai, seniausioFilialoIndeksas);
            populiariausiasGamintojas = 
                p.PopuliariausioGamintojoRadimas(miestai);
            populiariausioGamintojoAutomobiliuKiekis 
                = p.PopuliariausioGamintojoKiekis(miestai,
                populiariausiasGamintojas);
            Console.WriteLine("Populiariausias gamintojas ir automobilių " +
                "kiekis: {0}, {1}", populiariausiasGamintojas,
                populiariausioGamintojoAutomobiliuKiekis);
            KlaidinguAutoKonteineris klaidingiAutomobiliai
                = p.KlaidingiAutomobiliai(miestai);
            if (klaidingiAutomobiliai.Skaicius == 0)
            {
                Console.WriteLine("Klaidingų automobilių nėra");
            }
            else
            {
                p.SpausdintiAutoKonteineriITekstiniFaila2
                    (klaidingiAutomobiliai);
            }
            AutoKonteineris turinysProblemuSuTechnineApziura
                = new AutoKonteineris(MaksimalusAutomobiliuSkaicius);
            turinysProblemuSuTechnineApziura =
                p.TechninesApziurosPatikrinimas(miestai);
            if (turinysProblemuSuTechnineApziura.Skaicius == 0)
            {
                Console.WriteLine("Automobiliu turinčių problemų" +
                    " su technine apžiūra nėra");
            }
            else
            {
                p.SpausdintiAutoKonteineriITekstiniFaila1
                    (turinysProblemuSuTechnineApziura);
            }
        }
        private Miestas SkaitytiAutomobiliuDuomenis(string failas)
        {
            Miestas miestas = null;   
            using (StreamReader skaitytojas = new StreamReader(@failas))
            {
                string eilute = null;
                string pavadinimas = null;
                string adresas = null;
                string telefonas = null;
                pavadinimas = skaitytojas.ReadLine();
                adresas = skaitytojas.ReadLine();
                telefonas = skaitytojas.ReadLine();
                if (pavadinimas != null && adresas != null && telefonas != null)
                {
                    miestas = new Miestas(pavadinimas, adresas, telefonas);
                }
                while (null != (eilute = skaitytojas.ReadLine()))
                {
                    string[] reiksmes = eilute.Split(';');
                    string valstybiniaiNumeriai = reiksmes[0];
                    string gamintojas = reiksmes[1];
                    string modelis = reiksmes[2];
                    int pagaminimoMetai = int.Parse(reiksmes[3]);
                    int pagaminimoMenuo = int.Parse(reiksmes[4]);
                    DateTime techninesApziurosGaliojimoData =
                        DateTime.Parse(reiksmes[5]);
                    int kuras = int.Parse(reiksmes[6]);
                    double vidutinesKuroSanaudos = double.Parse(reiksmes[7]);

                    Automobilis automobilis =
                        new Automobilis(valstybiniaiNumeriai, gamintojas,
                        modelis, pagaminimoMetai, pagaminimoMenuo,
                        techninesApziurosGaliojimoData,
                        kuras, vidutinesKuroSanaudos);
                    miestas.Automobiliai.PridetiAuto(automobilis);
                }
            }
            return miestas;
        }
        /// <summary>
        /// Suranda duomenis, kuriuos reikia nuskaityti
        /// </summary>
        /// <param name="miestai">Grąžina miestų duomenis</param>
        private void SkaitytiMiestuDuomenis(out Miestas[] miestai)
        {
            miestai = new Miestas[MiestuSkaicius];
            string[] failuKeliai =
                Directory.GetFiles(Directory.GetCurrentDirectory(), 
                "L2Data*.csv");
            int k = 0;
            foreach (string kelias in failuKeliai)
            {
                miestai[k++] = SkaitytiAutomobiliuDuomenis(kelias);
            }
        }
        /// <summary>
        /// Metodas, kuris pradinius duomenis pateikia lentele atskirame faile.
        /// </summary>
        /// <param name="miestas">Filialų rinkinys</param>
        void PradiniuDuomenuPateikimasLentele(Miestas[] miestas)
        {
            using (StreamWriter spausdintojas
                = new StreamWriter(@"PradiniaiDuomenys.txt")) 
            {
                spausdintojas.WriteLine("Pradiniai duomenys: ");
                spausdintojas.WriteLine("-----------------------------------" +
                    "-------------------------------------------------------" +
                    "-------------------------------------------------------" +
                    "----------");
                for (int i = 0; i < miestas.Length; i++)
                {
                    spausdintojas.WriteLine("{0}", miestas[i].Pavadinimas);
                    spausdintojas.WriteLine("{0}", miestas[i].Adresas);
                    spausdintojas.WriteLine("{0}", miestas[i].Telefonas);
                    spausdintojas.WriteLine("------------------" +
                        "-----------------" +
                    "-------------------------------------------------------" +
                    "-------------------------------------------------------" +
                    "----------");
                    spausdintojas.WriteLine("{0,-22}{1,-16}{2,-11}{3,-21}" +
                        "{4,-20}{5,-33}{6,-7}{7,-28}",
                        "|Valstybiniai numeriai","|Gamintojas","|Modelis",
                        "|Pagaminimo metai","|Pagaminimo menuo",
                        "|Tech. apziuros galiojimo data",
                        "| Kuras","|Vidutines kuro sanaudos|");
                    
                    for (int j = 0; j < miestas[i].Automobiliai.Skaicius; j++)
                    {
                        spausdintojas.WriteLine(miestas[i]
                            .Automobiliai.PaimtiAuto(j).ToString());
                    }
                    spausdintojas.WriteLine("------------------" +
                        "-----------------" +
                    "-------------------------------------------------------" +
                    "-------------------------------------------------------" +
                    "----------");
                }
            }
        }
        /// <summary>
        /// Spausdina duomenis į konsolę
        /// </summary>
        /// <param name="miestas">miestų konteineris</param>
        /// <param name="indeksas">kurio miesto duomenis spausdinti</param>
        private void SpausdintiIKonsole(Miestas[] miestas, int indeksas)
        {
            for (int i = 0; i < miestas[indeksas].Automobiliai.Skaicius; i++)
            {
                Console.WriteLine(miestas[indeksas].
                    Automobiliai.PaimtiAuto(i).ToString());
            }
        }
        /// <summary>
        /// Spausdina duomenis į tekstinį failą
        /// </summary>
        /// <param name="automobiliai">autombilių konteineris</param>
        private void SpausdintiAutoKonteineriITekstiniFaila1
            (AutoKonteineris automobiliai)
        {
            using (StreamWriter spausdintojas =
                new StreamWriter(@"Apžiūra.csv"))
            {
                spausdintojas.WriteLine("{0,-16},{1,-11},{2,-22},{3,-33},",
                    "Gamintojas","Modelis","Valstybiniai numeriai",
                    "Techn. apziuros galiojimo data");
                for (int i = 0; i < automobiliai.Skaicius; i++)
                {
                    spausdintojas.WriteLine("{0,-16},{1,-11},{2,-22},{3,-33}" +
                        ",{4,8}",
                        automobiliai.PaimtiAuto(i).Gamintojas,
                        automobiliai.PaimtiAuto(i).Modelis,
                        automobiliai.PaimtiAuto(i).ValstybiniaiNumeriai,
                        automobiliai.PaimtiAuto(i).
                        TechninesApziurosGaliojimoData,
                        automobiliai.PaimtiAuto(i).Skubiai);
                }
            }
        }
        /// <summary>
        /// Spausdina duomenis į tekstinį failą
        /// </summary>
        /// <param name="automobiliai">autombilių konteineris</param>
        private void SpausdintiAutoKonteineriITekstiniFaila2
            (KlaidinguAutoKonteineris automobiliai)
        {
            using (StreamWriter spausdintojas =
                new StreamWriter(@"Klaidos.csv"))
            {
                spausdintojas.WriteLine("{0,-25},{1,-11},{2,-50}",
                    "Valstybiniai numeriai", "Modelis", "Filialiai" +
                    " kuriems priklauso");
                for (int i = 0; i < automobiliai.Skaicius; i++)
                {
                    spausdintojas.Write("{0,-25},{1,-11},",
                        automobiliai.PaimtiAuto(i).ValstybiniaiNumeriai,
                        automobiliai.PaimtiAuto(i).Modelis);
                    for (int j = 0; j < automobiliai.PaimtiAuto(i).
                        Filialai.Count; j++)
                    {
                        spausdintojas.Write(automobiliai.PaimtiAuto(i).
                            Filialai[j]);
                        spausdintojas.Write(" ");
                    }
                    spausdintojas.WriteLine(",");
                }

            }
        }
        /// <summary>
        /// Suranda kiekvieno filialo automobilių amžiaus vidurkį 
        /// </summary>
        /// <param name="miestas">Miestų konteineris</param>
        private void VidurkiuRadimas(Miestas[] miestas)
        {
            for (int i = 0; i < miestas.Length; i++)
            {
                double suma = 0;
                double laikinasisKintamasis = 0;
                for (int j = 0; j < miestas[i].Automobiliai.Skaicius; j++)
                {
                    DateTime data = 
                        new DateTime(miestas[i].Automobiliai.
                        PaimtiAuto(j).PagaminimoMetai,
                        miestas[i].Automobiliai.PaimtiAuto(j).
                        PagaminimoMenuo, 1);
                    suma += data.Ticks;
                }
                laikinasisKintamasis = suma / miestas[i].Automobiliai.Skaicius;
                miestas[i].AutomobiliuAmziausVidurkis = 
                    new DateTime((long)laikinasisKintamasis);
            }
        }
        /// <summary>
        /// Randa seniausią filialą
        /// </summary>
        /// <param name="miestas">miestų konteineris</param>
        /// <returns>seniausio filialo indeksą</returns>
        private int SeniausioFilialoRadimas (Miestas[] miestas)
        {
            DateTime seniausi = miestas[0].AutomobiliuAmziausVidurkis;
            int indeksas = 0;
            for (int i = 0; i < miestas.Length; i++)
            {
                if (seniausi > miestas[i].AutomobiliuAmziausVidurkis)
                {
                    seniausi = miestas[i].AutomobiliuAmziausVidurkis;
                    indeksas = i;
                }
            }
            return indeksas;
        }
        /// <summary>
        /// Randa populiariausiojo gamintojo pavadinimą
        /// </summary>
        /// <param name="miestas">miestų konteineris</param>
        /// <returns>populiariausio gamintojo pavadinimą</returns>
        string PopuliariausioGamintojoRadimas (Miestas[] miestas)
        {
            //int k = 0;
            int PopuliariausioGamintojoAutomobiliuKiekis = 0;
            string populiariausiasGamintojas = null;
            int z = 0;
            for (int i = 0; i < miestas.Length; i++)
            {
                for (int j = 0; j < miestas[i].Automobiliai.Skaicius; j++)
                {
                    z = 0;
                    for (int l = 0; l < miestas[i].Automobiliai.Skaicius; l++)
                    {
                        if (miestas[i].Automobiliai.PaimtiAuto(j).Gamintojas
                            == miestas[i].Automobiliai.PaimtiAuto(l).Gamintojas)
                        {
                            z++;
                        }
                    }

                    if (z > PopuliariausioGamintojoAutomobiliuKiekis)
                    {                       
                        PopuliariausioGamintojoAutomobiliuKiekis = z;
                        populiariausiasGamintojas = 
                            miestas[i].Automobiliai.PaimtiAuto(j).Gamintojas;
                    }
                }
            }
            return populiariausiasGamintojas;
        }
        /// <summary>
        /// Randa populiariausio gamintojo mašinų kiekį
        /// </summary>
        /// <param name="miestas">miestų konteineris</param>
        /// <param name="gamintojas">gamintojo pavadinimas</param>
        /// <returns>populiariausio gamintojo mašinų kiekį</returns>
        int PopuliariausioGamintojoKiekis (Miestas[] miestas, string gamintojas)
        {
            int kiekis = 0;
            for (int i = 0; i < miestas.Length; i++)
            {
                for (int j = 0; j < miestas[i].Automobiliai.Skaicius; j++)
                {
                    if (miestas[i].Automobiliai.PaimtiAuto(j).Gamintojas == 
                        gamintojas)
                    {
                        kiekis++;
                    }
                }
            }
            return kiekis;
        }
        /// <summary>
        /// Suranda klaidingus automobilius
        /// </summary>
        /// <param name="miestas">miestu konteineris</param>
        /// <returns>klaidingų automobilių konteinerį</returns>
        private KlaidinguAutoKonteineris KlaidingiAutomobiliai 
            (Miestas[] miestas)
        {
            KlaidinguAutoKonteineris klaidingiAutomobiliai 
                = new KlaidinguAutoKonteineris(MaksimalusAutomobiliuSkaicius);
            int m = 0;
            int z = 0;
            for (int i = 0; i < miestas.Length; i++)
            {

                if (i + 1 == miestas.Length)
                {
                    z++;
                    if (m==1)
                    {
                        break;
                    }
                    i--;
                    m++;
                }
                for (int j = 0; j < miestas[z].Automobiliai.Skaicius; j++)
                {
                    for (int k = 0; k < miestas[i + 1].Automobiliai.Skaicius;
                        k++)
                    {
                        if (miestas[z].Automobiliai.PaimtiAuto(j) 
                            == miestas[i + 1].Automobiliai.PaimtiAuto(k))
                        {
                            KlaidingasAutomobilis automobilis =
                                new KlaidingasAutomobilis
                                (miestas[i+1].Automobiliai.PaimtiAuto(k).
                                ValstybiniaiNumeriai,
                                miestas[i+1].Automobiliai.PaimtiAuto(k).
                                Modelis);
                            if (klaidingiAutomobiliai.Turi(automobilis))
                            {
                                int o1 = -1;
                                for (int o = 0; 
                                    o < klaidingiAutomobiliai.Skaicius; o++)
                                {
                                    o1++;
                                    if (klaidingiAutomobiliai.
                                        PaimtiAuto(o).ValstybiniaiNumeriai 
                                        == automobilis.ValstybiniaiNumeriai )
                                    {
                                        break;
                                    }
                                }
                                if (klaidingiAutomobiliai.PaimtiAuto(o1).
                                    Filialai.Count == MiestuSkaicius)
                                {
                                    break;
                                }
                                klaidingiAutomobiliai.PaimtiAuto(o1).Filialai.
                                    Add(miestas[i+1].Pavadinimas);
                            }
                            else
                            {
                                automobilis.Filialai.Add(miestas[z].
                                    Pavadinimas);
                                automobilis.Filialai.Add(miestas[i+1].
                                    Pavadinimas);
                                klaidingiAutomobiliai.PridetiAuto(automobilis);
                            }
                        }
                    }
                }
            }
            return klaidingiAutomobiliai;
        }
        
        /// <summary>
        /// Suranda automobilius, kurių techninė apžiūra baigsis už mėnesio,
        /// arba jau yra pasibaigusi
        /// </summary>
        /// <param name="miestai">miestų konteineris</param>
        /// <returns>Automobilių konteinerį, kurių techninė apžiūra
        /// baigiasi, arba jau yra pasibaigusi</returns>
        private AutoKonteineris TechninesApziurosPatikrinimas(Miestas[] miestai)
        {
            AutoKonteineris beTechninesApziuros =
                new AutoKonteineris(MaksimalusAutomobiliuSkaicius);
            for (int i = 0; i < miestai.Length; i++)
            {
                for (int j = 0; j < miestai[i].Automobiliai.Skaicius; j++)
                {
                    if (miestai[i].Automobiliai.PaimtiAuto(j).
                        TechninesApziurosGaliojimoData.
                        AddMonths(-1) < DateTime.Now)
                    {
                        if (miestai[i].Automobiliai.PaimtiAuto(j).
                            TechninesApziurosGaliojimoData < DateTime.Now)
                        {
                            Automobilis automobilis =
                                new Automobilis(miestai[i].Automobiliai.
                                PaimtiAuto(j).Gamintojas,
                                miestai[i].Automobiliai.PaimtiAuto(j).Modelis,
                                miestai[i].Automobiliai.PaimtiAuto(j).
                                ValstybiniaiNumeriai,
                                miestai[i].Automobiliai.PaimtiAuto(j).
                                TechninesApziurosGaliojimoData);
                            automobilis.Skubiai = "SKUBIAI";
                            beTechninesApziuros.PridetiAuto(automobilis);
                        }
                        else
                        {
                            Automobilis automobilis = 
                                new Automobilis(miestai[i].Automobiliai.
                                PaimtiAuto(j).Gamintojas,
                                miestai[i].Automobiliai.PaimtiAuto(j).Modelis,
                                miestai[i].Automobiliai.PaimtiAuto(j).
                                ValstybiniaiNumeriai,
                                miestai[i].Automobiliai.PaimtiAuto(j).
                                TechninesApziurosGaliojimoData);
                            beTechninesApziuros.PridetiAuto(automobilis);
                        }
                    }
                }
            }
            return beTechninesApziuros;
        }
    }
}
