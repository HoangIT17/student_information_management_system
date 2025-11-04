using Microsoft.AspNetCore.Identity;
using WebStudentMVC.Data;
using WebStudentMVC.Models;

namespace WebStudentMVC.Services
{
    public class SeedService
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                // Ensure database is created
                logger.LogInformation("Ensuring the database is created.");
                await context.Database.EnsureCreatedAsync();

                //  Seed roles
                logger.LogInformation("Seeding roles...");
                await AddRoleIfNotExists(roleManager, "Admin", logger);
                await AddRoleIfNotExists(roleManager, "Teacher", logger);
                await AddRoleIfNotExists(roleManager, "Student", logger);

                //  Seed admin user
                var adminEmail = "admin@gmail.com";
                var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
                if (existingAdmin != null)
                {
                    logger.LogInformation("Deleting existing admin user...");
                    await userManager.DeleteAsync(existingAdmin);
                }

                var adminUser = new Users
                {
                    FullName = "System Admin",
                    UserName = adminEmail,
                    NormalizedUserName = adminEmail.ToUpper(),
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var resultAdmin = await userManager.CreateAsync(adminUser, "Admin@123");
                if (resultAdmin.Succeeded)
                {
                    logger.LogInformation("Admin user created successfully.");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    logger.LogInformation("Admin role assigned.");
                }
                else
                {
                    logger.LogError("Failed to create admin user: {Errors}",
                        string.Join(", ", resultAdmin.Errors.Select(e => e.Description)));
                }

                //  Seed teacher user
                var teacherEmail = "teacher@gmail.com";
                var existingTeacher = await userManager.FindByEmailAsync(teacherEmail);
                if (existingTeacher != null)
                {
                    logger.LogInformation("Deleting existing teacher user...");
                    await userManager.DeleteAsync(existingTeacher);
                }

                var teacherUser = new Users
                {
                    FullName = "Teacher User",
                    UserName = teacherEmail,
                    NormalizedUserName = teacherEmail.ToUpper(),
                    Email = teacherEmail,
                    NormalizedEmail = teacherEmail.ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var resultTeacher = await userManager.CreateAsync(teacherUser, "Admin@123");
                if (resultTeacher.Succeeded)
                {
                    logger.LogInformation("Teacher user created successfully.");
                    await userManager.AddToRoleAsync(teacherUser, "Teacher");
                    logger.LogInformation("Teacher role assigned.");
                }
                else
                {
                    logger.LogError("Failed to create teacher user: {Errors}",
                        string.Join(", ", resultTeacher.Errors.Select(e => e.Description)));
                }

                //Seed student user
                var studentEmail = "student@gmail.com";
                var existingStudent = await userManager.FindByEmailAsync(studentEmail);
                if (existingStudent != null)
                {
                    logger.LogInformation("Deleting existing student user...");
                    await userManager.DeleteAsync(existingStudent);
                }

                var studentUser = new Users
                {
                    FullName = "Student User",
                    UserName = studentEmail,
                    NormalizedUserName = studentEmail.ToUpper(),
                    Email = studentEmail,
                    NormalizedEmail = studentEmail.ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var resultStudent = await userManager.CreateAsync(studentUser, "Admin@123");
                if (resultStudent.Succeeded)
                {
                    logger.LogInformation("Student user created successfully.");
                    await userManager.AddToRoleAsync(studentUser, "Student");
                    logger.LogInformation("Student role assigned.");
                }
                else
                {
                    logger.LogError("Failed to create student user: {Errors}",
                        string.Join(", ", resultStudent.Errors.Select(e => e.Description)));
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding roles and users.");
            }
        }

        private static async Task AddRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName, ILogger logger)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (roleResult.Succeeded)
                {
                    logger.LogInformation($"Role '{roleName}' created.");
                }
                else
                {
                    logger.LogError($"Failed to create role '{roleName}': {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                    throw new Exception($"Failed to create role '{roleName}'");
                }
            }
            else
            {
                logger.LogInformation($"Role '{roleName}' already exists.");
            }
        }
    }
}
