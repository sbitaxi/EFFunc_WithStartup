using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Azure.Services.AppAuthentication;

namespace EFFunc_WithStartup.EF
{
    public class EDCContext : DbContext
    {
        public EDCContext(DbContextOptions options)
            : base(options)
        {
            var conn = (SqlConnection)this.Database.GetDbConnection();
            conn.AccessToken = (new Microsoft.Azure.Services
                            .AppAuthentication.AzureServiceTokenProvider())
                            .GetAccessTokenAsync("https://database.windows.net/").Result;
        }

    }
}
