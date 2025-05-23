﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;
// Chat-GPT4o helped me with the IdentityDbContext<UserEntity> and what it does is that it gives it 
// automatic access to all tables and functions that is needed to manage users, roles and logins on a correct and secure way
public class AppDbContext : IdentityDbContext<UserEntity>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }

    //Chat-GPT4o helped me with OnModelCreating, defining a precision and scale for the budget column.
    //Since i had a warning-message when creating a migration - No store type was specified for the decimal property 'Budget' on entity type 'ProjectEntity'.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectEntity>()
            .Property(p => p.Budget)
            .HasColumnType("decimal(18,2)");

        //Chat-GPT4o helped me with seeding a few clients so i could test create projects before i had the functions of creating clients done in the project stage. 
        //modelBuilder.Entity<ClientEntity>().HasData(
        //        new ClientEntity { Id = "Client-1", ClientName = "KlågerupPåSinVakt Ab" },
        //        new ClientEntity { Id = "Client-2", ClientName = "GaEmJo SI Ab" },
        //        new ClientEntity { Id = "Client-3", ClientName = "JLM Test" }
        //);
    }
}
