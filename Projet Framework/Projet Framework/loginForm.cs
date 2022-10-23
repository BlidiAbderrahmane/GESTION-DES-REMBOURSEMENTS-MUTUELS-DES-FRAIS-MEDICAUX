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
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }
        SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-0GDF4CR\\SQLEXPRESS;Initial Catalog=cimentsbizerte;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        
        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
                cnx.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public int trouver()
        {
            int x = 0,ad,ag,ae;
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select count(*) from administrateur where loginadmin= UPPER('"+textBox1.Text+"') and pwdadmin ='"+textBox2.Text+"'",cnx);
            ad = (int)cmd.ExecuteScalar();
            if (ad > 0)
            {
                x = 1;
            }
            else
            {
                cmd = new SqlCommand("select count(*) from agentsocial where loginagent= UPPER('" + textBox1.Text + "') and pwdagent ='" + textBox2.Text + "'", cnx);
                ag = (int)cmd.ExecuteScalar();
                if (ag > 0)
                    x = 2;
                else
                {
                    cmd = new SqlCommand("select count(*) from employe where matricule= UPPER('" + textBox1.Text + "') and cin ='" + textBox2.Text + "'", cnx);
                    ae = (int)cmd.ExecuteScalar();
                    if (ae > 0)
                        x = 3;
                }
            }
            cnx.Close();
            return x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
                MessageBox.Show("Le champ de Login est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (textBox2.Text=="")
                MessageBox.Show("Le champ de mot de passe est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (trouver() == 0)
                    MessageBox.Show("Login ou mot de passe est invalide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (trouver() == 1)
                    {
                        var myForm = new espaceAdmin();
                        this.Hide();
                        myForm.Show();
                    }
                    else if (trouver()==2)
                    {
                        var myForm = new espaceAgent();
                        this.Hide();
                        myForm.Show();
                    }
                    else
                    {
                        var myForm = new espaceEmploye();
                        this.Hide();
                        myForm.Show();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
                textBox2.PasswordChar = '\0';
            else
                textBox2.PasswordChar = '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
