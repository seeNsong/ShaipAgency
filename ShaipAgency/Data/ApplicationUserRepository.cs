using Microsoft.EntityFrameworkCore;
using ShaipAgency.Model;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace ShaipAgency.Data
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context )
        {
            this._context = context;
        }

        public async Task<IList<ApplicationUser>> GetApplicationUsers()
        {
            return await _context.ApplicationUsers.ToListAsync();
        }

    }
}
