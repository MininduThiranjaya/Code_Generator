using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Code_Generator
{
    public partial class Form2 : Form
    {

        SqlConnection connection = new SqlConnection("Data Source=thiranjaya\\sqlexpress;Initial Catalog=codeDatabase;Integrated Security=True");

        private Form1 form1;
        private string allDetails = "";

        private string folderLocation;
        private string saveLocation;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 details)
        {
            form1 = details;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            concatDetails();
            CreateCode();
        }

        //Generate the code according to the values that we are entered(Barcode or QRcode)
        public void CreateCode()
        {
            concatDetails();
            if (form1["codeType"].Equals("QR"))
            {
                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                pictureBox1.Image = qrcode.Draw(allDetails, 200);
            }
            else if (form1["codeType"].Equals("Barcode"))
            {
                Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                pictureBox1.Image = barcode.Draw(allDetails, 75);
            }
        }

        //Get all details asign into a variabe (allDetails)
        public void concatDetails()
        {
            allDetails = form1["name"] + "\n" + form1["price"] + "\n" + form1["weigth"] + "\n" + form1["manufacture"] + "\n" + form1["expaire"] + "\n" + form1["comName"] + "\n" + form1["website"] + "\n" + form1["phoneNumber"] + "\n" + form1["description"];
        }

        //Save button - all details save into database and the code image will save in a local file
        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            saveImageFile();
            saveIntoDatabase();

            this.Hide();
            Form1 form1Back = new Form1(form1);
            form1Back.Show();
        }

        //Save the image in a local file
        public void saveImageFile()
        {
            createFolder();
            saveLocation = (folderLocation + "\\" + form1["name"] + ".jpg");
            pictureBox1.Image.Save(saveLocation, ImageFormat.Jpeg);
            
        }

        //If there is no derectory then create a new local directory called GenerateCodes in starting path
        public void createFolder()
        {
            folderLocation = (Application.StartupPath + "\\GeneratedCodes");
            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }
        }

        //Insert all details into database
        public void saveIntoDatabase()
        {
            SqlCommand command = new SqlCommand("insert into codeDetails values(@codeType,  " +
                                                                                "@name, " +
                                                                                "@price, " +
                                                                                "@weight, " +
                                                                                "@comName," +
                                                                                "@conNumber," +
                                                                                "@website," +
                                                                                "@manufac," +
                                                                                "@expaire," +
                                                                                "@des," +
                                                                                "@imageURL)", connection);
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@codeType", form1["codeType"]);
            command.Parameters.AddWithValue("@name", form1["name"]);
            command.Parameters.AddWithValue("@price", form1["price"]);
            command.Parameters.AddWithValue("@weight", form1["weigth"]);
            command.Parameters.AddWithValue("@comName", form1["comName"]);
            command.Parameters.AddWithValue("@conNumber", form1["phoneNumber"]);
            command.Parameters.AddWithValue("@website", form1["website"]);
            command.Parameters.AddWithValue("@manufac", form1["manufacture"]);
            command.Parameters.AddWithValue("@expaire", form1["expaire"]);
            command.Parameters.AddWithValue("@des", form1["description"]);
            command.Parameters.AddWithValue("@imageURL", saveLocation);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
