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
    public partial class modifierBulletin2 : Form
    {
        public modifierBulletin2(String num)
        {
            InitializeComponent();
            remplir(num);
        }

        SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-0GDF4CR\\SQLEXPRESS;Initial Catalog=cimentsbizerte;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
        }


        public void remplir(String num)
        {
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select * from bulletinsoin where numbulletin = '" + num + "'", cnx);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                textBox1.Text = reader["numBulletin"].ToString();
                textBox3.Text = reader["fraisActe"].ToString();
                dateTimePicker1.Value = (DateTime)reader["dateDepot"];
                textBox4.Text = reader["matricule"].ToString();
                textBox5.Text = reader["acte"].ToString();
            }
            cnx.Close();
        }

        public int trouverMatricule()
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select count(*) from employe where matricule= Upper('" + textBox4.Text + "')", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
        }

        public int trouver()
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            String daten = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            cmd = new SqlCommand("select count(*) from bulletinsoin where matricule= Upper('" + textBox4.Text + "') and datedepot='" + daten + "' and numbulletin<>'"+ textBox1.Text + "'", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
        }

        private void modifierBulletin2_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        public bool IsFloatOrInt(string value)
        {
            int intValue;
            float floatValue;
            return Int32.TryParse(value, out intValue) || float.TryParse(value, out floatValue);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
                MessageBox.Show("Le champ de l'acte est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ((textBox3.Text == "") || (!IsFloatOrInt(textBox3.Text)))
                MessageBox.Show("Vérifier le champ du frais de l'acte", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (textBox4.Text == "")
                MessageBox.Show("Le champ du matricule est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (trouverMatricule() == 0)
                MessageBox.Show("Il n'y a pas un employé avec cette matricule", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                deconnecter();
                cnx.Open();
                String daten = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                cmd = new SqlCommand("update bulletinsoin set datedepot='" + daten + "', acte='"
                    + textBox5.Text + "', fraisacte=" + textBox3.Text + ", matricule='"
                    + textBox4.Text + "', acceptation=NULL where numbulletin='" + textBox1.Text + "'", cnx);
                cmd.ExecuteNonQuery();
                cnx.Close();
                var myForm = new rapportBulletin(textBox1.Text);
                this.Hide();
                myForm.Show();           
            }
        }
    }
}
