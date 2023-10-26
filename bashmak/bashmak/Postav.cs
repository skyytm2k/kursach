using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bashmak
{
    public partial class Postav : Form
    {
        public Postav()
        {
            InitializeComponent();
        }

        private void Postav_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bashmakDataSet.Поставщики". При необходимости она может быть перемещена или удалена.
            this.поставщикиTableAdapter.Fill(this.bashmakDataSet.Поставщики);
            DataView myDataView = new DataView(bashmakDataSet.Поставщики);
            myDataView.Sort = "Код поставщика ASC";
            поставщикиDataGridView.DataSource = myDataView;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"provider=LAPTOP-DKJC5U4V\SQLEXPRESS;Data Source=bashmak.mdb";// строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);                   // создаем соеденение
            int kod = Convert.ToInt32(textBox1.Text);
            string nazv = textBox2.Text;
            string predst = textBox3.Text;
            string tel = textBox4.Text;
            string adres = textBox5.Text;
            string query = "INSERT INTO поставщики ([Код поставщика],[Название поставщика], [Представитель], [Телефон], [Адрес]) VALUES ('" + kod + "','" + nazv + "', '" + predst + "','" + tel + "','" + adres + "')";
            OleDbCommand command = new OleDbCommand(query, dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("Поставщик добавлен");
            this.поставщикиTableAdapter.Fill(this.bashmakDataSet.Поставщики);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = @"provider=LAPTOP-DKJC5U4V\SQLEXPRESS;Data Source=bashmak.mdb";// строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);                   // создаем соеденение
            int сod = Convert.ToInt32(textBox6.Text);
            string query = "DELETE FROM Поставщики WHERE [Код поставщика] = " + сod;
            OleDbCommand command = new OleDbCommand(query, dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("Данные о поставщике удалены");
            this.поставщикиTableAdapter.Fill(this.bashmakDataSet.Поставщики);
            textBox6.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
