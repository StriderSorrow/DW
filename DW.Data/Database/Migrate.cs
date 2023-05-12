using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.Data.Database
{
    public class Migrate : IDesignTimeDbContextFactory<DwDbContext>
    {
        public DwDbContext CreateDbContext(string[] args)
        {
            var factory = new DbContextOptionsBuilder<DwDbContext>();
            factory.UseNpgsql("User ID = postgres; Password = _Telk0nt@R_; Host = localhost; Port = 5432; Database = dwdb;");
            var context = new DwDbContext(factory.Options);
            return context;
        }
    }

}
