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
            var requestCode = new RequestCodeModel()
            {
                RequestCode = newRequestCode["RequestCode"].ToString(),
                RequestName = newRequestCode["RequestName"].ToString(),
                UseYN = newRequestCode["UseYN"].ToString(),
                UserId = int.Parse(newRequestCode["UserId"].ToString()),
                CreationDateTime = DateTime.Parse(newRequestCode["CreationDateTime"].ToString())
            };

            _context.STD_REQUEST_CODE.Add(requestCode);
            return (await _context.SaveChangesAsync());
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async  Task<IEnumerable<RequestCodeModel>> ToList(CancellationToken ct = default)
        {
            var List = await _context.STD_REQUEST_CODE.ToListAsync();
            return List.AsEnumerable();
        }

        /// <summary>
        /// Update - Key는 Update하지 않음
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="newRequestCode"></param>
        /// <returns></returns>
        public async Task<int> Update(RequestCodeModel requestCode, IDictionary<string, object> newRequestCode)
        {
            // Key는 Update하지 않음
            //requestCode.RequestCode = newRequestCode["RequestCode"].ToString();
            requestCode.RequestName = newRequestCode["RequestName"].ToString();
            requestCode.UseYN = newRequestCode["UseYN"].ToString();
            requestCode.UserId = int.Parse(newRequestCode["UserId"].ToString());
            requestCode.CreationDateTime = DateTime.Parse(newRequestCode["CreationDateTime"].ToString());
            _context.STD_REQUEST_CODE.Update(requestCode);
            return (await _context.SaveChangesAsync());
        }

        /// <summary>
        ///  delete - 기준정보의 삭제는 그 UseYN을 바꾸기만 함
        /// </summary>
        /// <param name="requestCode"></param>
        /// <returns></returns>
        public async Task<int> Remove(RequestCodeModel requestCode)
        {
            //_context.Remove(requestCode);
            //return (await _context.SaveChangesAsync());
            requestCode.UseYN = "false";
            _context.STD_REQUEST_CODE.Update(requestCode);
            return await _context.SaveChangesAsync();
        }

    }
}
