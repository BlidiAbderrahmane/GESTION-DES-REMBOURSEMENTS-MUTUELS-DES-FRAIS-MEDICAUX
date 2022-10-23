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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Projet_Framework
{
    public partial class consulterBulletin : Form
    {
        public consulterBulletin()
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
            cmd = new SqlCommand("select * from bulletinsoin", cnx);
            reader = cmd.ExecuteReader();
            table.Clear();
            table.Load(reader);
            dataGridView1.DataSource = table;
            cnx.Close();
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

        public void exportpdf()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Bulletins Employé "+textBox1.Text+".pdf";
                bool ErrorMessage = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Impossible d'écrire des données sur le disque" + ex.Message);
                        }
                    }
                    if (!ErrorMessage)
                    {
                        try
                        {
                            PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow viewRow in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell dcell in viewRow.Cells)
                                {
                                    pTable.AddCell(dcell.Value.ToString());
                                }
                            }
                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Fichier PDF exporté", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erreur lors de l'exportation des données" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun Enregistrement Trouvé", "Info");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Le champ du matricule est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (trouver() == 0)
                    MessageBox.Show("Il n'y a pas un employé avec cette matricule", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (nbrBulletins() == 0)
                    MessageBox.Show("Cet employé ne dispose d'aucun bulletin de soin", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    deconnecter();
                    cnx.Open();
                    cmd = new SqlCommand("select * from bulletinsoin where matricule=upper('" + textBox1.Text + "')", cnx);
                    reader = cmd.ExecuteReader();
                    table.Clear();
                    table.Load(reader);
                    dataGridView1.DataSource = table;
                    exportpdf();
                    textBox1.Text = "";
                    cnx.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var myForm = new espaceAdmin();
            this.Hide();
            myForm.Show();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        public int nbrBulletins()
        {
            int x = 0;
            deconnecter();
            cnx.Open();
            cmd = new SqlCommand("select count(*) from bulletinsoin where matricule= Upper('" + textBox1.Text + "')", cnx);
            x = (int)cmd.ExecuteScalar();
            cnx.Close();
            return x;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
                MessageBox.Show("Le champ du matricule est vide", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (trouver() == 0)
                    MessageBox.Show("Il n'y a pas un employé avec cette matricule", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (nbrBulletins()==0)
                    MessageBox.Show("Cet employé ne dispose d'aucun bulletin de soin", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    deconnecter();
                    cnx.Open();
                    cmd = new SqlCommand("select * from bulletinsoin where matricule=upper('"+textBox1.Text+"')", cnx);
                    reader = cmd.ExecuteReader();
                    table.Clear();
                    table.Load(reader);
                    dataGridView1.DataSource = table;
                    textBox1.Text = "";
                    cnx.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            remplirdgv();
        }
    }
}
