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
        //public List<string> lstSN { get; set; }
        public int ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public List<TransProductSerial> lstSerial {get; set;}

        public string SNConcat
        {
            get
            {
                string result = "";
                try
                {
                    if(lstSerial != null && lstSerial.Count > 0)
                    {
                        foreach (var item in lstSerial)
                        {
                            result = result + item.SerialNumber + ", ";
                        }
                    }
                }
                catch (Exception)
                {
                    
                }
                return result;
            }
        }
        #endregion
    }
}