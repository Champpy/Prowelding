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
    public class LogsDal //: BaseDal
    {
        #region Init Dal

        private BaseDal conn;

        #region " | Instance | "

        private static volatile LogsDal _instance;

        private LogsDal()
        {
            conn = new BaseDal();
        }

        public static LogsDal Instance
        {
            get
            {
                _instance = new LogsDal();
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
        ~LogsDal()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion | Dispose |

        #endregion

        public void InsertLogs(string Desc, string Type)
        {
            string err = "";
            try
            {
                List<SqlParameter> paramI = new List<SqlParameter>();
                paramI.Add(new SqlParameter() { ParameterName = "LogType", Value = Type });
                paramI.Add(new SqlParameter() { ParameterName = "Description", Value = Desc });

                conn.ExcuteNonQueryNClose("InsLogs", paramI, out err);
            }
            catch (Exception ex)
            {

            }
        }
        
    }
}
