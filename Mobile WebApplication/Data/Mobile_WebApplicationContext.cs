using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mobile_WebApplication.Models;

namespace Mobile_WebApplication.Data
{
    public class Mobile_WebApplicationContext : DbContext
    {
        public Mobile_WebApplicationContext (DbContextOptions<Mobile_WebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Mobile_WebApplication.Models.items> items { get; set; } = default!;
        public DbSet<Mobile_WebApplication.Models.usersaccounts> usersaccounts { get; set; } = default!;
        public DbSet<Mobile_WebApplication.Models.orders> orders { get; set; } = default!;
    }
}
