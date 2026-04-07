using Microsoft.EntityFrameworkCore;
using Company.Intranet.Models.CMS;

namespace Company.Intranet.Data
{
    public class CompanyIntranetContext : DbContext
    {
        public CompanyIntranetContext (DbContextOptions<CompanyIntranetContext> options)
            : base(options)
        {
        }

        public DbSet<Page> Page { get; set; } = default!;
        public DbSet<News> News { get; set; } = default!;
    }
}
