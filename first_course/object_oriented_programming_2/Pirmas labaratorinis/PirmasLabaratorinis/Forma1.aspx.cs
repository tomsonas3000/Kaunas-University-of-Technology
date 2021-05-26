using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace PirmasLabaratorinis
{
    public partial class Forma1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string CFd = Server.MapPath("App_Data/Trecias.txt");
            string CFd2 = Server.MapPath("App_Data/Zodziai.txt");
            ZodziuKonteineris zodziai = new ZodziuKonteineris(100);
            SkaitytiTeksta(CFd, out double n, out char[,] A);
            SkaitytiZodzius(CFd2, zodziai);
            PasalintiZodzius(zodziai, n);

        }
        protected void SkaitytiZodzius(string failas, ZodziuKonteineris zodziai)
        {
            using (StreamReader skaitytojas = new StreamReader(failas))
            {
                string eilute = null;
                while ((eilute = skaitytojas.ReadLine()) != null)
                {
                    Zodis zodis = new Zodis(eilute, 0);
                    //TextBox1.Text = zodis.Pavadinimas + zodis.Kiekis.ToString();
                    if (!zodziai.ArYra(zodis))
                    {
                        zodziai.PridetiZodi(zodis);
                    }
                }
            }
        }
        protected void SkaitytiTeksta(string failas, out double n, out char[,] A)
        {
            string tekstas = File.ReadAllText(failas);
            tekstas = tekstas.Replace("\n", String.Empty);
            tekstas = tekstas.Replace("\r", String.Empty);
            tekstas = tekstas.Replace("\t", String.Empty);
            int simboliuSkaicius = tekstas.Count();
            n = SurastiN(simboliuSkaicius);
            TextBox2.Text = n.ToString();
            char[] tekstasChar = tekstas.ToCharArray();
            A = new char[(int)n, (int)n];
            int skaitliukas = -1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j = j + 1)
                {
                    skaitliukas++;
                    if (skaitliukas < tekstasChar.Length)
                    {
                        A[i, j] = tekstasChar[skaitliukas];
                    }
                    else
                    {
                        A[i, j] = ' ';
                    }
                }
            }


        }
        protected double SurastiN(double n)
        {
            n = Math.Ceiling(Math.Pow(n, 0.5));
            return n;
        }
        protected void PasalintiZodzius(ZodziuKonteineris zodziai, double n)
        {
            for (int i = 0; i < zodziai.Skaicius; i++)
            {
                if ((zodziai.PaimtiZodi(i).Pavadinimas.Length > n))
                {
                    zodziai.PasalintiZodi(zodziai.PaimtiZodi(i));
                }
            }
        }
        protected void SurastiZodzius(ZodziuKonteineris zodziai)
        {
            for (int i = 0; i < zodziai.Skaicius; i++)
            {

            }
        }
        protected void SurastiZodzius2(Zodis zodzis)
        {

        }
    }
}