using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using System.Drawing.Drawing2D;

namespace WrongCode_and_Answers
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConn = new SqlConnection("Server=DESKTOP-LVA4E65\\MSSQLSERVER01; Database=renCODEesmee; Integrated security=true;");
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Form add = new AddNewData();
            add.ShowDialog();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Form del = new FrmDelMessage(id);
            del.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Form edit = new AddNewData(id);
            edit.ShowDialog();
        }

        private void pctExit_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_selectAll", sqlConn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Width = ((5 * dataGridView1.Width) / 100);
                dataGridView1.Columns[1].Width = ((20 * dataGridView1.Width) / 100);
                dataGridView1.Columns[2].Width = ((15 * dataGridView1.Width) / 100);
                dataGridView1.Columns[3].Width = ((20 * dataGridView1.Width) / 100);
                dataGridView1.Columns[4].Width = ((40 * dataGridView1.Width) / 100);
                dataGridView1.Columns[5].Visible = false;
                

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                Form frm = new ErrorMessage("DataGridView have an error");

                frm.ShowDialog();
            }

            // Create a GraphicsPath object to define the shape of the form
            GraphicsPath path = new GraphicsPath();

            // Define the rounded rectangle shape with desired corner radius and dimensions
            int cornerRadius = 20;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            path.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseAllFigures();

            // Set the form's region to the defined path
            Region = new Region(path);


        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            byte[] picByte = (byte[])dataGridView1.CurrentRow.Cells[5].Value;
            MemoryStream memory = new MemoryStream(picByte);
            Image pic = Image.FromStream(memory);

            Form frmImage = new FrmImage(pic);
            frmImage.ShowDialog();

        }

        private void txtSearch_MouseDown(object sender, MouseEventArgs e)
        {
            txtSearch.SelectAll();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("sp_search", sqlConn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.SelectCommand.Parameters.AddWithValue("@para", "%" + txtSearch.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Width = ((5 * dataGridView1.Width) / 100);
                    dataGridView1.Columns[1].Width = ((20 * dataGridView1.Width) / 100);
                    dataGridView1.Columns[2].Width = ((15 * dataGridView1.Width) / 100);
                    dataGridView1.Columns[3].Width = ((20 * dataGridView1.Width) / 100);
                    dataGridView1.Columns[4].Width = ((40 * dataGridView1.Width) / 100);
                    dataGridView1.Columns[5].Visible = false;

                }
                catch (Exception ex)
                {
                    Form error = new ErrorMessage($"The Error is: {ex}");
                    error.ShowDialog();

                }
            }
            else
                Application.Restart();

        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath buttonPath = new GraphicsPath();
            buttonPath.AddEllipse(0,0, button1.Width, button1.Height);
            button1.Region = new Region(buttonPath);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form insertLng = new frmLanguage();
            insertLng.ShowDialog();
        }
    }
}