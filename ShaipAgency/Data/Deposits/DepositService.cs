using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ShaipAgency.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ShaipAgency.Data.Deposits
{


    public class DepositService
    {

        string _dbConnectionString;        

        enum Code{
            Deposit = 10,
            Withdraw = 11
        }
        public DepositService(IConfiguration configuration)
        {
            _dbConnectionString = configuration[configuration["DbMode"]];
            
        }
        public async Task<string> AddRequestAsync(Dictionary<string, object> newRow)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {

                    await connection.OpenAsync();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_REQ_DEPOSIT_MOD_01";

                        {
                            SqlParameter[] sqlParameters = new SqlParameter[newRow.Count()];
                            command.Parameters.Add(new SqlParameter("@RequestCode", SqlDbType.Char));
                            command.Parameters.Add(new SqlParameter("@DepositDate", SqlDbType.DateTime));
                            command.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int));
                            command.Parameters.Add(new SqlParameter("@PersonName", SqlDbType.NVarChar));
                            command.Parameters.Add(new SqlParameter("@AccountNumber", SqlDbType.NVarChar));
                            command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int));

                            var i = 0;
                            for (; i < newRow.Count(); i++)
                            {
                                command.Parameters[i].SqlValue = newRow.ElementAt(i).Value;
                            }

                            // EventCode "10" = 의뢰등록
                            command.Parameters.Add(new SqlParameter("@EventCode", SqlDbType.Char));
                            command.Parameters[i].SqlValue = "10";
                            // UserId string값으로 받아와서 여기서 int로 넘기는거 오류 없는지 확인 
                        }

                        using (SqlDataReader sqlDataReader = await command.ExecuteReaderAsync())
                        {
                            if (await sqlDataReader.ReadAsync())
                                return sqlDataReader.GetString(0);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                // 에러 처리 어떻게 하지?
                System.Console.WriteLine(e.Message);                
            }

            return string.Empty;
        }
    }
}
