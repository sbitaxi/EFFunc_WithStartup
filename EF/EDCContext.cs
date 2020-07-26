using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFFunc_WithStartup.EF
{
    public class EDCContext : DbContext
    {
        public EDCContext(DbContextOptions options)
            : base(options)
        {

        }

    }
}
