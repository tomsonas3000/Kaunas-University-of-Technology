using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L4
{
    public class Lygintojas : IComparer<Vertybe>
    {
        /// <summary>
        /// Grąžina 1 ar 0 ar -1, kai naudojama List<>.Sort() Metodas
        /// </summary>
        /// <param name="x">Viena vertybė</param>
        /// <param name="y">Antra vertybė</param>
        /// <returns>1 arba 0 arba -1</returns>
        public int Compare(Vertybe x, Vertybe y)
        {
            if (x.Kaina == y.Kaina)
                return 0;
            if (x.Kaina > y.Kaina)
                return 1;
            else
                return -1;
        }
    }
}