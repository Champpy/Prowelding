using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities.DTO
{
    public class StockDetailDTO : MasProduct
    {
        public int StockHeaderID { get; set; }
        public int StockDetailID { get; set; }
        public string AmountStr 
        {
            get
            {
                return Amount.ToString();
            }
        }
        
    }
}