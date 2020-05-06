using Microsoft.EntityFrameworkCore;
using ShaipAgency.Model.Standards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace ShaipAgency.Data.Standards
{
    public class RequestCodeService 
    {
        private readonly ApplicationDbContext _context;

        public RequestCodeService(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        ///  Create
        /// </summary>
        /// <param name="newRequestCode"></param>
        /// <returns></returns>
        public async Task<int> Add(IDictionary<string, object> newRequestCode)
        {
            var requestCode = new StdRequestCodeModel()
            {
                RequestCode = newRequestCode["RequestCode"].ToString(),
                RequestName = newRequestCode["RequestName"].ToString(),
                UseYN = (bool)newRequestCode["UseYN"],
                CreationUserId = int.Parse(newRequestCode["CreationUserId"].ToString()),
                CreationDateTime = DateTime.Parse(newRequestCode["CreationDateTime"].ToString())
            };

            _context.TB_STD_REQUEST_CODE.Add(requestCode);
            return (await _context.SaveChangesAsync());
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async  Task<IEnumerable<StdRequestCodeModel>> ToList(CancellationToken ct = default)
        {
            var List = await _context.TB_STD_REQUEST_CODE.ToListAsync();
            return List.AsEnumerable();
        }

        /// <summary>
        /// Update - Key는 Update하지 않음
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="newRequestCode"></param>
        /// <returns></returns>
        public async Task<int> Update(StdRequestCodeModel requestCode, IDictionary<string, object> newRequestCode)
        {
            // Key는 Update하지 않음
            //requestCode.RequestCode = newRequestCode["RequestCode"].ToString();
            foreach (var column in newRequestCode)
            {
                if(column.Key.Equals("RequestName"))
                    requestCode.RequestName = newRequestCode["RequestName"].ToString();
                else if (column.Key.Equals("UseYN"))
                    requestCode.UseYN = (bool)newRequestCode["UseYN"];
                else if (column.Key.Equals("CreationUserId"))
                    requestCode.CreationUserId = int.Parse(newRequestCode["CreationUserId"].ToString());
                else if (column.Key.Equals("CreationDateTime"))
                    requestCode.CreationDateTime = DateTime.Parse(newRequestCode["CreationDateTime"].ToString());
            }
            _context.TB_STD_REQUEST_CODE.Update(requestCode);
            return (await _context.SaveChangesAsync());
        }

        /// <summary>
        ///  delete - 기준정보의 삭제는 그 UseYN을 바꾸기만 함
        /// </summary>
        /// <param name="requestCode"></param>
        /// <returns></returns>
        public async Task<int> Remove(StdRequestCodeModel requestCode)
        {
            //_context.Remove(requestCode);
            //return (await _context.SaveChangesAsync());
            requestCode.UseYN = false;
            _context.TB_STD_REQUEST_CODE.Update(requestCode);
            return await _context.SaveChangesAsync();
        }

    }
}
