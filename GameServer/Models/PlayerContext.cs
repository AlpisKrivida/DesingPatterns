using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1_1;
using Lab1_1.Observer;

namespace GameServer.Models
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerPost> PP_ID { get; set; }
        public DbSet<PlayerGet> PG_ID { get; set; }
        public DbSet<MapUnit> Map { get; set; }
        public DbSet<State> State { get; set; }
    }
}
