using ShaipAgency.Model.TestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShaipAgency.Data.Test
{
    public interface ITestModelService
    {
        Task<List<TestModel>> GetTestModelsByQueryAsync();
        Task<List<TestModel>> GetTestModelsBySPAsync();

    }
}
