using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace L4
{
    public partial class L4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox2.Visible = false;
            TextBox3.Visible = false;
            TextBox4.Visible = false;
            Table1.Visible = false;
            Table3.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sandelis = "Sandelis*.txt";
            string uzsakymasPaieska = "Uzsakymas.txt";
            string rezultatuKelias = 
                Server.MapPath("App_Data/PradiniaiDuomenysIrRezultatai.txt");
            string isimciuKelias = Server.MapPath("App_Data/Isimtys.txt");
            var Vertybes = new LinkedList<Vertybe>();
            var Uzsakymai = new LinkedList<Uzsakymas>();
            var RastosVertybes = new LinkedList<Vertybe>();
            SukurtiFaila(isimciuKelias);
            SukurtiFaila(rezultatuKelias);
            string[] keliaiSandeliu = Directory.GetFiles
                (Server.MapPath("App_Data"), sandelis);
            string[] keliasUzsakymo = Directory.GetFiles
                (Server.MapPath("App_Data"), uzsakymasPaieska);
            SkaitytiDuomenis(Vertybes, keliaiSandeliu, Uzsakymai, 
                keliasUzsakymo, isimciuKelias);
            SpausdintiPradiniusDuomenis(rezultatuKelias, Vertybes,
                Uzsakymai, "Pradiniai duomenys", isimciuKelias);
            SpausdintiPradiniusDuomenisIEkrana(Vertybes, Uzsakymai);
            RastosVertybes = SurastiVertybes(Uzsakymai, Vertybes);
            List<Vertybe> RastosVertybesList = RastosVertybes.ToList();
            RastosVertybesList.Sort();

            decimal uzsakymoSuma = SumosSkaiciavimas(RastosVertybesList);
            decimal ivestaSuma = decimal.Parse(TextBox1.Text);
            if (uzsakymoSuma > ivestaSuma)
            {
                uzsakymoSuma = VertybiuMazinimas(uzsakymoSuma,
                    ivestaSuma, RastosVertybesList);
                RastosVertybesList.Sort();
                TextBox4.Visible = true;
                TextBox4.Text = "Kadangi užsisakėte daugiau, " +
                    "nei galite nusipirkti, užsakymą apkarpėme.";
            }
            SpausdintiRezultatuDuomenisIEkrana(RastosVertybesList, 
                uzsakymoSuma);
            SpausdintiRezultatus(rezultatuKelias, RastosVertybesList,
                uzsakymoSuma, isimciuKelias);




        }
        /// <summary>
        /// Sukuria tuščią failą
        /// </summary>
        /// <param name="kelias">failo kelias</param>
        protected void SukurtiFaila(string kelias)
        {
            using(StreamWriter rasytojas = new StreamWriter(kelias))
            {
                
            }
        }
        /// <summary>
        /// Išspausdina pradinius duomenis į tinklapio aplinką
        /// </summary>
        /// <param name="Vertybes">Vertybių sąrašas</param>
        /// <param name="Uzsakymai">Užsakymų sąrašas</param>
        protected void SpausdintiPradiniusDuomenisIEkrana(
            LinkedList<Vertybe> Vertybes, LinkedList<Uzsakymas> Uzsakymai)
        {
            VertybiuSpausdinimas(Vertybes.ToList(), Table1);
            UzsakymuSpausdinimas(Uzsakymai);
        }
        /// <summary>
        /// Spausdina rezultatus į tinklapio aplinką
        /// </summary>
        /// <param name="Vertybes">Vertybių sąrašas</param>
        /// <param name="suma">Sumą, kurią reikia sumokėti</param>
        protected void SpausdintiRezultatuDuomenisIEkrana(
            List<Vertybe> Vertybes, decimal suma)
        {
            TextBox2.Visible = true;
            VertybiuSpausdinimas(Vertybes,Table3);
            if (suma != 0)
            {
                TextBox2.Text = "Suma, kurią reikia sumokėti";
                TextBox3.Visible = true;
                TextBox3.Text = suma.ToString();
            }
            else
            {
                TextBox2.Text = "Nereikia mokėti, nes nėra už ką mokėti";
            }
        }
        /// <summary>
        /// Vertybių spausdinimas lentelėmis tinklapio aplinkoje
        /// </summary>
        /// <param name="Vertybes">Vertybių sąrašas</param>
        /// <param name="lentele">Lentelė į kurią įdeti duomenis</param>
        protected void VertybiuSpausdinimas(List<Vertybe> Vertybes,
            Table lentele)
        {
            try
            {
                if (Vertybes.Count == 0)
                    throw new Exception("Nėra vertybių");
                TableHeaderCell langelis11 = new TableHeaderCell();
                langelis11.Text = "Vertybių duomenys";
                TableHeaderRow eilute1 = new TableHeaderRow();
                eilute1.Cells.Add(langelis11);
                TableRow eilute2 = new TableRow();
                TableCell langelis21 = new TableCell();
                TableCell langelis22 = new TableCell();
                TableCell langelis23 = new TableCell();
                langelis21.Text = "Pavadinimas";
                langelis22.Text = "Kiekis";
                langelis23.Text = "Kaina";
                eilute2.Cells.Add(langelis21);
                eilute2.Cells.Add(langelis22);
                eilute2.Cells.Add(langelis23);
                lentele.Rows.Add(eilute1);
                lentele.Rows.Add(eilute2);
                foreach (Vertybe v in Vertybes)
                {
                    TableRow eiluteX = LentelesPapildymas(v);
                    lentele.Rows.Add(eiluteX);
                }
            }
            catch(Exception ex)
            {
                TableCell ac = new TableCell();
                TableRow ar = new TableRow();
                ac.Text = ex.Message;
                ar.Cells.Add(ac);
                Table3.Rows.Add(ar);
                Table3.Visible = true;
            }
            
        }
        /// <summary>
        /// Išspausdina užsakymus į pirmąją lentelę
        /// </summary>
        /// <param name="Uzsakymai">Užsakymų sąrašas</param>
        protected void UzsakymuSpausdinimas(LinkedList<Uzsakymas> Uzsakymai)
        {
            try
            {
                if (Uzsakymai.Count == 0)
                    throw new Exception("Nėra užsakymų");
                Table1.Visible = true;
                Table3.Visible = true;
                TableHeaderCell langelis11 = new TableHeaderCell();
                TableHeaderRow eilute1 = new TableHeaderRow();
                langelis11.Text = "Užsakymų duomenys";
                eilute1.Cells.Add(langelis11);
                TableRow eilute2 = new TableRow();
                TableCell langelis21 = new TableCell();
                TableCell langelis22 = new TableCell();
                TableCell langelis23 = new TableCell();
                langelis21.Text = "Pavadinimas";
                langelis22.Text = "Kiekis";
                eilute2.Cells.Add(langelis21);
                eilute2.Cells.Add(langelis22);
                Table1.Rows.Add(eilute1);
                Table1.Rows.Add(eilute2);
                foreach (Uzsakymas u in Uzsakymai)
                {
                    TableRow eiluteX = LentelesPapildymas2(u);
                    Table1.Rows.Add(eiluteX);
                }
            }
            catch (Exception ex)
            {
                TableCell ac = new TableCell();
                TableRow ar = new TableRow();
                ac.Text = ex.Message;
                ar.Cells.Add(ac);
                Table1.Visible = true;
                Table1.Rows.Add(ar);
                return;
            }
        }
        /// <summary>
        ///Prideda į lentelę vieną vertybę 
        /// </summary>
        /// <param name="v">Vertybė</param>
        /// <returns>Lentelės eilutė</returns>
        protected TableRow LentelesPapildymas (Vertybe v)
        {
            TableCell l1 = new TableCell();
            TableCell l2 = new TableCell();
            TableCell l3 = new TableCell();
            l1.Text = v.Vardas;
            l2.Text = v.Kiekis.ToString();
            l3.Text = v.Kaina.ToString();
            TableRow e1 = new TableRow();
            e1.Cells.Add(l1);
            e1.Cells.Add(l2);
            e1.Cells.Add(l3);
            return e1;
        }
        /// <summary>
        /// Prideda į lentelę vieną užsakymą
        /// </summary>
        /// <param name="u">Užsakymas</param>
        /// <returns>Eilutės eilutę</returns>
        protected TableRow LentelesPapildymas2 (Uzsakymas u)
        {
            TableCell l1 = new TableCell();
            TableCell l2 = new TableCell();
            l1.Text = u.Vardas;
            l2.Text = u.Kiekis.ToString();
            TableRow e1 = new TableRow();
            e1.Cells.Add(l1);
            e1.Cells.Add(l2);
            return e1;
        }
        /// <summary>
        /// Spausdina rezultatus į tekstinį failą
        /// </summary>
        /// <param name="kelias">failo kelias</param>
        /// <param name="Vertybes">Vertybių sąrašas</param>
        /// <param name="suma">Užsakymo suma</param>
        /// <param name="isimciuKelias">failo kelias, į kurį spausdinamos 
        /// išimtys</param>
        protected void SpausdintiRezultatus(string kelias,
            List<Vertybe> Vertybes, decimal suma, string isimciuKelias)
        {
            try
            {
                if (Vertybes.Count == 0)
                    throw new Exception("Nėra vertybių po užsakymo įvygdymo");
                string bruksnelis = new string('-',
                    Vertybes[0].Antraste().Length);
                using (StreamWriter failas = File.AppendText(kelias))
                {
                    failas.WriteLine("Rezultatai:");
                    failas.WriteLine("Vertybiu duomenys:");
                    failas.WriteLine(bruksnelis);
                    failas.WriteLine(Vertybes[0].Antraste());
                    failas.WriteLine(bruksnelis);
                    for (int i = 0; i < Vertybes.Count; i++)
                    {
                        failas.WriteLine(Vertybes[i].ToString());
                        if (i + 1 < Vertybes.Count)
                            if (Vertybes[i].Vardas != Vertybes[i + 1].Vardas)
                                failas.WriteLine(bruksnelis);
                    }
                    failas.WriteLine(bruksnelis);
                    failas.WriteLine("{0}: {1}", "Sumą, kurią reikia sumokėti",
                        suma);
                }
            }
            catch(Exception ex)
            {
                using (StreamWriter failas = File.AppendText(isimciuKelias))
                    failas.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Spausdina pradinius duomenis į failą
        /// </summary>
        /// <param name="kelias">failo kelias</param>
        /// <param name="Vertybes">vertybių sąrašas</param>
        /// <param name="Uzsakymai">užsakymų sąrašas</param>
        /// <param name="tekstas">Pradiniai duomenys arba rezultatai</param>
        /// <param name="isimciuKelias">failo kelias, į kurį bus rašomos
        /// išimtys</param>
        protected void SpausdintiPradiniusDuomenis(string kelias, 
            LinkedList<Vertybe> Vertybes, LinkedList<Uzsakymas> Uzsakymai,
            string tekstas, string isimciuKelias)
        {
            try
            {
                using (StreamWriter failas = File.AppendText(kelias))
                {
                    failas.WriteLine(tekstas);
                    if (Vertybes.Count == 0)
                        throw new Exception("Nėra vertybių");
                    string bruksnelis = new string('-',
                        Vertybes.First().Antraste().Length);
                    failas.WriteLine("Vertybių duomenys:");
                    failas.WriteLine(bruksnelis);
                    failas.WriteLine(Vertybes.First().Antraste());
                    failas.WriteLine(bruksnelis);
                    foreach (Vertybe v in Vertybes)
                    {
                        failas.WriteLine(v.ToString());
                    }
                    failas.WriteLine(bruksnelis);
                    if (Uzsakymai.Count == 0)
                        throw new IOException("Nėra užsakymų");
                    string bruksnelis1 = new string('-',
                        Uzsakymai.First().Antraste().Length);
                    failas.WriteLine("Užsakymų duomenys:");
                    failas.WriteLine(bruksnelis1);
                    failas.WriteLine(Uzsakymai.First().Antraste());
                    failas.WriteLine(bruksnelis1);
                    foreach (Uzsakymas u in Uzsakymai)
                    {
                        failas.WriteLine(u.ToString());
                    }
                    failas.WriteLine(bruksnelis1);
                }
            }
            catch(Exception ex)
            {
                using (StreamWriter failas1 = File.AppendText(isimciuKelias))
                {
                    failas1.WriteLine(ex.Message);
                }
            }
        }
        /// <summary>
        /// Nuskaito duomenis į objektų rinkinius
        /// </summary>
        /// <param name="Sarasas1">Vertybių sąrašas</param>
        /// <param name="keliai">Sandėlių failų keliai</param>
        /// <param name="Sarasas2">Užsakymų sąrašas</param>
        /// <param name="keliasUzsakymo">Užsakymo failo kelias</param>
        /// <param name="isimciuKelias">failo kelias, į kurį bus rašomos
        /// išimtys</param>
        protected void SkaitytiDuomenis(LinkedList<Vertybe> Sarasas1,
            string[] keliai, LinkedList<Uzsakymas> Sarasas2,
            string[] keliasUzsakymo, string isimciuKelias)
        {
            string eilute;
            try
            {
                if (keliai.Length == 0)
                {
                    throw new FileNotFoundException(
                        "Nėra vertybių failo (-ų)");
                }
                for (int i = 0; i < keliai.Length; i++)
                {
                    using (StreamReader skaitytojas =
                        new StreamReader(keliai[i]))
                    {
                        while ((eilute = skaitytojas.ReadLine()) != null)
                        {
                            Vertybe nauja = new Vertybe(eilute);
                            Sarasas1.AddLast(nauja);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                using (StreamWriter failas = File.AppendText(isimciuKelias))
                {
                    failas.WriteLine(ex.Message);
                    TableCell ac = new TableCell();
                    ac.Text = ex.Message;
                    TableRow ar = new TableRow();
                    ar.Cells.Add(ac);
                    Table1.Rows.Add(ar);
                    Table1.Visible = true;
                }
            }
            try
            {
                if (keliasUzsakymo.Length == 0)
                {
                    throw new FileNotFoundException("Nėra užsakymo failo");
                }
                using (StreamReader skaitytojas = 
                    new StreamReader(keliasUzsakymo[0]))
                {
                    while ((eilute = skaitytojas.ReadLine()) != null)
                    {
                        Uzsakymas naujas = new Uzsakymas(eilute);
                        Sarasas2.AddLast(naujas);

                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                using (StreamWriter failas = File.AppendText(isimciuKelias))
                {
                    failas.WriteLine(ex.Message);
                    TableCell ac = new TableCell();
                    ac.Text = ex.Message;
                    TableRow ar = new TableRow();
                    ar.Cells.Add(ac);
                    Table1.Rows.Add(ar);
                    Table1.Visible = true;
                }
            }
        }
        /// <summary>
        /// Atrenka vertybes pagal užsakymus
        /// </summary>
        /// <param name="Uzsakymai">Užsakymų sąrašas</param>
        /// <param name="Vertybes">Vertybių sąrašas</param>
        /// <returns>Atrinktų vertybių sąrašą</returns>
        protected LinkedList<Vertybe> SurastiVertybes(
            LinkedList<Uzsakymas> Uzsakymai, LinkedList<Vertybe> Vertybes)
        {
            var Naujas = new LinkedList<Vertybe>();
            try
            {
                Lygintojas Lygina = new Lygintojas();
                List<Vertybe> LaikinasSarasas = Vertybes.ToList();
                LaikinasSarasas.Sort(Lygina);
                if (Uzsakymai.Count == 0)
                    throw new IndexOutOfRangeException(
                        "Negalima vykdyti programos, nes nėra užsakymų");
                foreach (Uzsakymas uzsakymas in Uzsakymai)
                {
                    UzsakymoIvygdymas(uzsakymas, LaikinasSarasas, Naujas);
                }
            }
            catch(IndexOutOfRangeException ex)
            {
                TableCell ac = new TableCell();
                TableRow ar = new TableRow();
                ac.Text = ex.Message;
                ar.Cells.Add(ac);
                Table3.Rows.Add(ar);
            }
            return Naujas;

        }
        /// <summary>
        /// Atrenka vertybes pagal vieną užskymą
        /// </summary>
        /// <param name="uzsakymas">Užsakymas</param>
        /// <param name="Vertybes">Vertybių sąrašas</param>
        /// <param name="Naujas">Atrinktų vertybių sąrašas</param>
        protected void UzsakymoIvygdymas(Uzsakymas uzsakymas,
            List<Vertybe> Vertybes, LinkedList<Vertybe> Naujas)
        {
            int kiekis = 0;
            if (Vertybes.Count == 0)
                throw new IndexOutOfRangeException(
                    "Negalima vykdyti programos, nes nėra vertybių");
            foreach(Vertybe vertybe in Vertybes)
            {
                Vertybe laikina = new Vertybe(uzsakymas.Vardas,
                    uzsakymas.Kiekis, 0);
                if (vertybe.Equals(laikina))
                {
                    if (kiekis < uzsakymas.Kiekis)
                    {
                        if(kiekis + vertybe.Kiekis <= uzsakymas.Kiekis)
                        {
                            Naujas.AddLast(vertybe);
                            kiekis += vertybe.Kiekis;
                        }
                        else
                        {
                            int laikinas = uzsakymas.Kiekis - kiekis;
                            vertybe.Kiekis = laikinas;
                            Naujas.AddLast(vertybe);
                            kiekis +=vertybe.Kiekis;
                        }
                    }
                    if (kiekis >= uzsakymas.Kiekis)
                        break;
                }
            }
        }
        /// <summary>
        /// Suskaičiuoja sumą
        /// </summary>
        /// <param name="RastosVertybes">Atrinktos vertybės</param>
        /// <returns>Vertybių sumą</returns>
        protected decimal SumosSkaiciavimas(List<Vertybe> RastosVertybes)
        {
            decimal suma = 0;
            foreach(Vertybe v in RastosVertybes)
            {
                suma += v.Kaina * v.Kiekis;
            }
            return suma;
        }
        /// <summary>
        /// Panaikina vertybes, arba sumažina jų kiekį, suskaičiuoja sumą
        /// </summary>
        /// <param name="uzsakymoSuma">Pradinė suma</param>
        /// <param name="ivestaSuma">Suma, kurią įvedė vartotojas</param>
        /// <param name="Vertybes">Vertybių sąrašas</param>
        /// <returns>Sumą, kurią reiks sumokėti vartotojui</returns>
        protected decimal VertybiuMazinimas(decimal uzsakymoSuma,
            decimal ivestaSuma, List<Vertybe> Vertybes)
        {
            Lygintojas lygina = new Lygintojas();
            Vertybes.Sort(lygina);
            while (uzsakymoSuma > ivestaSuma)
            {
                if (Vertybes[0].Kiekis != 0)
                {
                    uzsakymoSuma -= Vertybes[0].Kaina;
                    Vertybes[0].Kiekis--;
                    if (Vertybes[0].Kiekis == 0)
                    {
                        Vertybes.RemoveAt(0);
                    }
                }
                else
                {
                    Vertybes.RemoveAt(0);
                }
            }
            return uzsakymoSuma;
        }
    }
}