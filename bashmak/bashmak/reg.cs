using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bashmak
{

    public partial class reg : Form
    {
        private readonly auth _auth;
        public static string connectionString = @"server=LAPTOP-DKJC5U4V\SQLEXPRESS;database=bashmak;Integrated Security=true";
        public SqlConnection cn;
        public reg(auth auth)
        {
            InitializeComponent();
            _auth = auth;
            cn = new SqlConnection(connectionString);
            cn.Open();
        }
        
        private void butto1_Click(object sender, EventArgs e)
        {

            string[] login = new string[авторизацияDataGridView.Rows.Count];
            string[] pass = new string[авторизацияDataGridView.Rows.Count];


            for (int i = 0; i < авторизацияDataGridView.Rows.Count; i++)
            {
                login[i] = авторизацияDataGridView[1, i].Value.ToString();
                pass[i] = авторизацияDataGridView[2, i].Value.ToString();
            }


            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                MessageBox.Show("Введите логин и пароль");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO авторизация([Код],[Логин],[Пароль],[Админ]) Values(@Код, @Логин, @Пароль, " + false, cn);
                cmd.Parameters.AddWithValue("@Код", Convert.ToInt32(авторизацияDataGridView[0, авторизацияDataGridView.Rows.Count - 1].Value) + 1);
                cmd.Parameters.AddWithValue("@Логин", textBox1.Text);
                cmd.Parameters.AddWithValue("@Пароль", textBox2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Вы успешно зарегестрировали аккаунт", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                авторизацияDataGridView[0, авторизацияDataGridView.Rows.Count - 1].Value = Convert.ToInt32(авторизацияDataGridView[0, авторизацияDataGridView.Rows.Count - 2].Value) + 1;
                авторизацияDataGridView[1, авторизацияDataGridView.Rows.Count - 1].Value = textBox1.Text;
                авторизацияDataGridView[2, авторизацияDataGridView.Rows.Count - 1].Value = textBox2.Text;
                авторизацияDataGridView[3, авторизацияDataGridView.Rows.Count - 1].Value = false;
                auth registr = new auth();
                registr.Show();
                this.Close();
                Application.Restart();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] login = new string[авторизацияDataGridView.Rows.Count];
            string[] pass = new string[авторизацияDataGridView.Rows.Count];

            for (int i = 0; i < авторизацияDataGridView.Rows.Count; i++)
            {
                login[i] = авторизацияDataGridView[1, i].Value.ToString();
                pass[i] = авторизацияDataGridView[2, i].Value.ToString();
            }
            string curlog = "";
            string curpass = "";
            curlog = textBox1.Text;
            curpass = textBox2.Text;
            if (curlog == "" || curpass == "")
            {
                MessageBox.Show("Введите логин и пароль");
            }
            else
            {

                OleDbCommand command1 = new OleDbCommand();
                command1.CommandText = "INSERT INTO авторизация([Код],[Логин],[Пароль],[Админ]) Values (@Код,@Логин,@Пароль," + false + ")";
                command1.Parameters.AddWithValue("@Код", Convert.ToInt32(авторизацияDataGridView[0, авторизацияDataGridView.Rows.Count - 1].Value) + 1);
                command1.Parameters.AddWithValue("@Логин", textBox1.Text);
                command1.Parameters.AddWithValue("@Пароль", textBox2.Text);
                command1.ExecuteNonQuery();
                авторизацияDataGridView[0, авторизацияDataGridView.Rows.Count - 1].Value = Convert.ToInt32(авторизацияDataGridView[0, авторизацияDataGridView.Rows.Count - 2].Value) + 1;
                авторизацияDataGridView[1, авторизацияDataGridView.Rows.Count - 1].Value = textBox1.Text;
                авторизацияDataGridView[2, авторизацияDataGridView.Rows.Count - 1].Value = textBox2.Text;
                авторизацияDataGridView[3, авторизацияDataGridView.Rows.Count - 1].Value = false;
                auth registr = new auth();
                registr.Show();
                this.Close();
                Application.Restart();
            }
        }




        private void reg_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bashmakDataSet.Авторизация". При необходимости она может быть перемещена или удалена.
            this.авторизацияTableAdapter.Fill(this.bashmakDataSet.Авторизация);

        }
    }
}
