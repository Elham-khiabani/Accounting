using Accounting.DataLayer;
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
using ValidationComponents;

namespace Accounting_App
{
    public partial class frmNewTransaction : Form
    {
        UnitOfWork db = new UnitOfWork();

        public frmNewTransaction()
        {
            InitializeComponent();
        }

        private void frmNewTransaction_Load(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetCustomerByName();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.DataSource = db.CustomerRepository.GetCustomerByName(txtFilter.Text);
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rbPay.Checked || rbRecive.Checked)
            {
                Accounting.DataLayer.Accounting accounting = new Accounting.DataLayer.Accounting()
                {
                    Amount = int.Parse(txtAmount.Value.ToString()),
                    CustomerID = db.CustomerRepository.GetCustomerIDByName(txtName.Text),
                    TypeID = (byte)(rbRecive.Checked ? 1 : 2),
                    Desceription = txtDescription.Text,
                    DateTime = DateTime.Now
                };
                db.AccountingRepository.Insert(accounting);
                db.Save();
                DialogResult = DialogResult.OK;
            }
            else
            {
                RtlMessageBox.Show("لطفا نوع تراکنش را انتخاب کنید.");
            }

        }

    }
}