using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Lab5
{
    public partial class L5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Visible = false;
            TextBox2.Visible = false;
            TextBox3.Visible = false;
            TextBox4.Visible = false;
            TextBox5.Visible = false;
            Table1.Visible = false;
            Table2.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string pathOfResults = Server.MapPath(
                "App_Data/InitialDataAndResults.txt");
            string pathOfExceptions = Server.MapPath("App_Data/Exceptions.txt")
                ;
            CreateFile(pathOfExceptions);
            CreateFile(pathOfResults);
            var Items = new List<Item>();
            var Orders = new List<Order>();
            string[] pathsOfWareHouses = Directory.GetFiles(Server.MapPath(
                "App_Data"), "Warehouse*.txt");
            string orderPath = Server.MapPath("App_Data/Order.txt");
            ReadData(Items, pathsOfWareHouses, Orders, orderPath,
                pathOfExceptions);
            PrintInitialDataToFile(pathOfResults, Items, Orders,
                pathOfExceptions);
            PrintInitialDataToScreen(Items, Orders);
            List<Item> FoundItems = FindItems(Orders, Items);
            Session["foundItems"] = FoundItems;
            Session["pathOfResults"] = pathOfResults;
            Session["exceptionPath"] = pathOfExceptions;
            Session["Items"] = Items;
            Session["Orders"] = Orders;
            TextBox1.Visible = true;
            TextBox5.Visible = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<Item> Items = (List<Item>)Session["Items"];
            List<Order> Orders = (List<Order>)Session["Orders"];
            PrintInitialDataToScreen(Items, Orders);
            List<Item> FoundItems = (List<Item>)Session["foundItems"];
            string pathOfResulsts = (string)Session["pathOfResults"];
            string pathOfExceptions = (string)Session["exceptionPath"];
            //decimal sumOfOrder = FoundItems.Select(item => item.Price 
            //* item.Amount).Sum();
            decimal sumOfOrder = (from s in FoundItems
                                  select s.Price * s.Amount).Sum();

            decimal userSum = decimal.Parse(TextBox1.Text);
            if(sumOfOrder > userSum)
            {
                sumOfOrder = MinimizeOrder(sumOfOrder, userSum, FoundItems);
                FoundItems = (from ar in FoundItems orderby  ar.Amount descending, ar.Name select ar).ToList(); 
                //FoundItems.OrderBy(nn => nn.Amount).ThenBy(nn => nn.Name);
                TextBox4.Visible = true;
                TextBox4.Text = "Because you have ordered more than you" +
                    " can afford, we have minimized the order";
            }
            PrintResultsToFile(pathOfResulsts, FoundItems, sumOfOrder,
                pathOfExceptions);
            PrintResultstToScreen(FoundItems, sumOfOrder);
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;
            TextBox1.Visible = false;
            TextBox1.Visible = false;
            TextBox2.Visible = false;
            TextBox3.Visible = false;
            TextBox4.Visible = false;
            TextBox5.Visible = false;
            Table1.Visible = false;
            Table2.Visible = false;
        }
        /// <summary>
        /// Reads data from files
        /// </summary>
        /// <param name="Items"></param>
        /// <param name="itemsPaths"></param>
        /// <param name="Orders"></param>
        /// <param name="orderPath"></param>
        /// <param name="exceptionPath"></param>
        protected void ReadData(List<Item> Items, string[] itemsPaths,
            List<Order> Orders, string orderPath, string exceptionPath)
        {
            string line;
            try
            {
                if(itemsPaths.Length == 0)
                {
                    throw new FileNotFoundException("There is no" +
                        " file of items");
                }
                for (int i = 0; i < itemsPaths.Length; i++)
                {
                    string warehouseName = String.Format("{0}{1}",
                        "Warehouse", i);
                    using (StreamReader reader = new 
                        StreamReader(itemsPaths[i]))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            Item add = new Item(line);
                            add.Warehouse = warehouseName;
                            Items.Add(add);
                        }
                    }
                }
            }
            catch(FileNotFoundException ex)
            {
                using (StreamWriter file = File.AppendText(exceptionPath))
                {
                    file.WriteLine(ex.Message);
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
                if (orderPath == null)
                    throw new FileNotFoundException("There is no order file");

                using (StreamReader reader = new StreamReader(orderPath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        Order add = new Order(line);
                        Orders.Add(add);
                    }
                }
            }
            catch(FileNotFoundException ex)
            {
                using (StreamWriter file = File.AppendText(exceptionPath))
                {
                    file.WriteLine(ex.Message);
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
        /// Prints initial data to web page
        /// </summary>
        /// <param name="Items"></param>
        /// <param name="Orders"></param>
        protected void PrintInitialDataToScreen(List<Item> Items,
            List<Order> Orders)
        {
            PrintItems(Items, Table1, 1);
            PrintOrders(Orders);
        }
        /// <summary>
        /// Prints items to web page
        /// </summary>
        /// <param name="Items"></param>
        /// <param name="table"></param>
        /// <param name="c"></param>
        protected void PrintItems(List<Item> Items, Table table, int c)
        {
            try
            {
                if (Items.Count == 0)
                    throw new Exception("There are no items");
                TableHeaderCell cell11 = new TableHeaderCell();
                cell11.Text = "Item data";
                TableHeaderRow line1 = new TableHeaderRow();
                line1.Cells.Add(cell11);
                TableRow line2 = new TableRow();
                TableCell cell21 = new TableCell();
                TableCell cell22 = new TableCell();
                TableCell cell23 = new TableCell();
                TableCell cell24 = new TableCell();
                cell21.Text = "Name";
                cell22.Text = "Amount";
                cell23.Text = "Price";
                cell24.Text = "Warehouse";
                line2.Cells.Add(cell21);
                line2.Cells.Add(cell22);
                line2.Cells.Add(cell23);
                if (c == 1)
                    line2.Cells.Add(cell24);
                table.Rows.Add(line1);
                table.Rows.Add(line2);
                foreach (Item i in Items)
                {
                    TableRow eiluteX = TableAddition(i, c);
                    table.Rows.Add(eiluteX);
                }
            }
            catch (Exception ex)
            {
                TableCell ac = new TableCell();
                TableRow ar = new TableRow();
                ac.Text = ex.Message;
                ar.Cells.Add(ac);
                Table2.Rows.Add(ar);
                Table2.Visible = true;
            }
        }
        /// <summary>
        /// Prints orders to web page
        /// </summary>
        /// <param name="Orders"></param>
        protected void PrintOrders(List<Order> Orders)
        {
            try
            {
                if (Orders.Count == 0)
                    throw new Exception("Nėra užsakymų");
                Table1.Visible = true;
                Table2.Visible = true;
                TableHeaderCell cell11 = new TableHeaderCell();
                TableHeaderRow row1 = new TableHeaderRow();
                cell11.Text = "Order data";
                row1.Cells.Add(cell11);
                TableRow row2 = new TableRow();
                TableCell cells21 = new TableCell();
                TableCell cel22 = new TableCell();
                TableCell cell23 = new TableCell();
                cells21.Text = "Name";
                cel22.Text = "Amount";
                row2.Cells.Add(cells21);
                row2.Cells.Add(cel22);
                Table1.Rows.Add(row1);
                Table1.Rows.Add(row2);
                foreach (Order o in Orders)
                {
                    TableRow rowX = TableAddition2(o);
                    Table1.Rows.Add(rowX);
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
        /// Return a table row of item data
        /// </summary>
        /// <param name="i">Item</param>
        /// <param name="c">Print warehouse info if c = 1 </param>
        /// <returns>Table row</returns>
        protected TableRow TableAddition(Item i, int c)
        {
            TableCell l1 = new TableCell();
            TableCell l2 = new TableCell();
            TableCell l3 = new TableCell();
            TableCell l4 = new TableCell();
            l1.Text = i.Name;
            l2.Text = i.Amount.ToString();
            l3.Text = i.Price.ToString();
            l4.Text = i.Warehouse.ToString();
            TableRow e1 = new TableRow();
            e1.Cells.Add(l1);
            e1.Cells.Add(l2);
            e1.Cells.Add(l3);
            if(c==1)
                e1.Cells.Add(l4);
            return e1;
        }
        /// <summary>
        /// Returns a table row of order data
        /// </summary>
        /// <param name="o">Order</param>
        /// <returns>Table row</returns>
        protected TableRow TableAddition2(Order o)
        {
            TableCell l1 = new TableCell();
            TableCell l2 = new TableCell();
            l1.Text = o.Name.ToString();
            l2.Text = o.Amount.ToString();
            TableRow e1 = new TableRow();
            e1.Cells.Add(l1);
            e1.Cells.Add(l2);
            return e1;
        }
        /// <summary>
        /// Prints initial data to file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Items"></param>
        /// <param name="Orders"></param>
        /// <param name="exceptionPath"></param>
        protected void PrintInitialDataToFile(string path,
            List<Item> Items, List<Order> Orders, string exceptionPath)
        {
            try
            {
                using (StreamWriter file = File.AppendText(path))
                {
                    file.WriteLine("Initial data");
                    if (Items.Count == 0)
                        throw new Exception("There are no items");
                    string dash = new string('-',
                        Items.First().Header().Length + 11);
                    file.WriteLine("Item data:");
                    file.WriteLine(dash);
                    file.WriteLine(Items.First().Header() + " Warehouse|");
                    file.WriteLine(dash);
                    foreach (Item i in Items)
                    {
                        string warehouse = String.Format("{0,-10}|",
                            i.Warehouse);
                        file.WriteLine(i.ToString() + warehouse);
                    }
                    file.WriteLine(dash);
                    if (Orders.Count == 0)
                        throw new IOException("There are no orders");
                    string bruksnelis1 = new string('-',
                        Orders.First().Antraste().Length);
                    file.WriteLine("Užsakymų duomenys:");
                    file.WriteLine(bruksnelis1);
                    file.WriteLine(Orders.First().Antraste());
                    file.WriteLine(bruksnelis1);
                    foreach (Order o in Orders)
                    {
                        file.WriteLine(o.ToString());
                    }
                    file.WriteLine(bruksnelis1);
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter failas1 = File.AppendText(exceptionPath))
                {
                    failas1.WriteLine(ex.Message);
                }
            }
        }
        /// <summary>
        /// Prints results to file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Items"></param>
        /// <param name="sum"></param>
        /// <param name="exceptionPath"></param>
        protected void PrintResultsToFile(string path,
            List<Item> Items, decimal sum, string exceptionPath)
        {
            try
            {
                if (Items.Count == 0)
                    throw new Exception("There are no items after order");
                string dash = new string('-',
                    Items[0].Header().Length);
                using (StreamWriter failas = File.AppendText(path))
                {
                    failas.WriteLine("Results:");
                    failas.WriteLine("Item data:");
                    failas.WriteLine(dash);
                    failas.WriteLine(Items[0].Header());
                    failas.WriteLine(dash);
                    for (int i = 0; i < Items.Count; i++)
                    {
                        failas.WriteLine(Items[i].ToString());
                        if (i + 1 < Items.Count)
                            if (Items[i].Name != Items[i + 1].Name)
                                failas.WriteLine(dash);
                    }
                    failas.WriteLine(dash);
                    failas.WriteLine("{0}: {1}", "Price, that needs to be " +
                        "paid",
                        sum);
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter failas = File.AppendText(exceptionPath))
                    failas.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Prints results to web page
        /// </summary>
        /// <param name="Items"></param>
        /// <param name="sum"></param>
        protected void PrintResultstToScreen(
            List<Item> Items, decimal sum)
        {
            TextBox2.Visible = true;
            PrintItems(Items, Table2, 0);
            if (sum != 0)
            {
                TextBox2.Text = "Price, that need to be paid";
                TextBox3.Visible = true;
                TextBox3.Text = sum.ToString();
            }
            else
            {
                TextBox2.Text = "There is nothing to pay for";
            }
        }
        /// <summary>
        /// Creates an empty file
        /// </summary>
        /// <param name="path"></param>
        protected void CreateFile(string path)
        {
            using(StreamWriter writer = new StreamWriter(path))
            {

            }
        }
        /// <summary>
        /// Returns items, that are valid for order
        /// </summary>
        /// <param name="Orders"></param>
        /// <param name="Items"></param>
        /// <returns>List of items</returns>
        protected List<Item> FindItems(List<Order> Orders, List<Item> Items)
        {
            var New = new List<Item>();
            Items = Items.OrderBy(n => n.Price).ToList();
            foreach(Order order in Orders)
            {
                List<Item> Temp = OrderExecution(order, Items);
                New = New.Concat<Item>(Temp).ToList<Item>();
                
            }

            return New;
        }
        /// <summary>
        /// Returns list of items, that are valid for a certain order
        /// </summary>
        /// <param name="order"></param>
        /// <param name="Items"></param>
        /// <returns>List of items</returns>
        protected List<Item> OrderExecution(Order order, List<Item> Items)
        {
            List<Item> New = new List<Item>();
            int amount = 0;
            List<Item> Temp = (from item in Items where item.Name.CompareTo(order.Name) == 0 & item.Price == 990  select item).ToList();
            //List<Item> Temp = Items.Where(item =>
            //item.Name.CompareTo(order.Name) == 0).ToList();
            foreach(Item item in Temp)
            {
                if (amount < order.Amount)
                {
                    if (amount + item.Amount <= order.Amount)
                    {
                        New.Add(item);
                        amount += item.Amount;
                    }
                    else
                    {
                        int temp = order.Amount - amount;
                        item.Amount = temp;
                        New.Add(item);
                        amount += item.Amount;
                    }
                }
                if (amount >= order.Amount)
                    break;
            }
            return New;
        }
        /// <summary>
        /// Minimizes order, if user can not pay for it
        /// </summary>
        /// <param name="sumOfOrder"></param>
        /// <param name="userSum"></param>
        /// <param name="Items"></param>
        /// <returns>Order price</returns>
        protected decimal MinimizeOrder(decimal sumOfOrder,
            decimal userSum, List<Item> Items)
        {
            Items.OrderBy(n => n.Price).ToList();
            while (sumOfOrder > userSum)
            {
                if (Items[0].Amount != 0)
                {
                    sumOfOrder -= Items[0].Price;
                    Items[0].Amount--;
                    if (Items[0].Amount == 0)
                    {
                        Items.RemoveAt(0);
                    }
                }
                else
                {
                    Items.RemoveAt(0);
                }
            }
            return sumOfOrder;
        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}