using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class TransSaleDetailProduct
    {
        public int SaleDetailProductID { get; set; }
        public int SaleDetailID { get; set; }
        public int ProductID { get; set; }
        public double? Amount { get; set; }
        
    }
}