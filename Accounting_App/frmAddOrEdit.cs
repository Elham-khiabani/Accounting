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
        public int customerId = 0;
        
        public frmAddOrEdit()
        {
            InitializeComponent();
        }

        private void frmAddOrEdit_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                if (customerId!=0)
                {
                    this.Text = "ویرایش شخص";
                    btnSubmit.Text = "ویرایش";
                    var customer = db.CustomerRepository.GetCustomerById(customerId);
                    txtFullName.Text = customer.FullName;
                    txtMobile.Text = customer.Mobile;
                    txtEmailAddress.Text = customer.EmailAddress;
                    txtAddress.Text = customer.Address;
                    pcCustomerPhoto.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerImage;
                }
                else
                {
                    this.Text = "افزودن شخص";
                }
            }
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
            using (UnitOfWork db = new UnitOfWork())
            {

                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomerPhoto.ImageLocation);
                string PathName = Application.StartupPath + "/Images/";
                if (!Directory.Exists(PathName))
                {
                    Directory.CreateDirectory(PathName);
                }
                pcCustomerPhoto.Image.Save(PathName + ImageName);
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
                    if (customerId==0)
                    {
                        db.CustomerRepository.InsertCustomer(customer);
                    }
                    else
                    {
                        customer.CustomerID = customerId;
                        db.CustomerRepository.UpdateCustomer(customer);
                    }
                    db.Save();
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
