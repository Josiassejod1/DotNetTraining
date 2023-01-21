
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DotnetAPI.Data
{
  class DataContextDapper
  {
    private readonly IConfiguration _config;

    public DataContextDapper(IConfiguration config)
    {
      _config = config;
    }

    public IEnumerable<T> LoadData<T>(string sql)
    {
      IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.Query<T>(sql);
    }

    public bool Execute(string sql)
    {
      IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
      return (dbConnection.Execute(sql) > 0);
    }

    public int ExecuteWithRows<T>(string sql)
    {
      IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.Execute(sql);
    }

    public T LoadDataSingle<T>(string sql)
    {
      IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
      return dbConnection.QuerySingle<T>(sql);
    }
  }
}
