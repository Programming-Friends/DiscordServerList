using DiscordServerListLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordServerListLib.Data
{
    public class DiscordListDbContext : DbContext
    {
        public DiscordListDbContext(DbContextOptions<DiscordListDbContext> options) : base (options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<DiscordServer> DiscordServers { get; set; }
    }
}
