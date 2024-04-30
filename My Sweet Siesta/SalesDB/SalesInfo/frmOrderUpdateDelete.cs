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
using System.Xml.Linq;

namespace SalesInfo
{
    public partial class frmOrderUpdateDelete : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-K1J0264; Initial Catalog=SalesDB; Integrated Security=True;");
        public frmOrderUpdateDelete()
        {
            InitializeComponent();
        }

        private void btnUplod_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtPimage.Text = openFileDialog1.FileName;
            }
        }

        private void frmOrderUpdateDelete_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }
        private void LoadCombo()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Products", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            cmbProduct.DataSource = ds.Tables[0];
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductID";
            con.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT OrderID,OrderDate,CustomerName,ProductID,Quantity,TotalPrice,ProductImage FROM Orders WHERE OrderID=" + txtOrderID.Text + "", con);
            DataTable dt =new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][1].ToString());
                txtCustomerName.Text = dt.Rows[0][2].ToString();
                cmbProduct.SelectedValue = dt.Rows[0][3].ToString();
                txtQuantity.Text = dt.Rows[0][4].ToString();
                txtTotalPrice.Text = dt.Rows[0][5].ToString();
                MemoryStream ms = new MemoryStream((byte[])dt.Rows[0][6]);
                Image img = Image.FromStream(ms);
                pictureBox1.Image = img;
            }
            else
            {
                lblmsg.Text = "No Data Found!";
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtPimage.Text != "")
            {
                //Image
                Image img = Image.FromFile(txtPimage.Text);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                //
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE Orders set OrderDate=@d,CustomerName=@c,ProductID=@p,Quantity=@q,TotalPrice=@t,ProductImage=@pi WHERE OrderID=@i";
                cmd.Parameters.AddWithValue("@i", txtOrderID.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@c", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@p", cmbProduct.SelectedValue);
                cmd.Parameters.AddWithValue("@q", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@t", txtTotalPrice.Text);
                cmd.Parameters.Add(new SqlParameter("@pi", SqlDbType.VarBinary) { Value = ms.ToArray() });
                cmd.ExecuteNonQuery();
                lblmsg.Text = "Data Updated Successfully!";
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE Orders set OrderDate=@d,CustomerName=@c,ProductID=@p,Quantity=@q,TotalPrice=@t WHERE OrderID=@i";
                cmd.Parameters.AddWithValue("@i", txtOrderID.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@c", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@p", cmbProduct.SelectedValue);
                cmd.Parameters.AddWithValue("@q", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@t", txtTotalPrice.Text);
                cmd.ExecuteNonQuery();
                lblmsg.Text = "Data Updated Successfully!";
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd= new SqlCommand("DELETE FROM Orders WHERE OrderID=@i",con);
            cmd.Parameters.AddWithValue("@i", txtOrderID.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Data Deleted Successfully!";
            con.Close();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtOrderID.Clear();
            dateTimePicker1.Text = "";
            txtCustomerName.Clear();
            cmbProduct.SelectedIndex = -1;
            txtQuantity.Clear();
            txtTotalPrice.Clear();
            txtPimage.Clear();
        }
    }
}
