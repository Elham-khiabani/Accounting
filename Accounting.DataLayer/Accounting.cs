//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Accounting.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Accounting
    {
        public int AccountingID { get; set; }
        public int CustomerID { get; set; }
        public byte TypeID { get; set; }
        public decimal Amount { get; set; }
        public string Desceription { get; set; }
        public System.DateTime DateTime { get; set; }
    
        public virtual AccountingType AccountingType { get; set; }
        public virtual Customers Customers { get; set; }
    }
}
