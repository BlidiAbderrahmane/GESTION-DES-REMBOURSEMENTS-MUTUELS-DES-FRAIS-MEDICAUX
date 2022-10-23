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
    public partial class ajouterEmploye : Form
    {
        public ajouterEmploye()
        {
            InitializeComponent();
            generateMatricule();
        }

        SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-0GDF4CR\\SQLEXPRESS;Initial Catalog=cimentsbizerte;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
        }

        public int trouver(String ch)
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select count(*) from employe where matricule= Upper('" + ch + "')", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
        }

        public void generateMatricule()
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
                if (trouver(finalString) == 0)
                {
                    textBox1.Text = finalString;
                    b = false;
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                MessageBox.Show("Le champ du nom est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (textBox3.Text == "")
                MessageBox.Show("Le champ du prénom est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ((textBox4.Text == "") || (!IsDigitsOnly(textBox4.Text))||(textBox4.Text.Length!=8) || ((textBox4.Text[0] != '0') && (textBox4.Text[0] != '1')))
                MessageBox.Show("Vérifier le champ du CIN", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (textBox6.Text == "")
                MessageBox.Show("Le champ d'adresse est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (comboBox1.SelectedIndex<0)
                MessageBox.Show("Vérifier le choix du grade", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ((textBox8.Text == "") || (!IsDigitsOnly(textBox8.Text)) || (textBox8.Text.Length != 8))
                MessageBox.Show("Vérifier le champ du numéro de téléphone", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ((textBox9.Text == "") || (!IsDigitsOnly(textBox9.Text)))
                MessageBox.Show("Vérifier le champ du code CNAM", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (comboBox2.SelectedIndex < 0)
                MessageBox.Show("Vérifier le choix d'état civil", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ((textBox13.Text == "") || (!IsDigitsOnly(textBox13.Text)))
                MessageBox.Show("Vérifier le champ du nombre d'enfants", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                deconnecter();
                cnx.Open();
                String daten = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                int grade = comboBox1.SelectedIndex + 1;
                String etat = comboBox2.SelectedItem.ToString();
                cmd = new SqlCommand("insert into employe values ('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"
                    +textBox4.Text+"','"+daten+"','"+textBox6.Text+"',"+grade+",'"+textBox8.Text+"','"+textBox9.Text+"','"
                    +etat+"','"+textBox11.Text+"','"+textBox12.Text+"',"+textBox13.Text+")", cnx);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employé Ajouté", "Ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cnx.Close();
                var myForm = new gestionEmploye();
                this.Hide();
                myForm.Show();
            }
        }

        private void label12_Click(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var myForm = new gestionEmploye();
            this.Hide();
            myForm.Show();
        }
    }
}
