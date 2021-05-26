using System;
using System.Diagnostics;

namespace Algoritmu_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] s = { 1, 2, 3, 4, 5, 6 };
            int[] p = { 5, 6, 7, 8, 9 };
            int n = 2;
            int w = 2;
            var rnd = new Random();

            //for (int i = 0; i < 20; i++)
            //{
            //    s[i] = rnd.Next();
            //    p[i] = rnd.Next();
            //}
            Stopwatch stopWatch1 = new Stopwatch();
            stopWatch1.Start();
            Console.WriteLine("F(n,w) = " + Func1(n, w, s, p));
            stopWatch1.Stop();
            long ts1 = stopWatch1.ElapsedMilliseconds;
            Console.WriteLine("Kiek truko Func1 vykdymas: " + ts1);

            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();
            Func2(6);
            Console.WriteLine("Kai n = 6 skirtingų būdų skaičius " + Func2(6));
            stopWatch2.Stop();
            long ts2 = stopWatch2.ElapsedMilliseconds;

            Console.WriteLine("Kiek truko Func1 vykdymas: " + ts2);




        }

        static int Func1(int k, int r, int[] s, int[] p)                        //kaina | kiekis
        {           
            int a = 0; //c   |   1
            if (k == 0 || r == 0)                                                 //c   |   1  
            {
                return 0;                                                        // c   |   1
            }
            else                                                                  //c   |   1
            {
                for (int i = 0; i<s.Length; i++)                                  //c   |   s
                {
                    if (s[i] > r)                                                 //c   |   s
                    {
                        Func1(k - 1, r, s, p);                              //T(k-1, r) |   s
                    }
                    else                                                          
                    {
          return a = Math.Max(Func1(k - 1, r, s, p),   //T(k-1, r) + T(k - 1, r - S[i]) |   s
            p[i] + Func1(k - 1, r - s[i], s, p));
                    }
                }
            }
            return a;                                                             //c   |   1
        }

        static int Func2(int n) {                                                   //kaina | kiekis
            int[] DP = new int[n + 1];                                                //c   |    1

            DP[0] = DP[1] = DP[2] = 1;                                               // c   |    3
            DP[3] = 2;                                                               // c   |    1

            for (int i = 4; i <= n; i++) {                                           // c   |   n - 4              
                DP[i] = DP[i - 1] + DP[i - 3] + DP[i - 4];                           // c   |   n - 4
            }
            return DP[n];                                                            // c   |     1
        }

    }
}
