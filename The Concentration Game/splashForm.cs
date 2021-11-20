using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Concentration_Game
{
    public partial class splashForm : Form
    {
        public splashForm()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splashForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
