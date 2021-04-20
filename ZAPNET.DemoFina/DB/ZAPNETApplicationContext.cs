using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.DB
{
    public class ZAPNETApplicationContext : DbContext
    {
        public ZAPNETApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>().HasKey(e => e.Id);
        }
    }
}
