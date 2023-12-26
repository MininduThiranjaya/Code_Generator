using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code_Generator
{
    public partial class Form1 : Form
    {
        //private Form1 form1 = new Form1();
        //private Form3 form3 = new Form3();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        //Goto Generate panel
        private void buttonCodeGenerator_Click(object sender, EventArgs e)
        {
            //form1.Show();
            this.Hide();
        }

        //Goto admin panel
        private void buttonAdmin_Click_1(object sender, EventArgs e)
        {           
           // form3.Show();
            this.Hide();
        }
    }
}
