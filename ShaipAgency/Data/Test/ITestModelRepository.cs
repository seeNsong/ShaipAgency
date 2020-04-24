using System.Collections.Generic;
using System.Threading.Tasks;
using ShaipAgency.Model.TestModel;

namespace ShaipAgency.Data.Test
{
    public interface ITestModelRepository
    {
        Task<List<TestModel>> GetTestModelsByQueryAsync();
        Task<List<TestModel>> GetTestModelsBySPAsync();
    }
}
