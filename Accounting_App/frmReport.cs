﻿using Accounting.DataLayer.Context;
using Accounting_Utility.Convertor;
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
            using (UnitOfWork db = new UnitOfWork())
            {
                var result = db.AccountingRepository.Get(a => a.TypeID == TypeID);
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
            if (dgReport.CurrentRow!=null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                frmNewTransaction frmNew = new frmNewTransaction();
                frmNew.accountID = id;
                if (frmNew.ShowDialog()==DialogResult.OK)
                {
                    Filter();
                }
            }
        }
    }
}