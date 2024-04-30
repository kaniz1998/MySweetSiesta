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
    public partial class frmOrdersEntry : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-K1J0264; Initial Catalog=SalesDB; Integrated Security=True;");

        public frmOrdersEntry()
        {
            InitializeComponent();
        }

        private void frmOrdersEntry_Load(object sender, EventArgs e)
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

        private void btnUplod_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtPimage.Text = openFileDialog1.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Image
            Image img = Image.FromFile(txtPimage.Text);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            //
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Orders(OrderID,OrderDate,CustomerName,ProductID,Quantity,TotalPrice,ProductImage) VALUES(@i,@d,@c,@p,@q,@t,@pi)";
            cmd.Parameters.AddWithValue("@i", txtOrderID.Text);
            cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@c", txtCustomerName.Text);
            cmd.Parameters.AddWithValue("@p", cmbProduct.SelectedValue);
            cmd.Parameters.AddWithValue("@q", nmbrBox.Value);
            cmd.Parameters.AddWithValue("@t", txtTotalPrice.Text);
            cmd.Parameters.Add(new SqlParameter("@pi", SqlDbType.VarBinary) { Value = ms.ToArray() });
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Inserted Successfully!");
            con.Close();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtOrderID.Clear();
            dateTimePicker1.Text = "";
            txtCustomerName.Clear();
            cmbProduct.SelectedIndex = -1;
            nmbrBox.Value = 0;
            txtTotalPrice.Clear();
            txtPimage.Clear();
        }
    }
}
