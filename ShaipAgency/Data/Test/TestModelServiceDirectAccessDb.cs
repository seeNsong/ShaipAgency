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
    public class TestModelServiceDirectAccessDb : ITestModelService
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
                using ( SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ProjectConnection")))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SELECT ApplyNo, Charge, ShaipName, DateTime From UserPassbook WITH(NOLOCK)", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var row = new TestModel { ApplyNo = reader.GetString(0), Charge = reader.GetInt32(1), ShaipName = reader.GetString(2), DateTime = reader.GetDateTime(3) };
                                testModels.Add(row);
                            }
                        }
                    }
                    await conn.CloseAsync();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            

            return testModels;
        }

        public Task<List<TestModel>> GetTestModelsBySPAsync()
        {
            throw new NotImplementedException();
        }
    }
}
