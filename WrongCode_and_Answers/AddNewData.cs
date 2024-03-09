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
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace WrongCode_and_Answers
{
    public partial class AddNewData : Form
    {
        SqlConnection sqlConn = new SqlConnection("Server=DESKTOP-LVA4E65\\MSSQLSERVER01; Database=renCODEesmee; Integrated security=true;");
        private string id;

        string  cmbt;
        public AddNewData()
        {
            InitializeComponent();
        }
        public AddNewData(string id)
        {
            InitializeComponent();
            this.id = id;

            try
            {
                SqlCommand sqlComm = new SqlCommand("sp_selectId", sqlConn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                sqlConn.Open();
                sqlComm.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = sqlComm.ExecuteReader();
                while (dr.Read())
                {
                    txtID.Text = dr[0].ToString();
                    txtName.Text = dr[1].ToString();
                    cmbt= dr[2].ToString();
                    txtDescription.Text = dr[3].ToString();
                    txtProcess.Text = dr[4].ToString();
                    byte[] pic = (byte[])dr[5];
                    MemoryStream memory = new MemoryStream(pic);
                    picture1.Image = Image.FromStream(memory);
                }
            }
            catch (Exception ex)
            {
                Form error = new ErrorMessage($"The Error is: {ex}");
                error.ShowDialog();
                
            }
            finally
            {
                sqlConn.Close();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddNewData_Load(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("sp_selectProgrammingLanguage", sqlConn);
                da.Fill(dt);
                cmbLanguage.DataSource = dt;
                cmbLanguage.DisplayMember = "Name";
                cmbLanguage.SelectedIndex = 8;
            }
            catch (Exception)
            {
                Form error = new ErrorMessage("There is an error in combobox");
                error.ShowDialog();

            }
            if(!string.IsNullOrEmpty(cmbt))
            {
                cmbLanguage.Text = cmbt;
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

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(id))
            {
                try
                {
                    MemoryStream mem = new MemoryStream();
                    picture1.Image.Save(mem, ImageFormat.Png);
                    var pics = mem.ToArray();
                    SqlCommand sqlComm = new SqlCommand("sp_AddEntry", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@name", txtName.Text);
                    sqlComm.Parameters.AddWithValue("@programmingLanguage", cmbLanguage.Text);
                    sqlComm.Parameters.AddWithValue("@description", txtDescription.Text);
                    sqlComm.Parameters.AddWithValue("@process", txtProcess.Text);
                    sqlComm.Parameters.AddWithValue("@image", pics);

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    Form frm = new ErrorMessage("button1_Click have an error");
                    frm.ShowDialog();

                }
                finally
                {
                    sqlConn.Close();
                    Application.Restart();
                }
            }
            else
            {
                try
                {
                    MemoryStream memory = new MemoryStream();
                    picture1.Image.Save(memory, ImageFormat.Png);
                    var pic = memory.ToArray();

                    SqlCommand sqlComm = new SqlCommand("sp_editEntry", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@id", id);
                    sqlComm.Parameters.AddWithValue("@name", txtName.Text);
                    sqlComm.Parameters.AddWithValue("@programmingLanguage", cmbLanguage.Text);
                    sqlComm.Parameters.AddWithValue("@description", txtDescription.Text);
                    sqlComm.Parameters.AddWithValue("@process", txtProcess.Text);
                    sqlComm.Parameters.AddWithValue("@image", pic);

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Form error = new ErrorMessage($"The Error is: {ex}");
                    error.ShowDialog();

                }
                finally
                {
                    sqlConn.Close();
                    Application.Restart();
                }
            }
            }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OpenFileDialog pic = new OpenFileDialog();
            pic.Filter = "Image| *.jpg ; *.png";
            if(pic.ShowDialog() == DialogResult.OK)
            {
                picture1.Image = Image.FromFile(pic.FileName);
                picture1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            
        }
    }
}
