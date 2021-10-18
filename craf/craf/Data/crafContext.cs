using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using craf.Models;

namespace craf.Data
{
    public class crafContext : DbContext
    {
        public crafContext (DbContextOptions<crafContext> options)
            : base(options)
        {
        }

        public DbSet<craf.Models.Search> Search { get; set; }
    }
}
