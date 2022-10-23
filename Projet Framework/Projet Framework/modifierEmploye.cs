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
    public partial class modifierEmploye : Form
    {
        public modifierEmploye(String mat)
        {
            InitializeComponent();
            remplir(mat);
            
        }
        SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-0GDF4CR\\SQLEXPRESS;Initial Catalog=cimentsbizerte;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
        }

        public void remplir(String mat)
        {
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select * from employe where matricule = Upper('" + mat + "')", cnx);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                textBox1.Text = reader["matricule"].ToString();
                textBox2.Text = reader["nom"].ToString();
                textBox3.Text = reader["prenom"].ToString();
                textBox4.Text = reader["cin"].ToString();
                dateTimePicker1.Value = (DateTime)reader["dateNaissance"];
                textBox6.Text = reader["adresse"].ToString();
                comboBox1.SelectedIndex = Int32.Parse(reader["grade"].ToString()) - 1;
                textBox8.Text = reader["tel"].ToString();
                textBox9.Text = reader["cnam"].ToString();
                textBox11.Text = reader["nomConjoint"].ToString();
                textBox12.Text = reader["prenomConjoint"].ToString();
                textBox13.Text = reader["nbrEnfants"].ToString();
                switch (reader["etatCivil"].ToString())
                {
                    case "Marié(e)":
                        comboBox2.SelectedIndex = 2;
                        break;
                    case "Divorcé(e)":
                        comboBox2.SelectedIndex = 1;
                        break;
                    case "Célibataire":
                        comboBox2.SelectedIndex = 0;
                        break;
                    case "Veuf/Veuve":
                        comboBox2.SelectedIndex = 3;
                        break;
                }
            }
            cnx.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
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
            else if ((textBox4.Text == "") || (!IsDigitsOnly(textBox4.Text)) || (textBox4.Text.Length != 8) || ((textBox4.Text[0] != '0') && (textBox4.Text[0] != '1')))
                MessageBox.Show("Vérifier le champ du CIN", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (textBox6.Text == "")
                MessageBox.Show("Le champ d'adresse est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (comboBox1.SelectedIndex < 0)
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
                String etat=comboBox2.SelectedItem.ToString();
                cmd = new SqlCommand("update employe set nom = '"+textBox2.Text+ "', prenom = '" + textBox3.Text + "', cin = '" 
                 + textBox4.Text + "', datenaissance = '" + daten + "', adresse = '" + textBox6.Text + "', grade = " 
                 + grade + ", tel='"+textBox8.Text+"', cnam='"+textBox9.Text+"', etatcivil='"+etat+"', nomconjoint='"
                 +textBox11.Text+"', prenomconjoint='"+textBox12.Text+"', nbrenfants="+textBox13.Text+" where matricule='"
                 +textBox1.Text+"'",cnx);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employé Modifié", "Mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cnx.Close();
                var myForm = new gestionEmploye();
                this.Hide();
                myForm.Show();
            }
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            var myForm = new gestionEmploye();
            this.Hide();
            myForm.Show();
        }
    }
}
