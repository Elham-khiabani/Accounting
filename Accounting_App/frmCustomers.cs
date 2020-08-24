using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting_App
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        void BindGrid()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgCustomers.AutoGenerateColumns = false;
                dgCustomers.DataSource = db.CustomerRepository.GetAllCustomers();
            }

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db=new UnitOfWork())
            {
                dgCustomers.DataSource= db.CustomerRepository.GetCustomersByFilter(txtFilter.Text);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            BindGrid();
        }
    }
}
