using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShaipAgency.Model.Standards;

namespace ShaipAgency.Data.Standards
{    
    /* 복붙할 경우 다음 세 가지 변경할 것
     * 1. Constructor의 이름
     * 2. DbSet의 이름
     * 3. Model의 이름 
     * 4. using EntityFrameworkCore/Model.Standards/Threading
     * */
    public class RequestStatusRouteInfoService
    {
        private readonly ApplicationDbContext _context;

        public RequestStatusRouteInfoService(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        ///  Create
        /// </summary>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public async Task<int> Add(IDictionary<string, object> newRow)
        {
            var newData = new StdRequestStatusRouteInfoModel()
            {

                RequestCode = newRow["RequestCode"].ToString(),
                EventCode = newRow["EventCode"].ToString(),
                NextStatusCode = newRow["NextStatusCode"].ToString(),
                CreationUserId = int.Parse(newRow["CreationUserId"].ToString()),
                CreationDateTime = DateTime.Parse(newRow["CreationDateTime"].ToString())
            };

            _context.TB_STD_REQUEST_STATUS_ROUTE_INFO.Add(newData);
            return (await _context.SaveChangesAsync());
        }


        /// <summary>
        ///  Read
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StdRequestStatusRouteInfoModel>> ToList(CancellationToken ct = default)
        {
            var List = await _context.TB_STD_REQUEST_STATUS_ROUTE_INFO.ToListAsync();
            return List.AsEnumerable();
        }

        /// <summary>
        ///  Update는 targetRow의 Key는 변경하지 않음
        /// </summary>
        /// <param name="targetRow"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public async Task<int> Update(StdRequestStatusRouteInfoModel targetRow, IDictionary<string, object> newRow)
        {
            // Key는 Update하지 않음
            foreach (var column in newRow)
            {
                if(column.Key.Equals("NextStatusCode"))
                    targetRow.NextStatusCode = newRow["NextStatusCode"].ToString();
                else if (column.Key.Equals("CreationUserId"))
                    targetRow.CreationUserId = int.Parse(newRow["CreationUserId"].ToString());
                else if (column.Key.Equals("CreationDateTime"))
                    targetRow.CreationDateTime = DateTime.Parse(newRow["CreationDateTime"].ToString());
            }
            _context.TB_STD_REQUEST_STATUS_ROUTE_INFO.Update(targetRow);
            return (await _context.SaveChangesAsync());
        }

        /// <summary>
        ///  Delete
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns></returns>
        public async Task<int> Remove(StdRequestStatusRouteInfoModel targetRow)
        {
            _context.Remove(targetRow);            
            return await _context.SaveChangesAsync();
        }


    }
}
