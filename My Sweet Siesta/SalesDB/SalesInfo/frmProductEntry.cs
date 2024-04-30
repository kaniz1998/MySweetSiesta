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
    public partial class frmProductEntry : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-K1J0264; Initial Catalog=SalesDB; Integrated Security=True;");

        public frmProductEntry()
        {
            InitializeComponent();
        }
        private void frmProductEntry_Load(object sender, EventArgs e)
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
            cmbCategory.DisplayMember= "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            con.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtImage.Text = openFileDialog1.FileName;
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Image
            Image img = Image.FromFile(txtImage.Text);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            //
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Products(ProductName,ProductImage,CategoryID,UnitPrice) VALUES(@n,@i,@c,@p)";
            cmd.Parameters.AddWithValue("@n", txtProductName.Text);
            cmd.Parameters.Add(new SqlParameter("@i", SqlDbType.VarBinary) { Value = ms.ToArray() });
            cmd.Parameters.AddWithValue("@c", cmbCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@p", txtUnitPrice.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Inserted Successfully!");
            con.Close();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtProductName.Clear();
            txtImage.Clear();
            cmbCategory.SelectedIndex = -1;
            txtUnitPrice.Clear();
        }
    }
}
