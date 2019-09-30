﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Linq;
    
    public partial class BillingEntities : DbContext
    {
        public BillingEntities()
            : base("name=BillingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MasAccount> MasAccounts { get; set; }
        public virtual DbSet<MasUserLogin> MasUserLogins { get; set; }
        public virtual DbSet<MasItem> MasItems { get; set; }
        public virtual DbSet<TransSaleDetail> TransSaleDetails { get; set; }
        public virtual DbSet<MasSender> MasSenders { get; set; }
        public virtual DbSet<TransSaleHeader> TransSaleHeaders { get; set; }
        public virtual DbSet<MasCategory> MasCategories { get; set; }
        public virtual DbSet<MasValueList> MasValueLists { get; set; }
        public virtual DbSet<MasAccountTransfer> MasAccountTransfers { get; set; }
        public virtual DbSet<MasDistributor> MasDistributors { get; set; }
        public virtual DbSet<MasItemType> MasItemTypes { get; set; }
        public virtual DbSet<MasUnit> MasUnits { get; set; }
    
        public virtual ObjectResult<GetReportSaleItem_Result> GetReportSaleItem(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, string itemCode, string group)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var itemCodeParameter = itemCode != null ?
                new ObjectParameter("ItemCode", itemCode) :
                new ObjectParameter("ItemCode", typeof(string));
    
            var groupParameter = group != null ?
                new ObjectParameter("Group", group) :
                new ObjectParameter("Group", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReportSaleItem_Result>("GetReportSaleItem", dateFromParameter, dateToParameter, itemCodeParameter, groupParameter);
        }
    
        public virtual ObjectResult<GetReportSaleItemItemCode_Result> GetReportSaleItemItemCode(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo, string itemCode)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            var itemCodeParameter = itemCode != null ?
                new ObjectParameter("ItemCode", itemCode) :
                new ObjectParameter("ItemCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReportSaleItemItemCode_Result>("GetReportSaleItemItemCode", dateFromParameter, dateToParameter, itemCodeParameter);
        }
    
        public virtual ObjectResult<GetCustomerDist_Result> GetCustomerDist()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCustomerDist_Result>("GetCustomerDist");
        }
    
        public virtual ObjectResult<GetTransSaleList_Result1> GetTransSaleList(Nullable<System.DateTime> dateFrom, Nullable<System.DateTime> dateTo)
        {
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            var dateToParameter = dateTo.HasValue ?
                new ObjectParameter("DateTo", dateTo) :
                new ObjectParameter("DateTo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTransSaleList_Result1>("GetTransSaleList", dateFromParameter, dateToParameter);
        }
    }
}
