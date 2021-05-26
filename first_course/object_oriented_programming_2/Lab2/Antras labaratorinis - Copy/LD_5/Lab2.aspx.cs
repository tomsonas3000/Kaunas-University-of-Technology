using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace LD_5
{
    public partial class Lab2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Button1_Click(object sender, EventArgs e)
        {
            string CFd = Server.MapPath("App_Data/U5a.txt");
            string CFd2 = Server.MapPath("App_Data/U5b.txt");
            string CFr = Server.MapPath
                ("App_Data/PradiniaiDuomenysIrRezultatai.txt");
            TaskuSarasas Taskai = new TaskuSarasas();
            SpalvuSarasas Spalvos = new SpalvuSarasas();
            TrikampiuSarasas Trikampiai = new TrikampiuSarasas();
            SkaitytiDuomenisA(CFd, Taskai);
            SkaitytiDuomenisB(CFd2, Spalvos);
            Trikampiai = 
                SurastiDidziausiusLygiasoniusTrikampius(Spalvos, Taskai);
            Trikampiai.Rikiuoti();
            using (StreamWriter rasytojas = new StreamWriter(CFr))
            {
                rasytojas.WriteLine("Pradiniai duomenys ");
                rasytojas.WriteLine("Taskai:");
                SpausdintiTaskuDuomenis(CFr, Taskai, rasytojas);
                rasytojas.WriteLine("Spalvos:");
                SpausdintiSpalvuDuomenis(CFr, Spalvos, rasytojas);
                rasytojas.WriteLine("Rezultatai: ");
                SpausdintiTrikampiuDuomenis(CFr, Trikampiai, rasytojas);
            }
            SpausdintiIEkranaPradiniusDuomenis(Taskai, Spalvos);
            Session["trikampiai"] = Trikampiai;
            Session["spalvos"] = Spalvos;
            Session["taskai"] = Taskai;
            SpausdintiIEkranaRezultatus(Trikampiai);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string trikampioKoordinatesS = TextBox2.Text;
            string[] TrikampioKoordinates = trikampioKoordinatesS.Split(',');
            int[] Koordinates = SugeneruotiKoordinates(TrikampioKoordinates);
            TrikampiuSarasas Trikampiai =
                (TrikampiuSarasas)Session["trikampiai"];
            TaskuSarasas Taskai = (TaskuSarasas)Session["taskai"];
            SpalvuSarasas Spalvos = (SpalvuSarasas)Session["spalvos"];
            Trikampiai.Salinti(
                Koordinates[0], Koordinates[1], Koordinates[2],
                Koordinates[3], Koordinates[4], Koordinates[5]);
            SpausdintiIEkranaPradiniusDuomenis(Taskai, Spalvos);
            SpausdintiIEkranaRezultatus(Trikampiai);


        }

        /// <summary>
        /// Nuskaito duomenis į taškų sąrašą
        /// </summary>
        /// <param name="failas">Failo kelias</param>
        /// <param name="Taskai">Taškų sąrašas</param>
        public void SkaitytiDuomenisA(string failas, TaskuSarasas Taskai)
        {
            string eilute;
            using (StreamReader skaitytojas = new StreamReader(failas))
            {
                if ((eilute = skaitytojas.ReadLine()) != null)
                {
                    int n = int.Parse(eilute);

                }
                while ((eilute = skaitytojas.ReadLine()) != null)
                {
                    string[] reiksmes = eilute.Split(',');
                    int x = int.Parse(reiksmes[0]);
                    int y = int.Parse(reiksmes[1]);
                    string spalva = reiksmes[2];
                    Taskas taskas = new Taskas(x, y, spalva);

                    Taskai.DetiDuomenisIPabaiga(taskas);
                }
            }
        }
        /// <summary>
        /// Nuskaito duomenis iš failo į spalvų sąrašą
        /// </summary>
        /// <param name="failas">Failo kelias</param>
        /// <param name="Spalvos">Spalvų sąrašas</param>
        public void SkaitytiDuomenisB(string failas, SpalvuSarasas Spalvos)
        {
            string eilute;
            using (StreamReader skaitytojas = new StreamReader(failas))
            {
                while ((eilute = skaitytojas.ReadLine()) != null)
                {
                    string[] reiksmes = eilute.Split(',');
                    string spalv = reiksmes[0];
                    bool arGalima;
                    if (reiksmes[1].Equals("taip"))
                        arGalima = true;
                    else
                        arGalima = false;
                    Spalv spalva = new Spalv(spalv, arGalima);
                    Spalvos.DetiDuomenisIPabaiga(spalva);
                }
            }
        }
        /// <summary>
        /// Išspausdina taškų duomenis į tekstinį failą
        /// </summary>
        /// <param name="failas">Failo kelias</param>
        /// <param name="Sarasas">Taškų sąrašas</param>
        /// <param name="rasytojas">StreamWriteris</param>
        public void SpausdintiTaskuDuomenis(string failas, 
            TaskuSarasas Sarasas, StreamWriter rasytojas)
        {
            Sarasas.Pradzia();
            if(Sarasas.Yra() == false)
            {
                rasytojas.WriteLine("Nėra taškų");
            }
            else
            {
                Taskas t = Sarasas.GrazintiPirmaTaska();
                rasytojas.WriteLine(t.Antraste());
                string bruksnelis = new string('-', t.Antraste().Length);
                rasytojas.WriteLine(bruksnelis);
                for (Sarasas.Pradzia(); Sarasas.Yra(); Sarasas.Kitas())
                {
                    rasytojas.WriteLine(Sarasas.ImtiDuomenis().ToString());
                }
                rasytojas.WriteLine(bruksnelis);
            }
        }
        /// <summary>
        /// Išspaudina spalvų duomenis į tekstinį failą
        /// </summary>
        /// <param name="failas">Failo vardas</param>
        /// <param name="Spalvos">Spalvų sąrašas</param>
        /// <param name="rasytojas">Streamwriteris</param>
        public void SpausdintiSpalvuDuomenis(string failas,
            SpalvuSarasas Spalvos, StreamWriter rasytojas)
        {
            Spalvos.Pradzia();
            if (Spalvos.Yra() == false)
            {
                rasytojas.WriteLine("Nėra spalvų");
            }
            else
            {
                Spalv s = Spalvos.ImtiDuomenis();
                string bruksnelis = new string('-', s.Antraste().Length);
                rasytojas.WriteLine(s.Antraste());
                rasytojas.WriteLine(bruksnelis);
                for (Spalvos.Pradzia(); Spalvos.Yra(); Spalvos.Kitas())
                {
                    rasytojas.WriteLine(Spalvos.ImtiDuomenis().ToString());
                }
                rasytojas.WriteLine(bruksnelis);
            }
        }
        /// <summary>
        /// Spausdina trikampių sąrašo duomenis į tekstinį failą
        /// </summary>
        /// <param name="failas">Failo vardas</param>
        /// <param name="Trikampiai">Trikampių sąrašas</param>
        /// <param name="rasytojas">StreamWriteris</param>
        public void SpausdintiTrikampiuDuomenis(string failas,
            TrikampiuSarasas Trikampiai, StreamWriter rasytojas)
        {
            Trikampiai.Pradzia();
            if (Trikampiai.Yra() == false)
            {
                rasytojas.WriteLine("Nėra trikampių");
            }
            else
            {
                Trikampis t = Trikampiai.ImtiDuomenis();
                string bruksnelis = new string('-', t.Antraste().Length);
                rasytojas.WriteLine(t.Antraste());
                rasytojas.WriteLine(bruksnelis);
                for (Trikampiai.Pradzia(); Trikampiai.Yra();
                    Trikampiai.Kitas())
                {
                    if (Trikampiai.ImtiDuomenis().Pozymis != null)
                    {
                        rasytojas.WriteLine("|{0,-15}|{1}", 
                            Trikampiai.ImtiDuomenis().Spalva, 
                            Trikampiai.ImtiDuomenis().Pozymis);
                    }
                    else
                    {
                        Trikampiai.ImtiDuomenis().RikiuotiTaskus();
                        rasytojas.WriteLine(
                            Trikampiai.ImtiDuomenis().ToString());
                    }
                }
                rasytojas.WriteLine(bruksnelis);
            }
        }
        /// <summary>
        /// Išspausdina pradinius duomenis į tinklapio aplinką
        /// </summary>
        /// <param name="Taskai">Taškų sąrašas</param>
        /// <param name="Spalvos">Spalvų sąrašas</param>
        public void SpausdintiIEkranaPradiniusDuomenis(TaskuSarasas Taskai,
            SpalvuSarasas Spalvos)
        {
            Table3.Visible = true;
            Taskai.Pradzia();
            Spalvos.Pradzia();
            if (!Taskai.Yra())
            {
                TableCell pranesimas = new TableCell();
                pranesimas.Text = "Taškų ir spalvų nėra";
                TableRow eilute = new TableRow();
                eilute.Cells.Add(pranesimas);
                Table3.Rows.Add(eilute);
            }
            else
            {
                int i = 0;
                TableCell antraste1 = new TableCell();
                antraste1.Text = Taskai.ImtiDuomenis().Antraste();
                TableCell antraste2 = new TableCell();
                antraste2.Text = Spalvos.ImtiDuomenis().Antraste();
                TableRow eilute1 = new TableRow();
                eilute1.Cells.Add(antraste1);
                eilute1.Cells.Add(antraste2);
                Table3.Rows.Add(eilute1);
                for (Taskai.Pradzia(); Taskai.Yra(); Taskai.Kitas())
                {
                    TableCell langelis = new TableCell();
                    langelis.Text = Taskai.ImtiDuomenis().ToString();
                    TableCell langelis2 = new TableCell();
                    if (i == 0)
                    {
                        langelis2.Text = Spalvos.ImtiDuomenis().ToString();
                    }
                    if (Spalvos.Yra() == true && i != 0)
                    {
                        langelis2.Text = Spalvos.ImtiDuomenis().ToString();
                    }
                    if(!Spalvos.Yra() && i != 0)
                    {
                        langelis2.Text = null;
                    }
                    TableRow eilute = new TableRow();
                    eilute.Cells.Add(langelis);
                    eilute.Cells.Add(langelis2);
                    Table3.Rows.Add(eilute);
                    if (Spalvos.Yra())
                    {
                        Spalvos.Kitas();
                    }
                    i++;
                }
            }

        }
        /// <summary>
        /// Spausdina trikampių duomenis į tinklapių aplinką
        /// </summary>
        /// <param name="Trikampiai">Trikampių sąrašas</param>
        public void SpausdintiIEkranaRezultatus(TrikampiuSarasas Trikampiai)
        {
            Table1.Visible = true;
            Trikampiai.Pradzia();
            if (!Trikampiai.Yra())
            {
                TableCell pranesimas = new TableCell();
                pranesimas.Text = "Trikampių nėra";
                TableRow eilute = new TableRow();
                eilute.Cells.Add(pranesimas);
                Table1.Rows.Add(eilute);
            }
            else
            {
                TableCell antraste = new TableCell();
                antraste.Text = Trikampiai.ImtiDuomenis().Antraste()
                    + " Arba požymis";
                TableRow eilute1 = new TableRow();
                eilute1.Cells.Add(antraste);
                Table1.Rows.Add(eilute1);
                for (Trikampiai.Pradzia(); Trikampiai.Yra();
                    Trikampiai.Kitas())
                {
                    TableCell trikampis = new TableCell();
                    if (Trikampiai.ImtiDuomenis().Pozymis != null)
                    {
                        trikampis.Text = String.Format("|{0,-15}|{1}",
                            Trikampiai.ImtiDuomenis().Spalva,
                            Trikampiai.ImtiDuomenis().Pozymis);
                    }
                    else
                    {
                        Trikampiai.ImtiDuomenis().RikiuotiTaskus();
                        trikampis.Text = String.Format(
                            Trikampiai.ImtiDuomenis().ToString());
                    }
                    TableRow eilute = new TableRow();
                    eilute.Cells.Add(trikampis);
                    Table1.Rows.Add(eilute);
                }

            }
        }
        /// <summary>
        /// Suranda didžiausius lygiašonius kiekvienos spalvos trikampius
        /// </summary>
        /// <param name="Spalvos">Spalvų sąrašas</param>
        /// <param name="Taskai">Taškų sąrašas</param>
        /// <returns>Didžiausių lygiašonių trikampių sąrašą</returns>
        public TrikampiuSarasas SurastiDidziausiusLygiasoniusTrikampius (
            SpalvuSarasas Spalvos, TaskuSarasas Taskai)
        {
            TrikampiuSarasas Trikampiai = new TrikampiuSarasas();
            for (Spalvos.Pradzia(); Spalvos.Yra(); Spalvos.Kitas())
            {
                Trikampis trikampis = new Trikampis();
                if (Spalvos.ImtiDuomenis().ArGalima)
                {
                    string spalva = Spalvos.ImtiDuomenis().Spalva;
                    Trikampis trikampisDid = 
                        Taskai.RastiDidziausiaTrikampi(spalva);
                    if (trikampisDid.Perimetras == 0)
                        trikampisDid.Pozymis = "Nėra";
                    Trikampiai.DetiDuomenisIPabaiga(trikampisDid);
                }
                else
                {
                    trikampis.Pozymis = "Negalima";
                    trikampis.Spalva = Spalvos.ImtiDuomenis().Spalva;
                    Trikampiai.DetiDuomenisIPabaiga(trikampis);

                }
            }
            return Trikampiai;
        }
        /// <summary>
        /// Grąžina koordinates iš vartotojo įvestos teksto eilutės
        /// </summary>
        /// <param name="Koordinates">Teksto eilutė</param>
        /// <returns>Trikampio koordinates</returns>
        public int[] SugeneruotiKoordinates(string[] Koordinates)
        {
            int[] Koord = new int[Koordinates.Length];
            for (int i = 0; i < Koordinates.Length; i++)
            {
                Koord[i] = int.Parse(Koordinates[i]);
            }
            return Koord;
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}