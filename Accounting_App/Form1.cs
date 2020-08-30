using Accounting_Utility.Convertor;
using Accounting_ViewModel.Accounting;
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

            this.Hide();
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog()==DialogResult.OK)
            {
                this.Show();
                lblDate.Text = DateTime.Now.ToShamsi();
                lblTime.Text = DateTime.Now.ToShortTimeString();     
                Report();
            }
            else
            {
                Application.Exit();
            }
        }

        private void btnReportRecive_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport();
            frm.TypeID = 2;
            frm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToShortTimeString();

        }

        private void btnEditLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.IsEdit = true;
            loginForm.ShowDialog();
        }

        void Report()
        {
            ReportViewModel report = Accounting_Business.Account.ReportForMain();
            lblPay.Text = report.Pay.ToString("#,0");
            lblRecive.Text = report.Recive.ToString("#,0");
            lblRecive.Text = report.AccountBalance.ToString("#,0");
        }
    }
}
