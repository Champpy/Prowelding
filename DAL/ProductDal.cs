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

        public string InsertMasProduct(MasProduct item)
        {
            string err = "";
            try
            {
                List<SqlParameter> paramI = new List<SqlParameter>();
                //paramI.Add(new SqlParameter() { ParameterName = "ItemCode", Value = item.ItemCode });
                //paramI.Add(new SqlParameter() { ParameterName = "ItemName", Value = item.ItemName });
                //paramI.Add(new SqlParameter() { ParameterName = "ItemDesc", Value = item.ItemDesc });
                //paramI.Add(new SqlParameter() { ParameterName = "ItemPrice", Value = item.ItemPrice, DbType = DbType.Double });
                //paramI.Add(new SqlParameter() { ParameterName = "UnitID", Value = item.UnitID, DbType = DbType.Int32 });
                //paramI.Add(new SqlParameter() { ParameterName = "ItemTypeID", Value = item.ItemTypeID, DbType = DbType.Int32 });
                //paramI.Add(new SqlParameter() { ParameterName = "DistID", Value = item.UnitID, DbType = DbType.Int32 });
                //paramI.Add(new SqlParameter() { ParameterName = "MinRemaining", Value = item.MinRemaining, DbType = DbType.Int32 });
                //paramI.Add(new SqlParameter() { ParameterName = "User", Value = item.CreatedBy });
                //conn.ExcuteNonQueryNClose("InsertMasItem", paramI, out err);
                
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }

        public string UpdateMasProduct(MasProduct item)
        {
            string err = "";
            try
            {
                List<SqlParameter> paramI = new List<SqlParameter>();
                //paramI.Add(new SqlParameter() { ParameterName = "ItemID", Value = item.ItemID, DbType = DbType.Int32 });
                //paramI.Add(new SqlParameter() { ParameterName = "ItemCode", Value = item.ItemCode });
                //paramI.Add(new SqlParameter() { ParameterName = "ItemName", Value = item.ItemName });
                //paramI.Add(new SqlParameter() { ParameterName = "ItemDesc", Value = item.ItemDesc });
                //paramI.Add(new SqlParameter() { ParameterName = "ItemPrice", Value = item.ItemPrice, DbType = DbType.Double });
                //paramI.Add(new SqlParameter() { ParameterName = "UnitID", Value = item.UnitID, DbType = DbType.Int32 });
                //paramI.Add(new SqlParameter() { ParameterName = "ItemTypeID", Value = item.ItemTypeID, DbType = DbType.Int32 });
                //paramI.Add(new SqlParameter() { ParameterName = "DistID", Value = item.UnitID, DbType = DbType.Int32 });
                //paramI.Add(new SqlParameter() { ParameterName = "MinRemaining", Value = item.MinRemaining, DbType = DbType.Int32 });
                //paramI.Add(new SqlParameter() { ParameterName = "User", Value = item.UpdatedBy });
                //conn.ExcuteNonQueryNClose("UpdateMasItem", paramI, out err);

            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }

        public string DeleteMasProduct(MasProduct item)
        {
            string err = "";
            try
            {
                List<SqlParameter> paramI = new List<SqlParameter>();
                //paramI.Add(new SqlParameter() { ParameterName = "ItemID", Value = item.ItemID, DbType = DbType.Int32 });
                //paramI.Add(new SqlParameter() { ParameterName = "User", Value = item.UpdatedBy });
                //conn.ExcuteNonQueryNClose("DeleteMasItem", paramI, out err);

            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }

        public List<MasProduct> GetSearchProduct()
        {
            List<MasProduct> lst = new List<MasProduct>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                DataSet ds = conn.GetDataSet("GetSearchItem", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    MasProduct o = new MasProduct();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //o = new MasItemDTO();
                        //o.ItemID = Convert.ToInt32(dr["ItemID"].ToString());
                        //o.ItemCode = dr["ItemCode"].ToString();
                        //o.ItemName = dr["ItemName"].ToString();
                        //o.ItemDesc = dr["ItemDesc"].ToString();
                        //o.ItemPrice = Convert.ToDouble(dr["ItemPrice"].ToString());
                        //o.UnitName = dr["UnitName"].ToString();
                        //o.Active = dr["Active"].ToString();
                        //lst.Add(o);
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
                        lst.Add(item);
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
