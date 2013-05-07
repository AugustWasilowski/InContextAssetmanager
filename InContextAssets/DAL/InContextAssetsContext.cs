using System;
using System.Collections.Generic;
using System.Data.Entity;
using InContextAssets.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InContextAssets.DAL
{
    public class InContextAssetsContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }        
    }
}