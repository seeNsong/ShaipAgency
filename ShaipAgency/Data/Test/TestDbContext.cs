using Microsoft.EntityFrameworkCore;
using ShaipAgency.Model.TestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShaipAgency.Data.Test
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {

        }
        //public DbSet<TestModel> TestModels { get; set; }
    }
}
