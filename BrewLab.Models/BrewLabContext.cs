using BrewLab.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewLab.Models;

public class BrewLabContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Experimenter> Experimenters { get; set; }
    public DbSet<ExperimentalModel> ExperimentalModels { get; set; }
    public DbSet<ExperimentalPlanning> ExperimentalPlannings { get; set; }
    public DbSet<Experiment> Experiments { get; set; }
    public DbSet<Experimentation> Experimentations { get; set; }
    public DbSet<OperationLog> OperationLogs { get; set; }
    public DbSet<TemperatureLog> TemperatureLogs { get; set; }
}
