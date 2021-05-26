using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5
{
    public class Item
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Warehouse { get; set; }
        /// <summary>
        /// Constructor without arguments
        /// </summary>
        public Item()
        {

        }
        /// <summary>
        /// Constructor with arguments
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        public Item(string name, int amount, decimal price)
        {
            Name = name;
            Amount = amount;
            Price = price;
        }
        /// <summary>
        /// Constructor with argument
        /// </summary>
        /// <param name="line"></param>
        public Item(string line)
        {
            GetData(line);
        }
        /// <summary>
        /// Takes data from a string
        /// </summary>
        /// <param name="line"></param>
        public void GetData(string line)
        {
            string[] values = line.Split(',');
            Name = values[0];
            Amount = int.Parse(values[1]);
            Price = decimal.Parse(values[2]);
        }
        /// <summary>
        /// Returns formated text line
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("|{0,-20}|{1,10}|{2,15}|", Name, Amount,
                Price);
        }
        /// <summary>
        /// Return formated text line
        /// </summary>
        /// <returns></returns>
        public string Header()
        {
            return String.Format("|{0,-20}|{1,-10}|{2,-15}|",
                "Name", "Amount", "Price");
        }
    }
    }