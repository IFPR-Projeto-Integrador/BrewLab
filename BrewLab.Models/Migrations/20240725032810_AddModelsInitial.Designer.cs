﻿// <auto-generated />
using System;
using BrewLab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BrewLab.Models.Migrations
{
    [DbContext(typeof(BrewLabContext))]
    [Migration("20240725032810_AddModelsInitial")]
    partial class AddModelsInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BrewLab.Models.Models.Experiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ExperimentalPlanningId")
                        .HasColumnType("integer");

                    b.Property<int>("IdExperimentalPlanning")
                        .HasColumnType("integer");

                    b.Property<string>("ParsedModel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentalPlanningId");

                    b.ToTable("Experiments");
                });

            modelBuilder.Entity("BrewLab.Models.Models.ExperimentalModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ExperimenterId")
                        .HasColumnType("integer");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ExperimenterId");

                    b.ToTable("ExperimentalModels");
                });

            modelBuilder.Entity("BrewLab.Models.Models.ExperimentalPlanning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExperimentalMatrix")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ExperimentalModelId")
                        .HasColumnType("integer");

                    b.Property<int>("IdExperimentalModel")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentalModelId");

                    b.ToTable("ExperimentalPlannings");
                });

            modelBuilder.Entity("BrewLab.Models.Models.Experimentation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ExperimentId")
                        .HasColumnType("integer");

                    b.Property<int>("IdExperiment")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentId");

                    b.ToTable("Experimentations");
                });

            modelBuilder.Entity("BrewLab.Models.Models.Experimenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTime?>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Experimenters");
                });

            modelBuilder.Entity("BrewLab.Models.Models.OperationLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ExperimentationId")
                        .HasColumnType("integer");

                    b.Property<int>("IdExperimentation")
                        .HasColumnType("integer");

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ReadAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentationId");

                    b.ToTable("OperationLogs");
                });

            modelBuilder.Entity("BrewLab.Models.Models.TemperatureLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ExperimentationId")
                        .HasColumnType("integer");

                    b.Property<int>("IdExperimentation")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReadAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Reading")
                        .HasColumnType("double precision");

                    b.Property<int>("Vase")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentationId");

                    b.ToTable("TemperatureLogs");
                });

            modelBuilder.Entity("BrewLab.Models.Models.Experiment", b =>
                {
                    b.HasOne("BrewLab.Models.Models.ExperimentalPlanning", "ExperimentalPlanning")
                        .WithMany()
                        .HasForeignKey("ExperimentalPlanningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExperimentalPlanning");
                });

            modelBuilder.Entity("BrewLab.Models.Models.ExperimentalModel", b =>
                {
                    b.HasOne("BrewLab.Models.Models.Experimenter", "Experimenter")
                        .WithMany()
                        .HasForeignKey("ExperimenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Experimenter");
                });

            modelBuilder.Entity("BrewLab.Models.Models.ExperimentalPlanning", b =>
                {
                    b.HasOne("BrewLab.Models.Models.ExperimentalModel", "ExperimentalModel")
                        .WithMany()
                        .HasForeignKey("ExperimentalModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExperimentalModel");
                });

            modelBuilder.Entity("BrewLab.Models.Models.Experimentation", b =>
                {
                    b.HasOne("BrewLab.Models.Models.Experiment", "Experiment")
                        .WithMany()
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Experiment");
                });

            modelBuilder.Entity("BrewLab.Models.Models.OperationLog", b =>
                {
                    b.HasOne("BrewLab.Models.Models.Experimentation", "Experimentation")
                        .WithMany()
                        .HasForeignKey("ExperimentationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Experimentation");
                });

            modelBuilder.Entity("BrewLab.Models.Models.TemperatureLog", b =>
                {
                    b.HasOne("BrewLab.Models.Models.Experimentation", "Experimentation")
                        .WithMany()
                        .HasForeignKey("ExperimentationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Experimentation");
                });
#pragma warning restore 612, 618
        }
    }
}
