using Accounting.DataLayer.Repositories;
using Accounting_ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private Accounting_DBEntities db;

        public CustomerRepository(Accounting_DBEntities context)
        {
            db = context;
        }
        public bool DeleteCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;
            }
            catch
            {

                return false;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                var customer = GetCustomerById(customerId);
                DeleteCustomer(customer);
                return true;
            }
            catch 
            {

                return false;
            }
        }

        public List<Customers> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        public Customers GetCustomerById(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public List<ListCustomerView> GetCustomerByName(string filter = "")
        {
            if (filter == "")
            {
                return db.Customers.Select(c => new ListCustomerView()
                {
                    CustomerID = c.CustomerID,
                    FullName = c.FullName
                }
                ).ToList();
            }
            return db.Customers.Where(c => c.FullName.Contains(filter)).Select(c => new ListCustomerView()
            {
                CustomerID = c.CustomerID,
                FullName = c.FullName
            }
            ).ToList();
        }

        public int GetCustomerIDByName(string CustomerName)
        {
            return db.Customers.First(c => c.FullName == CustomerName).CustomerID;
        }

        public string GetCustomerNameById(int customerId)
        {
            return db.Customers.Find(customerId).FullName;
        }

        public IEnumerable<Customers> GetCustomersByFilter(string parameter)
        {
            return db.Customers.Where(c =>
                c.FullName.Contains(parameter) || c.EmailAddress.Contains(parameter) || c.Mobile.Contains(parameter)).ToList();
        }

        public bool InsertCustomer(Customers customer)
        {
            try
            {
                db.Customers.Add(customer);
                return true;
            }
            catch 
            {

                return false;
            } 
        }



        public bool UpdateCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Modified;
                return true;
            }
            catch
            {

                return false;
            }
        }


    }
}
