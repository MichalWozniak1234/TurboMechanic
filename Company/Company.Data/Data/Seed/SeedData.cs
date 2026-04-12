using Company.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Data.Data.Seed;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var databaseContext = scope.ServiceProvider.GetRequiredService<MechanicDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await SeedRolesAsync(roleManager);
        await SeedAdminUserAsync(userManager);
        await SeedTestUsersAsync(userManager);
        await SeedDamageCategoriesAsync(databaseContext);
        await SeedMechanicsAsync(databaseContext);
        await SeedMechanicServicesAsync(databaseContext);
        await SeedRepairHistoriesAsync(databaseContext);
        await SeedVehiclesAsync(databaseContext, userManager);
        await SeedDamageReportsAsync(databaseContext, userManager);
        await SeedReviewsAsync(databaseContext, userManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = ["Admin", "Mechanic", "User"];

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        const string adminEmail = "admin@turbomechanik.local";
        const string adminPassword = "Admin123!";

        var existingAdminUser = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdminUser is not null)
        {
            if (!await userManager.IsInRoleAsync(existingAdminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(existingAdminUser, "Admin");
            }

            return;
        }

        var adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "System",
            LastName = "Administrator",
            EmailConfirmed = true
        };

        var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);

        if (!createAdminResult.Succeeded)
        {
            var errorMessages = string.Join(", ", createAdminResult.Errors.Select(identityError => identityError.Description));
            throw new InvalidOperationException($"Admin user could not be created. Errors: {errorMessages}");
        }

        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    private static async Task SeedTestUsersAsync(UserManager<ApplicationUser> userManager)
    {
        var usersToSeed = new List<(string Email, string Password, string FirstName, string LastName, string Role)>
        {
            ("user1@turbomechanik.local", "User123!", "Jan", "Kowalski", "User"),
            ("user2@turbomechanik.local", "User123!", "Anna", "Nowak", "User"),
            ("mechanic1@turbomechanik.local", "Mechanic123!", "Piotr", "Mechanik", "Mechanic"),
            ("mechanic2@turbomechanik.local", "Mechanic123!", "Tomasz", "Serwis", "Mechanic")
        };

        foreach (var userToSeed in usersToSeed)
        {
            var existingUser = await userManager.FindByEmailAsync(userToSeed.Email);

            if (existingUser is null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = userToSeed.Email,
                    Email = userToSeed.Email,
                    FirstName = userToSeed.FirstName,
                    LastName = userToSeed.LastName,
                    EmailConfirmed = true
                };

                var createUserResult = await userManager.CreateAsync(newUser, userToSeed.Password);

                if (!createUserResult.Succeeded)
                {
                    var errorMessages = string.Join(", ", createUserResult.Errors.Select(identityError => identityError.Description));
                    throw new InvalidOperationException($"User {userToSeed.Email} could not be created. Errors: {errorMessages}");
                }

                await userManager.AddToRoleAsync(newUser, userToSeed.Role);
            }
            else
            {
                if (!await userManager.IsInRoleAsync(existingUser, userToSeed.Role))
                {
                    await userManager.AddToRoleAsync(existingUser, userToSeed.Role);
                }
            }
        }
    }

    private static async Task SeedDamageCategoriesAsync(MechanicDbContext databaseContext)
    {
        if (await databaseContext.DamageCategories.AnyAsync())
        {
            return;
        }

        var damageCategories = new List<DamageCategory>
        {
            new() { Name = "Front bumper damage", Description = "Repair or replacement of a damaged front bumper.", IsActive = true },
            new() { Name = "Rear bumper damage", Description = "Repair or replacement of a damaged rear bumper.", IsActive = true },
            new() { Name = "Headlight replacement", Description = "Replacement of a damaged or broken headlight.", IsActive = true },
            new() { Name = "Scratched door", Description = "Repair and painting of scratched vehicle doors.", IsActive = true },
            new() { Name = "Damaged fender", Description = "Repair or replacement of a damaged fender.", IsActive = true },
            new() { Name = "Clutch replacement", Description = "Replacement of worn or damaged clutch components.", IsActive = true },
            new() { Name = "Timing belt replacement", Description = "Replacement of timing belt and related service parts.", IsActive = true },
            new() { Name = "Suspension repair", Description = "Repair of suspension components and diagnostics.", IsActive = true },
            new() { Name = "Brake discs and pads", Description = "Replacement of brake discs and brake pads.", IsActive = true },
            new() { Name = "Paintwork repair", Description = "Bodywork painting and paint correction.", IsActive = true }
        };

        await databaseContext.DamageCategories.AddRangeAsync(damageCategories);
        await databaseContext.SaveChangesAsync();
    }

    private static async Task SeedMechanicsAsync(MechanicDbContext databaseContext)
    {
        if (await databaseContext.Mechanics.AnyAsync())
        {
            return;
        }

        var mechanics = new List<Mechanic>
        {
            new()
            {
                WorkshopName = "TurboFix Garage",
                City = "Warsaw",
                AddressLine = "Marszalkowska 12",
                PhoneNumber = "500100100",
                Description = "General mechanic workshop focused on quick diagnostics and repairs.",
                IsActive = true
            },
            new()
            {
                WorkshopName = "AutoPro Service",
                City = "Krakow",
                AddressLine = "Dluga 45",
                PhoneNumber = "500200200",
                Description = "Bodywork and paint specialist workshop.",
                IsActive = true
            },
            new()
            {
                WorkshopName = "Mechanix Center",
                City = "Wroclaw",
                AddressLine = "Legnicka 30",
                PhoneNumber = "500300300",
                Description = "Comprehensive suspension, brakes, and drivetrain service.",
                IsActive = true
            },
            new()
            {
                WorkshopName = "SpeedCar Workshop",
                City = "Poznan",
                AddressLine = "Glogowska 77",
                PhoneNumber = "500400400",
                Description = "Fast service workshop for popular passenger vehicles.",
                IsActive = true
            },
            new()
            {
                WorkshopName = "Premium Auto Repair",
                City = "Gdansk",
                AddressLine = "Grunwaldzka 101",
                PhoneNumber = "500500500",
                Description = "Premium repair workshop with body and mechanical services.",
                IsActive = true
            }
        };

        await databaseContext.Mechanics.AddRangeAsync(mechanics);
        await databaseContext.SaveChangesAsync();
    }

    private static async Task SeedMechanicServicesAsync(MechanicDbContext databaseContext)
    {
        if (await databaseContext.MechanicServices.AnyAsync())
        {
            return;
        }

        var mechanics = await databaseContext.Mechanics
            .OrderBy(mechanic => mechanic.Id)
            .ToListAsync();

        var damageCategories = await databaseContext.DamageCategories
            .OrderBy(damageCategory => damageCategory.Id)
            .ToListAsync();

        if (mechanics.Count == 0 || damageCategories.Count == 0)
        {
            return;
        }

        var mechanicServices = new List<MechanicService>
        {
            new() { MechanicId = mechanics[0].Id, DamageCategoryId = damageCategories[0].Id, MinimumPrice = 800, MaximumPrice = 1400, EstimatedRepairDays = 2 },
            new() { MechanicId = mechanics[0].Id, DamageCategoryId = damageCategories[8].Id, MinimumPrice = 600, MaximumPrice = 1200, EstimatedRepairDays = 1 },
            new() { MechanicId = mechanics[1].Id, DamageCategoryId = damageCategories[1].Id, MinimumPrice = 850, MaximumPrice = 1450, EstimatedRepairDays = 2 },
            new() { MechanicId = mechanics[1].Id, DamageCategoryId = damageCategories[9].Id, MinimumPrice = 900, MaximumPrice = 1800, EstimatedRepairDays = 3 },
            new() { MechanicId = mechanics[2].Id, DamageCategoryId = damageCategories[7].Id, MinimumPrice = 700, MaximumPrice = 1600, EstimatedRepairDays = 2 },
            new() { MechanicId = mechanics[2].Id, DamageCategoryId = damageCategories[5].Id, MinimumPrice = 1400, MaximumPrice = 2500, EstimatedRepairDays = 2 },
            new() { MechanicId = mechanics[3].Id, DamageCategoryId = damageCategories[2].Id, MinimumPrice = 500, MaximumPrice = 1100, EstimatedRepairDays = 1 },
            new() { MechanicId = mechanics[3].Id, DamageCategoryId = damageCategories[3].Id, MinimumPrice = 700, MaximumPrice = 1300, EstimatedRepairDays = 2 },
            new() { MechanicId = mechanics[4].Id, DamageCategoryId = damageCategories[4].Id, MinimumPrice = 900, MaximumPrice = 1700, EstimatedRepairDays = 3 },
            new() { MechanicId = mechanics[4].Id, DamageCategoryId = damageCategories[6].Id, MinimumPrice = 1200, MaximumPrice = 2200, EstimatedRepairDays = 2 }
        };

        await databaseContext.MechanicServices.AddRangeAsync(mechanicServices);
        await databaseContext.SaveChangesAsync();
    }

    private static async Task SeedRepairHistoriesAsync(MechanicDbContext databaseContext)
    {
        if (await databaseContext.RepairHistories.AnyAsync())
        {
            return;
        }

        var damageCategories = await databaseContext.DamageCategories
            .OrderBy(damageCategory => damageCategory.Id)
            .ToListAsync();

        if (damageCategories.Count == 0)
        {
            return;
        }

        var repairHistories = new List<RepairHistory>
        {
            new() { VehicleBrand = "Volkswagen", VehicleModel = "Golf", VehicleProductionYear = 2018, DamageCategoryId = damageCategories[0].Id, FinalRepairPrice = 1100, RepairDurationDays = 2, Notes = "Front bumper replacement after parking collision." },
            new() { VehicleBrand = "Skoda", VehicleModel = "Octavia", VehicleProductionYear = 2019, DamageCategoryId = damageCategories[0].Id, FinalRepairPrice = 1250, RepairDurationDays = 2, Notes = "Front bumper repair and repaint." },
            new() { VehicleBrand = "Toyota", VehicleModel = "Corolla", VehicleProductionYear = 2017, DamageCategoryId = damageCategories[1].Id, FinalRepairPrice = 1180, RepairDurationDays = 2, Notes = "Rear bumper repair after low-speed impact." },
            new() { VehicleBrand = "Ford", VehicleModel = "Focus", VehicleProductionYear = 2016, DamageCategoryId = damageCategories[2].Id, FinalRepairPrice = 780, RepairDurationDays = 1, Notes = "Left headlight replacement." },
            new() { VehicleBrand = "Audi", VehicleModel = "A4", VehicleProductionYear = 2020, DamageCategoryId = damageCategories[3].Id, FinalRepairPrice = 950, RepairDurationDays = 2, Notes = "Door scratch removal and paint correction." },
            new() { VehicleBrand = "BMW", VehicleModel = "3 Series", VehicleProductionYear = 2018, DamageCategoryId = damageCategories[4].Id, FinalRepairPrice = 1450, RepairDurationDays = 3, Notes = "Front fender replacement and alignment." },
            new() { VehicleBrand = "Opel", VehicleModel = "Astra", VehicleProductionYear = 2015, DamageCategoryId = damageCategories[5].Id, FinalRepairPrice = 1900, RepairDurationDays = 2, Notes = "Clutch replacement with labor included." },
            new() { VehicleBrand = "Renault", VehicleModel = "Megane", VehicleProductionYear = 2014, DamageCategoryId = damageCategories[6].Id, FinalRepairPrice = 1750, RepairDurationDays = 2, Notes = "Timing belt kit replacement." },
            new() { VehicleBrand = "Peugeot", VehicleModel = "308", VehicleProductionYear = 2017, DamageCategoryId = damageCategories[7].Id, FinalRepairPrice = 1300, RepairDurationDays = 2, Notes = "Front suspension repair with diagnostics." },
            new() { VehicleBrand = "Mazda", VehicleModel = "6", VehicleProductionYear = 2019, DamageCategoryId = damageCategories[8].Id, FinalRepairPrice = 920, RepairDurationDays = 1, Notes = "Brake discs and pads replacement." },
            new() { VehicleBrand = "Honda", VehicleModel = "Civic", VehicleProductionYear = 2021, DamageCategoryId = damageCategories[9].Id, FinalRepairPrice = 1500, RepairDurationDays = 3, Notes = "Paintwork restoration on rear quarter panel." },
            new() { VehicleBrand = "Mercedes-Benz", VehicleModel = "C-Class", VehicleProductionYear = 2018, DamageCategoryId = damageCategories[9].Id, FinalRepairPrice = 2100, RepairDurationDays = 4, Notes = "Complete paintwork repair for side panel." }
        };

        await databaseContext.RepairHistories.AddRangeAsync(repairHistories);
        await databaseContext.SaveChangesAsync();
    }

    private static async Task SeedVehiclesAsync(MechanicDbContext databaseContext, UserManager<ApplicationUser> userManager)
    {
        if (await databaseContext.Vehicles.AnyAsync())
        {
            return;
        }

        var user1 = await userManager.FindByEmailAsync("user1@turbomechanik.local");
        var user2 = await userManager.FindByEmailAsync("user2@turbomechanik.local");

        if (user1 is null || user2 is null)
        {
            return;
        }

        var vehicles = new List<Vehicle>
        {
            new()
            {
                Vin = "WAUZZZ8V1JA000001",
                Brand = "Audi",
                Model = "A3",
                ProductionYear = 2018,
                EngineVersion = "1.5 TFSI",
                UserId = user1.Id
            },
            new()
            {
                Vin = "WVWZZZAUZJW000002",
                Brand = "Volkswagen",
                Model = "Golf",
                ProductionYear = 2019,
                EngineVersion = "2.0 TDI",
                UserId = user1.Id
            },
            new()
            {
                Vin = "VF1RFB00664000003",
                Brand = "Renault",
                Model = "Megane",
                ProductionYear = 2017,
                EngineVersion = "1.3 TCe",
                UserId = user2.Id
            },
            new()
            {
                Vin = "JTNB23HK503000004",
                Brand = "Toyota",
                Model = "Corolla",
                ProductionYear = 2020,
                EngineVersion = "1.8 Hybrid",
                UserId = user2.Id
            }
        };

        await databaseContext.Vehicles.AddRangeAsync(vehicles);
        await databaseContext.SaveChangesAsync();
    }

    private static async Task SeedDamageReportsAsync(MechanicDbContext databaseContext, UserManager<ApplicationUser> userManager)
    {
        if (await databaseContext.DamageReports.AnyAsync())
        {
            return;
        }

        var user1 = await userManager.FindByEmailAsync("user1@turbomechanik.local");
        var user2 = await userManager.FindByEmailAsync("user2@turbomechanik.local");

        var vehicles = await databaseContext.Vehicles
            .OrderBy(vehicle => vehicle.Id)
            .ToListAsync();

        var damageCategories = await databaseContext.DamageCategories
            .OrderBy(damageCategory => damageCategory.Id)
            .ToListAsync();

        if (user1 is null || user2 is null || vehicles.Count < 4 || damageCategories.Count < 10)
        {
            return;
        }

        var damageReports = new List<DamageReport>
        {
            new()
            {
                VehicleId = vehicles[0].Id,
                DamageCategoryId = damageCategories[3].Id,
                UserId = user1.Id,
                Description = "Scratch visible on left front door after parking.",
                Status = "New",
                CreatedAtUtc = DateTime.UtcNow.AddDays(-10),
                EstimatedCostMinimum = 700,
                EstimatedCostMaximum = 1200
            },
            new()
            {
                VehicleId = vehicles[1].Id,
                DamageCategoryId = damageCategories[0].Id,
                UserId = user1.Id,
                Description = "Front bumper cracked after minor collision.",
                Status = "InProgress",
                CreatedAtUtc = DateTime.UtcNow.AddDays(-7),
                EstimatedCostMinimum = 900,
                EstimatedCostMaximum = 1500
            },
            new()
            {
                VehicleId = vehicles[2].Id,
                DamageCategoryId = damageCategories[6].Id,
                UserId = user2.Id,
                Description = "Timing belt replacement recommended during inspection.",
                Status = "Completed",
                CreatedAtUtc = DateTime.UtcNow.AddDays(-20),
                EstimatedCostMinimum = 1500,
                EstimatedCostMaximum = 2100
            },
            new()
            {
                VehicleId = vehicles[3].Id,
                DamageCategoryId = damageCategories[8].Id,
                UserId = user2.Id,
                Description = "Brake discs and pads need replacement.",
                Status = "New",
                CreatedAtUtc = DateTime.UtcNow.AddDays(-3),
                EstimatedCostMinimum = 800,
                EstimatedCostMaximum = 1300
            }
        };

        await databaseContext.DamageReports.AddRangeAsync(damageReports);
        await databaseContext.SaveChangesAsync();
    }

    private static async Task SeedReviewsAsync(MechanicDbContext databaseContext, UserManager<ApplicationUser> userManager)
    {
        if (await databaseContext.Reviews.AnyAsync())
        {
            return;
        }

        var user1 = await userManager.FindByEmailAsync("user1@turbomechanik.local");
        var user2 = await userManager.FindByEmailAsync("user2@turbomechanik.local");

        var mechanics = await databaseContext.Mechanics
            .OrderBy(mechanic => mechanic.Id)
            .ToListAsync();

        if (user1 is null || user2 is null || mechanics.Count < 3)
        {
            return;
        }

        var reviews = new List<Review>
        {
            new()
            {
                MechanicId = mechanics[0].Id,
                UserId = user1.Id,
                Rating = 5,
                Comment = "Very fast service and fair pricing.",
                CreatedAtUtc = DateTime.UtcNow.AddDays(-14),
                IsApproved = true
            },
            new()
            {
                MechanicId = mechanics[1].Id,
                UserId = user2.Id,
                Rating = 4,
                Comment = "Good quality bodywork repair, slightly longer waiting time.",
                CreatedAtUtc = DateTime.UtcNow.AddDays(-8),
                IsApproved = true
            },
            new()
            {
                MechanicId = mechanics[2].Id,
                UserId = user1.Id,
                Rating = 5,
                Comment = "Professional suspension diagnosis and repair.",
                CreatedAtUtc = DateTime.UtcNow.AddDays(-5),
                IsApproved = true
            }
        };

        await databaseContext.Reviews.AddRangeAsync(reviews);
        await databaseContext.SaveChangesAsync();
    }
}