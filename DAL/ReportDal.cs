using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.DTO;

namespace DAL
{
    public class ReportDal //: BaseDal
    {
        #region Init Dal

        private BaseDal conn;

        #region " | Instance | "

        private static volatile ReportDal _instance;

        private ReportDal()
        {
            conn = new BaseDal();
        }

        public static ReportDal Instance
        {
            get
            {
                _instance = new ReportDal();
                return _instance;
            }
        }

        #endregion

        #region | Dispose |

        private bool _disposed;
        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).

                // Set large fields to null.
                _disposed = true;
            }
        }

        // Destructor syntax for finalization code.
        ~ReportDal()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion | Dispose |

        #endregion
        
        public List<ReportSaleMonthDTO> GetSearchReportSaleMonth(DateTime dateFrom, DateTime dateTo, double vat, double mm200, double mm225)
        {
            List<ReportSaleMonthDTO> lst = new List<ReportSaleMonthDTO>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "DateFrom", Value = dateFrom, DbType = DbType.DateTime });
                param.Add(new SqlParameter() { ParameterName = "DateTo", Value = dateTo, DbType = DbType.DateTime });
                
                DataSet ds = conn.GetDataSet("GetReportSaleMonth", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ReportSaleMonthDTO item = new ReportSaleMonthDTO();
                        item.HeaderID = Convert.ToInt32(dr["SaleHeaderID"].ToString());
                        item.Remark = dr["Remark"].ToString();
                        item.CustomerName = dr["CustomerName"].ToString();
                        item.CustomerAddress = dr["CustomerAddress"].ToString();
                        item.CustomerDistrict = dr["CustomerDistrict"].ToString();
                        item.CustomerCountry = dr["CustomerCountry"].ToString();
                        item.CustomerProvince = dr["CustomerProvince"].ToString();
                        item.CustomerPostalCode = dr["CustomerPostalCode"].ToString();
                        item.ReceivedDate = Convert.ToDateTime(dr["ReceivedDate"].ToString());
                        item.WarrantyDate = Convert.ToDateTime(dr["WarrantyDate"].ToString());
                        item.SaleNumber = dr["SaleNumber"].ToString();
                        if(!string.IsNullOrEmpty(dr["SaleDetailID"].ToString()))
                        {
                            item.DetailID = Convert.ToInt32(dr["SaleDetailID"].ToString());
                            item.ItemCode = dr["PackageCode"].ToString();
                            item.ItemName = dr["PackageName"].ToString();
                            item.ItemDescription = dr["ItemDetail"].ToString();
                            item.ItemPrice = Convert.ToDouble(dr["ItemPrice"].ToString());
                            item.Discount = Convert.ToDouble(dr["Discount"].ToString());
                            item.Amount = Convert.ToDouble(dr["Amount"].ToString());
                        }
                        
                        item.SerialNumber = dr["SerialNumber"].ToString();
                        item.Tel = dr["Tel"].ToString();
                        item.Address = dr["CustomerAddress"].ToString();
                        item.District = dr["CustomerDistrict"].ToString();
                        item.Country = dr["CustomerCountry"].ToString();
                        item.Province = dr["CustomerProvince"].ToString();
                        item.PostalCode = dr["CustomerPostalCode"].ToString();
                        item.PayTypeID = dr["PayType"].ToString();
                        item.BillTypeID = dr["BillType"].ToString();
                        item.VAT = vat;
                        item.MAXMIG200 = mm200;
                        item.MAXMIG225 = mm225;
                        item.VisImgBtn = "true";
                        item.ConsignmentNo = dr["ConsignmentNo"].ToString();
                        item.AccountTransfer = dr["AccountTransfer"].ToString();
                        item.Installment = dr["Installment"].ToString();
                        item.Active = dr["Active"].ToString();
                        item.Remove = "N";
                        lst.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public List<ReportSaleItemDTO> GetSearchReportSaleItem(DateTime dateFrom, DateTime dateTo)
        {
            List<ReportSaleItemDTO> lst = new List<ReportSaleItemDTO>();
            try
            {
                ReportSaleItemDTO item = new ReportSaleItemDTO();
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "DateFrom", Value = dateFrom, DbType = DbType.DateTime });
                param.Add(new SqlParameter() { ParameterName = "DateTo", Value = dateTo, DbType = DbType.DateTime });

                DataSet ds = conn.GetDataSet("GetReportSaleItem", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    int i = 1;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        item = new ReportSaleItemDTO();
                        item.No = i;
                        item.ItemCode = dr["ItemCode"].ToString();
                        item.ItemName = dr["ItemName"].ToString();
                        item.Amount = string.IsNullOrEmpty(dr["Amount"].ToString()) ? 0 : Convert.ToDouble(dr["Amount"].ToString());
                        item.AmountFree = string.IsNullOrEmpty(dr["AmountFree"].ToString()) ? 0 : Convert.ToDouble(dr["AmountFree"].ToString());

                        lst.Add(item);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

    }
}
