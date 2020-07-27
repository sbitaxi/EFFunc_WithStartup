using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EFFunc_WithStartup.EF;
using Microsoft.Extensions.DependencyInjection.Extensions;

[assembly: FunctionsStartup(typeof(EFFunc_WithStartup.Startup))]

namespace EFFunc_WithStartup
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("SQL_DB_CONNECTION");
            builder.Services.AddDbContext<EDCContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
            builder.Services.TryAddScoped<IPerson, PersonService>();
        }
    }
}
