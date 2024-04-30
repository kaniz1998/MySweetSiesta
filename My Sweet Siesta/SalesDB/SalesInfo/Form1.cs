using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void entryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoryEntry fce = new frmCategoryEntry();
            fce.Show();
            //fce.MdiParent = this;
        }

        private void entryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmProductEntry fpe = new frmProductEntry();
            fpe.Show();
            //fpe.MdiParent = this;
        }

        private void entryToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmCustomersEntry fce = new frmCustomersEntry();
            fce.Show();
            //fce.MdiParent = this;
        }

        private void entryToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmOrdersEntry foe = new frmOrdersEntry();
            foe.Show();
            //foe.MdiParent = this;
        }

        private void updateDeleteToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmOrderUpdateDelete foe = new frmOrderUpdateDelete();
            foe.Show();
            //foe.MdiParent = this;
        }

        private void updateDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoryUpdateDelete fcu = new frmCategoryUpdateDelete();
            fcu.Show();
            //fcu.MdiParent = this;
        }

        private void updateDeleteToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmCustomerSUpdateDelete fcu = new frmCustomerSUpdateDelete();
            fcu.Show();
            //fcu.MdiParent = this;
        }


        private void orderInformationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrderInformationReport foe = new frmOrderInformationReport();
            foe.Show();
            //foe.MdiParent = this;
        }

        private void updateDeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmProductUpdateDelete fpu = new frmProductUpdateDelete();
            fpu.Show();
            //fpu.MdiParent = this;
        }
    }
}
