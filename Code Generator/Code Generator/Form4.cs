using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Code_Generator
{
    public partial class Form4 : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=thiranjaya\\sqlexpress;Initial Catalog=codeDatabase;Integrated Security=True");

        private string searchCodeType;
        private string searchProductName;
        private string searchComName;

        private List<string> listItems = new List<string>();
        private int id = 0;

        private string name;
        private string price;
        private string weigth;
        private string manufacture;
        private string expaire;
        private string comName;

        private string webSite;
        private string phoneNumber;
        private string description;
        private string imageUrl;

        private string allDetails;

        public Form4()
        {
            InitializeComponent();
        }

        //set the gridView text and background color when initializing 
        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'codeDatabaseDataSet.codeDetails' table. You can move, or remove it, as needed.
            this.codeDetailsTableAdapter.Fill(this.codeDatabaseDataSet.codeDetails);
            this.dataGridView1.DefaultCellStyle.BackColor = Color.Black;
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.LimeGreen;
        }

        //Select the type using comboBox (QRcode or Barcode)
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedItem.ToString() == "QR")
            {
                searchCodeType = "QR";
            }
            else
            {
                searchCodeType = "Barcode";
            }
        }

        //Search button
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            searchDetails();
        }

        //Search function - retrive details form data base and show in the gridView
        public void searchDetails()
        {
            if(string.IsNullOrEmpty(searchCodeType))
            {
                errorProvider1.SetError(comboBoxType, "Fill this fieald");
            }
            else
            {
                if(string.IsNullOrEmpty(searchProductName) && string.IsNullOrEmpty(searchComName))
                {
                    string quary = "select * from codeDetails where codeType = '" + searchCodeType + "'";
                    showAllDetails(quary);
                }
                else
                {
                    string quary = "select * from codeDetails where codeType = '" + searchCodeType + "' and (name = '" + searchProductName + "' or comName = '" + searchComName + "')";
                    showAllDetails(quary);
                }
                
            }
        }

        //Show all retrive details in data gridView
        public void showAllDetails(string quary)
        {
            SqlCommand command = new SqlCommand(quary, connection);
            DataTable dt = new DataTable();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            dt.Load(reader);
            connection.Close();
            dataGridView1.DataSource = dt;
        }

        //Show button - all details in the data base
        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            string quary = "select * from codeDetails";
            showAllDetails(quary);//show all details on the dataGridView
        }

        //Show the selected fields in a particular item
        private void buttonShow_Click(object sender, EventArgs e)
        {
            listItems.Clear();
            allDetails = null;
            selectedItems();
            setValues();
            labelDetails.Text = allDetails;
            getImage("select imageURL from codeDetails where id = " + id, imageUrl);
            settingUpImage();

        }

        //Store the selected fields in a list called listItems
        public void selectedItems()
        {
            foreach(var value in checkedListBoxFields.CheckedItems)
            {
                listItems.Add(value.ToString());
            }
        }

        //Retrive values from the data base for fields that we are selected
        public void setValues()
        {
            for (int i = 0; i < listItems.Count; i++)
            {
                string value = listItems[i].ToString();

                switch(value) {
                    case "Product Name":
                        gettingValues("select name from codeDetails where id = " + id, name);
                        break;
                    case "Product Price":
                        gettingValues("select price from codeDetails where id = " + id , price);
                        break;
                    case "Product Weight":
                        gettingValues("select weight from codeDetails where id = " + id , weigth);
                        break;
                    case "Manufacture Date":
                        gettingValues("select manufac from codeDetails where id = " + id , manufacture);
                        break;
                    case "Expaire Date":
                        gettingValues("select expaire from codeDetails where id = " + id , expaire);
                        break;
                    case "Product Company":
                        gettingValues("select comName from codeDetails where id = " + id , comName);
                        break;
                    case "Telephone":
                        gettingValues("select conNumber from codeDetails where id = " + id , phoneNumber);
                        break;
                    case "Website":
                        gettingValues("select website from codeDetails where id = " + id , webSite);
                        break;
                    case "Comment":
                        gettingValues("select des from codeDetails where id = " + id , description);
                        break;

                }
            }
        }

        //Retrive function - add all selected fields details to a single variable called allDetails 
        public void gettingValues(string quary, string variable)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(quary, connection);
            variable = command.ExecuteScalar().ToString();
            retrieveAllDetails(variable);
            connection.Close();
        }

        //Show all details without null values
        public void retrieveAllDetails(string variable)
        {
            if(!(variable.Equals("null")))
            {
                allDetails = allDetails + "\n" + variable;
            }
        }

        //Retrive image url from the data base - separately
        public void getImage(string quary, string variable)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(quary, connection);
            imageUrl = command.ExecuteScalar().ToString();
            connection.Close();
        }

        //gridView Click event - select a particular item
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                id = Convert.ToInt16(selectedRow.Cells[0].Value.ToString());
            }
        }

        //Re-load the image of a selected item 
        public void settingUpImage()
        {
            if(!(imageUrl.Equals(null)))
            {
                pictureBox1.Load(imageUrl);
            }
            else
            {
                pictureBox1.Load("Error, There is no any image..!");
            }
            
        }

        //Goto admin login panel
        private void button2_Click(object sender, EventArgs e)
        {
            
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }

        //Goto Generate panel
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();  
            form1.Show();
        }

        //Text box productName changes
        private void textBoxSearchName_TextChanged(object sender, EventArgs e)
        {
            searchProductName = this.textBoxSearchName.Text;
        }

        //text box companyName changes
        private void textBoxSearchCompany_TextChanged(object sender, EventArgs e)
        {
            searchComName = this.textBoxSearchCompany.Text;
        }
    }
}
