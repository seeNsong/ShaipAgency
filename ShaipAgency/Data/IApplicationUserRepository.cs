using ShaipAgency.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShaipAgency.Data
{
    public interface IApplicationUserRepository
    {
        Task<IList<ApplicationUser>> GetApplicationUsers();
    }
}
