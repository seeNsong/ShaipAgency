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
    public class RequestStatusCodeService
	{
        private readonly ApplicationDbContext _context;

        public RequestStatusCodeService(ApplicationDbContext context)
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
            var newData = new StdRequestStatusCodeModel()
            {
                RequestStatusCode = newRow["RequestStatusCode"].ToString(),
                RequestStatusName = newRow["RequestStatusName"].ToString(),
                UseYN = (bool)newRow["UseYN"],
                RequestAlertPriority = int.Parse(newRow["RequestAlertPriority"].ToString()),
                CreationUserId = int.Parse(newRow["CreationUserId"].ToString()),
                CreationDateTime = DateTime.Parse(newRow["CreationDateTime"].ToString())
            };

            _context.TB_STD_REQUEST_STATUS_CODE.Add(newData);
            return (await _context.SaveChangesAsync());
        }


        /// <summary>
        ///  Read
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StdRequestStatusCodeModel>> ToList(CancellationToken ct = default)
        {
            var List = await _context.TB_STD_REQUEST_STATUS_CODE.ToListAsync();
            return List.AsEnumerable();
        }

        /// <summary>
        ///  Update는 targetRow의 Key는 변경하지 않음
        /// </summary>
        /// <param name="targetRow"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public async Task<int> Update(StdRequestStatusCodeModel targetRow, IDictionary<string, object> newRow)
        {
            // Key는 Update하지 않음
            //targetRow.RequestStatusCode = newRow["RequestStatusCode"].ToString();
            foreach (var column in newRow)
            {
                if(column.Key.Equals("RequestStatusName"))
                    targetRow.RequestStatusName = newRow["RequestStatusName"].ToString();
                else if (column.Key.Equals("UseYN"))
                    targetRow.UseYN = (bool)newRow["UseYN"];
                else if (column.Key.Equals("RequestAlertPriority"))
                    targetRow.RequestAlertPriority = int.Parse(newRow["RequestAlertPriority"].ToString());
                else if (column.Key.Equals("CreationUserId"))
                    targetRow.CreationUserId = int.Parse(newRow["CreationUserId"].ToString());
                else if (column.Key.Equals("CreationDateTime"))
                    targetRow.CreationDateTime = DateTime.Parse(newRow["CreationDateTime"].ToString());
            }
            _context.TB_STD_REQUEST_STATUS_CODE.Update(targetRow);
            return (await _context.SaveChangesAsync());
        }

        /// <summary>
        ///  Delete - targetRow의 UseYN 만 변경. 삭제하지 않음
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns></returns>
        public async Task<int> Remove(StdRequestStatusCodeModel targetRow)
        {
            //_context.Remove(requestCode);
            //return (await _context.SaveChangesAsync());
            targetRow.UseYN = false;
            _context.TB_STD_REQUEST_STATUS_CODE.Update(targetRow);
            return await _context.SaveChangesAsync();
        }


    }


	

}
