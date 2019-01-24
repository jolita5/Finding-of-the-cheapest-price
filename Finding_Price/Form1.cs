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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.facebook.com/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlElement user = webBrowser1.Document.GetElementById("email");
                HtmlElement password = webBrowser1.Document.GetElementById("pass");


                user.SetAttribute("value", textBox1.Text);
                password.SetAttribute("value", textBox2.Text);

            }
            catch (Exception)
            {

                MessageBox.Show("Error!");
            }
            


        }

        private void button2_Click(object sender, EventArgs e)
        {
            HtmlElement btnClick = webBrowser1.Document.GetElementById("u_0_8");

            btnClick.InvokeMember("click");

        }


    }
}
