using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class StockDetail
    {
        public int StockDetailID { get; set; }
        public int StockHeaderID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }


        #region Other
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string AmountStr
        {
            get
            {
                return Amount.ToString();
            }
        }
        #endregion
    }
}