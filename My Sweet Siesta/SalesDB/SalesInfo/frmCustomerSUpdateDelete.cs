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
    public partial class frmCustomerSUpdateDelete : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-K1J0264; Initial Catalog=SalesDB; Integrated Security=True;");
        public frmCustomerSUpdateDelete()
        {
            InitializeComponent();
        }

        private void frmCustomerSUpdateDelete_Load(object sender, EventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT CustomerName,CustomerAddress,CustomerPhone FROM Customers WHERE CustomerID=" + txtCustomerID.Text + "", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtCustomerName.Text = dt.Rows[0][0].ToString();
                txtCustomerAddress.Text = dt.Rows[0][1].ToString();
                txtCustomerPhone.Text = dt.Rows[0][2].ToString();
            }
            else
            {
                lblmsg.Text = "No Data Found!";
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Customers SET CustomerName=@n,CustomerAddress=@a,CustomerPhone=@p WHERE CustomerID=@i";
            cmd.Parameters.AddWithValue("@i", txtCustomerID.Text);
            cmd.Parameters.AddWithValue("@n", txtCustomerName.Text);
            cmd.Parameters.AddWithValue("@a", txtCustomerAddress.Text);
            cmd.Parameters.AddWithValue("@p", txtCustomerPhone.Text);
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Data Updated Successfully!";
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerID=@i", con);
            cmd.Parameters.AddWithValue("@i", txtCustomerID.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Data Deleted Successfully!";
            con.Close();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtCustomerID.Clear();
            txtCustomerName.Clear();
            txtCustomerAddress.Clear();
            txtCustomerPhone.Clear();
        }
    }
}
