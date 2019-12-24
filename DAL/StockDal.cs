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
    public class StockDal //: BaseDal
    {
        #region Init Dal

        private BaseDal conn;

        #region " | Instance | "

        private static volatile StockDal _instance;

        private StockDal()
        {
            conn = new BaseDal();
        }

        public static StockDal Instance
        {
            get
            {
                _instance = new StockDal();
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
        ~StockDal()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion | Dispose |

        #endregion

        //public string InsertStock(List<InventoryDTO> lst, string User)
        //{
        //    string err = "";
        //    try
        //    {
        //        List<SqlParameter> paramI = new List<SqlParameter>();
        //        foreach (InventoryDTO item in lst)
        //        {
        //            paramI = new List<SqlParameter>();
        //            paramI.Add(new SqlParameter() { ParameterName = "Remark", Value = item.Remark });
        //            paramI.Add(new SqlParameter() { ParameterName = "ItemID", Value = item.ItemID, DbType = DbType.Int32 });
        //            paramI.Add(new SqlParameter() { ParameterName = "Amount", Value = item.Amount, DbType = DbType.Int32 });
        //            paramI.Add(new SqlParameter() { ParameterName = "Serial", Value = item.Serial });
        //            paramI.Add(new SqlParameter() { ParameterName = "User", Value = User });
        //            conn.ExcuteNonQueryNClose("InsertTransInboundNStock", paramI, out err);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        err = ex.Message;
        //    }
        //    return err;
        //}

        public List<TransStock> CheckRemainingItem(Int32 ItemID)
        {
            List<TransStock> lst = new List<TransStock>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "ItemID", Value = ItemID, DbType = DbType.Int32 });
                DataSet ds = conn.GetDataSet("GetItemRemaining", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    TransStock o = new TransStock();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new TransStock();
                        o.StockID = Convert.ToInt32(dr["StockID"].ToString());
                        o.ItemID = Convert.ToInt32(dr["ItemID"].ToString());
                        o.Serial = dr["Serial"].ToString();
                        o.SaleHeaderID = Convert.ToInt32(dr["SaleHeaderID"].ToString());
                        o.SaleDetailID = Convert.ToInt32(dr["SaleDetailID"].ToString());
                        o.Active = dr["Active"].ToString();
                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public List<InventoryDTO> GetItemStock(string ItemCode, string ItemName)
        {
            List<InventoryDTO> lst = new List<InventoryDTO>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "ItemName", Value = ItemName });
                param.Add(new SqlParameter() { ParameterName = "ItemCode", Value = ItemCode });
                DataSet ds = conn.GetDataSet("GetSearchStock", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    InventoryDTO o = new InventoryDTO();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new InventoryDTO();
                        o.StockID = Convert.ToInt32(dr["StockID"].ToString());
                        o.ItemID = Convert.ToInt32(dr["ItemID"].ToString());
                        o.ItemCode = dr["ItemCode"].ToString();
                        o.ItemName = dr["ItemName"].ToString();
                        o.ItemDesc = dr["ItemDesc"].ToString();
                        o.Serial = dr["Serial"].ToString();
                        o.ItemPrice = Convert.ToDouble(dr["ItemPrice"].ToString());
                        o.UnitName = dr["UnitName"].ToString();
                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        //public List<InventoryDTO> GetItemInStock()
        //{
        //    List<InventoryDTO> lst = new List<InventoryDTO>();
        //    try
        //    {
        //        List<SqlParameter> param = new List<SqlParameter>();
        //        //DataSet ds = conn.GetDataSet("GetSearchItemInStock", param);
        //        DataSet ds = conn.GetDataSet("GetSearchItem", param);
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
        //        {
        //            InventoryDTO o = new InventoryDTO();
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                o = new InventoryDTO();
        //                //o.StockID = Convert.ToInt32(dr["StockID"].ToString());
        //                o.ItemID = Convert.ToInt32(dr["ItemID"].ToString());
        //                o.ItemCode = dr["ItemCode"].ToString();
        //                o.ItemName = dr["ItemName"].ToString();
        //                o.ItemDesc = dr["ItemDesc"].ToString();
        //                //o.Serial = dr["Serial"].ToString();
        //                o.ItemPrice = Convert.ToDouble(dr["ItemPrice"].ToString());
        //                o.UnitName = dr["UnitName"].ToString();
        //                lst.Add(o);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return lst;
        //}

        public List<MasPackageHeader> GetItemInStock()
        {
            List<MasPackageHeader> lst = new List<MasPackageHeader>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                //DataSet ds = conn.GetDataSet("GetSearchItemInStock", param);
                DataSet ds = conn.GetDataSet("GetSearchItem", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    MasPackageHeader o = new MasPackageHeader();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new MasPackageHeader();
                        //o.StockID = Convert.ToInt32(dr["StockID"].ToString());
                        o.PackageHeaderID = Convert.ToInt32(dr["PackageHeaderID"].ToString());
                        o.PackageCode = dr["PackageCode"].ToString();
                        o.PackageName = dr["PackageName"].ToString();
                        o.SellPrice = Convert.ToDouble(dr["SellPrice"].ToString());
                        //o.UnitName = dr["UnitName"].ToString();
                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public List<MasPackageHeader> GetPackageAll()
        {
            List<MasPackageHeader> lst = new List<MasPackageHeader>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                //DataSet ds = conn.GetDataSet("GetSearchItemInStock", param);
                DataSet ds = conn.GetDataSet("GetSearchPackage", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    MasPackageHeader o = new MasPackageHeader();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new MasPackageHeader();
                        //o.StockID = Convert.ToInt32(dr["StockID"].ToString());
                        o.PackageHeaderID = Convert.ToInt32(dr["PackageHeaderID"].ToString());
                        o.PackageCode = dr["PackageCode"].ToString();
                        o.PackageName = dr["PackageName"].ToString();
                        o.SellPrice = Convert.ToDouble(dr["SellPrice"].ToString());
                        //o.UnitName = dr["UnitName"].ToString();
                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        #region Stock
        public string InsertStock(StockHeader header, string User)
        {
            string err = "";
            try
            {
                conn.BeginTransaction();
                DataSet ds = new DataSet();
                Int32 HeaderID = 0;
                bool chk = false;
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "StockType", Value = header.StockType });
                param.Add(new SqlParameter() { ParameterName = "StockTime", Value = header.StockTime });
                param.Add(new SqlParameter() { ParameterName = "Remark", Value = header.Remark });
                param.Add(new SqlParameter() { ParameterName = "CreatedBy", Value = User });
                conn.CallStoredProcedure("InsStockHeader", param, out ds, out err);
                if(ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && string.IsNullOrEmpty(err) && header.detail != null && header.detail.Count > 0)
                {
                    HeaderID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    string cal = header.StockType == "IN" ? "+" : "-";
                    foreach (StockDetail item in header.detail)
                    {
                        param = new List<SqlParameter>();
                        param.Add(new SqlParameter() { ParameterName = "StockHeaderID", Value = HeaderID });
                        param.Add(new SqlParameter() { ParameterName = "ProductID", Value = item.ProductID, DbType = DbType.Int32 });
                        param.Add(new SqlParameter() { ParameterName = "Amount", Value = item.Amount, DbType = DbType.Int32 });
                        param.Add(new SqlParameter() { ParameterName = "CAL", Value = cal });
                        param.Add(new SqlParameter() { ParameterName = "SerialNumber", Value = item.SNConcat });
                        conn.CallStoredProcedure("InsStockDetail", param, out err);

                        if(item.lstSerial != null && item.lstSerial.Count > 0)
                        {
                            foreach (TransProductSerial tps in item.lstSerial)
                            {
                                ds = new DataSet();
                                param = new List<SqlParameter>();
                                param.Add(new SqlParameter() { ParameterName = "ProductID", Value = item.ProductID, DbType = DbType.Int32 });
                                param.Add(new SqlParameter() { ParameterName = "SerialNumber", Value = tps.SerialNumber });
                                param.Add(new SqlParameter() { ParameterName = "CAL", Value = cal });
                                conn.CallStoredProcedure("InsTransProductSerial", param, out ds, out err);
                                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                                {
                                    err = err + tps.SerialNumber + ", ";
                                    chk = true;
                                }
                            }

                            if (chk)
                                err = "ไม่พบ S/N หมายเลข : " + err.Substring(0, err.Length - 2);
                        }
                    }
                }

                if (string.IsNullOrEmpty(err))
                    conn.Commit();
                else
                    conn.RollBack();
                
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }

        public List<StockHeader> GetStockHeader(DateTime DateFrom, DateTime DateTo)
        {
            List<StockHeader> lst = new List<StockHeader>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "DateFrom", Value = DateFrom });
                param.Add(new SqlParameter() { ParameterName = "DateTo", Value = DateTo });
                DataSet ds = conn.GetDataSet("GetStockHeader", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    StockHeader o = new StockHeader();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new StockHeader();
                        o.StockHeaderID = Convert.ToInt32(dr["StockHeaderID"].ToString());
                        o.StockTime = Convert.ToDateTime(dr["StockTime"].ToString());
                        o.StockType = dr["StockType"].ToString();
                        o.CreatedBy = dr["CreatedBy"].ToString();
                        o.StockFrom = dr["StockFrom"].ToString(); 
                        o.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public List<StockDetailDTO> GetStockDetail(Int32 HeaderID)
        {
            List<StockDetailDTO> lst = new List<StockDetailDTO>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "StockHeaderID", Value = HeaderID });
                DataSet ds = conn.GetDataSet("GetStockDetail", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    StockDetailDTO o = new StockDetailDTO();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new StockDetailDTO();
                        o.StockHeaderID = Convert.ToInt32(dr["StockHeaderID"].ToString());
                        o.StockDetailID = Convert.ToInt32(dr["StockDetailID"].ToString());
                        o.ProductID = Convert.ToInt32(dr["ProductID"].ToString());
                        o.Amount = Convert.ToInt32(dr["Amount"].ToString());
                        o.ProductCode = dr["ProductCode"].ToString();
                        o.ProductName = dr["ProductName"].ToString();
                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }
        #endregion

        #region Stock HeadQ
        public string InsertStockHeadQ(StockHeader header, string User, ref string result, ref List<string> errSN)
        {
            string err = "";
            try
            {
                result = "";
                conn.BeginTransaction();
                DataSet ds = new DataSet();
                Int32 HeaderID = 0;
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "StockType", Value = header.StockType });
                param.Add(new SqlParameter() { ParameterName = "StockTime", Value = header.StockTime });
                param.Add(new SqlParameter() { ParameterName = "Remark", Value = header.Remark });
                param.Add(new SqlParameter() { ParameterName = "CreatedBy", Value = User });
                conn.CallStoredProcedure("InsStockHeadQHeader", param, out ds, out err);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && string.IsNullOrEmpty(err) && header.detail != null && header.detail.Count > 0)
                {
                    HeaderID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    string cal = "";
                    switch (header.StockType)
                    {
                        case "IN":
                            cal = "+";
                            break;
                        case "OUT":
                            cal = "-";
                            break;
                        case "TRANSFER":
                            cal = "+-";
                            break;
                        default:
                            cal = "";
                            break;
                    }
                    //header.StockType == "IN" ? "+" : "-";
                    foreach (StockDetail item in header.detail)
                    {
                        param = new List<SqlParameter>();
                        param.Add(new SqlParameter() { ParameterName = "StockHeaderID", Value = HeaderID });
                        param.Add(new SqlParameter() { ParameterName = "ProductID", Value = item.ProductID, DbType = DbType.Int32 });
                        param.Add(new SqlParameter() { ParameterName = "Amount", Value = item.Amount, DbType = DbType.Int32 });
                        param.Add(new SqlParameter() { ParameterName = "CAL", Value = cal });
                        param.Add(new SqlParameter() { ParameterName = "SerialNumber", Value = item.SNConcat });
                        conn.CallStoredProcedure("InsStockHeadQDetail", param, out err);
                        
                        if (item.lstSerial != null && item.lstSerial.Count > 0)
                        {
                            foreach (TransProductSerial tps in item.lstSerial)
                            {
                                ds = new DataSet();
                                param = new List<SqlParameter>();
                                param.Add(new SqlParameter() { ParameterName = "ProductID", Value = item.ProductID, DbType = DbType.Int32 });
                                param.Add(new SqlParameter() { ParameterName = "SerialNumber", Value = tps.SerialNumber });
                                param.Add(new SqlParameter() { ParameterName = "CAL", Value = cal });
                                conn.CallStoredProcedure("InsTransProductSerial", param, out ds, out err);
                                if (!string.IsNullOrEmpty(err))
                                {
                                    errSN.Add(err);
                                    break;
                                }
                                
                                if(ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                                {
                                    result = result + "Product Name : " + item.ProductName + " --> " + ds.Tables[0].Rows[0][0].ToString() + "\\r";
                                }
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(err) && string.IsNullOrEmpty(result))
                    conn.Commit();
                else
                    conn.RollBack();

            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }

        public List<StockHeader> GetStockHeaderHeadQ(DateTime DateFrom, DateTime DateTo)
        {
            List<StockHeader> lst = new List<StockHeader>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "DateFrom", Value = DateFrom });
                param.Add(new SqlParameter() { ParameterName = "DateTo", Value = DateTo });
                DataSet ds = conn.GetDataSet("GetStockHeaderHeadQ", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    StockHeader o = new StockHeader();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new StockHeader();
                        o.StockHeaderID = Convert.ToInt32(dr["StockHeadQHeaderID"].ToString());
                        o.StockTime = Convert.ToDateTime(dr["StockTime"].ToString());
                        o.StockType = dr["StockType"].ToString();
                        o.CreatedBy = dr["CreatedBy"].ToString();
                        o.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public List<StockDetailDTO> GetStockDetailHeadQ(Int32 HeaderID)
        {
            List<StockDetailDTO> lst = new List<StockDetailDTO>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "StockHeaderID", Value = HeaderID });
                DataSet ds = conn.GetDataSet("GetStockDetailHeadQ", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    StockDetailDTO o = new StockDetailDTO();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new StockDetailDTO();
                        o.StockHeaderID = Convert.ToInt32(dr["StockHeadQHeaderID"].ToString());
                        o.StockDetailID = Convert.ToInt32(dr["StockHeadQDetailID"].ToString());
                        o.ProductID = Convert.ToInt32(dr["ProductID"].ToString());
                        o.Amount = Convert.ToInt32(dr["Amount"].ToString());
                        o.ProductCode = dr["ProductCode"].ToString();
                        o.ProductName = dr["ProductName"].ToString();
                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }
        #endregion

        public string UpdateSaleCancel(Int32 HeaderID, string User)
        {
            string err = "";
            try
            {
                DataSet ds = new DataSet();
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "SaleHeaderID", Value = HeaderID });
                param.Add(new SqlParameter() { ParameterName = "UpdatedBy", Value = User });
                conn.ExcuteNonQueryNClose("UpdTransSaleCancel", param, out err);
                
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }

        
    }
}
