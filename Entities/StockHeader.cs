using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class StockHeader
    {
        public StockHeader()
        {
            detail = new List<StockDetail>();
        }
        public int StockHeaderID { get; set; }
        public string StockType { get; set; }
        public DateTime? StockTime { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<StockDetail> detail { get; set; }

    }
}