using Accounting.DataLayer.Context;
using Accounting_ViewModel.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_Business
{
    public class Account
    {
        public static ReportViewModel ReportForMain()
        {
            ReportViewModel rp = new ReportViewModel();
            using (UnitOfWork db = new UnitOfWork())
            {
                DateTime stateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 30);
                var recive = db.AccountingRepository.Get(a => a.TypeID == 1 && a.DateTime >= stateDate && a.DateTime <= endDate)
                    .Select(
                   a => a.Amount
                              ).ToList();
                var pay = db.AccountingRepository.Get(a => a.TypeID == 2 && a.DateTime >= stateDate && a.DateTime <= endDate)
                    .Select(
                    a => a.Amount
                                ).ToList();
                rp.Recive = (int)recive.Sum();
                rp.Pay =(int) pay.Sum();
                rp.AccountBalance = (int)recive.Sum() - (int)pay.Sum();


            }
            return rp;
        }
    }
}
