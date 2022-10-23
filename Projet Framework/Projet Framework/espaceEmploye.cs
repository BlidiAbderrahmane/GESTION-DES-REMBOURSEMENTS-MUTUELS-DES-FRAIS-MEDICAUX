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
    public partial class espaceEmploye : Form
    {
        public espaceEmploye()
        {
            InitializeComponent();
        }

        SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-0GDF4CR\\SQLEXPRESS;Initial Catalog=cimentsbizerte;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
        }

        public int trouver()
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            String daten = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            cmd = new SqlCommand("select count(*) from bulletinsoin where matricule= Upper('" + textBox1.Text + "') and datedepot='" + daten + "'", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
        }

        private void espaceEmploye_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



        private void button4_Click(object sender, EventArgs e)
        {
            var myForm = new loginForm();
            this.Hide();
            myForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Le champ du matricule est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (trouver() == 0)
                    MessageBox.Show("Il n'y a pas un bulletin avec cette matricule et cette date", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    deconnecter();
                    cnx.Open();
                    String daten = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    cmd = new SqlCommand("select * from bulletinsoin where matricule='" + textBox1.Text + "' and datedepot='" + daten + "'", cnx);
                    reader = cmd.ExecuteReader();
                    string message = "";
                    while (reader.Read())
                    {
                        if (reader["acceptation"].ToString() == "True")
                            message += "Votre demande de remboursement numéro " + reader["numbulletin"].ToString() + " est acceptée\n";
                        else if (reader["acceptation"].ToString() == "False")
                            message += "Votre demande de remboursement numéro " + reader["numbulletin"].ToString() + " est réfusée\n";
                    }
                    MessageBox.Show(message, "Réponse", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cnx.Close();
                }
            }
        }
    }
}