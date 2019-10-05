using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class MasPackageHeader
    {
        public int PackageHeaderID { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public double PurchasePrice { get; set; }
        public double SellPrice { get; set; }
        public string Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        
    }
}