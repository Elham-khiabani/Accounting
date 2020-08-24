﻿using Accounting.DataLayer.Context;
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

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dgCustomers.CurrentRow.Cells[0].Value.ToString());
            string fullName = dgCustomers.CurrentRow.Cells[1].Value.ToString();
            using (UnitOfWork db=new UnitOfWork())
            {
                if (dgCustomers.CurrentRow!=null)
                {
                    if (RtlMessageBox.Show($"ایا از حذف {fullName} اطمینان دارید.", "هشدار", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        db.CustomerRepository.DeleteCustomer(id);
                        db.Save();
                        BindGrid();
                    }
                }
                else
                {
                    RtlMessageBox.Show("لطفا یک شخص را انتخاب کنید.");
                }
            }
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            frmAddOrEdit frm = new frmAddOrEdit();
            if (frm.ShowDialog()==DialogResult.OK)
            {
                BindGrid();
            }
            
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgCustomers.CurrentRow!=null)
            {
                int customerId = int.Parse(dgCustomers.CurrentRow.Cells[0].Value.ToString());
                frmAddOrEdit frmEdit = new frmAddOrEdit();
                frmEdit.customerId = customerId;
                if(frmEdit.ShowDialog()==DialogResult.OK)
                {
                    BindGrid();
                }
            }
        }
    }
}
