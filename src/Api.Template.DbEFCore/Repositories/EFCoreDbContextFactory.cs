using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Template.DbEFCore.Repositories
{
    public class EfCoreDbContextFactory : IDesignTimeDbContextFactory<EfCoreDbContext>
    {
        public EfCoreDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EfCoreDbContext>();

            var connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;Database=Api.Template_LOCAL;Trusted_Connection=yes;";

            builder.UseSqlServer(connectionString);

            return new EfCoreDbContext(builder.Options);
        }
    }
}