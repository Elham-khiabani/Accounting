using Accounting.DataLayer.Context;
using Accounting_Utility.Convertor;
using Accounting_ViewModel.Customer;
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
    public partial class frmReport : Form
    {
        public int TypeID = 0;

        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<ListCustomerView> list = new List<ListCustomerView>();
                list.Add(new ListCustomerView()
                {
                    CustomerID = 0,
                    FullName = "انتخاب کنید."
                });

                list.AddRange(db.CustomerRepository.GetCustomerByName());
                cbCustomer.DataSource = list;
                cbCustomer.DisplayMember = "FullName";
                cbCustomer.ValueMember = "CustomerID";
            }
            if (TypeID == 2)
            {
                this.Text = "گزارش دریافتی";
            }
            else
            {
                this.Text = "گزارش پرداختی";
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        void Filter()
        {
            List<Accounting.DataLayer.Accounting> result = new List<Accounting.DataLayer.Accounting>();
            DateTime? startDate;
            DateTime? endDate;

            using (UnitOfWork db = new UnitOfWork())
            {
                if ((int)cbCustomer.SelectedValue != 0)
                {
                    int customerID = int.Parse(cbCustomer.SelectedValue.ToString());
                    result.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeID && a.CustomerID == customerID));
                }
                else
                {
                    result.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeID));
                }
                if (txtDateFrom.Text != "    /  /")
                {
                    startDate = Convert.ToDateTime(txtDateFrom.Text);
                    startDate = DateConvertor.ToMiladi(startDate.Value);
                    result = result.Where(r => r.DateTime >= startDate.Value).ToList();
                }
                if (txtDateFrom.Text != "    /  /")
                {
                    endDate = Convert.ToDateTime(txtDateTo.Text);
                    endDate = DateConvertor.ToMiladi(endDate.Value);
                    result = result.Where(r => r.DateTime <= endDate.Value).ToList();
                }
                dgReport.AutoGenerateColumns = false;
                dgReport.Rows.Clear();
                foreach (var item in result)
                {
                    string customerName = db.CustomerRepository.GetCustomerNameById(item.CustomerID);
                    dgReport.Rows.Add(item.AccountingID, customerName, item.Amount, item.DateTime.ToShamsi());
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                if (RtlMessageBox.Show("آیا از حذف اطمینان دارید؟", "هشدار", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        db.AccountingRepository.Delete(id);
                        db.Save();
                        Filter();
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                frmNewTransaction frmNew = new frmNewTransaction();
                frmNew.accountID = id;
                if (frmNew.ShowDialog() == DialogResult.OK)
                {
                    Filter();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtPrint = new DataTable();
            dtPrint.Columns.Add("Customer");
            dtPrint.Columns.Add("Amount");
            dtPrint.Columns.Add("Date");
            foreach (DataGridViewRow item in dgReport.Rows)
            {
                dtPrint.Rows.Add(
                      item.Cells[0].Value.ToString(),
                      item.Cells[1].Value.ToString(),
                      item.Cells[2].Value.ToString()
                    );

            }
            stiPrint.Load(Application.StartupPath+"/Report.mrt");
            stiPrint.RegData("DT", dtPrint);
            stiPrint.Show();
        }
    }
}
