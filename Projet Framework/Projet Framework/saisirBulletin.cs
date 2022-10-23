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
    public partial class saisirBulletin : Form
    {
        public saisirBulletin()
        {
            InitializeComponent();
            generateNumBulletin();
        }

        SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-0GDF4CR\\SQLEXPRESS;Initial Catalog=cimentsbizerte;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
        }

        public int trouverBulletin(String ch)
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select count(*) from bulletinsoin where numbulletin= Upper('" + ch + "')", cnx);
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
            cmd = new SqlCommand("select count(*) from bulletinsoin where matricule= Upper('" + textBox4.Text + "') and datedepot='"+daten+"'", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
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

        public void generateNumBulletin()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[6];
            var random = new Random();
            bool b = true;
            while (b)
            {
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                if (trouverBulletin(finalString) == 0)
                {
                    textBox1.Text = finalString;
                    b = false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var myForm = new espaceAgent();
            this.Hide();
            myForm.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void saisirBulletin_Load(object sender, EventArgs e)
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
            if (textBox5.Text=="")
                MessageBox.Show("Le champ de l'acte est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ((textBox3.Text == "") || (!IsFloatOrInt(textBox3.Text)))
                MessageBox.Show("Vérifier le champ du frais de l'acte", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (textBox4.Text=="")
                MessageBox.Show("Le champ du matricule est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (trouverMatricule()==0)
                MessageBox.Show("Il n'y a pas un employé avec cette matricule", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                deconnecter();
                cnx.Open();
                String daten = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                cmd = new SqlCommand("insert into bulletinsoin values ('" + textBox1.Text + "','" + daten + "','"
                    + textBox5.Text + "'," + textBox3.Text + ",'" + textBox4.Text + "',NULL)", cnx);
                cmd.ExecuteNonQuery();
                cnx.Close();
                var myForm = new rapportBulletin(textBox1.Text);
                this.Hide();
                myForm.Show();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
