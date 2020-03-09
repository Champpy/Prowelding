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
    public class ProductDal //: BaseDal
    {
        #region Init Dal

        private BaseDal conn;

        #region " | Instance | "

        private static volatile ProductDal _instance;

        private ProductDal()
        {
            conn = new BaseDal();
        }

        public static ProductDal Instance
        {
            get
            {
                _instance = new ProductDal();
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
        ~ProductDal()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion | Dispose |

        #endregion

        public string InsUpdDelMasProduct(MasProduct item)
        {
            string err = "";
            try
            {
                List<SqlParameter> paramI = new List<SqlParameter>();
                paramI.Add(new SqlParameter() { ParameterName = "ProductID", Value = item.ProductID, DbType = DbType.Int32 });
                paramI.Add(new SqlParameter() { ParameterName = "ProductCode", Value = item.ProductCode });
                paramI.Add(new SqlParameter() { ParameterName = "ProductName", Value = item.ProductName });
                paramI.Add(new SqlParameter() { ParameterName = "PurchasePrice", Value = item.PurchasePrice, DbType = DbType.Double });
                paramI.Add(new SqlParameter() { ParameterName = "SellPrice", Value = item.SellPrice, DbType = DbType.Double });
                paramI.Add(new SqlParameter() { ParameterName = "Active", Value = item.Active, DbType = DbType.String });
                paramI.Add(new SqlParameter() { ParameterName = "CreatedBy", Value = item.CreatedBy, DbType = DbType.String });
                paramI.Add(new SqlParameter() { ParameterName = "UnitID", Value = item.UnitID, DbType = DbType.Int32 });
                paramI.Add(new SqlParameter() { ParameterName = "TypeID", Value = item.TypeID, DbType = DbType.Int32 });
                paramI.Add(new SqlParameter() { ParameterName = "Remaining", Value = item.Remaining, DbType = DbType.Int32 });
                paramI.Add(new SqlParameter() { ParameterName = "RemainingHeadQ", Value = item.RemainingHeadQ, DbType = DbType.Int32 });
                paramI.Add(new SqlParameter() { ParameterName = "ProductSN", Value = item.ProductSN, DbType = DbType.String });
                paramI.Add(new SqlParameter() { ParameterName = "DMLFlag", Value = item.DMLFlag });

                conn.ExcuteNonQueryNClose("InsUpdDelMasProduct", paramI, out err);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }

        public MasProduct GetSearchProductID(Int32 ProductID)
        {
            MasProduct item = new MasProduct();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "ProductID", Value = ProductID, DbType = DbType.Int32 });

                DataSet ds = conn.GetDataSet("GetProductByID", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        item.ProductID = Convert.ToInt32(dr["ProductID"].ToString());
                        item.ProductCode = dr["ProductCode"].ToString();
                        item.ProductName = dr["ProductName"].ToString();
                        item.Active = dr["Active"].ToString();
                        item.PurchasePrice = string.IsNullOrEmpty(dr["PurchasePrice"].ToString()) ? 0 : Convert.ToDouble(dr["PurchasePrice"].ToString());
                        item.SellPrice = string.IsNullOrEmpty(dr["SellPrice"].ToString()) ? 0 : Convert.ToDouble(dr["SellPrice"].ToString());
                        item.CreatedBy = dr["CreatedBy"].ToString();
                        item.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        item.UpdatedBy = dr["UpdatedBy"].ToString();
                        item.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());
                        item.UnitID = string.IsNullOrEmpty(dr["UnitID"].ToString()) ? 0 : Convert.ToInt32(dr["UnitID"].ToString());
                        item.TypeID = string.IsNullOrEmpty(dr["TypeID"].ToString()) ? 0 : Convert.ToInt32(dr["TypeID"].ToString());
                        item.ProductSN = dr["ProductSN"].ToString();

                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return item;
        }
        public MasProduct GetSearchProductCode(string ProductCode)
        {
            MasProduct item = new MasProduct();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "ProductCode", Value = ProductCode, DbType = DbType.String });

                DataSet ds = conn.GetDataSet("GetProductByCode", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        item.ProductID = Convert.ToInt32(dr["ProductID"].ToString());
                        item.ProductCode = dr["ProductCode"].ToString();
                        item.ProductName = dr["ProductName"].ToString();
                        item.Active = dr["Active"].ToString();
                        item.PurchasePrice = Convert.ToDouble(dr["PurchasePrice"].ToString());
                        item.SellPrice = Convert.ToDouble(dr["SellPrice"].ToString());
                        item.CreatedBy = dr["CreatedBy"].ToString();
                        item.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        item.UpdatedBy = dr["UpdatedBy"].ToString();
                        item.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());

                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return item;
        }

        public List<MasProduct> GetSearchProduct(MasProduct item)
        {
            List<MasProduct> lst = new List<MasProduct>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();

                param.Add(new SqlParameter() { ParameterName = "ProductCode", Value = item.ProductCode });
                param.Add(new SqlParameter() { ParameterName = "ProductName", Value = item.ProductName });

                DataSet ds = conn.GetDataSet("GetSearchProductAll", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    MasProduct o = new MasProduct();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new MasProduct();
                        o.ProductID = Convert.ToInt32(dr["ProductID"].ToString());
                        o.ProductCode = dr["ProductCode"].ToString();
                        o.ProductName = dr["ProductName"].ToString();
                        o.Remaining = string.IsNullOrEmpty(dr["Remaining"].ToString()) ? 0 : Convert.ToInt32(dr["Remaining"].ToString());
                        o.RemainingHeadQ = string.IsNullOrEmpty(dr["RemainingHeadQ"].ToString()) ? 0 : Convert.ToInt32(dr["RemainingHeadQ"].ToString());
                        o.Active = dr["Active"].ToString();
                        o.PurchasePrice = string.IsNullOrEmpty(dr["PurchasePrice"].ToString()) ? 0 : Convert.ToDouble(dr["PurchasePrice"].ToString());
                        o.SellPrice = string.IsNullOrEmpty(dr["SellPrice"].ToString()) ? 0 : Convert.ToDouble(dr["SellPrice"].ToString());
                        o.CreatedBy = dr["CreatedBy"].ToString();
                        o.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        o.UpdatedBy = dr["UpdatedBy"].ToString();
                        o.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());

                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public MasItemDTO GetSearchItemByID(Int32 ItemID)
        {
            MasItemDTO item = new MasItemDTO();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "ItemID", Value = ItemID, DbType = DbType.Int32 });
                DataSet ds = conn.GetDataSet("GetSearchItemByID", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        item = new MasItemDTO();
                        item.ItemID = Convert.ToInt32(dr["ItemID"].ToString());
                        item.ItemCode = dr["ItemCode"].ToString();
                        item.ItemName = dr["ItemName"].ToString();
                        item.ItemDesc = dr["ItemDesc"].ToString();
                        item.ItemPrice = Convert.ToDouble(dr["ItemPrice"].ToString());
                        item.UnitID = Convert.ToInt32(dr["UnitID"].ToString());
                        item.ItemTypeID = Convert.ToInt32(dr["ItemTypeID"].ToString());
                        item.DistributorID = Convert.ToInt32(dr["DistributorID"].ToString());
                        item.MinRemaining = Convert.ToInt32(dr["MinRemaining"].ToString());

                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return item;
        }

        public List<MasUnit> GetSearchUnit()
        {
            List<MasUnit> lst = new List<MasUnit>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                DataSet ds = conn.GetDataSet("GetSearchUnit", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    MasUnit o = new MasUnit();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new MasUnit();
                        o.UnitID = Convert.ToInt32(dr["UnitID"].ToString());
                        o.UnitCode = dr["UnitCode"].ToString();
                        o.UnitName = dr["UnitName"].ToString();
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

        public List<MasItemType> GetSearchItemType()
        {
            List<MasItemType> lst = new List<MasItemType>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                DataSet ds = conn.GetDataSet("GetSearchItemType", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    MasItemType o = new MasItemType();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new MasItemType();
                        o.ItemTypeID = Convert.ToInt32(dr["ItemTypeID"].ToString());
                        o.ItemTypeCode = dr["ItemTypeCode"].ToString();
                        o.ItemTypeName = dr["ItemTypeName"].ToString();
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

        public List<MasItemDTO> GetSearchItemDetailByID(Int32 ItemID)
        {
            List<MasItemDTO> lst = new List<MasItemDTO>();
            MasItemDTO item = new MasItemDTO();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "ItemID", Value = ItemID, DbType = DbType.Int32 });
                DataSet ds = conn.GetDataSet("GetSearchItemDetailByID", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        item = new MasItemDTO();
                        item.TID = Convert.ToInt32(dr["TID"].ToString());
                        item.ItemID = Convert.ToInt32(dr["ItemID"].ToString());
                        item.ItemDetail = dr["ItemDetail"].ToString();
                        item.DetailOrder = Convert.ToInt32(dr["DetailOrder"].ToString());
                        item.CanChange = dr["CanChange"].ToString();
                        item.ProductSN = dr["ProductSN"].ToString();
                        lst.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        public MasProduct ValidateProductData(string ProductCode, string ProductName, int ProductID)
        {
            MasProduct item = new MasProduct();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "ProductID", Value = ProductID, DbType = DbType.Int32 });
                param.Add(new SqlParameter() { ParameterName = "ProductCode", Value = ProductCode, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "ProductName", Value = ProductName, DbType = DbType.String });

                DataSet ds = conn.GetDataSet("ValidateProductData", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        item.CHK_PRODUCT_CODE = Convert.ToInt32(dr["CHK_PRODUCT_CODE"].ToString());
                        item.CHK_PRODUCT_NAME = Convert.ToInt32(dr["CHK_PRODUCT_NAME"].ToString());
                        item.CHK_CODE_NAME = Convert.ToInt32(dr["CHK_CODE_NAME"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return item;
        }

        #region Product HeadQ
        public List<MasProduct> GetSearchProductHeadQ(MasProduct item)
        {
            List<MasProduct> lst = new List<MasProduct>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();

                param.Add(new SqlParameter() { ParameterName = "ProductCode", Value = item.ProductCode });
                param.Add(new SqlParameter() { ParameterName = "ProductName", Value = item.ProductName });

                DataSet ds = conn.GetDataSet("GetSearchProductAll", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    MasProduct o = new MasProduct();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        o = new MasProduct();
                        o.ProductID = Convert.ToInt32(dr["ProductID"].ToString());
                        o.ProductCode = dr["ProductCode"].ToString();
                        o.ProductName = dr["ProductName"].ToString();
                        o.Remaining = Convert.ToInt32(dr["Remaining"].ToString());
                        o.RemainingHeadQ = Convert.ToInt32(dr["RemainingHeadQ"].ToString());
                        o.Active = dr["Active"].ToString();
                        o.PurchasePrice = Convert.ToDouble(dr["PurchasePrice"].ToString());
                        o.SellPrice = Convert.ToDouble(dr["SellPrice"].ToString());
                        o.CreatedBy = dr["CreatedBy"].ToString();
                        o.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        o.UpdatedBy = dr["UpdatedBy"].ToString();
                        o.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());

                        lst.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lst;
        }
        public string InsUpdDelMasProductHeadQ(MasProduct item)
        {
            string err = "";
            try
            {
                List<SqlParameter> paramI = new List<SqlParameter>();
                paramI.Add(new SqlParameter() { ParameterName = "ProductID", Value = item.ProductID, DbType = DbType.Int32 });
                paramI.Add(new SqlParameter() { ParameterName = "ProductCode", Value = item.ProductCode });
                paramI.Add(new SqlParameter() { ParameterName = "ProductName", Value = item.ProductName });
                paramI.Add(new SqlParameter() { ParameterName = "PurchasePrice", Value = item.PurchasePrice, DbType = DbType.Double });
                paramI.Add(new SqlParameter() { ParameterName = "SellPrice", Value = item.SellPrice, DbType = DbType.Double });
                paramI.Add(new SqlParameter() { ParameterName = "Active", Value = item.Active, DbType = DbType.String });
                paramI.Add(new SqlParameter() { ParameterName = "CreatedBy", Value = item.CreatedBy, DbType = DbType.String });
                paramI.Add(new SqlParameter() { ParameterName = "UnitID", Value = item.UnitID, DbType = DbType.Int32 });
                paramI.Add(new SqlParameter() { ParameterName = "TypeID", Value = item.TypeID, DbType = DbType.Int32 });
                paramI.Add(new SqlParameter() { ParameterName = "Remaining", Value = item.Remaining, DbType = DbType.Int32 });
                paramI.Add(new SqlParameter() { ParameterName = "DMLFlag", Value = item.DMLFlag });

                conn.ExcuteNonQueryNClose("InsUpdDelMasProductHeadQ", paramI, out err);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }
        #endregion
    }
}
