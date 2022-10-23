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
    public partial class rapportBulletin : Form
    {
        public rapportBulletin(String num)
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
            cmd = new SqlCommand("select * from bulletinsoin where numbulletin='" + num + "'", cnx);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                labelNum.Text = reader["numBulletin"].ToString();
                labelFrais.Text = reader["fraisActe"].ToString();
                labelDate.Text = ((DateTime)reader["dateDepot"]).ToString("dd - MMMM - yyyy");
                labelMatricule.Text = reader["matricule"].ToString();
                labelActe.Text = reader["acte"].ToString();
            }
            cnx.Close();
            double rem = Double.Parse(labelFrais.Text)*0.3;
            int annee = int.Parse(labelDate.Text.Substring(12));
            cnx.Open();
            cmd = new SqlCommand("select ISNULL(sum(fraisacte)*0.3,0) as 'somme' from bulletinsoin where matricule='"
                +labelMatricule.Text+"' and datedepot>='"+annee+"0101' and datedepot<='"+annee+"1231' and acceptation='True'",cnx);
            float somme=(float)((double)cmd.ExecuteScalar());
            cnx.Close();
            cnx.Open();
            cmd = new SqlCommand("select grade from employe where matricule ='"+labelMatricule.Text+"'", cnx);
            int grade = (int)cmd.ExecuteScalar();
            int max;
            switch (grade)
            {
                case 1:
                    max = 1800;
                    break;
                case 2:
                    max = 1400;
                    break;
                case 3:
                    max = 1000;
                    break;
                default:
                    max = 600;
                    break;
            }
            if (rem + somme > max)
            {
                deconnecter();
                cnx.Open();
                cmd = new SqlCommand("update bulletinsoin set acceptation = 'False' where numbulletin='"
                    +labelNum.Text+"'",cnx);
                cmd.ExecuteNonQuery();
                labelDecision.Text = "Demande Réfusée";
                cnx.Close();
            }
            else
            {
                deconnecter();
                cnx.Open();
                cmd = new SqlCommand("update bulletinsoin set acceptation = 'True' where numbulletin='"
                    + labelNum.Text + "'", cnx);
                cmd.ExecuteNonQuery();
                labelDecision.Text = "Demande Acceptée";
                labelPlafond.Text = "Votre plafond resté est "+String.Format("{0:0.00}",max -(rem+somme));
                cnx.Close();
;           }
            cnx.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var myForm = new espaceAgent();
            this.Hide();
            myForm.Show();
        }

        private void rapportBulletin_Load(object sender, EventArgs e)
        {

        }
    }
}
