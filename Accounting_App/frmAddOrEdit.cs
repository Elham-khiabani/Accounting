using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting_App
{
    public partial class frmAddOrEdit : Form
    {
        UnitOfWork db = new UnitOfWork();
        public frmAddOrEdit()
        {
            InitializeComponent();
        }

        private void frmAddOrEdit_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pcCustomerPhoto.ImageLocation = openFileDialog.FileName;
            }


        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomerPhoto.ImageLocation);
            string Path = Application.StartupPath + "/Images/";
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            pcCustomerPhoto.Image.Save(Path + ImageName);
            if (BaseValidator.IsFormValid(this.components))
            {
                Customers customer = new Customers()
                {
                    FullName = txtFullName.Text,
                    Address = txtAddress.Text,
                    EmailAddress = txtEmailAddress.Text,
                    Mobile = txtMobile.Text,
                    CustomerImage = ImageName
                };
                db.CustomerRepository.InsertCustomer(customer);
                db.Save();
                DialogResult = DialogResult.OK;
            }
        }
    }
}
