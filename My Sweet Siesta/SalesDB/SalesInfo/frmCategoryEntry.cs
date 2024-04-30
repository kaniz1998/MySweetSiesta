 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SalesInfo
{
    public partial class frmCategoryEntry : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-K1J0264; Initial Catalog=SalesDB; Integrated Security=True;");

        public frmCategoryEntry()
        {
            InitializeComponent();
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            con.Open();
            if (txtCategoryName.Text != "")
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Category VALUES('" + txtCategoryName.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Inserted Successfully!");
                LoadGrid();
                txtCategoryName.Text = "";
            }
            else
            {
                MessageBox.Show("Please Enter A CategoryName!");
            }          
            con.Close();
        }

        private void frmCategoryEntry_Load(object sender, EventArgs e)
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

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtCategoryName.Clear();
        }
    }
}
