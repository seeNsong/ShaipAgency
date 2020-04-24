using ShaipAgency.Model.TestModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;         


namespace ShaipAgency.Data.Test
{
    public class TestModelServiceUsingEF : ITestModelService
    {
        private readonly ApplicationDbContext _context;

        public TestModelServiceUsingEF(ApplicationDbContext context)
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
                    string query = "SELECT ApplyNo, ShaipName, Charge, CDateTime From UserPassbook WITH(NOLOCK)";
                    command.CommandText = query;

                    DbDataReader dataReader = await command.ExecuteReaderAsync();

                    if (dataReader.HasRows)
                    {
                        while (await dataReader.ReadAsync())
                        {
                            var row = new TestModel { ApplyNo = dataReader.GetString(0), ShaipName = dataReader.GetString(1), Charge = dataReader.GetInt32(2), CDateTime = dataReader.GetDateTime(3) };
                            testModels.Add(row);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {

                conn.Close();
                //conn.Dispose();
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
                    string query = "UserPassbook_Sel_01";
                    var param = new SqlParameter("@ShaipName", SqlDbType.NVarChar);
                    param.Value = "김성민";

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = query;
                    command.Parameters.Add(param);                   
                    

                    DbDataReader dataReader = await command.ExecuteReaderAsync();
                                       

                    if (dataReader.HasRows)
                    {
                        while (await dataReader.ReadAsync())
                        {
                            var row = new TestModel { ApplyNo = dataReader.GetString(0), ShaipName = dataReader.GetString(1),  Charge = dataReader.GetInt32(2), CDateTime = dataReader.GetDateTime(3) };
                            testModels.Add(row);
                        }
                    }
                    dataReader.Dispose();
                }
            }
            finally
            {
                conn2.Close();
                //conn2.Dispose();          // EF는 

            }
            return testModels;
        }

    }
}
