using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bashmak
{
    public partial class Edit : Form
    {
        private readonly Cells cells;

        public string kod;
        public string post;
        public string naimen;
        public string edizm;
        public string vid;
        public string cena;
        public Edit(Cells Cells)
        {
            cells = Cells;
            InitializeComponent();
        }
        private void Edit_Load(object sender, EventArgs e)
        {
            textBox1.Text = post;
            textBox2.Text = naimen;
            comboBox1.Text = edizm;
            comboBox2.Text = vid;
            textBox3.Text = cena;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cells.d = true;
            cells.post = textBox1.Text;
            cells.naimen = textBox2.Text;
            cells.vid = comboBox2.Text;
            cells.edizm = comboBox1.Text;
            cells.cena = textBox3.Text;
            this.Close();
        }
    }
}
