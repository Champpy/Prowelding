//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Billing.AppData
{
    using System;
    using System.Collections.Generic;
    
    public partial class MasItem
    {
        public int ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDesc { get; set; }
        public Nullable<double> ItemPrice { get; set; }
        public string Active { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<int> ItemTypeID { get; set; }
        public Nullable<int> DistributorID { get; set; }
        public Nullable<int> MinRemaining { get; set; }
    }
}
