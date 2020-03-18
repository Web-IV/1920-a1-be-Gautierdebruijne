using Microsoft.EntityFrameworkCore;
using Recipe_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe_API.Data
{
    public class MeetingContext : DbContext  
    {
        public MeetingContext(DbContextOptions<MeetingContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Meeting>().HasMany(m => m.Verkopers).WithOne().IsRequired().HasForeignKey("MeetingId");
            builder.Entity<Meeting>().Property(m => m.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Meeting>().Property(m => m.Planned).IsRequired();

            builder.Entity<Verkoper>().Property(v => v.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Verkoper>().Property(v => v.Title).HasMaxLength(50);
        }

        public DbSet<Meeting> Meetings { get; set; }

    }
}
