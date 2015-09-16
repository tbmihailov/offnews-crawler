using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OffnewsCrawler.Data
{
    public class OffnewsDbContext : DbContext
    {
        public DbSet<Opinion> Opinions { get; set; }
    }
}
