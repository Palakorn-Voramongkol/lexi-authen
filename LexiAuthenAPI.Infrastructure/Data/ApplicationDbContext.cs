// LexiAuthenAPI.Infrastructure/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using LexiAuthenAPI.Domain.Entities;

namespace LexiAuthenAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor accepting DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Refreshtoken> Refreshtokens { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Uiitem> Uiitems { get; set; }
        public DbSet<Uipermission> Uipermissions { get; set; }
        public DbSet<UipermissionUiitem> UipermissionUiitems { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        // Configure relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Enum mapping for UIPermissionAccessLevel
            modelBuilder.Entity<Uipermission>()
                .Property(u => u.AccessLevel)
                .HasConversion<int>();

            // Many to many of [User >-< Role]
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Configure RefreshToken relationship
            modelBuilder.Entity<Refreshtoken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.Refreshtokens)
                .HasForeignKey(rt => rt.UserId);

            // Many to many of [Role >-< Permission]
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed

            // Many to many of [Uipermission >-< Uiitem]
            modelBuilder.Entity<UipermissionUiitem>()
               .HasKey(ui => new { ui.UipermissionId, ui.UiitemId });
            modelBuilder.Entity<UipermissionUiitem>()
                .HasOne(ui => ui.Uipermission)
                .WithMany(up => up.UipermissionUiitems)
                .HasForeignKey(ui => ui.UipermissionId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UipermissionUiitem>()
                .HasOne(ui => ui.Uiitem)
                .WithMany(i => i.UipermissionUiitem)
                .HasForeignKey(ui => ui.UiitemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many to many of [Permission >-< Uipermission]
            modelBuilder.Entity<PermissionUipermission>()
                .HasKey(up => new { up.PermissionId, up.UipermissionId}); // Composite primary key
            modelBuilder.Entity<PermissionUipermission>()
                .HasOne(up => up.Permission)
                .WithMany(u => u.PermissionUipermissions)
                .HasForeignKey(up => up.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PermissionUipermission>()
                .HasOne(up => up.Uipermission)
                .WithMany(p => p.PermissionUipermissions)
                .HasForeignKey(up => up.UipermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many to many of [Permission >-< User]
            modelBuilder.Entity<UserPermission>()
               .HasKey(up => new { up.UserId, up.PermissionId });
            modelBuilder.Entity<UserPermission>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed
            modelBuilder.Entity<UserPermission>()
                .HasOne(up => up.Permission)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(up => up.PermissionId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed

            base.OnModelCreating(modelBuilder);
        }
    }
}
