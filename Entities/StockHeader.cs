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

        public string StockTimeStr
        {
            get
            {
                return StockTime.HasValue ? StockTime.Value.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")) : "";
            }
        }

        public string StockTypeDesc
        {
            get
            {
                string result = "";
                switch (StockType.ToLower())
                {
                    case "transfer":
                        result = "ย้ายเข้าคลังขาย";
                        break;
                    case "in":
                        result = "รับเข้า";
                        break;
                    case "out":
                        result = "นำออก";
                        break;
                    default:
                        break;
                }
                return result;
            }
        }
        public string StockFrom { get; set; }
    }
}