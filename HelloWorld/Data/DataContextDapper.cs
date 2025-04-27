using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
    public class DataContextDapper : IDisposable
    {
        private readonly string _connectionString;
        private readonly IDbConnection _dbConnection;

        private IConfiguration? _config;


        public DataContextDapper(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default")
           ?? throw new ArgumentNullException(nameof(config), "Connection string 'default' not found.");
            _config = config;
            _dbConnection = new SqlConnection(_connectionString);
            _dbConnection.Open();
        }

        public IEnumerable<T> LoadData<T>(string sqlQuery)
        {
            try
            {
                return _dbConnection.Query<T>(sqlQuery);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<T>();
        }

        public T? LoadDataSingle<T>(string sqlQuery)
        {
            try
            {
                return _dbConnection.QuerySingle<T>(sqlQuery);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return default;
        }

        public bool ExecuteSql(string sqlQuery, object parameters = null)
        {
            try
            {
                return _dbConnection.Execute(sqlQuery, parameters) > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }
        public int ExecuteSqlWithCount(string sqlQuery, object parameters = null)
        {
            try
            {
                return _dbConnection.Execute(sqlQuery, parameters);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return 0;
        }

        public void Dispose()
        {
            if (_dbConnection.State != ConnectionState.Closed)
            {
                _dbConnection.Close();
            }
            _dbConnection.Dispose();
        }
    }
}
