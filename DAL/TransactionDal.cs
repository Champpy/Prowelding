using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTO;
using Entities;

namespace DAL
{
    public class TransactionDal //: BaseDal
    {
        #region Init Dal

        private BaseDal conn;

        #region " | Instance | "

        private static volatile TransactionDal _instance;

        private TransactionDal()
        {
            conn = new BaseDal();
        }

        public static TransactionDal Instance
        {
            get
            {
                _instance = new TransactionDal();
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
        ~TransactionDal()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion | Dispose |

        #endregion

        public List<SaleHeaderDTO> GetSearchCustomer(string CusName)
        {
            List<SaleHeaderDTO> lst = new List<SaleHeaderDTO>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "CustomerName", Value = CusName });
                DataSet ds = conn.GetDataSet("GetCustomerHistorty", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    SaleHeaderDTO o = new SaleHeaderDTO();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new SaleHeaderDTO();
                        o.SaleHeaderID = Convert.ToInt32(dr["SaleHeaderID"].ToString());
                        o.CustomerName = dr["CustomerName"].ToString();   
                        o.Tel = dr["Tel"].ToString();
                        if (dr["ReceivedDate"].ToString() != "")
                        {
                            o.ReceivedDate = Convert.ToDateTime(dr["ReceivedDate"].ToString());
                        }
                        
                        o.ReceivedBy = dr["ReceivedBy"].ToString();
                        o.SaleNumber = dr["SaleNumber"].ToString();
                        o.ItemCode = dr["ItemCode"].ToString();
                        o.ItemName = dr["ItemName"].ToString();
                        o.ItemID = dr["ItemID"].ToString() == "" ? 0 : Convert.ToInt32(dr["ItemID"].ToString());
                        o.dAmount = Convert.ToDouble(dr["Amount"].ToString());
                        o.ItemPrice = Convert.ToDouble(dr["ItemPrice"].ToString());
                        o.Discount = Convert.ToDouble(dr["Discount"].ToString());
                        o.SerialNumber = dr["SerialNumber"].ToString();
                        o.BillType = dr["BillType"].ToString();

                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public string InsertSaleHeader(TransSaleHeader o, List<SaleDetailDTO> lstDetail)
        {
            string error = "";
            try
            {
                DataSet ds = new DataSet();
                List<SqlParameter> param = new List<SqlParameter>();
                Int32 HeaderID = 0, DetailID = 0;

                conn.BeginTransaction();
                #region Header Params
                param.Add(new SqlParameter() { ParameterName = "tel", Value = o.Tel, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerName", Value = o.CustomerName, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerAddress", Value = o.CustomerAddress, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerDistrict", Value = o.CustomerDistrict, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerCountry", Value = o.CustomerCountry, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerProvince", Value = o.CustomerProvince, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerPostalCode", Value = o.CustomerPostalCode, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverName", Value = o.DeliveryName, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverAddress", Value = o.DeliverAdd, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverDistrict", Value = o.DeliverDistrict, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverCountry", Value = o.DeliverCountry, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverProvince", Value = o.DeliverProvince, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverPostalCode", Value = o.DeliverPostalCode, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "ReceivedDate", Value = o.ReceivedDate, DbType = DbType.DateTime });
                param.Add(new SqlParameter() { ParameterName = "ReceivedBy", Value = o.ReceivedBy, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "SaleNumber", Value = o.SaleNumber, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "Remark", Value = o.Remark, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "ConsignmentNo", Value = o.ConsignmentNo, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "COD", Value = o.COD, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "WarrantyDate", Value = o.WarrantyDate, DbType = DbType.DateTime });
                param.Add(new SqlParameter() { ParameterName = "BillType", Value = o.BillType, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "PayType", Value = o.PayType, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "SaleName", Value = o.SaleName, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "AccountTransfer", Value = o.AccountTransfer, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "TimeTransfer", Value = o.TimeTransfer, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "Installment", Value = o.Installment, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "Active", Value = o.Active, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CreatedBy", Value = o.CreatedBy, DbType = DbType.String });
                #endregion

                conn.CallStoredProcedure("InsTransSaleHeader", param, out ds, out error);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && string.IsNullOrEmpty(error))
                {
                    HeaderID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    if(lstDetail != null && lstDetail.Count > 0)
                    {
                        Int32 TransID = 0;
                        string tid = "";
                        string[] tidArr;
                        foreach (SaleDetailDTO item in lstDetail)
                        {
                            param = new List<SqlParameter>();
                            param.Add(new SqlParameter() { ParameterName = "HeaderID", Value = HeaderID, DbType = DbType.Int32 });
                            param.Add(new SqlParameter() { ParameterName = "ItemID", Value = item.ItemID, DbType = DbType.Int32 });
                            param.Add(new SqlParameter() { ParameterName = "ItemPrice", Value = item.ItemPrice, DbType = DbType.Decimal });
                            param.Add(new SqlParameter() { ParameterName = "Amount", Value = item.Amount, DbType = DbType.Decimal });
                            param.Add(new SqlParameter() { ParameterName = "Discount", Value = item.Discount, DbType = DbType.Decimal });
                            param.Add(new SqlParameter() { ParameterName = "SerialNumber", Value = item.SerialNumber, DbType = DbType.String });
                            param.Add(new SqlParameter() { ParameterName = "ItemDetail", Value = item.ItemDescription, DbType = DbType.String });
                            conn.CallStoredProcedure("InsTransSaleDetail", param, out ds, out error);

                            #region Status = 'N' on SerialNumber
                            tid = item.SNID;
                            if(!string.IsNullOrEmpty(tid) && string.IsNullOrEmpty(error))
                            {
                                tidArr = tid.Split(',');
                                if(tidArr != null && tidArr.Length > 0)
                                {
                                    foreach (string s in tidArr)
                                    {
                                        TransID = Convert.ToInt32(s);
                                        param = new List<SqlParameter>();
                                        param.Add(new SqlParameter() { ParameterName = "TransID", Value = TransID, DbType = DbType.Int32 });
                                        conn.CallStoredProcedure("UpdTransProductSerialStatus", param, out error);
                                    }
                                }
                            }
                            #endregion

                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && item.ProductDetail != null && item.ProductDetail.Count > 0 && string.IsNullOrEmpty(error))
                            {
                                DetailID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                                foreach (MasPackageDetail pd in item.ProductDetail)
                                {
                                    param = new List<SqlParameter>();
                                    param.Add(new SqlParameter() { ParameterName = "SaleDetailID", Value = DetailID, DbType = DbType.Int32 });
                                    param.Add(new SqlParameter() { ParameterName = "ProductID", Value = pd.ProductID, DbType = DbType.Int32 });
                                    param.Add(new SqlParameter() { ParameterName = "Amount", Value = pd.Amount, DbType = DbType.Int32 });
                                    conn.CallStoredProcedure("InsTransSaleDetailProduct", param, out error);
                                }
                            }
                        }
                    }
                }
                
                if(string.IsNullOrEmpty(error))
                {
                    conn.Commit();
                }
                else
                {
                    conn.RollBack();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return error;
        }

        public string UpdateSaleHeader(TransSaleHeader o, List<SaleDetailDTO> lstDetail, ref string result)
        {
            string error = "";
            try
            {
                DataSet ds = new DataSet();
                List<SqlParameter> param = new List<SqlParameter>();
                conn.BeginTransaction();

                #region Header Params
                param.Add(new SqlParameter() { ParameterName = "SaleHeaderID", Value = o.SaleHeaderID, DbType = DbType.Int32 });
                param.Add(new SqlParameter() { ParameterName = "tel", Value = o.Tel, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerName", Value = o.CustomerName, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerAddress", Value = o.CustomerAddress, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerDistrict", Value = o.CustomerDistrict, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerCountry", Value = o.CustomerCountry, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerProvince", Value = o.CustomerProvince, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "CustomerPostalCode", Value = o.CustomerPostalCode, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverName", Value = o.DeliveryName, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverAddress", Value = o.DeliverAdd, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverDistrict", Value = o.DeliverDistrict, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverCountry", Value = o.DeliverCountry, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverProvince", Value = o.DeliverProvince, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "DeliverPostalCode", Value = o.DeliverPostalCode, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "ReceivedDate", Value = o.ReceivedDate, DbType = DbType.DateTime });
                param.Add(new SqlParameter() { ParameterName = "ReceivedBy", Value = o.ReceivedBy, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "SaleNumber", Value = o.SaleNumber, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "Remark", Value = o.Remark, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "ConsignmentNo", Value = o.ConsignmentNo, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "COD", Value = o.COD, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "WarrantyDate", Value = o.WarrantyDate, DbType = DbType.DateTime });
                param.Add(new SqlParameter() { ParameterName = "BillType", Value = o.BillType, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "PayType", Value = o.PayType, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "SaleName", Value = o.SaleName, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "AccountTransfer", Value = o.AccountTransfer, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "TimeTransfer", Value = o.TimeTransfer, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "Installment", Value = o.Installment, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "UpdatedBy", Value = o.UpdatedBy, DbType = DbType.String });
                #endregion

                conn.CallStoredProcedure("UpdTransSaleHeader", param, out error);
                if (lstDetail != null && lstDetail.Count > 0 && string.IsNullOrEmpty(error))
                {
                    lstDetail = lstDetail.Where(w => w.UpdateSN.Equals("Y")).ToList();
                    foreach (SaleDetailDTO item in lstDetail)
                    {
                        param = new List<SqlParameter>();
                        param.Add(new SqlParameter() { ParameterName = "PackageID", Value = item.ItemID, DbType = DbType.Int32 });
                        param.Add(new SqlParameter() { ParameterName = "SaleDetailID", Value = item.SaleDetailID, DbType = DbType.Int32 });
                        param.Add(new SqlParameter() { ParameterName = "SerialNumber", Value = item.SerialNumber, DbType = DbType.String });
                        conn.CallStoredProcedure("UpdTransSaleDetail", param, out ds, out error);
                        if (!string.IsNullOrEmpty(error))
                            break;

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                        {
                            result = result + "Product Name : " + item.ItemName + " --> " + ds.Tables[0].Rows[0][0].ToString() + "\\r";
                        }
                    }
                }

                if (string.IsNullOrEmpty(error))
                {
                    conn.Commit();
                }
                else
                {
                    conn.RollBack();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                conn.RollBack();
            }
            return error;
        }

        public void GetTransSaleByID(Int32 HeaderID, ref TransSaleHeader header, ref List<SaleDetailDTO> detail)
        {
            TransSaleHeader obj = new TransSaleHeader();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "SaleHeaderID", Value = HeaderID, DbType = DbType.Int32 });
                DataSet ds = conn.GetDataSet("GetTransSaleByID", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        header = new TransSaleHeader();
                        header.SaleHeaderID = Convert.ToInt32(dr["SaleHeaderID"].ToString());
                        header.Tel = dr["Tel"].ToString();
                        if (dr["ReceivedDate"].ToString() != "")
                        {
                            header.ReceivedDate = Convert.ToDateTime(dr["ReceivedDate"].ToString());
                        }
                        header.CustomerName = dr["CustomerName"].ToString();
                        header.CustomerAddress = dr["CustomerAddress"].ToString();
                        header.CustomerDistrict = dr["CustomerDistrict"].ToString();
                        header.CustomerCountry = dr["CustomerCountry"].ToString();
                        header.CustomerProvince = dr["CustomerProvince"].ToString();
                        header.CustomerPostalCode = dr["CustomerPostalCode"].ToString();
                        header.DeliveryName = dr["DeliveryName"].ToString();
                        header.DeliverAdd = dr["DeliverAdd"].ToString();
                        header.DeliverDistrict = dr["DeliverDistrict"].ToString();
                        header.DeliverCountry = dr["DeliverCountry"].ToString();
                        header.DeliverProvince = dr["DeliverProvince"].ToString();
                        header.DeliverPostalCode = dr["DeliverPostalCode"].ToString();

                        header.SaleNumber = dr["SaleNumber"].ToString();
                        header.Remark = dr["Remark"].ToString();
                        header.ConsignmentNo = dr["ConsignmentNo"].ToString();
                        if (dr["WarrantyDate"].ToString() != "")
                        {
                            header.WarrantyDate = Convert.ToDateTime(dr["WarrantyDate"].ToString());
                        }
                        header.PayType = dr["PayType"].ToString();
                        header.BillType = dr["BillType"].ToString();
                        header.AccountTransfer = dr["AccountTransfer"].ToString();
                        header.SaleName = dr["SaleName"].ToString();
                        header.Installment = dr["Installment"].ToString();

                        break;
                    }
                }

                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1] != null)
                {
                    SaleDetailDTO d = new SaleDetailDTO();
                    MasPackageDetail pd = new MasPackageDetail();
                    DataRow[] drArr;
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        d = new SaleDetailDTO();
                        d.SaleHeaderID = Convert.ToInt32(dr["SaleHeaderID"].ToString());
                        d.SaleDetailID = Convert.ToInt32(dr["SaleDetailID"].ToString());
                        d.ItemID = Convert.ToInt32(dr["ItemID"].ToString());
                        d.ItemName = dr["PackageName"].ToString();
                        d.ItemCode = dr["PackageCode"].ToString();
                        d.ItemPrice = Convert.ToDouble(dr["ItemPrice"].ToString());
                        d.ItemDescription = dr["ItemDetail"].ToString();
                        d.Amount = Convert.ToDouble(dr["Amount"].ToString());
                        d.Discount = Convert.ToDouble(dr["Discount"].ToString());
                        d.SerialNumber = dr["SerialNumber"].ToString();
                        d.Status = "Old";
                        d.UpdateSN = "N";

                        if (d.ProductDetail == null)
                            d.ProductDetail = new List<MasPackageDetail>();

                        if(ds.Tables.Count > 2 && ds.Tables[2] != null)
                        {
                            drArr = ds.Tables[2].Select("SaleDetailID=" + d.SaleDetailID);
                            if(drArr != null && drArr.Length > 0)
                            {
                                foreach (DataRow row in drArr)
                                {
                                    pd = new MasPackageDetail();
                                    pd.ProductID = Convert.ToInt32(row["ProductID"].ToString());
                                    pd.ProductCode = row["ProductCode"].ToString();
                                    pd.ProductName = row["ProductName"].ToString();
                                    pd.Amount = Convert.ToInt32(row["Amount"].ToString());
                                    pd.CanChange = "N";
                                    d.ProductDetail.Add(pd);
                                }
                            }
                        }

                        detail.Add(d);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
