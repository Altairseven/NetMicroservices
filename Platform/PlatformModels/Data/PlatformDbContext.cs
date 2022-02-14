using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformModels.Models;

namespace PlatformModels.Data;

public class PlatformDbContext : DbContext {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public PlatformDbContext(DbContextOptions<PlatformDbContext> opt) : base(opt) {
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }

    public DbSet<Platform> Platforms { get; set; }

   




}
