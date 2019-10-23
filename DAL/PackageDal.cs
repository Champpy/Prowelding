using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PackageDal
    {
        #region Init Dal

        private BaseDal conn;

        #region " | Instance | "

        private static volatile PackageDal _instance;

        private PackageDal()
        {
            conn = new BaseDal();
        }

        public static PackageDal Instance
        {
            get
            {
                _instance = new PackageDal();
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
        ~PackageDal()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion | Dispose |

        #endregion

        public string InsUpdDelMasPackageHeader(MasPackageHeader item)
        {
            string err = "";
            try
            {
                List<SqlParameter> paramI = new List<SqlParameter>();
                paramI.Add(new SqlParameter() { ParameterName = "PackageCode", Value = item.PackageCode });
                paramI.Add(new SqlParameter() { ParameterName = "PackageName", Value = item.PackageName });
                paramI.Add(new SqlParameter() { ParameterName = "SellPrice", Value = item.SellPrice, DbType = DbType.Double });
                paramI.Add(new SqlParameter() { ParameterName = "Active", Value = item.Active, DbType = DbType.String });
                paramI.Add(new SqlParameter() { ParameterName = "CreatedBy", Value = item.CreatedBy, DbType = DbType.String });
                paramI.Add(new SqlParameter() { ParameterName = "DMLFlag", Value = item.DMLFlag });
                conn.ExcuteNonQueryNClose("InsUpdDelMasPackageHeader", paramI, out err);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }


        public string InsUpdDelMasPackageDetail(List<MasProduct> item, string PackageCode,string Model = "I")
        {
            string err = "";
            try
            {
                MasPackageHeader DataMasPackageHeader = GetSearchMasPackageHeaderByID(PackageCode);


                if (Model == "D")
                {
                    List<SqlParameter> paramI = new List<SqlParameter>();
                    paramI.Add(new SqlParameter() { ParameterName = "PackageHeaderID", Value = DataMasPackageHeader.PackageHeaderID });
                    paramI.Add(new SqlParameter() { ParameterName = "DMLFlag", Value = Model });
                    conn.ExcuteNonQueryNClose("InsUpdDelMasPackageDetail", paramI, out err);
                    return err;
                }


                int rowOrder = 1;
                foreach (MasProduct Data in item)
                {
                    List<SqlParameter> paramI = new List<SqlParameter>();
                    paramI.Add(new SqlParameter() { ParameterName = "PackageHeaderID", Value = DataMasPackageHeader.PackageHeaderID });
                    paramI.Add(new SqlParameter() { ParameterName = "ProductID", Value = Data.ProductID });
                    paramI.Add(new SqlParameter() { ParameterName = "Amount", Value = Data.Amount, DbType = DbType.Int32 });
                    paramI.Add(new SqlParameter() { ParameterName = "OrderNo", Value = rowOrder, DbType = DbType.Int32 });
                    paramI.Add(new SqlParameter() { ParameterName = "Active", Value = Data.Active, DbType = DbType.String });
                    paramI.Add(new SqlParameter() { ParameterName = "CreatedBy", Value = Data.CreatedBy, DbType = DbType.String });
                    paramI.Add(new SqlParameter() { ParameterName = "DMLFlag", Value = Data.DMLFlag });
                    paramI.Add(new SqlParameter() { ParameterName = "CanChange", Value = Data.CanChange == "Change" ? "Y" : "N" });
                    conn.ExcuteNonQueryNClose("InsUpdDelMasPackageDetail", paramI, out err);

                    rowOrder++;
                }


            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }


        public MasPackageHeader GetSearchMasPackageHeaderByID(string PackageCode)
        {
            MasPackageHeader item = new MasPackageHeader();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "PackageCode", Value = PackageCode, DbType = DbType.String });

                DataSet ds = conn.GetDataSet("GetMasPackageHeaderByID", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        item.PackageHeaderID = Convert.ToInt32(dr["PackageHeaderID"].ToString());
                        item.PackageCode = dr["PackageCode"].ToString();
                        item.PackageName = dr["PackageName"].ToString();
                        item.Active = dr["Active"].ToString();
                        item.PurchasePrice = !dr.IsNull("PurchasePrice") ? Convert.ToDouble(dr["PurchasePrice"].ToString()) : 0;
                        item.SellPrice = Convert.ToDouble(dr["SellPrice"].ToString());
                        item.CreatedBy = dr["CreatedBy"].ToString();
                        item.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        item.UpdatedBy = dr["UpdatedBy"].ToString();
                        item.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());
                        item.DMLFlag = "U";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return item;
        }

        public List<MasPackageHeader> GetSearchPackageHeader(MasPackageHeader ModelSer)
        {
            MasPackageHeader item = new MasPackageHeader();
            List<MasPackageHeader> lstData = new List<MasPackageHeader>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "PackageCode", Value = ModelSer.PackageCode, DbType = DbType.String });
                param.Add(new SqlParameter() { ParameterName = "PackageName", Value = ModelSer.PackageName, DbType = DbType.String });

                DataSet ds = conn.GetDataSet("GetSearchPackageHeader", param);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        item = new MasPackageHeader();
                        item.PackageHeaderID = Convert.ToInt32(dr["PackageHeaderID"].ToString());
                        item.PackageCode = dr["PackageCode"].ToString();
                        item.PackageName = dr["PackageName"].ToString();
                        item.Active = dr["Active"].ToString();
                        item.SellPrice = Convert.ToDouble(dr["SellPrice"].ToString());
                        item.Remaining = Convert.ToInt32(dr["Remaining"].ToString());
                        //item.CreatedBy = dr["CreatedBy"].ToString();
                        //item.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        //item.UpdatedBy = dr["UpdatedBy"].ToString();
                        //item.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());

                        lstData.Add(item);

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lstData;
        }

        public List<MasProduct> GetSearchPackageDetail(MasPackageHeader ModelSer)
        {
            MasProduct item = new MasProduct();
            List<MasProduct> lstData = new List<MasProduct>();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "PackageHeaderID", Value = ModelSer.PackageHeaderID, DbType = DbType.String });

                DataSet ds = conn.GetDataSet("GetSearchPackageDetail", param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        item = new MasProduct();
                        item.ProductID = Convert.ToInt32(dr["ProductID"].ToString());
                        item.ProductCode = dr["ProductCode"].ToString();
                        item.ProductName = dr["ProductName"].ToString();
                        item.Amount = Convert.ToInt32(dr["Amount"].ToString());
                        item.Active = dr["Active"].ToString();
                        item.SellPrice = Convert.ToDouble(dr["SellPrice"].ToString());
                        item.CreatedBy = dr["CreatedBy"].ToString();
                        item.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        item.UpdatedBy = dr["UpdatedBy"].ToString();
                        item.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"].ToString());
                        item.CanChange = dr["CanChange"].ToString() == "Y" ? "Change" : "Fix";
                        item.DMLFlag = "I";
                        lstData.Add(item);

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lstData;
        }
    }
}
