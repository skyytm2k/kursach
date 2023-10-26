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

namespace bashmak
{
    public partial class Cells : Form
    {
        public Cells()
        {
            InitializeComponent();
        }

        private void Cells_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bashmakDataSet.Товары". При необходимости она может быть перемещена или удалена.
            this.товарыTableAdapter.Fill(this.bashmakDataSet.Товары);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bashmakDataSet.Товары". При необходимости она может быть перемещена или удалена.
            this.товарыTableAdapter.Fill(this.bashmakDataSet.Товары);
            DataView myDataView = new DataView(bashmakDataSet.Товары);
            myDataView.Sort = "Код товара ASC";
            товарыDataGridView.DataSource = myDataView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"provider=LAPTOP-DKJC5U4V\SQLEXPRESS;Data Source=C:\bd\bashmak.mdf""";// строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);                   // создаем соеденение
            int kod = Convert.ToInt32(textBox1.Text);
            string post = textBox2.Text;
            string naimen = textBox3.Text;
            string edizm = comboBox1.Text;
            string vid = comboBox2.Text;            
            string cena = textBox4.Text;
            string query = "INSERT INTO товары ([Код товара],[Код поставщика], [Наименование товара], [Единица измерения], [Вид товара], [Цена]) VALUES ('" + kod + "','"+ post +"', '" + naimen + "','" + edizm + "','" + vid + "','" + cena  + "')";
            OleDbCommand command = new OleDbCommand(query, dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("Товар добавлен");
            this.товарыTableAdapter.Fill(this.bashmakDataSet.Товары);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "provider=Microsoft.ACE.OLEDB.12.0;Data Source=OmeStore.mdb";  // строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);                   // создаем соеденение
            int сod = Convert.ToInt32(textBox6.Text);
            string query = "DELETE FROM Товары WHERE [Код товара] = " + сod;
            OleDbCommand command = new OleDbCommand(query, dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("Данные о товаре удалены");
            this.товарыTableAdapter.Fill(this.bashmakDataSet.Товары);
            textBox6.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string kod;
        public string post;
        public string naimen;
        public string edizm;
        public string vid;
        public string cena;
        public string kolich;

        public bool d = false;

        private void button4_Click(object sender, EventArgs e)
        {
            int kod = Convert.ToInt32(товарыDataGridView[0, товарыDataGridView.CurrentCell.RowIndex].Value);
            Edit ek = new Edit(this);   
            ek.post = товарыDataGridView[1, товарыDataGridView.CurrentCell.RowIndex].Value.ToString();
            ek.naimen = товарыDataGridView[2, товарыDataGridView.CurrentCell.RowIndex].Value.ToString();
            ek.edizm = товарыDataGridView[3, товарыDataGridView.CurrentCell.RowIndex].Value.ToString();
            ek.vid = товарыDataGridView[4, товарыDataGridView.CurrentCell.RowIndex].Value.ToString();
            ek.cena = товарыDataGridView[5, товарыDataGridView.CurrentCell.RowIndex].Value.ToString();
            ek.ShowDialog();

            if (d == false) return;
            else d = false;

            string put = @"E:\Develop Debilnik\Praktika\bin\Debug\OmeStore.mdb";

            OleDbConnection connection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", put));
            OleDbCommand updCommand = new OleDbCommand("UPDATE товары SET [Код поставщика] = ?, [Наименование товара] = ?, [Единица измерения] = ?, [Вид товара] = ?, [Цена] = ?, [Кол-во на складе] = ? WHERE [Код товара] = ?", connection);

            connection.Open();

            updCommand.Parameters.AddWithValue("@Код товара", kod);
            updCommand.Parameters.AddWithValue("@Код поставщика", post);
            updCommand.Parameters.AddWithValue("@Наименование товара", naimen);
            updCommand.Parameters.AddWithValue("@Единица измерения", edizm);
            updCommand.Parameters.AddWithValue("@Вид товара", vid);
            updCommand.Parameters.AddWithValue("@Цена", cena);

            updCommand.ExecuteNonQuery();   

            товарыTableAdapter.Update(bashmakDataSet.Товары);

            connection.Close();

            товарыTableAdapter.Update(bashmakDataSet.Товары);

            this.товарыTableAdapter.Fill(this.bashmakDataSet.Товары);
            for (int i = 0; i < товарыDataGridView.RowCount; i++)
                if (Convert.ToInt32(товарыDataGridView[0, i].Value) == kod)
                {
                    товарыDataGridView[1, i].Value = post;
                    товарыDataGridView[2, i].Value = naimen;
                    товарыDataGridView[3, i].Value = edizm;
                    товарыDataGridView[4, i].Value = vid;
                    товарыDataGridView[5, i].Value = cena;
                }

            товарыDataGridView.Rows[0].Selected = false;
            for (int i = 0; i < товарыDataGridView.Rows.Count; i++)
                if (Convert.ToInt32(товарыDataGridView[0, i].Value) == kod)
                    товарыDataGridView.Rows[i].Selected = false;

        }
    }
}
