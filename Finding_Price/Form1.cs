using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finding_Price
{

    public class NameAndPrice
    {
        public string Name { get; set; }
        public string Price { get; set; }


    }



    public partial class Form1 : Form
    {
        DataTable table;
        HtmlWeb web = new HtmlWeb();

        public Form1()
        {
            InitializeComponent();
            InitTable();
        }

    


        private void InitTable()
        {

            table = new DataTable("Chepeast Price");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(string));


            dataGridView1.DataSource = table;
        }


        private async Task<List<NameAndPrice>> AllPrices()
        {

            string url = "https://www.kainos.lt/mobilieji-telefonai/huawei-mate-20-pro-128gb-black-juodas-v439503";


            var doc = await Task.Factory.StartNew(() => web.Load(url));
            var nameNodes = doc.DocumentNode.SelectNodes("//*[@id=\"item_prices\"]//table//tr//td//a");
            var priceNodes = doc.DocumentNode.SelectNodes("//*[@id=\"item_prices\"]//table//tr//td//div//span");


            var names = nameNodes.Select(node => node.InnerText);
            var prices = priceNodes.Select(node => node.InnerText);

            return names.Zip(prices, (name, price) => new NameAndPrice() { Name = name, Price = price }).ToList();

        }

        private async void Form1_Loadc(object sender, EventArgs e)
        {
            //int pageNum = 0;

            var raknings = await AllPrices();

            while (raknings.Count > 0)
            {
                foreach (var rakning in raknings)
                {
                    table.Rows.Add(rakning.Name, rakning.Price);
                    raknings = await AllPrices();

                }
            }
        }

    }
}
