using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vega.Core.Models;

namespace Vega.Persistence
{
    public class VegaDbContext : IdentityDbContext<VegaUser>
    {
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleFeature> VehicleFeatures { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
              new { vf.VehicleId, vf.FeatureId });

            this.SeedUsers(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedUserRoles(modelBuilder);
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            var adminUser = new VegaUser()
            {
                Id = "231f71e7-6ef6-4da3-be60-f6d09852b9c5",
                Name = "John Admin",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@vega.com",
                NormalizedEmail = "ADMIN@VEGA.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "0721458796",
                PhoneNumberConfirmed = true
            };

            var passwordHasher = new PasswordHasher<VegaUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "$3cUr3P@$$W0rD");

            modelBuilder.Entity<VegaUser>().HasData(adminUser);
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "bdea8fa1-fc61-423d-9832-ebf111d0cc1e", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "863315b4-55cb-4255-bd6f-01df1c8d988f" },
                new IdentityRole() { Id = "35dbbc4f-8843-49f7-b3e0-bde48d27b0ea", Name = "Moderator", NormalizedName = "MODERATOR", ConcurrencyStamp = "0aedbac8-e009-4de0-81cf-6e71cbc0d38d" },
                new IdentityRole() { Id = "a09194fc-1e87-4e14-a34f-414e10936f9e", Name = "User", NormalizedName = "USER", ConcurrencyStamp = "db023a06-a3db-4404-94b9-0c29ebf66fdb" }
                );
        }

        private void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { UserId = "231f71e7-6ef6-4da3-be60-f6d09852b9c5", RoleId = "bdea8fa1-fc61-423d-9832-ebf111d0cc1e" }
                );
        }
    }
}
