using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Common;
using System.Windows.Input;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Runtime.Remoting.Contexts;

namespace bashmak
{
    public partial class User : Form
    {
        private readonly auth _auth;
        public static string ConnectionString = @"provider=LAPTOP-DKJC5U4V\\SQLEXPRESS;Data Source=" + new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString() + "\\bashmak.mdb";
        public OleDbConnection connection;


        public User(auth auth)
        {
            _auth = auth;
            InitializeComponent();
            connection = new OleDbConnection(ConnectionString);
            connection.Open();
        }

        private void User_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bashmakDataSet.Товары". При необходимости она может быть перемещена или удалена.
            this.товарыTableAdapter.Fill(this.bashmakDataSet.Товары);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bashmakDataSet.Заказы". При необходимости она может быть перемещена или удалена.
            this.заказыTableAdapter.Fill(this.bashmakDataSet.Заказы);
            DataView myDataView = new DataView(bashmakDataSet.Заказы);
            myDataView.Sort = "Код заказа ASC";
            заказыDataGridView.DataSource = myDataView;
            //
            DataView myDataView1 = new DataView(bashmakDataSet.Товары);
            myDataView1.Sort = "Код товара ASC";
            товарыDataGridView.DataSource = myDataView1;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            auth newForm = new auth();
            newForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int s = заказыDataGridView.CurrentCell.RowIndex;
            listBox1.Items.Add("Код заказа - " + заказыDataGridView[0, s].Value.ToString());
            listBox1.Items.Add("Код товара - " + заказыDataGridView[1, s].Value.ToString());
            listBox1.Items.Add("Дата размещения - " + заказыDataGridView[2, s].Value.ToString());
            listBox1.Items.Add("Количество - " + заказыDataGridView[3, s].Value.ToString());
            listBox1.Items.Add("Цена - " + заказыDataGridView[4, s].Value.ToString());
            listBox1.Items.Add("Итоговая цена - " + заказыDataGridView[5, s].Value.ToString());
            string str = заказыDataGridView[5, s].Value.ToString();
            int w = listBox1.Items.Count;
            int q = -1;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ';')
                {
                    listBox1.Items.Add(str.Substring(q + 1, i - q));
                    q = i;
                }
            }

            for (int i = w; i < listBox1.Items.Count; i++)
            {
                listBox1.Items[i] = listBox1.Items[i].ToString().Substring(0, listBox1.Items[i].ToString().Length - 1);
            }
            printDocument1.Print();
        }

        public Bitmap Scre(Control control)
        {
            Bitmap bmp = new Bitmap(control.Width, control.Height);
            control.DrawToBitmap(bmp, control.ClientRectangle);
            return bmp;
        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(Scre(panel1), 0, 0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox3.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string connectionString = @"provider=Microsoft.ACE.OLEDB.12.0;Data Source=OmeStore.mdb";  // строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);                   // создаем соеденение
            int kod = Convert.ToInt32(textBox1.Text);
            string tovar = textBox2.Text;
            string dataraz = textBox3.Text;
            string datados = textBox4.Text;
            int kolich = Convert.ToInt32(textBox5.Text);
            int chena = Convert.ToInt32(textBox6.Text);
            int ichena = kolich * chena;
            string query = "INSERT INTO заказы ([Код заказа],[Код товара], [Дата размещения], [Дата исполнения], [Цена], [Количество], [Итоговая цена]) VALUES ('" + kod + "','" + tovar + "','" + dataraz + "','" + datados + "','" + kolich + "','" + chena + "','" + ichena + "')";
            OleDbCommand command = new OleDbCommand(query, dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("Заказ произведен");
            this.заказыTableAdapter.Fill(this.bashmakDataSet.Заказы);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
    }
}
