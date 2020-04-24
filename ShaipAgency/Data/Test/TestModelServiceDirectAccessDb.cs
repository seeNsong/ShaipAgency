using ShaipAgency.Model.TestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ShaipAgency.Data.Test
{
    public class TestModelServiceDirectAccessDb
    {
        IConfiguration _configuration;

        public TestModelServiceDirectAccessDb(IConfiguration configurtion)
        {
            _configuration = configurtion;
        }

        public async Task<List<TestModel>> GetTestModelsByQueryAsync()
        {
            List<TestModel> testModels = new List<TestModel>();

            try
            {
                using ( SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("LocalDbConnection")))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SELECT ApplyNo, ShaipName, Charge, CDateTime From UserPassbook WITH(NOLOCK)", conn))
                    {
                        using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                        {
                            while (await dataReader.ReadAsync())
                            {
                                var row = new TestModel { ApplyNo = dataReader.GetString(0), ShaipName = dataReader.GetString(1), Charge = dataReader.GetInt32(2), CDateTime = dataReader.GetDateTime(3) };
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

        public async Task<List<TestModel>> GetTestModelsBySPAsync()
        {
            List<TestModel> testModels = new List<TestModel>();


            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("LocalDBConnection")))
                {
                    await conn.OpenAsync();

                    using (SqlCommand command = new SqlCommand("",conn))
                    {

                        string query = "UserPassbook_Sel_01";
                        var param = new SqlParameter("@ShaipName", SqlDbType.NVarChar);
                        param.Value = "김성민";

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = query;
                        command.Parameters.Add(param);

                        using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                        {

                            while (await dataReader.ReadAsync())
                            {
                                testModels.Add(new TestModel { ApplyNo = dataReader.GetString(0), ShaipName = dataReader.GetString(1), Charge = dataReader.GetInt32(2), CDateTime = dataReader.GetDateTime(3) });
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return testModels;

        }


        public async void 

    }
}
