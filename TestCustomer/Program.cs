using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Services;
using Accounting.DataLayer.Context;

namespace TestCustomer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dependency Injection
            //Accounting_DBEntities db = new Accounting_DBEntities();
            //ICustomerRepository customer = new CustomerRepository(db);
            //----------------------------------------------------------
            //var list = customer.GetAllCustomers();
            //Console.WriteLine("لطفا کد شخص را وارد کنید");
            //int id = int.Parse(Console.ReadLine());
            //customer.DeleteCustomer(id);
            //customer.Save();
            //Customers list = customer.GetCustomerById(id);
            //Customers AddCustomer = new Customers()
            //{
            //    FullName = "Pedram",
            //    EmailAddress="pedi@ymail.com",
            //    CustomerImage="NO",
            //    Mobile="09358566006"
            //};
            //customer.InsertCustomer(AddCustomer);
            //customer.Save();
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"Name: {item.FullName} , Mobile: {item.Mobile} , Email: {item.EmailAddress}");
            //}
            //UnitOfWork

            UnitOfWork db = new UnitOfWork();
            var list = db.CustomerRepository.GetAllCustomers();
            db.Dispose();
            Console.ReadKey();
        }

    }
}
