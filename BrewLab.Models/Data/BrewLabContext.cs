using BrewLab.Common.Configuration;
using BrewLab.Models.Base;
using BrewLab.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace BrewLab.Models;

public class BrewLabContext : IdentityDbContext<Experimenter, IdentityRole<int>, int>
{
    public DbSet<Experimenter> Experimenters { get; set; }
    public DbSet<ExperimentalModel> ExperimentalModels { get; set; }
    public DbSet<ExperimentalPlanning> ExperimentalPlannings { get; set; }
    public DbSet<Experiment> Experiments { get; set; }

    public BrewLabContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbConf = Configs.Database;

        optionsBuilder
            .UseNpgsql($"Host={dbConf.Host};Database={dbConf.DatabaseName};Username={dbConf.User};Password={dbConf.Password}");
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var deletedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Deleted && e.Entity is IVirtualDeleteable)
            .ToList();

        foreach (var entry in deletedEntities)
        {
            // Cancel the delete operation
            entry.State = EntityState.Modified;

            // Set the IsDeleted flag to true
            ((IVirtualDeleteable) entry.Entity).Deleted = true;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        var deletedEntities = ChangeTracker.Entries()
    .Where(e => e.State == EntityState.Deleted && e.Entity is IVirtualDeleteable)
    .ToList();

        foreach (var entry in deletedEntities)
        {
            // Cancel the delete operation
            entry.State = EntityState.Modified;

            // Set the IsDeleted flag to true
            ((IVirtualDeleteable)entry.Entity).Deleted = true;
        }

        return base.SaveChanges();
    }
}
