using ShaipAgency.Model.TestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace ShaipAgency.Data.Test
{
    public class TestModelServiceDirectAccessDb
    {        
        string _dbConnectionString;
        //IConfiguration _configuration;

        public TestModelServiceDirectAccessDb(IConfiguration configurtion)
        {
            //_configuration = configurtion;
            _dbConnectionString = configurtion[configurtion["DbMode"]];
            
        }

        public async Task<List<TestModel>> GetTestModelsByQueryAsync()
        {
            List<TestModel> testModels = new List<TestModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SELECT RequestNo, UserId, Charge, CDateTime From UserPassbook WITH(NOLOCK)", conn))
                    {
                        using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                        {
                            while (await dataReader.ReadAsync())
                            {
                                var row = new TestModel { RequestNo = dataReader.GetString(0), UserId = dataReader.GetInt32(1), Charge = dataReader.GetInt32(2), CDateTime = dataReader.GetDateTime(3) };
                                testModels.Add(row);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }


            return testModels;
        }

        public async Task<IEnumerable<TestModel>> GetTestModelsBySPAsync()
        {
            List<TestModel> testModels = new List<TestModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand command = new SqlCommand("", conn))
                    {

                        string query = "UserPassbook_Sel_01";
                        var param = new SqlParameter("@UserId", SqlDbType.Int);
                        param.Value = "1";

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = query;
                        command.Parameters.Add(param);

                        using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                        {

                            while (await dataReader.ReadAsync())
                            {
                                testModels.Add(new TestModel { RequestNo = dataReader.GetString(0), UserId = dataReader.GetInt32(1), ShaipName = dataReader.GetString(2), Charge = dataReader.GetInt32(3), CDateTime = dataReader.GetDateTime(4) });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return testModels.AsEnumerable();
        }

        // for DevExpress DataGrid
        public async Task<IEnumerable<TestModel>> GetTestModelsByQueryAsyncEnum(CancellationToken ct = default)
        {
            List<TestModel> testModels = new List<TestModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_dbConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SELECT RequestNo, UserId, Charge, CDateTime From UserPassbook WITH(NOLOCK)", conn))
                    {
                        using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                        {
                            while (await dataReader.ReadAsync())
                            {
                                var row = new TestModel { RequestNo = dataReader.GetString(0), UserId = dataReader.GetInt32(1), Charge = dataReader.GetInt32(2), CDateTime = dataReader.GetDateTime(3) };
                                testModels.Add(row);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return testModels.AsEnumerable();
        }

    }
}
