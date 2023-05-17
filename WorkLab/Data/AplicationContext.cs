﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkLab.Model;

namespace WorkLab.Data
{
    class AplicationContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();

        public AplicationContext()
        {
            Database.EnsureCreated();
        }


        string folderpath = Path.Combine(Directory.GetCurrentDirectory(), @"DB\Product.db");
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Suorce = {folderpath}");
        }
    }
}
