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

namespace WrongCode_and_Answers
{
    public partial class frmLanguage : Form
    {
        SqlConnection sqlConn = new SqlConnection("Server=.; Database=renCODEesmee; Integrated security=true;");
        public frmLanguage()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveLng_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sqlComm = new SqlCommand("sp_insertLanguage", sqlConn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                sqlComm.Parameters.AddWithValue("@para", txtLng.Text);

                sqlConn.Open();
                sqlComm.ExecuteNonQuery();

            }
            catch (Exception)
            {

                Form frm = new ErrorMessage("btnSaveLng_Click have an error");
                frm.ShowDialog();

            }
            finally
            {
                sqlConn.Close();
                Application.Restart();
            }
        }
    }
}
