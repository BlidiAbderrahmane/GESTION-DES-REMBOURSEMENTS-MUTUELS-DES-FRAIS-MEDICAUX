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
    public partial class infosEmploye : Form
    {
        public infosEmploye(String mat)
        {
            InitializeComponent();
            remplir(mat);
        }

        SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-0GDF4CR\\SQLEXPRESS;Initial Catalog=cimentsbizerte;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        public void remplir(String mat)
        {
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select * from employe where matricule='"+mat+"'", cnx);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                labelMatricule.Text = reader["matricule"].ToString();
                labelNom.Text = reader["nom"].ToString();
                labelPrenom.Text = reader["prenom"].ToString();
                labelCIN.Text = reader["cin"].ToString();
                labelDate.Text = ((DateTime)reader["dateNaissance"]).ToString("dd - MMMM - yyyy");
                labelAdresse.Text = reader["adresse"].ToString();
                labelGrade.Text = reader["grade"].ToString();
                labelTel.Text = reader["tel"].ToString();
                labelCNAM.Text = reader["cnam"].ToString();
                labelNomCJ.Text = reader["nomConjoint"].ToString();
                labelPrenomCJ.Text = reader["prenomConjoint"].ToString();
                labelNbrEnfants.Text = reader["nbrEnfants"].ToString();
                labelEtat.Text = reader["etatCivil"].ToString();
            }
            cnx.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var myForm = new gestionEmploye();
            this.Hide();
            myForm.Show();
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelNbrEnfants_Click(object sender, EventArgs e)
        {

        }

        private void labelPrenomCJ_Click(object sender, EventArgs e)
        {

        }

        private void labelNomCJ_Click(object sender, EventArgs e)
        {

        }

        private void labelEtat_Click(object sender, EventArgs e)
        {

        }

        private void labelCNAM_Click(object sender, EventArgs e)
        {

        }

        private void labelTel_Click(object sender, EventArgs e)
        {

        }

        private void labelAdresse_Click(object sender, EventArgs e)
        {

        }

        private void labelDate_Click(object sender, EventArgs e)
        {

        }

        private void labelCIN_Click(object sender, EventArgs e)
        {

        }

        private void labelPrenom_Click(object sender, EventArgs e)
        {

        }

        private void labelNom_Click(object sender, EventArgs e)
        {

        }

        private void labelMatricule_Click(object sender, EventArgs e)
        {

        }
    }
}
