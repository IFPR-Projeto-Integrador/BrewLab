﻿using BrewLab.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class Experiment : IBrewLabModel<int>, ILoggedEntity, IVirtualDeleteable
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string ParsedModel { get; set; } = "";
    [Required]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    [Required]
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    [Required]
    public bool Deleted { get; set; } = false;

    [Required]
    public int IdExperimentalPlanning { get; set; }
    [Required]
    public ExperimentalPlanning? ExperimentalPlanning { get; set; }
}
