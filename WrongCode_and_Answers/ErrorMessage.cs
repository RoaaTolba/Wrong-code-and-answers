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
    public partial class ErrorMessage : Form
    {
        public ErrorMessage(string msg)
        {
            InitializeComponent();
            lblMsg.Text = msg;
            lblMsg.Location = new System.Drawing.Point((this.Width - lblMsg.Width) / 2 ,90);
        }

        private void TimeClose_Tick(object sender, EventArgs e)
        {
            this.Close();

        }

        private void ErrorMessage_Load(object sender, EventArgs e)
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
