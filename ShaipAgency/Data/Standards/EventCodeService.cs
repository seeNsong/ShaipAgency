using Microsoft.EntityFrameworkCore;
using ShaipAgency.Model.Standards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace ShaipAgency.Data.Standards
{
    public class EventCodeService
    {
        private readonly ApplicationDbContext _context;

        public EventCodeService(ApplicationDbContext context)
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
            var newData = new StdEventCodeModel()
            {
                EventCode = newRow["EventCode"].ToString(),
                EventName = newRow["EventName"].ToString(),
                UseYN = (bool)newRow["UseYN"],
                EventDescription = newRow["EventDescription"].ToString(),
            };

            _context.TB_STD_EVENT_CODE.Add(newData);
            return (await _context.SaveChangesAsync());
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async  Task<IEnumerable<StdEventCodeModel>> ToList(CancellationToken ct = default)
        {
            var List = await _context.TB_STD_EVENT_CODE.ToListAsync();
            return List.AsEnumerable();
        }

        /// <summary>
        /// Update - Key는 Update하지 않음
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public async Task<int> Update(StdEventCodeModel targetRow, IDictionary<string, object> newRow)
        {
            // Key는 Update하지 않음
            //requestCode.RequestCode = newRow["RequestCode"].ToString();
            foreach (var column in newRow)
            {
                if(column.Key.Equals("EventName"))
                    targetRow.EventName = newRow["EventName"].ToString();
                else if (column.Key.Equals("UseYN"))
                    targetRow.UseYN = (bool)newRow["UseYN"];
                else if (column.Key.Equals("EventDescription"))
                    targetRow.EventDescription = newRow["EventDescription"].ToString();
            }
            _context.TB_STD_EVENT_CODE.Update(targetRow);
            return (await _context.SaveChangesAsync());
        }

        /// <summary>
        ///  delete - 기준정보의 삭제는 그 UseYN을 바꾸기만 함
        /// </summary>
        /// <param name="requestCode"></param>
        /// <returns></returns>
        public async Task<int> Remove(StdEventCodeModel targetRow)
        {
            //_context.Remove(requestCode);
            //return (await _context.SaveChangesAsync());
            targetRow.UseYN = false;
            _context.TB_STD_EVENT_CODE.Update(targetRow);
            return await _context.SaveChangesAsync();
        }

    }
}
