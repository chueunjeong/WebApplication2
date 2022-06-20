using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class EngineContext : DbContext
    {
        public EngineContext(DbContextOptions<EngineContext> options) : base(options)
        {
        }

        public DbSet<EngineResponse> engineResponses { get; set; } = null;
    }

}
