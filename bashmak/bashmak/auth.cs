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
    public partial class auth : Form
    {

        public bool firstenrty = true;
        public static string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString() + @"C:\bd\bashmak.mdf";
        public auth()
        {
            InitializeComponent();
        }

        public bool getadminbylogin(string login)
        {
            for (int i = 0; i < авторизацияDataGridView.Rows.Count; i++)
            {
                if (авторизацияDataGridView[1, i].Value.ToString() == login)
                {
                    return Convert.ToBoolean(авторизацияDataGridView[3, i].Value);
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ВХОД
            string[] login = new string[5];
            string[] pass = new string[5];
            for (int i = 0; i < авторизацияDataGridView.Rows.Count; i++)
            {
                login[i] = авторизацияDataGridView[1, i].Value.ToString();
                pass[i] = авторизацияDataGridView[2, i].Value.ToString();
            }
            string curlog = "";
            string curpass = "";
            for (int i = 0; i < login.Length; i++)
            {
                if (textBox1.Text == login[i])
                {
                    curlog = textBox1.Text;
                }
                if (textBox2.Text == pass[i])
                {
                    curpass = textBox2.Text;
                }
            }
            if (curlog == "" || curpass == "")
            {
                MessageBox.Show("Введите логин и пароль");
            }
            else
            {
                if (getadminbylogin(curlog))
                {
                    Menu newForm = new Menu(this);
                    newForm.Show();
                    Visible = false;
                    MessageBox.Show("Вход как админ");
                    this.Hide();
                }
                else
                {
                    User newForm = new User(this);
                    newForm.Show();
                    Visible = false;
                    MessageBox.Show("Вход как пользователь");
                    this.Hide();
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();    
            reg registr = new reg(this);
            registr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }   

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bashmakDataSet.Авторизация". При необходимости она может быть перемещена или удалена.
            this.авторизацияTableAdapter.Fill(this.bashmakDataSet.Авторизация);

        }
    }
}
