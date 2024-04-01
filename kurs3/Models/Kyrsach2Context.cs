using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kurs3.Models;

public partial class Kyrsach2Context : DbContext
{
    public Kyrsach2Context()
    {
    }

    public Kyrsach2Context(DbContextOptions<Kyrsach2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Host> Hosts { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Userss> Usersses { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5IC0PRS\\SQLEXPRESS;Database=kyrsach2;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__CAA247C8D46DE72C");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.NomerHouse).HasColumnName("nomerHouse");
            entity.Property(e => e.Street).HasMaxLength(50);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB85ABB7C49C");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(20)
                .HasColumnName("customerName");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(20)
                .HasColumnName("patronymic");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .HasColumnName("surname");
            entity.Property(e => e.Telephone)
                .HasMaxLength(11)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Events__2370F72741EC8166");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.EventType)
                .HasMaxLength(100)
                .HasColumnName("event_type");
            entity.Property(e => e.HostId).HasColumnName("host_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.TotalCost).HasColumnName("total_cost");
            entity.Property(e => e.VenueId).HasColumnName("venue_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Events)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_Customers");

            entity.HasOne(d => d.Host).WithMany(p => p.Events)
                .HasForeignKey(d => d.HostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_Hosts");

            entity.HasOne(d => d.Service).WithMany(p => p.Events)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_Services");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_Venues");
        });

        modelBuilder.Entity<Host>(entity =>
        {
            entity.HasKey(e => e.HostId).HasName("PK__Hosts__A397C7AEC802F4E6");

            entity.Property(e => e.HostId).HasColumnName("host_id");
            entity.Property(e => e.HostName)
                .HasMaxLength(20)
                .HasColumnName("hostName");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(20)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .HasColumnName("surname");
            entity.Property(e => e.Telephone)
                .HasMaxLength(11)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__3E0DB8AFE4EB21A9");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.ServiceName).HasMaxLength(100);
        });

        modelBuilder.Entity<Userss>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Userss__3213E83FA59C83CC");

            entity.ToTable("Userss");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__Venues__82A8BE8D8337BFF1");

            entity.Property(e => e.VenueId).HasColumnName("venue_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Photos)
                .HasMaxLength(500)
                .HasColumnName("photos");
            entity.Property(e => e.VenueName).HasMaxLength(100);

            entity.HasOne(d => d.Address).WithMany(p => p.Venues)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Venues__address___37A5467C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
