using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
  public class DataContextDapper
  {

    private string _connectionString;
    public DataContextDapper(IConfiguration config)
    {
      _connectionString = config.GetConnectionString("DefaultConnection");
    }


    public IEnumerable<T> LoadData<T>(string sql)
    {
      IDbConnection dbConnection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
      return dbConnection.Query<T>(sql);
    }

    public T LoadDataSingle<T>(string sql)
    {
      IDbConnection dbConnection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
      return dbConnection.QuerySingle<T>(sql);
    }

    public bool ExecuteSql(string sql)
    {
      IDbConnection dbConnection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
      return (dbConnection.Execute(sql) > 0);
    }

    public int ExecuteSqlWithRowCount(string sql)
    {
      IDbConnection dbConnection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
      return dbConnection.Execute(sql);
    }
  }
}
