using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace User_App.Models;

public partial class UserappContext : DbContext
{
    public UserappContext()
    {
    }

    public UserappContext(DbContextOptions<UserappContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OrOrganization> OrOrganizations { get; set; }

    public virtual DbSet<OrUser> OrUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WRT-PRAVEEN-S\\PRAVEENSERVER;Database=Userapp;User Id=sa;Password=Praveens@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrOrganization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK__OR_Organ__CADB0B127F048661");

            entity.ToTable("OR_Organization");

            entity.HasIndex(e => e.OrganizationName, "UQ__OR_Organ__F50959E49E9AF17B").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.OrganizationName).HasMaxLength(100);
        });

        modelBuilder.Entity<OrUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__OR_Users__1788CC4CBBD905CA");

            entity.ToTable("OR_Users");

            entity.HasIndex(e => e.UserName, "UQ__OR_Users__C9F2845631CB2040").IsUnique();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Organization).WithMany(p => p.OrUsers)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OR_Users_Organization");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
