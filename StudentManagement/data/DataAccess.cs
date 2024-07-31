
namespace ReadDB.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;


public class DataAccess
{
    private readonly string _connectionString;
    public DataAccess(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public List<YourModel> GetData()
    {
        List<YourModel> dataList = new List<YourModel>();
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            conn.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT Id,Name,Age FROM Students", conn))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        YourModel data = new YourModel
                        {
                            
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Age = reader.GetInt32(reader.GetOrdinal("Age"))
                           
                        };
                        dataList.Add(data);
                    }
                }
            }
        }
        return dataList;
    }
}
