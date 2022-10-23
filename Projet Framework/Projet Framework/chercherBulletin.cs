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
    public partial class chercherBulletin : Form
    {
        public chercherBulletin()
        {
            InitializeComponent();
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

        public int trouver1()
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select count(*) from bulletinsoin where numbulletin= Upper('" + textBox1.Text + "')", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
        }

        public int trouver2()
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            String daten = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            cmd = new SqlCommand("select count(*) from bulletinsoin where matricule= Upper('" + textBox1.Text + "') and datedepot='"+daten+"'", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
        }

        public int trouver3()
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            int annee = comboBox1.SelectedIndex + 2000;
            cmd = new SqlCommand("select count(*) from bulletinsoin where matricule= Upper('" + textBox2.Text + "') and datedepot>='" + annee + "0101' and datedepot<='"+annee+"1231'", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
        }



        private void button5_Click(object sender, EventArgs e)
        {
            var myForm = new espaceAgent();
            this.Hide();
            myForm.Show();
        }

        private void chercherBulletin_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Vérifier la manière de recherche", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    if (textBox1.Text == "")
                        MessageBox.Show("Le champ du numéro de bulletin est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (trouver1() == 0)
                        MessageBox.Show("Il n'y a pas un bulletin avec ce numéro", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        deconnecter();
                        cnx.Open();
                        table.Clear();
                        cmd = new SqlCommand("select * from bulletinsoin where numbulletin='" + textBox1.Text + "'", cnx);
                        reader = cmd.ExecuteReader();
                        table.Load(reader);
                        dataGridView1.DataSource = table;
                        comboBox2.SelectedIndex = -1;
                        textBox1.Text = "";
                        cnx.Close();
                    }
                }
                else
                {
                    if (textBox1.Text == "")
                        MessageBox.Show("Le champ du matricule est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (trouver2() == 0)
                        MessageBox.Show("Il n'y a pas un bulletin avec cette matricule et cette date", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        deconnecter();
                        cnx.Open();
                        table.Clear();
                        String daten = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                        cmd = new SqlCommand("select * from bulletinsoin where matricule='" + textBox1.Text + "' and datedepot='" + daten + "'", cnx);
                        reader = cmd.ExecuteReader();
                        table.Load(reader);
                        dataGridView1.DataSource = table;
                        comboBox2.SelectedIndex = -1;
                        textBox1.Text = "";
                        cnx.Close();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text=="")
                MessageBox.Show("Le champ du matricule est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (comboBox1.SelectedIndex<0)
                MessageBox.Show("Vérifier le choix de l'année", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (trouver3()==0)
                    MessageBox.Show("Il n'y a pas un bulletin avec cette matricule pendant cette année", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    deconnecter();
                    cnx.Open();
                    table.Clear();
                    int annee = comboBox1.SelectedIndex + 2000;
                    cmd = new SqlCommand("select * from bulletinsoin where matricule= Upper('" + textBox2.Text + "') and datedepot>='" + annee + "0101' and datedepot<='" + annee + "1231'", cnx);
                    reader = cmd.ExecuteReader();
                    table.Load(reader);
                    dataGridView1.DataSource = table;
                    comboBox1.SelectedIndex = -1;
                    textBox2.Text = "";
                    cnx.Close();
                }
            }
        }
    }
}
