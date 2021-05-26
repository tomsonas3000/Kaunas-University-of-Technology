using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5
{
    public class Order
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        /// <summary>
        /// Constructor without arguments
        /// </summary>
        public Order()
        {

        }
        /// <summary>
        /// Constructor with arguments
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        public Order(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
        /// <summary>
        /// Constructor with argument
        /// </summary>
        /// <param name="line"></param>
        public Order(string line)
        {
            GetData(line);
        }
        /// <summary>
        /// Takes data from a string
        /// </summary>
        /// <param name="line"></param>
        private void GetData(string line)
        {
            string[] values = line.Split(',');
            Name = values[0];
            Amount = int.Parse(values[1]);
        }
        /// <summary>
        /// Return formated text line
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("|{0,-15}|{1,10}|", Name, Amount);
        }
        /// <summary>
        /// Returns formated text line
        /// </summary>
        /// <returns></returns>
        public string Antraste()
        {
            return string.Format("|{0,-15}|{1,-10}|", "Name", "Amount");    
        }
    }
}