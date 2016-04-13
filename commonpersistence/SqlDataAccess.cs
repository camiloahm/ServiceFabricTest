using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace commonpersistence
{
    public class SqlDataAccess
    {

        const string CONNECTION_STRING =
            "Data Source=.\\SQL2012;Initial Catalog=ServiceFabric;Persist Security Info=True;User ID=sa;Password=Bizagi10GO";

        public IEnumerable<T> TableQuery<T>(string sql, dynamic param = null)
        {
            var result = Enumerable.Empty<T>();
            var connectionString =CONNECTION_STRING;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    result = connection.Query<T>(sql, param as object);
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                throw;
            }

            return result;
        }


        public void Execute(string sql, dynamic param = null)
        {

            var connectionString = CONNECTION_STRING;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute(sql, param as object);
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                throw;
            }
        }

        public string ExecuteEscalarStoreProcedure(string sql, dynamic param = null)
        {

            var connectionString = CONNECTION_STRING;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = sql;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    if (param != null)
                        command.Parameters.AddRange(param);
                    return Convert.ToString(command.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                throw;
            }
        }



    }
}