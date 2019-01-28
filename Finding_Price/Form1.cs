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


        private async Task<List<NameAndPrice>> AllPrices(int pageNr)
        {
            string url = "https://www.kainos.lt/mobilieji-telefonai/xiaomi-pocophone-f1-64gb-dual-black-juodas-v411339";


            if (pageNr != 0)
            {
                url = "https://www.kainos.lt/mobilieji-telefonai/xiaomi-pocophone-f1-64gb-dual-black-juodas-v411339" + pageNr.ToString();
            }




            var doc = await Task.Factory.StartNew(() => web.Load(url));
            var nameNodes = doc.DocumentNode.SelectNodes("//*[@id=\"item_prices\"]//table//tr//td//a");
            var priceNodes = doc.DocumentNode.SelectNodes("//*[@id=\"item_prices\"]//table//tr//td//div//span");


   

            if (nameNodes == null && priceNodes == null)
            {
                return new List<NameAndPrice>();
            }

            var names = nameNodes.Select(node => node.InnerText);
            var prices = priceNodes.Select(node => node.InnerText);
            

            return names.Zip(prices, (name, price) => new NameAndPrice() { Name = name, Price = price }).ToList();


        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            int pageNum = 0;

            var raknings = await AllPrices(pageNum);



            while (raknings.Count > 0)
            {

                foreach (var rakning in raknings)
                {
                    table.Rows.Add(rakning.Name, rakning.Price);
                    raknings = await AllPrices(++pageNum);

                }


            }


        }

    }
}
