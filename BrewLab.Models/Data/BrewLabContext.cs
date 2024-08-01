using BrewLab.Common.Configuration;
using BrewLab.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
}
