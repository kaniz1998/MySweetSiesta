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

namespace SalesInfo
{
    public partial class frmCategoryUpdateDelete : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-K1J0264; Initial Catalog=SalesDB; Integrated Security=True;");
        public frmCategoryUpdateDelete()
        {
            InitializeComponent();
        }

        private void frmCategoryUpdateDelete_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Category", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT CategoryName FROM Category WHERE CategoryID=" + txtID.Text + "", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtCategoryName.Text = dt.Rows[0][0].ToString();           
            }
            else
            {
                lblMsg.Text = "No Data Found!";
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Category set CategoryName WHERE CategoryID="+txtID.Text+"", con);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Updated Successfully!";
            LoadGrid();
            txtCategoryName.Text = "";
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Category WHERE CategoryID=@i", con);
            cmd.Parameters.AddWithValue("@i", txtID.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Deleted Successfully!";
            con.Close();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtCategoryName.Clear();
        }
    }
}
