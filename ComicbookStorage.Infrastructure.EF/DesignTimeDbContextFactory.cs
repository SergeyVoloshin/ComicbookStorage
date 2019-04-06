

namespace ComicbookStorage.Infrastructure.EF
{
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ComicbookStorageContext>
    {
        public ComicbookStorageContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ComicbookStorageContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new ComicbookStorageContext(builder.Options);
        }
    }
}
