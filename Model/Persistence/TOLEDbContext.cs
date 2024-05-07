﻿using TOLEAGRI.Model.Domain;
using Microsoft.EntityFrameworkCore;


namespace TOLEAGRI.Model.Persistence
{
    public class TOLEDbContext : DbContext
    {
        public TOLEDbContext(DbContextOptions options) : base(options) 
        { 
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TOLEDbContext).Assembly);
        }

        public DbSet<Estoque> Estoques { get; set; }
    }
}
