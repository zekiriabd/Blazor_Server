using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using Test.Models.Testx;

namespace Test.Data
{
  public partial class TestxContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public TestxContext(DbContextOptions<TestxContext> options):base(options)
    {
    }

    public TestxContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Test.Models.Testx.Role>()
              .HasOne(i => i.User)
              .WithMany(i => i.Roles)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.UserId);
        builder.Entity<Test.Models.Testx.User>()
              .HasOne(i => i.Category)
              .WithMany(i => i.Users)
              .HasForeignKey(i => i.CategoryId)
              .HasPrincipalKey(i => i.CategoryId);


        builder.Entity<Test.Models.Testx.Category>()
              .Property(p => p.CategoryId)
              .HasPrecision(10, 0);

        builder.Entity<Test.Models.Testx.Role>()
              .Property(p => p.id)
              .HasPrecision(10, 0);

        builder.Entity<Test.Models.Testx.Role>()
              .Property(p => p.UserId)
              .HasPrecision(10, 0);

        builder.Entity<Test.Models.Testx.User>()
              .Property(p => p.UserId)
              .HasPrecision(10, 0);

        builder.Entity<Test.Models.Testx.User>()
              .Property(p => p.Age)
              .HasPrecision(5, 0);

        builder.Entity<Test.Models.Testx.User>()
              .Property(p => p.CategoryId)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<Test.Models.Testx.Category> Categories
    {
      get;
      set;
    }

    public DbSet<Test.Models.Testx.Role> Roles
    {
      get;
      set;
    }

    public DbSet<Test.Models.Testx.User> Users
    {
      get;
      set;
    }
  }
}
