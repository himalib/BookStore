
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace AuthnAuthz.Infrastructure
{
    public class ConnectionFactory
    {
        public IDbConnection GetConnection
        {
            get
            {
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var conn = factory.CreateConnection();

                string serverName = ".", dbName = "BookStore";
                HttpContext context = HttpContext.Current;


                string connectionString = @"Data Source=" + serverName + ";Initial Catalog=" + dbName + ";Integrated Security='true' " ;
                conn.ConnectionString = connectionString;
                conn.Open();
                return conn;
            }
        }

        //public SqlConnection GetRawConnection
        //{
        //    get
        //    {
        //        string theConnection = ConfigurationManager.ConnectionStrings["authCon"].ConnectionString as string;
        //        return new SqlConnection(theConnection);
        //    }
        //}

        #region IDisposable Support
        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}