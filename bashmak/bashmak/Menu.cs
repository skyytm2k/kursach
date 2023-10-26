using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Common;
using System.Windows.Input;
using System.IO;

namespace bashmak
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private readonly auth _auth;
        public static string ConnectionString = @"provider=LAPTOP-DKJC5U4V\\SQLEXPRESS;Data Source=" + new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString() + "\\bashmak.mdb";
        public OleDbConnection connection;
        public Menu(auth auth)
        {
            _auth = auth;
            InitializeComponent();
            connection = new OleDbConnection(ConnectionString);
            connection.Open();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Chek newForm = new Chek();
            newForm.ShowDialog();
            newForm.SetDesktopLocation(this.Left, this.Top);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cells newForm = new Cells();
            newForm.ShowDialog();
            newForm.SetDesktopLocation(this.Left, this.Top);
        }

       private void button3_Click(object sender, EventArgs e)
        {
          Postav newForm = new Postav();
          newForm.ShowDialog();
          newForm.SetDesktopLocation(this.Left, this.Top);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            auth newForm = new auth();
            newForm.ShowDialog();
            newForm.SetDesktopLocation(this.Left, this.Top);
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
