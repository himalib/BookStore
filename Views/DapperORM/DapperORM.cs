using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AuthnAuthz.Model
{
    public class DapperORM
    {
        private static string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static void ExecuteWithoutReturn(string procedureName, DynamicParameters param)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
        //DapperORM.ExecuteReturnScalar<int>(_,_);
        public static T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.ExecuteScalar(procedureName, param, commandType: CommandType.StoredProcedure), typeof(T));
            }

        }

        //DapperORM.ReturnList<EmployeeModel> <=  IEnumerable<EmployeeModel>
        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }

        }

    }
}