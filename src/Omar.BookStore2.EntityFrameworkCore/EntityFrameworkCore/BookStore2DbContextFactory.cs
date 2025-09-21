using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Omar.BookStore2.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class BookStore2DbContextFactory : IDesignTimeDbContextFactory<BookStore2DbContext>
{
    public BookStore2DbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        BookStore2EfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<BookStore2DbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new BookStore2DbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Omar.BookStore2.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
