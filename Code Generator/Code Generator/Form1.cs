using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Code_Generator
{
    public partial class Form1 : Form
    {
        //all regex pattern
        public string namePattern = @"^[a-zA-Z0-9 ]+$";
        public string websitePattern = @"^www\.([a-zA-Z]+)\.com$";
        private string numberPattern = @"^[0-9]+$";
        private string weigthPattern = @"^([0-9]+)(kg|g)$";
        private string descriptionPattern = @"^([a-zA-Z0-9 ]+)(\S|\W)+$";

        private Regex regex;

        private string codeType = null;

        private string name = "null";
        private string price = "null";
        private string weigth = "null";
        private string manufacture = "null";
        private string expaire = "null";
        private string comName = "null";

        private string webSite = "null";
        private string phoneNumber = "null";
        private string description = "null";

        private Form1 detailsBack;

        private Form2 form2;
        private Form3 form3;
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(Form1 detailsBack)
        {
            InitializeComponent();
            this.detailsBack = detailsBack;
        }

        //groupBoxDetails will be  hidden when luanching
        private void Form1_Load(object sender, EventArgs e)
        {
            groupBoxDetails.Hide();
        }

        //when select one type the groupBoxDetails will be appiered
        public void enterDetails()
        {
            if (!codeType.Equals(""))
            {
                groupBoxDetails.Show();
            }
        }

        //Adding details to create the code and go to form2
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            addValuesToVariable();
           if (form2 == null || form2.IsDisposed)
           {
               MessageBox.Show("Generated Successfully..!");
               form2 = new Form2(this);
               form2.Show();
           }
           
        }

        //add times to variables(using dateTimePicker)
        public void addValuesToVariable()
        {
            manufacture = Convert.ToString(this.dateTimePickerManufacture.Text);
            expaire = Convert.ToString(this.dateTimePickerExpaire.Text);
        }

        //to check wether its a QRcode
        private void radioButtonQR_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButtonQR.Checked)
            {
                codeType = "QR";
                enterDetails();
            }
        }

        //to check wether its a Barcode
        private void radioButtonBarcode_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButtonBarcode.Checked)
            {
                codeType = "Barcode";
                enterDetails();
            }
        }

        //Index get method to get the variables value
        public string this[string index]
        {
            get
            {
                switch (index)
                {
                    case "codeType":
                        return codeType;
                    case "name":
                        return name;
                    case "price":
                        return price;
                    case "weigth":
                        return weigth;
                    case "manufacture":
                        return manufacture;
                    case "expaire":
                        return expaire;
                    case "comName":
                        return comName;
                    case "website":
                        return webSite;
                    case "phoneNumber":
                        return phoneNumber;
                    case "description":
                        return description;
                    default:
                        return null;
                }
            }
        }

        //validating all text box inputs using regex and assign the values into variables
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            name = validateInput(textBoxName, namePattern);
        }

        private void textBoxPrice_TextChanged(object sender, EventArgs e)
        {
            price = validateInput(textBoxPrice, numberPattern);
        }

        private void textBoxWeight_TextChanged(object sender, EventArgs e)
        {
            weigth = validateInput(textBoxWeight, weigthPattern);
        }

        private void textBoxComName_TextChanged(object sender, EventArgs e)
        {
            comName = validateInput(textBoxComName, namePattern);
        }

        private void textBoxConNumber_TextChanged(object sender, EventArgs e)
        {
            phoneNumber = validateInput(textBoxConNumber, numberPattern);
        }

        private void textBoxWebsite_TextChanged(object sender, EventArgs e)
        {
            webSite = validateInput(textBoxWebsite, websitePattern);
        }

        private void textBoxDes_TextChanged(object sender, EventArgs e)
        {
            description = validateInput(textBoxDes, descriptionPattern);
        }

        //validate method for text box inputs
        public string validateInput(TextBox textBox, string patternType)
        {
            regex = new Regex(patternType);
            if (regex.IsMatch(textBox.Text))
            {
                errorProvider1.Clear();
                return textBox.Text;
            }
            else
            {
                switch (textBox.Name)
                {

                    case "textBoxName": case "textBoxComName":
                        errorProvider1.SetError(textBox, "Numbers and Alphebatics only can be applied..!");
                        return null;
                    case "textBoxPrice":
                        errorProvider1.SetError(textBox, "Numbers only can be applied..!");
                        return null;
                    case "textBoxConNumber":
                        if(!(textBoxConNumber.Text.Length <= 10))
                        {
                            errorProvider1.SetError(textBox, "Should have 10 numbers..!");
                        }
                        return null;
                    case "textBoxWeight":
                        errorProvider1.SetError(textBox, "(Ex - 10g/10kg)..!");
                        return null;
                    case "textBoxWebsite":
                        errorProvider1.SetError(textBox, "(Ex - www.example.com)..!");
                        return null;
                    case "textBoxDes":
                        errorProvider1.SetError(textBox, "Symbols can be applied..!");
                        return null;
                    default:
                        return null;
                }
            }

        }

        //Go back to admin login panel
        private void button1_Click(object sender, EventArgs e)
        {
            
            form3 = new Form3();
            form3.Show();
            this.Close();
        }

        //Go back to home panel
        private void buttonHome_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Close();
        }
    }
}
