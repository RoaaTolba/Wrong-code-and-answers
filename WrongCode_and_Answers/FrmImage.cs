using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WrongCode_and_Answers
{
    public partial class FrmImage : Form
    {
        public FrmImage(Image image)
        {
            InitializeComponent();
            pictureBox1.Image = image;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pctExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
