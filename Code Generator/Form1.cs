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
        private const string admin = "admin";
        private const string password = "admin123";

        //private Form4 form4;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        //If (username and password) then goto admin panel else show error message
        private void buttonLogin_Click(object sender, EventArgs e)
        {

            if (admin.Equals(textBoxName.Text) && password.Equals(textBoxPass.Text))
            {
                //form4 = new Form4();
                //form4.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalied User Name or Password ..!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        //Go back to home panel
        private void buttonHome_Click(object sender, EventArgs e)
        {
            //Form5 form5 = new Form5();
            //form5.Show();
            this.Close();
        }
    }
}
