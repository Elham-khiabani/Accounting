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
        private UnitOfWork db;
        public int accountID = 0;
        public frmNewTransaction()
        {
            InitializeComponent();
        }

        private void frmNewTransaction_Load(object sender, EventArgs e)
        {
            db = new UnitOfWork();
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetCustomerByName();
            if (accountID != 0)
            {
                var account = db.AccountingRepository.GetById(accountID);
                txtAmount.Text = account.Amount.ToString();
                txtDescription.Text = account.Desceription.ToString();
                txtName.Text = db.CustomerRepository.GetCustomerNameById(account.CustomerID);
                if (account.TypeID == 1)
                {
                    rbRecive.Checked = true;
                }
                else
                {
                    rbPay.Checked = true;
                }
                this.Text = "ویرایش";
                btnSave.Text = "ویرایش";
            }
            db.Dispose();
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
            db = new UnitOfWork();
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
                if (accountID==0)
                {
                    db.AccountingRepository.Insert(accounting);
                }
                else
                {

                    accounting.AccountingID = accountID;
                    db.AccountingRepository.Update(accounting);
                }
                db.Save();
                db.Dispose();
                DialogResult = DialogResult.OK;
            }
            else
            {
                RtlMessageBox.Show("لطفا نوع تراکنش را انتخاب کنید.");
            }

        }

    }
}