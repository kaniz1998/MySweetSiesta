using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInfo
{
    public partial class frmProductUpdateDelete : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-K1J0264; Initial Catalog=SalesDB; Integrated Security=True;");

        public frmProductUpdateDelete()
        {
            InitializeComponent();
        }

        private void frmProductUpdateDelete_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }
        private void LoadCombo()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Category", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            cmbCategory.DataSource = ds.Tables[0];
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            con.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ProductName,ProductImage,CategoryID,UnitPrice FROM Products WHERE ProductID = " + txtProID.Text + "", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtProductName.Text = dt.Rows[0][0].ToString();
                MemoryStream ms = new MemoryStream((byte[])dt.Rows[0][1]);
                Image img = Image.FromStream(ms);
                pictureBox1.Image = img;
                cmbCategory.SelectedValue = dt.Rows[0][2].ToString();
                txtUnitPrice.Text = dt.Rows[0][3].ToString();
            }
            else
            {
                lblmsg.Text = "No Data Found!";
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Image
            Image img = Image.FromFile(txtImage.Text);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            //
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Products SET ProductName=@n,ProductImage=@i,CategoryID=@c,UnitPrice=@p WHERE ProductID=@i";
            cmd.Parameters.AddWithValue("@n", txtProductName.Text);
            cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.VarBinary) { Value = ms.ToArray() });
            cmd.Parameters.AddWithValue("@c", cmbCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@p", txtUnitPrice.Text);
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Data Saved Successfully!";
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ProductID=@i", con);
            cmd.Parameters.AddWithValue("@i", txtProductID.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Data Deleted Successfully!";
            con.Close();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtProID.Clear();
            txtProductName.Clear();
            cmbCategory.SelectedIndex = -1;
            txtUnitPrice.Clear();
            txtImage.Clear();
        }
    }
}
