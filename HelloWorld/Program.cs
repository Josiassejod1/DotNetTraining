using System;
using System.Data;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HelloWorld
{
  internal class Program
  {
    static void Main(string[] args)
    {

      IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

      DataContextDapper dapper = new DataContextDapper(config);

      string json = File.ReadAllText("Computers.json");

      Mapper mapper = new Mapper(new MapperConfiguration((cfg) =>
      {

      }));

      IEnumerable<Computer>? computerNewSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(json);

      if (computerNewSoft != null)
      {
        foreach (var computer in computerNewSoft)
        {
          string sql = @"INSERT INTO TutorialAppSchema.Computer (
          Motherboard,
          HasWifi,
          HasLTE,
          ReleaseDate,
          Price,
          VideoCard
        ) VALUES ('" + escapeSingleQuote(computer.Motherboard)
  + "','" + computer.HasWifi
  + "','" + computer.HasLTE
  + "','" + computer.ReleaseDate
  + "','" + computer.Price
  + "','" + escapeSingleQuote(computer.VideoCard) + "') \n";

          dapper.ExecuteSql(sql);
        }
      }

    }

    static public string escapeSingleQuote(string input)
    {
      return input.Replace("'", "''");
    }
  }


}
