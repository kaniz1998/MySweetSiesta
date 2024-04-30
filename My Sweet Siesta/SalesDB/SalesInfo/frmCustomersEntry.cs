using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInfo
{
    public partial class frmCustomersEntry : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-K1J0264; Initial Catalog=SalesDB; Integrated Security=True;");
        public frmCustomersEntry()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            if (txtCustomerName.Text != "")
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Customers(CustomerName,CustomerAddress,CustomerPhone) VALUES('" + txtCustomerName.Text + "','" + txtCustomerAddress.Text + "','" + txtCustomerPhone.Text + "')", con);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Inserted Successfully!");
                LoadGrid();
                txtCustomerName.Text = "";
                txtCustomerAddress.Text = "";
                txtCustomerPhone.Text = "";
            }
            else
            {
                MessageBox.Show("Please Enter A CustomerName!");
            }
            con.Close();
        }

        private void frmCustomersEntry_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Customers", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtCustomerName.Clear();
            txtCustomerAddress.Clear();
            txtCustomerPhone.Clear();
        }
    }
}
