using ShaipAgency.Model.TestModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ShaipAgency.Data.Test
{
    public class TestModelRepository : ITestModelRepository
    {
        private readonly ApplicationDbContext _context;

        public TestModelRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<List<TestModel>> GetTestModelsByQueryAsync()
        {

            List<TestModel> testModels = new List<TestModel>();
            var conn = _context.Database.GetDbConnection();

            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {                                        
                    string query = "SELECT ApplyNo, Charge, DateTime, ShaipName From UserPassbook WITH(NOLOCK)";
                    command.CommandText = query;

                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new TestModel { ApplyNo = reader.GetString(0), Charge = reader.GetInt32(1), DateTime = reader.GetDateTime(2), ShaipName=reader.GetString(3) };
                            testModels.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return testModels;            
        }

        public async Task<List<TestModel>> GetTestModelsBySPAsync()
        {

            List<TestModel> testModels = new List<TestModel>();
            var conn2 = _context.Database.GetDbConnection();

            try
            {
                await conn2.OpenAsync();
                using (var command = conn2.CreateCommand())
                {
                    string query = "User_Passbook_Sel_01";
                    var param = new SqlParameter("@ShaipName", SqlDbType.NVarChar);
                    param.Value = "김성민";

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = query;
                    command.Parameters.Add(param);                   
                    

                    DbDataReader reader = await command.ExecuteReaderAsync();
                                       

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new TestModel { ApplyNo = reader.GetString(0), Charge = reader.GetInt32(1), DateTime = reader.GetDateTime(2), ShaipName = reader.GetString(3) };
                            testModels.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn2.Close();
                conn2.Dispose();
            }
            return testModels;
        }

    }
}
