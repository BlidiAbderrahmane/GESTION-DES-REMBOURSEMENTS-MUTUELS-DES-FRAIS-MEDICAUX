using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_Framework
{
    public partial class modifierBulletin1 : Form
    {
        public modifierBulletin1()
        {
            InitializeComponent();
            remplirdgv();
        }

        SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-0GDF4CR\\SQLEXPRESS;Initial Catalog=cimentsbizerte;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable table = new DataTable();
        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
        }
        public void remplirdgv()
        {
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select * from bulletinsoin", cnx);
            reader = cmd.ExecuteReader();
            table.Load(reader);
            dataGridView1.DataSource = table;
            cnx.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            var myForm = new espaceAgent();
            this.Hide();
            myForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            var myForm = new espaceAgent();
            this.Hide();
            myForm.Show();
        }

        public int trouver()
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select count(*) from bulletinsoin where numbulletin= Upper('" + textBox1.Text + "')", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Le champ du numéro de bulletin est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (trouver() == 0)
                    MessageBox.Show("Il n'y a pas un bulletin avec ce numéro", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    String mat = textBox1.Text;
                    var myForm = new modifierBulletin2(mat);
                    this.Hide();
                    myForm.Show();
                }
            }
        }
    }
}
