using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class TransProductSerial
    {
        public int TransID { get; set; }
        public int ProductID { get; set; }
        public string SerialNumber { get; set; }
        //Used, 
        public string Status { get; set; }

    }
}