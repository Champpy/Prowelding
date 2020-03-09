using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class MasProduct
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double PurchasePrice { get; set; }
        public double SellPrice { get; set; }
        public string Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string DMLFlag { get; set; }
        public int Amount { get; set; }
        public string CanChange { get; set; }
        public string IsFree { get; set; }

        public int CHK_PRODUCT_CODE { get; set; }
        public int CHK_PRODUCT_NAME { get; set; }
        public int CHK_CODE_NAME { get; set; }

        #region Other
        public Int32 UnitID { get; set; }
        public string UnitName { get; set; }
        public Int32 TypeID { get; set; }
        public string TypeName { get; set; }
        public Int32 Remaining { get; set; }
        public Int32 RemainingHeadQ { get; set; }
        public string ProductSN { get; set; }
        public Int32 PackageDetailID { get; set; }
        #endregion

    }
}