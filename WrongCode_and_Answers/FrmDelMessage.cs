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
using System.Drawing.Drawing2D;

namespace WrongCode_and_Answers
{

    public partial class FrmDelMessage : Form
    {
        SqlConnection sqlConn = new SqlConnection("Server=DESKTOP-LVA4E65\\MSSQLSERVER01; Database=renCODEesmee; Integrated security=true;");

        string id;
        public FrmDelMessage(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sqlComm = new SqlCommand("sp_delEntry",sqlConn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                sqlComm.Parameters.AddWithValue("@id", id);
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

        private void FrmDelMessage_Load(object sender, EventArgs e)
        {
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
    }
}
