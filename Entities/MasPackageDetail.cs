using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class MasPackageDetail
    {
        public int PackageDetailID { get; set; }
        public int PackageHeaderID { get; set; }
        public int ProductID { get; set; }
        public Int32 Amount { get; set; }
        public Int32 OrderNo { get; set; }
        public string CanChange { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        #region Product Detail
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        #endregion
    }
}