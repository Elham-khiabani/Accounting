﻿using System;
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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomers frm = new frmCustomers();
            frm.ShowDialog();
        }

        private void btnNewAcconting_Click(object sender, EventArgs e)
        {
            frmNewTransaction frm = new frmNewTransaction();
            
            if (frm.ShowDialog()==DialogResult.OK)
            {
                RtlMessageBox.Show("عملیات با موفقیت انجام گردید.");
            }
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport();
            frm.TypeID = 1;
            frm.ShowDialog();

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnReportRecive_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport();
            frm.TypeID = 2;
            frm.ShowDialog();
        }
    }
}
