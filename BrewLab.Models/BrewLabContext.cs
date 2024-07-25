using BrewLab.Common;
using BrewLab.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewLab.Models;

public class BrewLabContext : DbContext
{
    public DbSet<Experimenter> Experimenters { get; set; }
    public DbSet<ExperimentalModel> ExperimentalModels { get; set; }
    public DbSet<ExperimentalPlanning> ExperimentalPlannings { get; set; }
    public DbSet<Experiment> Experiments { get; set; }
    public DbSet<Experimentation> Experimentations { get; set; }
    public DbSet<OperationLog> OperationLogs { get; set; }
    public DbSet<TemperatureLog> TemperatureLogs { get; set; }

    public BrewLabContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbConf = Configs.Database;

        optionsBuilder
            .UseNpgsql($"Host={dbConf.Host};Database={dbConf.DatabaseName};Username={dbConf.User};Password={dbConf.Password}");
    }
}
