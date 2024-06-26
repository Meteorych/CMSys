﻿using System.Reflection;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Entities.Membership;
using CMSys.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CMSys.Infrastructure;

public class AppContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseGroup> CoursesGroups { get; set; }
    public DbSet<CourseTrainer> CourseTrainers { get; set; }
    public DbSet<CourseType> CourseTypes { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<TrainerGroup> TrainersGroups { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public AppContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>().HaveConversion<DateOnlyConverter>().HaveColumnType("date");
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppContext>
{
    public AppContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=CMSys.Database;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new AppContext(optionsBuilder.Options);
    }
}