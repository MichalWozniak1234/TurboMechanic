using Company.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Company.Data;

public class MechanicDbContext : IdentityDbContext<ApplicationUser>
{
    public MechanicDbContext(DbContextOptions<MechanicDbContext> options)
        : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    public DbSet<DamageCategory> DamageCategories => Set<DamageCategory>();

    public DbSet<DamageReport> DamageReports => Set<DamageReport>();

    public DbSet<Mechanic> Mechanics => Set<Mechanic>();

    public DbSet<MechanicService> MechanicServices => Set<MechanicService>();

    public DbSet<RepairHistory> RepairHistories => Set<RepairHistory>();

    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vehicle>(entityBuilder =>
        {
            entityBuilder.HasKey(vehicle => vehicle.Id);

            entityBuilder.Property(vehicle => vehicle.Vin)
                .IsRequired()
                .HasMaxLength(17);

            entityBuilder.Property(vehicle => vehicle.Brand)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder.Property(vehicle => vehicle.Model)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder.Property(vehicle => vehicle.EngineVersion)
                .HasMaxLength(100);

            entityBuilder.HasOne(vehicle => vehicle.User)
                .WithMany(applicationUser => applicationUser.Vehicles)
                .HasForeignKey(vehicle => vehicle.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DamageCategory>(entityBuilder =>
        {
            entityBuilder.HasKey(damageCategory => damageCategory.Id);

            entityBuilder.Property(damageCategory => damageCategory.Name)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder.Property(damageCategory => damageCategory.Description)
                .HasMaxLength(500);
        });

        modelBuilder.Entity<DamageReport>(entityBuilder =>
        {
            entityBuilder.HasKey(damageReport => damageReport.Id);

            entityBuilder.Property(damageReport => damageReport.Description)
                .HasMaxLength(1000);

            entityBuilder.Property(damageReport => damageReport.Status)
                .IsRequired()
                .HasMaxLength(50);

            entityBuilder.Property(damageReport => damageReport.EstimatedCostMinimum)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(damageReport => damageReport.EstimatedCostMaximum)
                .HasColumnType("decimal(18,2)");

            entityBuilder.HasOne(damageReport => damageReport.Vehicle)
                .WithMany(vehicle => vehicle.DamageReports)
                .HasForeignKey(damageReport => damageReport.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(damageReport => damageReport.DamageCategory)
                .WithMany(damageCategory => damageCategory.DamageReports)
                .HasForeignKey(damageReport => damageReport.DamageCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasOne(damageReport => damageReport.User)
                .WithMany(applicationUser => applicationUser.DamageReports)
                .HasForeignKey(damageReport => damageReport.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Mechanic>(entityBuilder =>
        {
            entityBuilder.HasKey(mechanic => mechanic.Id);

            entityBuilder.Property(mechanic => mechanic.WorkshopName)
                .IsRequired()
                .HasMaxLength(150);

            entityBuilder.Property(mechanic => mechanic.City)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder.Property(mechanic => mechanic.AddressLine)
                .HasMaxLength(200);

            entityBuilder.Property(mechanic => mechanic.PhoneNumber)
                .HasMaxLength(30);

            entityBuilder.Property(mechanic => mechanic.Description)
                .HasMaxLength(1000);
        });

        modelBuilder.Entity<MechanicService>(entityBuilder =>
        {
            entityBuilder.HasKey(mechanicService => mechanicService.Id);

            entityBuilder.Property(mechanicService => mechanicService.MinimumPrice)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(mechanicService => mechanicService.MaximumPrice)
                .HasColumnType("decimal(18,2)");

            entityBuilder.HasOne(mechanicService => mechanicService.Mechanic)
                .WithMany(mechanic => mechanic.MechanicServices)
                .HasForeignKey(mechanicService => mechanicService.MechanicId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.HasOne(mechanicService => mechanicService.DamageCategory)
                .WithMany(damageCategory => damageCategory.MechanicServices)
                .HasForeignKey(mechanicService => mechanicService.DamageCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasIndex(mechanicService => new
            {
                mechanicService.MechanicId,
                mechanicService.DamageCategoryId
            }).IsUnique();
        });

        modelBuilder.Entity<RepairHistory>(entityBuilder =>
        {
            entityBuilder.HasKey(repairHistory => repairHistory.Id);

            entityBuilder.Property(repairHistory => repairHistory.VehicleBrand)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder.Property(repairHistory => repairHistory.VehicleModel)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder.Property(repairHistory => repairHistory.FinalRepairPrice)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(repairHistory => repairHistory.Notes)
                .HasMaxLength(1000);

            entityBuilder.HasOne(repairHistory => repairHistory.DamageCategory)
                .WithMany(damageCategory => damageCategory.RepairHistories)
                .HasForeignKey(repairHistory => repairHistory.DamageCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Review>(entityBuilder =>
        {
            entityBuilder.HasKey(review => review.Id);

            entityBuilder.Property(review => review.Comment)
                .HasMaxLength(1000);

            entityBuilder.HasOne(review => review.Mechanic)
                .WithMany(mechanic => mechanic.Reviews)
                .HasForeignKey(review => review.MechanicId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.HasOne(review => review.User)
                .WithMany(applicationUser => applicationUser.Reviews)
                .HasForeignKey(review => review.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}