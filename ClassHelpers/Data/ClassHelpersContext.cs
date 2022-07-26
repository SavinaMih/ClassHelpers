﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClassHelpers.Models.Database;

namespace ClassHelpers.Data;

public class ClassHelpersContext : IdentityDbContext<IdentityUser>
{
    public ClassHelpersContext(DbContextOptions<ClassHelpersContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<ClassHelpers.Models.Database.Database>? Database { get; set; }
}
