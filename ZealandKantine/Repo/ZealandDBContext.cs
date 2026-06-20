using System.Collections.Generic;
using ZealandKantine.models;
using Microsoft.EntityFrameworkCore;

namespace ZealandKantine.Repo
{
    public class ZealandDBContext: DbContext
    {
        public ZealandDBContext(DbContextOptions<ZealandDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
