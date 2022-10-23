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
    public partial class gestionEmploye : Form
    {
        public gestionEmploye()
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
            cmd = new SqlCommand("select * from employe", cnx);
            reader = cmd.ExecuteReader();
            table.Load(reader);
            dataGridView1.DataSource = table;
            cnx.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var myForm = new ajouterEmploye();
            this.Hide();
            myForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Le champ du matricule est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (trouver() == 0)
                    MessageBox.Show("Il n'y a pas un employé avec cette matricule", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    String mat = textBox1.Text; 
                    var myForm = new modifierEmploye(mat);
                    this.Hide();
                    myForm.Show();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var myForm = new espaceAdmin();
            this.Hide();
            myForm.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public int trouver()
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select count(*) from employe where matricule= Upper('" + textBox1.Text + "')", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Le champ du matricule est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (trouver()==0)
                    MessageBox.Show("Il n'y a pas un employé avec cette matricule", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    deconnecter();
                    cnx.Open();
                    cmd = new SqlCommand("delete from employe where matricule='"+textBox1.Text+"'", cnx);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employé est supprimé","SUPPRESSION",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    table.Clear();
                    remplirdgv();
                    textBox1.Text = "";
                    cnx.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Le champ du matricule est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (trouver() == 0)
                    MessageBox.Show("Il n'y a pas un employé avec cette matricule", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    var myForm = new infosEmploye(textBox1.Text);
                    this.Hide();
                    myForm.Show();
                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            var myForm = new espaceAdmin();
            this.Hide();
            myForm.Show();
        }
    }
}
