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

    }
}
