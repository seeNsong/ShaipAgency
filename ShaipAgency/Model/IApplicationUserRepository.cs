using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShaipAgency.Model
{
    public interface IApplicationUserRepository
    {
        Task<IList<ApplicationUser>> GetApplicationUsers();
    }
}
