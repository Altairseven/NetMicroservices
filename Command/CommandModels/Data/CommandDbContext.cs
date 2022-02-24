using CommandModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandModels.Data;

public class CommandDbContext : DbContext {

#pragma warning disable CS8618
    public CommandDbContext(DbContextOptions<CommandDbContext> opt) : base(opt) {}
#pragma warning restore CS8618

    public DbSet<Command> Commands { get; set; }
    public DbSet<Platform> Platforms { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder
            .Entity<Platform>()
            .HasMany(p => p.Commands)
            .WithOne(p => p.Platform!)
            .HasForeignKey(p=> p.PlatformId);

        builder
            .Entity<Command>()
            .HasOne(p => p.Platform)
            .WithMany(p => p.Commands)
            .HasForeignKey(p => p.PlatformId);


    }
}
