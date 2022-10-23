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
    public partial class calculerSR : Form
    {
        public calculerSR()
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var myForm = new espaceAdmin();
            this.Hide();
            myForm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedIndex < 0) || (comboBox2.SelectedIndex < 0))
                MessageBox.Show("Vérifier les champs du mois et année","Attention",MessageBoxButtons.OK,MessageBoxIcon.Error);
            else
            {
                deconnecter();
                cnx.Open();
                int annee = comboBox2.SelectedIndex + 2000;
                double tot = 0;
                if (comboBox1.SelectedIndex==0)
                {
                    cmd = new SqlCommand("select fraisacte, B.matricule, acceptation from bulletinsoin B, employe E where B.matricule=E.matricule and datedepot>='"+annee+"-01-01' and datedepot<='"+annee+"-12-31'", cnx);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["acceptation"].ToString()=="True")
                            tot += (double.Parse(reader["fraisacte"].ToString()))*0.3;
                    }
                    MessageBox.Show("La somme des remboursements est " + tot + " DT", "Somme des remboursements", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string mois=comboBox1.SelectedIndex.ToString();
                    switch (mois)
                    {
                        case "1":
                        case "3":
                        case "5":
                        case "7":
                        case "8":
                        case "10":
                        case "12":
                            cmd = new SqlCommand("select fraisacte, B.matricule,acceptation from bulletinsoin B, employe E where B.matricule=E.matricule and datedepot>='" + annee + "-" + mois + "-01' and datedepot<='" + annee + "-" + mois + "-31'", cnx);
                            break;
                        case "4":
                        case "6":
                        case "9":
                        case "11":
                            cmd = new SqlCommand("select fraisacte, B.matricule,acceptation from bulletinsoin B, employe E where B.matricule=E.matricule and datedepot>='" + annee + "-" + mois + "-01' and datedepot<='" + annee + "-" + mois + "-30'", cnx);
                            break;
                        case "2":
                            cmd = new SqlCommand("select fraisacte, B.matricule,acceptation from bulletinsoin B, employe E where B.matricule=E.matricule and datedepot>='" + annee + "-" + mois + "-01' and datedepot<='" + annee + "-" + mois + "-28'", cnx);
                            break;
                    }
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["acceptation"].ToString() == "True")
                            tot += (double.Parse(reader["fraisacte"].ToString())) * 0.3;
                    }
                    MessageBox.Show("La somme des remboursements est " + tot + " DT", "Somme des remboursements", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                cnx.Close();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
            }
        }
    }
}